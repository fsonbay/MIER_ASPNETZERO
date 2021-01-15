using Abp.Application.Navigation;

namespace DDM.Web.Areas.Portal.Models.Layout
{
    public class MenuViewModel
    {
        public UserMenu Menu { get; set; }

        public string CurrentPageName { get; set; }
    }
}