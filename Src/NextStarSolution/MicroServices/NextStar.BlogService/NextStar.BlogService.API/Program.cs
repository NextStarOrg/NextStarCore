using System.Configuration;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using NextStar.BlogService.API.Configs;
using NextStar.BlogService.API.Extensions;
using NextStar.Framework.AspNetCore.Auditing;
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

    var services = builder.Services;
// Add services to the container.
    services.AddSingleton(configuration);
    services.AddHttpContextAccessor();
    services.AddMemoryCache();
    //添加身份认证相关设定
    services.AddNextStarJwtAuthentication(appSetting);
    services.AddNextStarApiAuthorization();
    services.AddCustomRedisCache(appSetting);
    services.AddNextStarSession(appSetting);

    services.AddDatabase(appSetting);
    services.AddControllers(options =>
    {
        //追加Action审计日志
        options.Filters.Add<NextStarAuditActionFilter>();
    }).AddNewtonsoftJson(options =>
    {
        // options.SerializerSettings.DateFormatString = "yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'z'";
        options.SerializerSettings.ContractResolver = new DefaultContractResolver()
        {
            NamingStrategy = new CamelCaseNamingStrategy()
        };
        options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
    });
    services.AddCustomSwaggerGen();
    services.AddAutoMapper(typeof(MapperProfile));

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

    //app.UseHttpsRedirection();

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