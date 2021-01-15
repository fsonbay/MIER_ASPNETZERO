using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace DDM.Machines.Dtos
{
    public class GetMachineForEditOutput
    {
        public CreateOrEditMachineDto Machine { get; set; }

    }
}