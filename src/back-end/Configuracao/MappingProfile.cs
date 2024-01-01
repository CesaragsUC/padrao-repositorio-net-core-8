using AutoMapper;
using System.Reflection;

namespace back_end.Configuracao
{
    public interface IMapFrom<T>
    {
        void Mapping(Profile profile) => profile.CreateMap(typeof(T), GetType()).ReverseMap();
    }

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());
        }
        private void ApplyMappingsFromAssembly(Assembly assembly)
        {
            //obetem o tipo da interface IMapFrom<>
            var mapFromType = typeof(IMapFrom<>);

            // retorna o nome do método da interface
            var mappingMethodName = nameof(IMapFrom<object>.Mapping);

            // retorna true se o tipo T for uma interface genérica e o tipo genérico for igual ao tipo mapFromType
            bool HasInterface(Type t) => t.IsGenericType && t.GetGenericTypeDefinition() == mapFromType;

            // retorna todos os tipos exportados da assembly que implementam a interface IMapFrom<>
            var types = assembly.GetExportedTypes().Where(t => t.GetInterfaces().Any(HasInterface)).ToList();

            // retorna os tipos de argumentos do método Mapping
            var argumentTypes = new Type[] { typeof(Profile) };

            foreach (var type in types)
            {
                // cria uma instância do tipo 
                var instance = Activator.CreateInstance(type);

                // retorna o método Mapping do IMapFrom  
                var methodInfo = type.GetMethod(mappingMethodName);

                // se o método Mapping existir
                if (methodInfo != null)
                {
                    // invoca o método Mapping
                    methodInfo.Invoke(instance, new object[] { this });
                }
                else // se o método Mapping não existir
                {
                    // retorna todas as interfaces implementadas pelo tipo que possuem o método Mapping
                    var interfaces = type.GetInterfaces().Where(HasInterface).ToList();

                    if (interfaces.Count > 0)
                    {
                        // invoca o método Mapping de cada interface
                        foreach (var @interface in interfaces)
                        {
                            // retorna o método Mapping da interface e o tipo de argumento esperado
                            var interfaceMethodInfo = @interface.GetMethod(mappingMethodName, argumentTypes);

                            // invoca o método Mapping da interface passando o tipo atual como argumento
                            interfaceMethodInfo.Invoke(instance, new object[] { this });
                        }
                    }
                }
            }
        }
    }
}
