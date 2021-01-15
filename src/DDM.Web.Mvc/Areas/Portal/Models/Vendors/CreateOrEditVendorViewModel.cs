using DDM.Vendors.Dtos;
using System.Collections.Generic;

using Abp.Extensions;

namespace DDM.Web.Areas.Portal.Models.Vendors
{
    public class CreateOrEditVendorModalViewModel
    {
        public CreateOrEditVendorDto Vendor { get; set; }

        public string VendorCategoryName { get; set; }

        public List<VendorVendorCategoryLookupTableDto> VendorVendorCategoryList { get; set; }

        public bool IsEditMode => Vendor.Id.HasValue;
    }
}