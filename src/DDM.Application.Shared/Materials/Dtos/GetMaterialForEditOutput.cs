using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace DDM.Materials.Dtos
{
    public class GetMaterialForEditOutput
    {
        public CreateOrEditMaterialDto Material { get; set; }

    }
}