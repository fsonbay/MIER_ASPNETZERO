using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace DDM.ProductionStatuses.Dtos
{
    public class GetProductionStatusForEditOutput
    {
        public CreateOrEditProductionStatusDto ProductionStatus { get; set; }

    }
}