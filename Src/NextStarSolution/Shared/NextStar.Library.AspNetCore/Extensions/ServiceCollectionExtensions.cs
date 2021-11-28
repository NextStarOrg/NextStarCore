using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;
using NextStar.Library.AspNetCore.Abstractions;
using NextStar.Library.AspNetCore.Cache;
using NextStar.Library.AspNetCore.DbContexts;
using NextStar.Library.AspNetCore.Stores;
using NextStar.Library.Core.Abstractions;

namespace NextStar.Library.AspNetCore.Extensions;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Gateway 自定义认证
    /// </summary>
    /// <param name="services"></param>
    /// <param name="appSetting"></param>
    /// <returns></returns>
    public static IServiceCollection AddNextStarApiAuthentication(this IServiceCollection services,
        AppSetting appSetting)
    {
        services.AddAuthentication()
            .AddJwtBearer("nextstar", options =>
            {
                options.RequireHttpsMetadata = false;
                options.Authority = appSetting.Authority;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    ClockSkew = TimeSpan.FromSeconds(30),
                };
            });
        return services;
    }

    /// <summary>
    /// Api 自定义授权
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddNextStarApiAuthorization(this IServiceCollection services)
    {
        return services.AddAuthorization(options =>
        {
            options.AddPolicy("apiScope", policy =>
            {
                policy.RequireAuthenticatedUser();
                policy.RequireClaim("scope", "nextstarapi");
            });
        });
    }

    /// <summary>
    /// Api 自定义认证
    /// </summary>
    /// <param name="services"></param>
    /// <param name="appSetting"></param>
    /// <returns></returns>
    public static IServiceCollection AddNextStarJwtAuthentication(this IServiceCollection services,
        AppSetting appSetting)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.RequireHttpsMetadata = false;
                options.Authority = appSetting.Authority;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false, //验证
                    ValidateAudience = false, //
                    ClockSkew = TimeSpan.FromSeconds(30),
                };
            });
        return services;
    }

    /// <summary>
    /// Redis 缓存配置
    /// </summary>
    /// <param name="services"></param>
    /// <param name="appSetting"></param>
    /// <returns></returns>
    public static IServiceCollection AddCustomRedisCache(this IServiceCollection services, AppSetting appSetting)
    {
        return services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = appSetting.DataBaseSetting.Redis;
        });
    }
    
    /// <summary>
    /// Session 配置
    /// </summary>
    /// <param name="services"></param>
    /// <param name="appSetting"></param>
    /// <returns></returns>
    public static IServiceCollection AddSessionStore(this IServiceCollection services, AppSetting appSetting)
    {
        services.AddDbContext<SessionDbContext>(options =>
                options.UseSqlServer(appSetting.DataBaseSetting.Account),
            contextLifetime: ServiceLifetime.Transient,
            optionsLifetime: ServiceLifetime.Singleton);
        services.TryAddSingleton(typeof(IDistributedCache<>), typeof(DistributedCache<>));
        services.TryAddTransient<INextStarSessionStore,NextStarSessionStore>();
        return services;
    }
    
    /// <summary>
    /// ApplicationConfig 配置
    /// </summary>
    /// <param name="services"></param>
    /// <param name="appSetting"></param>
    /// <returns></returns>
    public static IServiceCollection AddApplicationConfigStore(this IServiceCollection services, AppSetting appSetting)
    {
        services.AddDbContext<ManagementDbContext>(options =>
                options.UseSqlServer(appSetting.DataBaseSetting.Management),
            contextLifetime: ServiceLifetime.Transient,
            optionsLifetime: ServiceLifetime.Singleton);
        services.TryAddSingleton(typeof(IDistributedCache<>), typeof(DistributedCache<>));
        services.TryAddTransient<IApplicationConfigStore,ApplicationConfigStore>();
        return services;
    }
}