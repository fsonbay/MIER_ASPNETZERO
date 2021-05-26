using Abp.Application.Services.Dto;
using System;

namespace DDM.SalesOrderLines.Dtos
{
    public class GetAllSalesOrderLinesInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }

        public string NameFilter { get; set; }

        public string DescriptionFilter { get; set; }

        public decimal? MaxQuantityFilter { get; set; }
        public decimal? MinQuantityFilter { get; set; }

        public string SalesOrderNumberFilter { get; set; }

    }
}