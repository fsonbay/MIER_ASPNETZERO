using DDM.Customers.Dtos;
using System.Collections.Generic;

using Abp.Extensions;
using Abp.AutoMapper;

namespace DDM.Web.Areas.Portal.Models.Customers
{
    [AutoMapFrom(typeof(GetCustomerForEditOutput))]
    public class CreateOrEditCustomerModalViewModel : GetCustomerForEditOutput
    {
        //public CreateOrEditCustomerDto Customer { get; set; }

        //public string CustomerCategoryName { get; set; }

        //public List<CustomerCustomerCategoryLookupTableDto> CustomerCustomerCategoryList { get; set; }

        public bool IsEditMode => Customer.Id.HasValue;
    }
}