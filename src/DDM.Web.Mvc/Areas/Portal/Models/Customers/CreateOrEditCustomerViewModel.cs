using DDM.Customers.Dtos;
using System.Collections.Generic;

using Abp.Extensions;

namespace DDM.Web.Areas.Portal.Models.Customers
{
    public class CreateOrEditCustomerModalViewModel
    {
        public CreateOrEditCustomerDto Customer { get; set; }

        public string CustomerCategoryName { get; set; }

        public List<CustomerCustomerCategoryLookupTableDto> CustomerCustomerCategoryList { get; set; }

        public bool IsEditMode => Customer.Id.HasValue;
    }
}