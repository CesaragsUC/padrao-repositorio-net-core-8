using System.Reflection;

namespace back_end.Configuracao
{
    public static class ConfigServices
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper();
        }
        private static void AddAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
        }
    }
}
