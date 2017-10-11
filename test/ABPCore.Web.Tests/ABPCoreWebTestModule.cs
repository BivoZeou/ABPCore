using System.Reflection;
using Abp.AspNetCore.TestBase;
using Abp.Modules;
using Abp.Reflection.Extensions;
using ABPCore.Web.Startup;

namespace ABPCore.Web.Tests
{
    [DependsOn(
        typeof(ABPCoreWebModule),
        typeof(AbpAspNetCoreTestBaseModule)
        )]
    public class ABPCoreWebTestModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(ABPCoreWebTestModule).GetAssembly());
        }
    }
}