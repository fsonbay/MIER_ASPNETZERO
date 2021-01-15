using Abp.Application.Services.Dto;
using System;

namespace DDM.CustomerCategories.Dtos
{
    public class GetAllCustomerCategoriesForExcelInput
    {
        public string Filter { get; set; }

        public string NameFilter { get; set; }

        public string DescriptionFilter { get; set; }

    }
}