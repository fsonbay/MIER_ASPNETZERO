using System;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Views;
using Abp.Runtime.Session;
using Microsoft.AspNetCore.Mvc.Razor.Internal;
using DDM.Configuration;
using DDM.UiCustomization;
using DDM.UiCustomization.Dto;
using DDM.Web.Areas.Portal.Views.Shared.Themes;

namespace DDM.Web.Views
{
    public abstract class DDMRazorPage<TModel> : AbpRazorPage<TModel>
    {
        [RazorInject] public IAbpSession AbpSession { get; set; }

        [RazorInject] public IUiThemeCustomizerFactory UiThemeCustomizerFactory { get; set; }

        protected DDMRazorPage()
        {
            LocalizationSourceName = DDMConsts.LocalizationSourceName;
        }

        public async Task<UiCustomizationSettingsDto> GetTheme()
        {
            var themeCustomizer = await UiThemeCustomizerFactory.GetCurrentUiCustomizer();
            var settings = await themeCustomizer.GetUiSettings();
            return settings;
        }

        public async Task<string> GetContainerClass()
        {
            var theme = await GetTheme();
            return theme.BaseSettings.Layout.LayoutType == "fluid" ? "container-fluid" : "container";
        }
    }
}
