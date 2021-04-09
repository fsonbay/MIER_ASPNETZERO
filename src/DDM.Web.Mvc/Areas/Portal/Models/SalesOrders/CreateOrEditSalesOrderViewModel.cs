using DDM.SalesOrders.Dtos;
using System.Collections.Generic;
using Abp.Extensions;
using Abp.AutoMapper;

namespace DDM.Web.Areas.Portal.Models.SalesOrders
{
    [AutoMapFrom(typeof(GetSalesOrderForEditOutput))]
    public class CreateOrEditSalesOrderViewModel : GetSalesOrderForEditOutput
    {
        //public CreateOrEditSalesOrderDto SalesOrder { get; set; }

        //public string CustomerName { get; set; }

        //public List<SalesOrderCustomerLookupTableDto> SalesOrderCustomerList { get; set; }
        //public List<SalesOrderMachineLookupTableDto> SalesOrderMachineList { get; set; }
        //public List<SalesOrderMaterialLookupTableDto> SalesOrderMaterialList { get; set; }

        public bool IsEditMode => SalesOrder.Id.HasValue;
    }
}