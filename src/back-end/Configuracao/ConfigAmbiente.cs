using Microsoft.Extensions.Configuration;

namespace back_end.Configuracao
{
    public static class ConfigAmbiente
    {
        public static IWebHostEnvironment AddAppSettingsAmbiente(this IWebHostEnvironment hostEnvironment, 
            WebApplicationBuilder app)
        {

            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var configuration = app.Configuration
             .SetBasePath(System.IO.Directory.GetCurrentDirectory())
             .AddJsonFile($"appsettings.json", optional: false)
             .AddJsonFile($"appsettings.{env}.json", optional: true)
             .AddEnvironmentVariables()
             .Build();

            //somente para teste
            var connectionString = app.Configuration.GetConnectionString("DefaultConnection");
            var section = configuration.GetSection("TokenSeguranca");

            if (hostEnvironment.IsDevelopment())
            {
                Console.WriteLine("IsDevelopment");

            }
            return hostEnvironment;
        }
    }
}
