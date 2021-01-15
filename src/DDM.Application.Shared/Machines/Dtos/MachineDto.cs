using System;
using Abp.Application.Services.Dto;

namespace DDM.Machines.Dtos
{
    public class MachineDto : EntityDto
    {
        public string Name { get; set; }

        public string Description { get; set; }

    }
}