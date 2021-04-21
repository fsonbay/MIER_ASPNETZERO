using DDM.SalesOrders.Dtos;
using System.Collections.Generic;
using Abp.Extensions;
using Abp.AutoMapper;

namespace DDM.Web.Areas.Portal.Models.SalesOrders
{
    [AutoMapFrom(typeof(_oldGetSalesOrderForEditOutput))]
    public class CreateOrEditSalesOrderViewModel : _oldGetSalesOrderForEditOutput
    {
        public bool IsEditMode => SalesOrder.Id.HasValue;
    }
}