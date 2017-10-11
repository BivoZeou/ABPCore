using Abp.AspNetCore.Mvc.Controllers;

namespace ABPCore.Web.Controllers
{
    public abstract class ABPCoreControllerBase: AbpController
    {
        protected ABPCoreControllerBase()
        {
            LocalizationSourceName = ABPCoreConsts.LocalizationSourceName;
        }
    }
}