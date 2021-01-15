using System;
using Abp.Application.Services.Dto;

namespace DDM.VendorCategories.Dtos
{
    public class VendorCategoryDto : EntityDto
    {
        public string Name { get; set; }

        public string Description { get; set; }

    }
}