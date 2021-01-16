using System;
using Abp.Application.Services.Dto;

namespace DDM.ProductionStatuses.Dtos
{
    public class ProductionStatusDto : EntityDto
    {
        public string Name { get; set; }

        public string Description { get; set; }

    }
}