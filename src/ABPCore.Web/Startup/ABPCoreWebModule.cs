using Abp.AspNetCore;
using Abp.AspNetCore.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using ABPCore.Configuration;
using ABPCore.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace ABPCore.Web.Startup
{
    [DependsOn(
        typeof(ABPCoreApplicationModule), 
        typeof(ABPCoreEntityFrameworkCoreModule), 
        typeof(AbpAspNetCoreModule))]
    public class ABPCoreWebModule : AbpModule
    {
        private readonly IConfigurationRoot _appConfiguration;

        public ABPCoreWebModule(IHostingEnvironment env)
        {
            _appConfiguration = AppConfigurations.Get(env.ContentRootPath, env.EnvironmentName);
        }

        public override void PreInitialize()
        {
            Configuration.DefaultNameOrConnectionString = _appConfiguration.GetConnectionString(ABPCoreConsts.ConnectionStringName);

            Configuration.Navigation.Providers.Add<ABPCoreNavigationProvider>();

            Configuration.Modules.AbpAspNetCore()
                .CreateControllersForAppServices(
                    typeof(ABPCoreApplicationModule).GetAssembly()
                );
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(ABPCoreWebModule).GetAssembly());
        }
    }
}