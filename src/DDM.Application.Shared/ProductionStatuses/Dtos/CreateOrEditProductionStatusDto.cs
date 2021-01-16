using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace DDM.ProductionStatuses.Dtos
{
    public class CreateOrEditProductionStatusDto : EntityDto<int?>
    {

        [Required]
        [StringLength(ProductionStatusConsts.MaxNameLength, MinimumLength = ProductionStatusConsts.MinNameLength)]
        public string Name { get; set; }

        public string Description { get; set; }

    }
}