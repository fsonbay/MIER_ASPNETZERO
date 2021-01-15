using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace DDM.Machines.Dtos
{
    public class CreateOrEditMachineDto : EntityDto<int?>
    {

        [Required]
        [StringLength(MachineConsts.MaxNameLength, MinimumLength = MachineConsts.MinNameLength)]
        public string Name { get; set; }

        public string Description { get; set; }

    }
}