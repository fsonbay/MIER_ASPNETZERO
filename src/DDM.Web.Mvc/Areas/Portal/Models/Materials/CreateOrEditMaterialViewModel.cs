using DDM.Materials.Dtos;

using Abp.Extensions;

namespace DDM.Web.Areas.Portal.Models.Materials
{
    public class CreateOrEditMaterialModalViewModel
    {
        public CreateOrEditMaterialDto Material { get; set; }

        public bool IsEditMode => Material.Id.HasValue;
    }
}