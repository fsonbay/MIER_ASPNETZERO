using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace DDM.SalesOrders.Dtos
{
    public class SalesOrderLineOutput
    {
        public SalesOrderLineDto SalesOrderLine { get; set; }
        public List<ComboboxItemDto> Machines { get; set; }
        public List<ComboboxItemDto> Materials { get; set; }
        public SalesOrderLineOutput()
        {
            Machines = new List<ComboboxItemDto>();
            Materials = new List<ComboboxItemDto>();
        }

    }
}
