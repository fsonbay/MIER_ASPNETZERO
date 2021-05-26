using Abp.Application.Services.Dto;
using System;

namespace DDM.SalesInvoiceAdditionalCosts.Dtos
{
    public class GetAllSalesInvoiceAdditionalCostsInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }

        public string SalesInvoiceSalesInvoiceLineNamesFilter { get; set; }

    }
}