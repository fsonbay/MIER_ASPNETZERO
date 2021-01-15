using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DDM.Web.Areas.Portal.Models.Layout;
using DDM.Web.Views;

namespace DDM.Web.Areas.Portal.Views.Shared.Components.
    PortalQuickThemeSelect
{
    public class PortalQuickThemeSelectViewComponent : DDMViewComponent
    {
        public Task<IViewComponentResult> InvokeAsync(string cssClass)
        {
            return Task.FromResult<IViewComponentResult>(View(new QuickThemeSelectionViewModel
            {
                CssClass = cssClass
            }));
        }
    }
}
