using System;
using System.Collections.Generic;
using System.Text;

namespace DDM.Tenants.Dashboard.Dto
{
    public class GetNewSalesOrderOutput
    {
        public GetNewSalesOrderOutput(List<NewSalesOrderStat> stats)
        {
            Stats = stats;
        }

        public GetNewSalesOrderOutput()
        {
            Stats = new List<NewSalesOrderStat>();
        }

        public List<NewSalesOrderStat> Stats { get; set; }

    }
}
