using Abp.AutoMapper;
using DDM.SalesOrders.Dtos;

namespace DDM.Web.Areas.Portal.Models.SalesOrders
{
    [AutoMapFrom(typeof(SalesOrderOutput))]
    public class SalesOrderViewModel : SalesOrderOutput
    {
        public bool IsEditMode => SalesOrder.Id.HasValue;
    }
}