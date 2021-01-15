using Abp.AspNetCore.Mvc.ViewComponents;

namespace DDM.Web.Views
{
    public abstract class DDMViewComponent : AbpViewComponent
    {
        protected DDMViewComponent()
        {
            LocalizationSourceName = DDMConsts.LocalizationSourceName;
        }
    }
}