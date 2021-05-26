using Abp.Application.Services.Dto;
using DDM.SalesInvoices.Dtos;
using DDM.SalesOrderLines.Dtos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DDM.SalesOrders.Dtos
{
    public class SalesOrderOutput
    {
        public SalesOrderDto SalesOrder { get; set; }
        public SalesInvoiceDto SalesInvoice { get; set; }
        public List<ComboboxItemDto> Customers { get; set; }
        public List<ComboboxItemDto> ProductionStatuses { get; set; }

        public List<SalesOrderLineDto> SalesOrderLines { get; set; }

        public SalesOrderOutput()
        {
            Customers = new List<ComboboxItemDto>();
            ProductionStatuses = new List<ComboboxItemDto>();
            SalesOrderLines = new List<SalesOrderLineDto>();
            
        }

    }
}
