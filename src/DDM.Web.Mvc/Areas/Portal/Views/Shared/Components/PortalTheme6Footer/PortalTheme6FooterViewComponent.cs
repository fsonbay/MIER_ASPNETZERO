using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DDM.Web.Areas.Portal.Models.Layout;
using DDM.Web.Session;
using DDM.Web.Views;

namespace DDM.Web.Areas.Portal.Views.Shared.Components.PortalTheme6Footer
{
    public class PortalTheme6FooterViewComponent : DDMViewComponent
    {
        private readonly IPerRequestSessionCache _sessionCache;

        public PortalTheme6FooterViewComponent(IPerRequestSessionCache sessionCache)
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
