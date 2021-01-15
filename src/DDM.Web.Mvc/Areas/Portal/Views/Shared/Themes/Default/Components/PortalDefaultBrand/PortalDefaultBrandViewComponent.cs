using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DDM.Web.Areas.Portal.Models.Layout;
using DDM.Web.Session;
using DDM.Web.Views;

namespace DDM.Web.Areas.Portal.Views.Shared.Themes.Default.Components.PortalDefaultBrand
{
    public class PortalDefaultBrandViewComponent : DDMViewComponent
    {
        private readonly IPerRequestSessionCache _sessionCache;

        public PortalDefaultBrandViewComponent(IPerRequestSessionCache sessionCache)
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
