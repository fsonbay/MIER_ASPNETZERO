using DDM.VendorCategories.Dtos;

using Abp.Extensions;

namespace DDM.Web.Areas.Portal.Models.VendorCategories
{
    public class CreateOrEditVendorCategoryModalViewModel
    {
        public CreateOrEditVendorCategoryDto VendorCategory { get; set; }

        public bool IsEditMode => VendorCategory.Id.HasValue;
    }
}