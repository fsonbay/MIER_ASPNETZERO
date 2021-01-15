using System.Collections.Generic;
using DDM.DashboardCustomization.Dto;

namespace DDM.Web.Areas.Portal.Models.CustomizableDashboard
{
    public class AddWidgetViewModel
    {
        public List<WidgetOutput> Widgets { get; set; }

        public string DashboardName { get; set; }

        public string PageId { get; set; }
    }
}
