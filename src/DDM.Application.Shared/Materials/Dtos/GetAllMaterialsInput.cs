using Abp.Application.Services.Dto;
using System;

namespace DDM.Materials.Dtos
{
    public class GetAllMaterialsInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }

    }
}