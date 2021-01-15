using Abp.AspNetCore.Mvc.Views;

namespace DDM.Web.Views
{
    public abstract class DDMRazorPage<TModel> : AbpRazorPage<TModel>
    {
        protected DDMRazorPage()
        {
            LocalizationSourceName = DDMConsts.LocalizationSourceName;
        }
    }
}
