using DDM.Customers.Dtos;
using System.Collections.Generic;

using Abp.Extensions;
using Abp.AutoMapper;

namespace DDM.Web.Areas.Portal.Models.Customers
{
    [AutoMapFrom(typeof(GetCustomerForEditOutput))]
    public class CreateOrEditCustomerModalViewModel : GetCustomerForEditOutput
    {
        public bool IsEditMode => Customer.Id.HasValue;
    }
}