using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace DDM.SalesOrders.Dtos
{
    public class GetSalesOrderLineForEditOutput
    {
        public CreateOrEditSalesOrderLineDto SalesOrderLine { get; set; }
        public List<ComboboxItemDto> Materials { get; set; }
        public List<ComboboxItemDto> Machines { get; set; }
        public GetSalesOrderLineForEditOutput()
        {
            Materials = new List<ComboboxItemDto>();
            Machines = new List<ComboboxItemDto>();
        }

    }
}
