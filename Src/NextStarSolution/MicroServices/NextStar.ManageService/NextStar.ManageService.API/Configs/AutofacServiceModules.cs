using System.Reflection;
using Autofac;
using NextStar.ManageService.Core;

namespace NextStar.ManageService.API.Configs
{
    public class AutofacServiceModules : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            // Core 核心方式
            var coreAssembly = typeof(ManageServiceCoreModule).GetTypeInfo().Assembly;
            // 注入IxxxBusiness 和 IxxxRepository
            builder.RegisterAssemblyTypes(coreAssembly)
                .Where(t => t.Name.EndsWith("Business") || t.Name.EndsWith("Repository"))
                .AsImplementedInterfaces();
            
            // 注入为单实例
            builder.RegisterAssemblyTypes(coreAssembly)
                .Where(t => t.Name.EndsWith("SingleBusiness") || t.Name.EndsWith("SingleRepository"))
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}