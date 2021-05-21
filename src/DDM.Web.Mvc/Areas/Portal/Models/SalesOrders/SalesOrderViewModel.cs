using Abp.AutoMapper;
using DDM.SalesOrders.Dtos;

namespace DDM.Web.Areas.Portal.Models.SalesOrders
{
    [AutoMapFrom(typeof(SalesOrderOutput))]
    public class SalesOrderViewModel : SalesOrderOutput
    {
        public int CustomerId { get; set; }
        public bool IsEditMode => SalesOrder.Id.HasValue;



    }
}