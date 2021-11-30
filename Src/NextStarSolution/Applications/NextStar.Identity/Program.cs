using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using NextStar.Identity.Configs;
using NextStar.Identity.Extensions;
using NextStar.Library.AspNetCore.Extensions;
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
    .WriteTo.AddNextStarLoggersWithoutAudit(appSetting)
    .CreateLogger();

try
{
    Log.Information("Getting the identity server running...");
    var builder = WebApplication.CreateBuilder(args);
    builder.Host.UseSerilog().UseServiceProviderFactory(new AutofacServiceProviderFactory());
    
    #region ConfigureServices
    var services = builder.Services;
    // Add services to the container.
    services.AddControllersWithViews();
    services.TryAddSingleton(Configuration);
    services.AddHttpContextAccessor();
    services.AddHttpClient();
    // shared
    services.AddCustomRedisCache(appSetting);
    services.AddSessionStore(appSetting);
    services.AddApplicationConfigStore(appSetting);
    // self
    services.AddNextStarIdentityServer(appSetting);
    services.AddCustomDbContext(appSetting);
    //services.AddDependencyInjection();
    services.AddAutoMapper(typeof(MapperProfile));
    #endregion

    // Register your own things directly with Autofac here. Don't
    // call builder.Populate(), that happens in AutofacServiceProviderFactory for you.
    builder.Host.ConfigureContainer<ContainerBuilder>(builder => builder.RegisterModule(new AutofacServiceModules()));
    var app = builder.Build();

    #region Configure

    // Configure the HTTP request pipeline.
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Home/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }

    // app.UseHttpsRedirection();
    app.UseStaticFiles();

    app.UseRouting();

    app.UseIdentityServer();

    app.UseAuthorization();

    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
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
