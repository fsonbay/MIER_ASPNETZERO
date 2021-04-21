using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace DDM.SalesOrders.Dtos
{
    public class _oldGetSalesOrderForEditOutput
    {
        public CreateOrEditSalesOrderDto SalesOrder { get; set; }
        public List<ComboboxItemDto> Customers { get; set; }
        public List<ComboboxItemDto> ProductionStatuses { get; set; }
        public List<GetSalesOrderLineForEditOutput> SalesOrderLines { get; set; }
        public _oldGetSalesOrderForEditOutput()
        {
            Customers = new List<ComboboxItemDto>();
            ProductionStatuses = new List<ComboboxItemDto>();
        }

    }
}