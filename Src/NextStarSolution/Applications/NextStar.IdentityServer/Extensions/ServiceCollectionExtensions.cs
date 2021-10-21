using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NextStar.Framework.Core.Consts;
using NextStar.IdentityServer.Businesses;
using NextStar.IdentityServer.Configs;
using NextStar.IdentityServer.DbContexts;
using NextStar.IdentityServer.Filters;

namespace NextStar.IdentityServer.Extensions
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 添加认证框架
        /// </summary>
        /// <param name="service"></param>
        /// <param name="appSetting"></param>
        /// <returns></returns>
        public static IServiceCollection AddNextStarIdentityServer(this IServiceCollection service,
            AppSettingPartial appSetting)
        {
            string basePath = Directory.GetCurrentDirectory();
            //获取证书
            //获取证书
            var certificate = new X509Certificate2(
                Path.Combine(basePath, appSetting.Certificates.Path),
                appSetting.Certificates.Password);
            //获取持久化数据库连接字符串
            var connectionString = appSetting.DataBaseSetting.Account;

            service.AddIdentityServer(option =>
                {
                    option.Authentication.CookieLifetime = TimeSpan.FromDays(30);
                    option.Authentication.CookieSameSiteMode = Microsoft.AspNetCore.Http.SameSiteMode.Lax;
                    option.Authentication.CheckSessionCookieSameSiteMode = Microsoft.AspNetCore.Http.SameSiteMode.Lax;
                    option.Authentication.CheckSessionCookieName = NextStarCookie.SessionCookieName;
                })
                .AddCustomAuthorizeRequestValidator<NextStarCustomAuthorizeRequestValidator>()
                .AddInMemoryIdentityResources(IdentityServerConfig.IdentityResources)
                .AddInMemoryApiScopes(IdentityServerConfig.ApiScopes)
                .AddInMemoryClients(appSetting.IdentityServer.Clients)
                .AddSigningCredential(certificate)
                .AddOperationalStore(options =>
                {
                    options.ConfigureDbContext = b => b.UseSqlServer(connectionString);
                    options.EnableTokenCleanup = true;
                });

            return service;
        }

        public static IServiceCollection AddCustomDbContext(this IServiceCollection service, AppSetting appSetting)
        {
            service.AddDbContext<NextStarAccountDbContext>(options =>
                options.UseSqlServer(appSetting.DataBaseSetting.Account));
            return service;
        }

        public static IServiceCollection AddDependencyInjection(this IServiceCollection service)
        {
            service.AddTransient<ICommonBusiness, CommonBusiness>();
            return service;
        }
    }
}