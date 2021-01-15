using Abp.Application.Services.Dto;
using System;

namespace DDM.SalesOrders.Dtos
{
    public class GetAllSalesOrdersInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }

        public string NumberFilter { get; set; }

        public DateTime? MaxDateFilter { get; set; }
        public DateTime? MinDateFilter { get; set; }

        public DateTime? MaxDueDateFilter { get; set; }
        public DateTime? MinDueDateFilter { get; set; }

        public string CustomerNameFilter { get; set; }

    }
}