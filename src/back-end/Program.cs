using back_end;
using back_end.Configuracao;
using back_end.Services;
using back_end.Services.Interfaces;
using Domain.Interfaces;
using Infra.Configuracao;
using Infra.Repositorio;
using Serilog;


Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

try
{

    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.

    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();


    builder.Services.AddScoped<IFuncionarioRepository, FuncionarioRepository>();
    builder.Services.AddScoped<IFuncionarioService, FuncionarioService>();
    builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
    builder.Services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));

    builder.Environment.AddAppSettingsAmbiente(builder);
    builder.Services.AddApplicationServices();
    builder.Services.AddInfraServices(builder.Configuration);

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();



    app.Run();

}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.Information("Server Shutting down...");
    Log.CloseAndFlush();
}


