using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NextStar.Framework.Core.Consts;
using NextStar.ManageService.Core.DbContexts;

namespace NextStar.ManageService.API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDatabase(this IServiceCollection service, AppSetting appSetting)
        {
            service.AddDbContext<AccountDbContext>(options => options.UseSqlServer(appSetting.DataBaseSetting.Account));
            return service;
        } 
    }
}