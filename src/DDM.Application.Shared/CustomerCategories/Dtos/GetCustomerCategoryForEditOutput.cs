using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace DDM.CustomerCategories.Dtos
{
    public class GetCustomerCategoryForEditOutput
    {
        public CreateOrEditCustomerCategoryDto CustomerCategory { get; set; }

    }
}