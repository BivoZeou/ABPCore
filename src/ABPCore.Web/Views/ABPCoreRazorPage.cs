using Abp.AspNetCore.Mvc.Views;

namespace ABPCore.Web.Views
{
    public abstract class ABPCoreRazorPage<TModel> : AbpRazorPage<TModel>
    {
        protected ABPCoreRazorPage()
        {
            LocalizationSourceName = ABPCoreConsts.LocalizationSourceName;
        }
    }
}
