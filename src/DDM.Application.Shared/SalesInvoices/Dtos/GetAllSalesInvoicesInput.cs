using Abp.Application.Services.Dto;
using System;

namespace DDM.SalesInvoices.Dtos
{
    public class GetAllSalesInvoicesInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }

        public string NumberFilter { get; set; }

        public DateTime? MaxDateFilter { get; set; }
        public DateTime? MinDateFilter { get; set; }

        public DateTime? MaxDueDateFilter { get; set; }
        public DateTime? MinDueDateFilter { get; set; }

        public decimal? MaxAmountFilter { get; set; }
        public decimal? MinAmountFilter { get; set; }

        public decimal? MaxPaidFilter { get; set; }
        public decimal? MinPaidFilter { get; set; }

        public decimal? MaxOutstandingFilter { get; set; }
        public decimal? MinOutstandingFilter { get; set; }

        public string SalesOrderNumberFilter { get; set; }

    }
}