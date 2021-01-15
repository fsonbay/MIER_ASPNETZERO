using System.Collections.Generic;
using DDM.Caching.Dto;

namespace DDM.Web.Areas.Portal.Models.Maintenance
{
    public class MaintenanceViewModel
    {
        public IReadOnlyList<CacheDto> Caches { get; set; }
    }
}