using Autofac;
using Autofac.Extensions.DependencyInjection;
using NextStar.BlogService.API.Configs;
using NextStar.Framework.AspNetCore.Extensions;
using Serilog;

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
    .AddEnvironmentVariables()
    .Build();

var appSetting = configuration.Get<AppSettingPartial>();
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(configuration)
    .Enrich.FromLogContext()
    .WriteTo.Debug()
    .WriteTo.Console()
    .WriteTo.AddNextStarLoggersWithoutAudit(appSetting)
    .CreateLogger();

try
{
    Log.Information("Getting the identity server running...");

    #region ConfigureServices

    var builder = WebApplication.CreateBuilder(args);
    builder.Host.UseSerilog();
    
// Add services to the container.

    builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    //使用AutoFac替换默认容易
    builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory()).Build();
    builder.Host.ConfigureContainer<ContainerBuilder>(builder =>
    {
        builder.RegisterModule<AutofacServiceModules>();
    });
   #endregion
   
   #region Configure
   
   var app = builder.Build();
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();
    
    #endregion

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "NextStar identity server Host terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}