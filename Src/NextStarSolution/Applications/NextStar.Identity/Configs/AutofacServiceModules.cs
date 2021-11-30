using System.Reflection;
using Autofac;

namespace NextStar.Identity.Configs;

public class AutofacServiceModules: Autofac.Module
{
    protected override void Load(ContainerBuilder builder)
    {
        base.Load(builder);

        var identityAssembly = typeof(IdentityModule).GetTypeInfo().Assembly;
        LoadAssembly(builder, identityAssembly);
    }

    private void LoadAssembly(ContainerBuilder builder, Assembly ass)
    {
        // 注入IxxxBusiness ,IxxxRepository,IXXXXManager位多实例
        builder.RegisterAssemblyTypes(ass)
            .Where(t => (t.Name.EndsWith("Business") && !t.Name.EndsWith("SingleBusiness"))
                        || t.Name.EndsWith("Repository")
                        || (t.Name.EndsWith("Manager") && !t.Name.EndsWith("SingleManager")))
            .AsImplementedInterfaces();
        //注入为单实例
        builder.RegisterAssemblyTypes(ass)
            .Where(t => t.Name.EndsWith("SingleBusiness") || t.Name.EndsWith("SingleManager") || t.Name.EndsWith("Store"))
            .AsImplementedInterfaces()
            .SingleInstance();
    }
}