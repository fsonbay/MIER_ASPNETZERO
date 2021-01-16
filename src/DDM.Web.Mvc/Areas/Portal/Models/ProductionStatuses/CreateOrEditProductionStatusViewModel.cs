using DDM.ProductionStatuses.Dtos;

using Abp.Extensions;

namespace DDM.Web.Areas.Portal.Models.ProductionStatuses
{
    public class CreateOrEditProductionStatusModalViewModel
    {
        public CreateOrEditProductionStatusDto ProductionStatus { get; set; }

        public bool IsEditMode => ProductionStatus.Id.HasValue;
    }
}