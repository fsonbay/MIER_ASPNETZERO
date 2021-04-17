using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace DDM.SalesOrders.Dtos
{
    public class GetSalesOrderForEditOutput
    {
        public CreateOrEditSalesOrderDto SalesOrder { get; set; }
        public List<ComboboxItemDto> Customers { get; set; }
        public List<ComboboxItemDto> ProductionStatuses { get; set; }
      //  public List<SalesOrderLine> SalesOrderLines { get; set; }
        public string CustomerName { get; set; }
        public GetSalesOrderForEditOutput()
        {
            Customers = new List<ComboboxItemDto>();
            ProductionStatuses = new List<ComboboxItemDto>();
        }

    }
}