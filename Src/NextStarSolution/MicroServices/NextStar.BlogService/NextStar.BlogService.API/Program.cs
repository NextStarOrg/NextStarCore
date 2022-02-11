using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using NextStar.BlogService.API.Configs;
using NextStar.BlogService.API.Extensions;
using NextStar.BlogService.Core;
using NextStar.BlogService.Core.Configs;
using NextStar.Library.AspNetCore.Audit;
using NextStar.Library.AspNetCore.Extensions;
using NextStar.Library.MicroService.Filters;
using Serilog;

var Configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
    .AddEnvironmentVariables()
    .Build();


var appSetting = Configuration.Get<AppSettingPartial>();
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(Configuration)
    .Enrich.FromLogContext()
    .WriteTo.Debug()
    .WriteTo.Console()
    .WriteTo.AddNextStarLoggers(appSetting)
    .CreateLogger();
try
{
    Log.Information("Getting the system service running...");
    var builder = WebApplication.CreateBuilder(args);
    builder.Host.UseSerilog().UseServiceProviderFactory(new AutofacServiceProviderFactory());

    #region ConfigureServices
    var services = builder.Services;
    // Add services to the container.
    services.TryAddSingleton(Configuration);
    services.AddHttpContextAccessor();
    services.AddHttpClient();
    // shared
    services.AddNextStarJwtAuthentication(appSetting);
    services.AddNextStarApiAuthorization();
    services.AddCustomRedisCache(appSetting);
    services.AddSessionStore(appSetting);
    services.AddApplicationConfigStore(appSetting);
    
    //self
    services.AddDatabase(appSetting);
    services.AddControllers(options =>
    {
        //追加Action审计日志
        options.Filters.Add<NextStarAuditActionFilter>();
        // 追加ServiceApplication错误
        options.Filters.Add(new ServiceApplicationExceptionFilter());
    }).AddNewtonsoftJson(options =>
    {
        //options.SerializerSettings.DateFormatString = "yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'+0900'";
        options.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Local;
        // 使用上面制定的string时区，或者使用这个操作转为带有时区的字符串
        options.SerializerSettings.DateFormatHandling = DateFormatHandling.IsoDateFormat;
        options.SerializerSettings.ContractResolver = new DefaultContractResolver()
        {
            NamingStrategy = new CamelCaseNamingStrategy()
        };
        options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
    });
    services.AddCustomSwaggerGen();
    // self
    
    //services.AddDependencyInjection();
    services.AddAutoMapper(typeof(AutoMapperProfile));
    #endregion

    builder.Host.ConfigureContainer<ContainerBuilder>(builderAction => builderAction.RegisterModule(new AutofacServiceModules()));
    var app = builder.Build();
    
    #region Configure
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
    #endregion

}
catch (Exception ex)
{
    Log.Fatal(ex, "NextStar system service Host terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}
