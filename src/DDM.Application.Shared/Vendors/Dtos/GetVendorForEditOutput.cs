using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace DDM.Vendors.Dtos
{
    public class GetVendorForEditOutput
    {
        public CreateOrEditVendorDto Vendor { get; set; }

        public string VendorCategoryName { get; set; }

    }
}