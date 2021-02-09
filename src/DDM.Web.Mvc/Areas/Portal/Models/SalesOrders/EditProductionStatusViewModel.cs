using DDM.SalesOrders.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DDM.Web.Areas.Portal.Models.SalesOrders
{
    public class EditProductionStatusViewModel
    {
        public int SalesOrderId { get; set; }
        public int ProductionStatusId { get; set; }
        public string Notes { get; set; }

        public List<SalesOrderProductionStatusLookupTableDto> ProductionStatusList { get; set; }

    }
}
