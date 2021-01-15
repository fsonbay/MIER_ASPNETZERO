using System;
using Abp.Application.Services.Dto;

namespace DDM.CustomerCategories.Dtos
{
    public class CustomerCategoryDto : EntityDto
    {
        public string Name { get; set; }

        public string Description { get; set; }

    }
}