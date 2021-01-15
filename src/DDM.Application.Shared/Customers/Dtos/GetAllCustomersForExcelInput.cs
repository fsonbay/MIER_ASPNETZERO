using Abp.Application.Services.Dto;
using System;

namespace DDM.Customers.Dtos
{
    public class GetAllCustomersForExcelInput
    {
        public string Filter { get; set; }

        public string NameFilter { get; set; }

        public string CompanyFilter { get; set; }

        public string PhoneFilter { get; set; }

        public string EmailFilter { get; set; }

        public string AddressFilter { get; set; }

        public string CustomerCategoryNameFilter { get; set; }

    }
}