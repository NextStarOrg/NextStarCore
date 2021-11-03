using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;
using NextStar.Framework.Abstractions.Cache;
using NextStar.Framework.Abstractions.Config;
using NextStar.Framework.Abstractions.Session;
using NextStar.Framework.AspNetCore.Cache;
using NextStar.Framework.AspNetCore.Config;
using NextStar.Framework.AspNetCore.DbContexts;
using NextStar.Framework.AspNetCore.Session;
using NextStar.Framework.AspNetCore.Stores;
using NextStar.Framework.Core.Consts;

namespace NextStar.Framework.AspNetCore.Extensions
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Gateway 自定义认证
        /// </summary>
        /// <param name="services"></param>
        /// <param name="appSetting"></param>
        /// <returns></returns>
        public static IServiceCollection AddNextStarApiAuthentication(this IServiceCollection services,AppSetting appSetting)
        {
            services.AddAuthentication()
                .AddJwtBearer("nextstarapi", options =>
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
        /// 添加NextStar相关的Session
        /// </summary>
        /// <param name="service"></param>
        /// <param name="appSetting"></param>
        /// <returns></returns>
        public static IServiceCollection AddNextStarSession(this IServiceCollection service,AppSetting appSetting)
        {
            service.AddDbContext<NextStarSessionDbContext>(options =>
                    options.UseSqlServer(appSetting.DataBaseSetting.Account),
                contextLifetime: ServiceLifetime.Transient,
                optionsLifetime: ServiceLifetime.Singleton);
            service.TryAddSingleton(typeof(IDistributedCache<>), typeof(DistributedCache<>));
            service.TryAddTransient<INextStarSessionStore,NextStarSessionStore>();
            service.TryAddTransient<INextStarSession,NextStarSession>();
            return service;
        }

        public static IServiceCollection AddApplicationConfig(this IServiceCollection service)
        {
            service.TryAddTransient<INextStarApplicationConfig,NextStarApplicationConfig>();
            return service;
        }
    }
}