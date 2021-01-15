using System.Collections.Generic;
using DDM.Editions.Dto;

namespace DDM.Web.Areas.Portal.Models.Tenants
{
    public class TenantIndexViewModel
    {
        public List<SubscribableEditionComboboxItemDto> EditionItems { get; set; }
    }
}