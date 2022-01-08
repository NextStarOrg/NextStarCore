using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection.Extensions;
using NextStar.Gateway.Manage.Configs;
using NextStar.Library.AspNetCore.Abstractions;
using NextStar.Library.AspNetCore.Extensions;
using NextStar.Library.Core.Abstractions;
using Ocelot.Configuration;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Serilog;

var Configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json",
        optional: true)
    .AddEnvironmentVariables()
    .Build();
IServiceProvider MyServiceProvider;
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
    builder.Host.UseSerilog();

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
    services.AddNextStarApiAuthentication(appSetting);
    //self
    services.AddCors(options =>
    {
        options.AddDefaultPolicy(builderAction =>
        {
            builderAction.WithOrigins(appSetting.AllowedOrigins.ToArray())
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials();
        });

    });

    //网关组件服务
    services.AddOcelot();

    #endregion

    // Register your own things directly with Autofac here. Don't
    // call builder.Populate(), that happens in AutofacServiceProviderFactory for you.
    var app = builder.Build();

    #region Configure

    MyServiceProvider = app.Services;

    if (app.Environment.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
    }

    app.UseRouting();
    app.UseCors();
    app.UseEndpoints(endpoints =>
    {
        endpoints.MapGet("/", async context => { await context.Response.WriteAsync("NextStar Gateway!"); });
    });

    var configuration = new OcelotPipelineConfiguration
    {
        AuthenticationMiddleware = NextStarAuthenticationMiddleware
    };

    //网关中间件
    app.UseOcelot(configuration).Wait();

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

async Task NextStarAuthenticationMiddleware(HttpContext httpContext, Func<Task> next)
{
    var downstreamRoute = httpContext.Items.DownstreamRoute();

    if (httpContext.Request.Method.ToUpper() != "OPTIONS" && IsAuthenticatedRoute(downstreamRoute))
    {
        Log.Information(
            "{@Path} is an authenticated route. PreAuthenticationMiddleware checking if client is authenticated",
            httpContext.Request.Path);

        var result =
            await httpContext.AuthenticateAsync(downstreamRoute.AuthenticationOptions.AuthenticationProviderKey);

        if (result.Principal?.Identity?.IsAuthenticated == true)
        {
            //判断Session是否存在在数据库中 不存在则返回401
            Guid? sessionId = result.Principal.GetNextStarSessionId();
            if (sessionId != null)
            {
                //判断SessionId是否存在
                var sessionStore = MyServiceProvider.GetService<INextStarSessionStore>();
                if (sessionStore != null && await sessionStore.IsExistsOrNotExpiredAsync(sessionId.Value))
                {
                    Log.Information("Client has been authenticated for {@Path}", httpContext.Request.Path);
                    //验证通过 执行下一步
                    await next.Invoke();
                    return;
                }
            }
        }

        // 认证失败返回401
        var error = new UnauthenticatedError(
            string.Format("Request for authenticated route {0} by {1} was unauthenticated", httpContext.Request.Path,
                httpContext.User.Identity.Name));

        Log.Warning(
            "Client has NOT been authenticated for {@Path} and pipeline error set. {@Error}", httpContext.Request.Path,
            error);

        httpContext.Items.SetError(error);
    }
    else
    {
        Log.Information("No authentication needed for {@Path}", httpContext.Request.Path);

        await next.Invoke();
    }
}

static bool IsAuthenticatedRoute(DownstreamRoute route)
{
    return route.IsAuthenticated;
}