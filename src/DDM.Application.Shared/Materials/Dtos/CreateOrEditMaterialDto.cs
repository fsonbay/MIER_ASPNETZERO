using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace DDM.Materials.Dtos
{
    public class CreateOrEditMaterialDto : EntityDto<int?>
    {

        [Required]
        [StringLength(MaterialConsts.MaxNameLength, MinimumLength = MaterialConsts.MinNameLength)]
        public string Name { get; set; }

        public string Description { get; set; }

    }
}