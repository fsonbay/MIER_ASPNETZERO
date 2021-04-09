using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace DDM.Customers.Dtos
{
    public class GetCustomerForEditOutput
    {
        public CreateOrEditCustomerDto Customer { get; set; }
        public List<ComboboxItemDto> CustomerCategories { get; set; }
        public string CustomerCategoryName { get; set; }
        public GetCustomerForEditOutput()
        {
            CustomerCategories = new List<ComboboxItemDto>();
        }

    }
}