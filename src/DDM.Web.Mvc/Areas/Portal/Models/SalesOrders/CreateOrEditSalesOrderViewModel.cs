using DDM.SalesOrders.Dtos;
using System.Collections.Generic;
using Abp.Extensions;
using Abp.AutoMapper;

namespace DDM.Web.Areas.Portal.Models.SalesOrders
{
    [AutoMapFrom(typeof(GetSalesOrderForEditOutput))]
    public class CreateOrEditSalesOrderViewModel : GetSalesOrderForEditOutput
    {
        public bool IsEditMode => SalesOrder.Id.HasValue;
    }
}