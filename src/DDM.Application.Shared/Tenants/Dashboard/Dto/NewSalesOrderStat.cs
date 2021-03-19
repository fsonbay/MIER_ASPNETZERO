using System;
using System.Collections.Generic;
using System.Text;

namespace DDM.Tenants.Dashboard.Dto
{
    public class NewSalesOrderStat
    {
        public string CustomerName { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime Deadline { get; set; }
    }
}
