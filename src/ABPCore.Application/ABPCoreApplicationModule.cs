using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using ABPCore.EntityFrameworkCore;

namespace ABPCore
{
    [DependsOn(
        typeof(ABPCoreCoreModule),
        typeof(ABPCoreEntityFrameworkCoreModule),
        typeof(AbpAutoMapperModule))]
    public class ABPCoreApplicationModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(ABPCoreApplicationModule).GetAssembly());
        }
    }
}