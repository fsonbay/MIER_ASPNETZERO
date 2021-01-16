using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace DDM.SalesOrders.Dtos
{
    public class CreateOrEditSalesOrderLineDto : EntityDto<int?>
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string Quantity { get; set; }

        public string UnitPrice { get; set; }

        public string LineAmount { get; set; }

        public int SalesOrderId { get; set; }

        public int MachineId { get; set; }

        public int MaterialId { get; set; }

        public bool MarkForDelete { get; set; }
    }
}
