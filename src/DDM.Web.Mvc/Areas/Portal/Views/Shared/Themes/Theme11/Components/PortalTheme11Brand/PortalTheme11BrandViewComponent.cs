using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DDM.Web.Areas.Portal.Models.Layout;
using DDM.Web.Session;
using DDM.Web.Views;

namespace DDM.Web.Areas.Portal.Views.Shared.Themes.Theme11.Components.PortalTheme11Brand
{
    public class PortalTheme11BrandViewComponent : DDMViewComponent
    {
        private readonly IPerRequestSessionCache _sessionCache;

        public PortalTheme11BrandViewComponent(IPerRequestSessionCache sessionCache)
        {
            _sessionCache = sessionCache;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var headerModel = new HeaderViewModel
            {
                LoginInformations = await _sessionCache.GetCurrentLoginInformationsAsync()
            };

            return View(headerModel);
        }
    }
}
