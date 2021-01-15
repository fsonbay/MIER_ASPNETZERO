using System;
using Abp.Application.Services.Dto;

namespace DDM.Materials.Dtos
{
    public class MaterialDto : EntityDto
    {
        public string Name { get; set; }

        public string Description { get; set; }

    }
}