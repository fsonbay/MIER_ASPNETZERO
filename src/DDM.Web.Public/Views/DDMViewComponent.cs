using Abp.AspNetCore.Mvc.ViewComponents;

namespace DDM.Web.Public.Views
{
    public abstract class DDMViewComponent : AbpViewComponent
    {
        protected DDMViewComponent()
        {
            LocalizationSourceName = DDMConsts.LocalizationSourceName;
        }
    }
}