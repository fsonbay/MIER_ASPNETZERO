using Abp.Application.Services.Dto;
using System;

namespace DDM.Machines.Dtos
{
    public class GetAllMachinesInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }

    }
}