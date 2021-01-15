using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace DDM.VendorCategories.Dtos
{
    public class CreateOrEditVendorCategoryDto : EntityDto<int?>
    {

        [Required]
        [StringLength(VendorCategoryConsts.MaxNameLength, MinimumLength = VendorCategoryConsts.MinNameLength)]
        public string Name { get; set; }

        public string Description { get; set; }

    }
}