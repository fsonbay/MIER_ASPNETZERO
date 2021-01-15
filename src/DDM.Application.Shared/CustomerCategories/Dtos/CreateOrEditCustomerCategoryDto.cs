using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace DDM.CustomerCategories.Dtos
{
    public class CreateOrEditCustomerCategoryDto : EntityDto<int?>
    {

        [Required]
        [StringLength(CustomerCategoryConsts.MaxNameLength, MinimumLength = CustomerCategoryConsts.MinNameLength)]
        public string Name { get; set; }

        public string Description { get; set; }

    }
}