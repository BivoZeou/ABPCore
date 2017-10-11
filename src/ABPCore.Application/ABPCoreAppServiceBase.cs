using Abp.Application.Services;

namespace ABPCore
{
    /// <summary>
    /// Derive your application services from this class.
    /// </summary>
    public abstract class ABPCoreAppServiceBase : ApplicationService
    {
        protected ABPCoreAppServiceBase()
        {
            LocalizationSourceName = ABPCoreConsts.LocalizationSourceName;
        }
    }
}