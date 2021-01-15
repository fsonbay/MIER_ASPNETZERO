using Abp.AspNetCore.Mvc.Views;
using Abp.Runtime.Session;
using Microsoft.AspNetCore.Mvc.Razor.Internal;

namespace DDM.Web.Public.Views
{
    public abstract class DDMRazorPage<TModel> : AbpRazorPage<TModel>
    {
        [RazorInject]
        public IAbpSession AbpSession { get; set; }

        protected DDMRazorPage()
        {
            LocalizationSourceName = DDMConsts.LocalizationSourceName;
        }
    }
}
