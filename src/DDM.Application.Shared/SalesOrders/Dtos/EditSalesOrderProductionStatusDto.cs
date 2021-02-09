using System;
using System.Collections.Generic;
using System.Text;

namespace DDM.SalesOrders.Dtos
{
    public class EditSalesOrderProductionStatusDto
    {
        public int SalesOrderId { get; set; }
        public int ProductionStatusID { get; set; }
        public string Notes { get; set; }
    }
}
