using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DDM.Web.Areas.Portal.Models.Layout;
using DDM.Web.Session;
using DDM.Web.Views;

namespace DDM.Web.Areas.Portal.Views.Shared.Themes.Theme8.Components.PortalTheme8Footer
{
    public class PortalTheme8FooterViewComponent : DDMViewComponent
    {
        private readonly IPerRequestSessionCache _sessionCache;

        public PortalTheme8FooterViewComponent(IPerRequestSessionCache sessionCache)
        {
            _sessionCache = sessionCache;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var footerModel = new FooterViewModel
            {
                LoginInformations = await _sessionCache.GetCurrentLoginInformationsAsync()
            };

            return View(footerModel);
        }
    }
}
