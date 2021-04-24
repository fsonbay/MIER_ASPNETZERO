using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DDM.SalesOrders.Dtos
{
    public class SalesOrderLineDto : EntityDto
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Quantity { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal LineAmount { get; set; }
        public bool MarkForDelete { get; set; }

        public int SalesOrderId { get; set; }

        public int MachineId { get; set; }

        public int MaterialId { get; set; }

        public List<ComboboxItemDto> Machines { get; set; }
        public List<ComboboxItemDto> Materials { get; set; }

    }
}
