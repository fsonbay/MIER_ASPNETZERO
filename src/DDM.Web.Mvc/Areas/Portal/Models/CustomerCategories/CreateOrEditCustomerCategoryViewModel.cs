using DDM.CustomerCategories.Dtos;

using Abp.Extensions;

namespace DDM.Web.Areas.Portal.Models.CustomerCategories
{
    public class CreateOrEditCustomerCategoryModalViewModel
    {
        public CreateOrEditCustomerCategoryDto CustomerCategory { get; set; }

        public bool IsEditMode => CustomerCategory.Id.HasValue;
    }
}