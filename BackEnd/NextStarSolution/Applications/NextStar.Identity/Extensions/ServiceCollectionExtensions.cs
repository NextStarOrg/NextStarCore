using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore;
using NextStar.Identity.Configs;
using NextStar.Identity.DbContexts;
using NextStar.Identity.Filters;
using NextStar.Library.Core.Abstractions;
using NextStar.Library.Core.Consts;

namespace NextStar.Identity.Extensions;

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
            var connectionString = appSetting.DataBaseSetting.NextStar;

            service.AddIdentityServer(option =>
                {
                    option.Authentication.CookieLifetime = TimeSpan.FromDays(30);
                    option.Authentication.CookieSameSiteMode = Microsoft.AspNetCore.Http.SameSiteMode.Lax;
                    option.Authentication.CheckSessionCookieSameSiteMode = Microsoft.AspNetCore.Http.SameSiteMode.Lax;
                    option.Authentication.CheckSessionCookieName = NextStarCookie.IdentitySessionCookieName;
                })
                .AddCustomAuthorizeRequestValidator<NextStarCustomAuthorizeRequestValidator>()
                .AddInMemoryIdentityResources(IdentityServerConfig.IdentityResources)
                .AddInMemoryApiScopes(IdentityServerConfig.ApiScopes)
                .AddInMemoryClients(appSetting.IdentityServer.Clients)
                .AddSigningCredential(certificate)
                // .AddOperationalStore(options =>
                // {
                //     options.ConfigureDbContext = b => b.UseSqlServer(connectionString);
                //     options.EnableTokenCleanup = true;
                // })
                ;

            return service;
        }

        public static IServiceCollection AddCustomDbContext(this IServiceCollection service, AppSetting appSetting)
        {
            service.AddDbContext<NextStarDbContext>(options =>
                options.UseSqlServer(appSetting.DataBaseSetting.NextStar));
            return service;
        }

        public static IServiceCollection AddDependencyInjection(this IServiceCollection service)
        {
            // service.AddTransient<ICommonBusiness, CommonBusiness>();
            // service.AddTransient<IAccountBusiness, AccountBusiness>();
            // service.AddTransient<IUserRepository, UserRepository>();
            return service;
        }
}