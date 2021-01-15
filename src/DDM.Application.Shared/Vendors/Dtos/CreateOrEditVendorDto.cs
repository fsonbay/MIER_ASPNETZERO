using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace DDM.Vendors.Dtos
{
    public class CreateOrEditVendorDto : EntityDto<int?>
    {

        [Required]
        [StringLength(VendorConsts.MaxNameLength, MinimumLength = VendorConsts.MinNameLength)]
        public string Name { get; set; }

        public string Description { get; set; }

        public int VendorCategoryId { get; set; }

    }
}