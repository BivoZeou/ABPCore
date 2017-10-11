using Abp.Modules;
using Abp.Reflection.Extensions;
using ABPCore.Localization;

namespace ABPCore
{
    public class ABPCoreCoreModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Auditing.IsEnabledForAnonymousUsers = true;

            


            ABPCoreLocalizationConfigurer.Configure(Configuration.Localization);
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(ABPCoreCoreModule).GetAssembly());
        }
    }
}