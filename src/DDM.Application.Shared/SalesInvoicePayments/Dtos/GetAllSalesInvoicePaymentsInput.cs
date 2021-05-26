using Abp.Application.Services.Dto;
using System;

namespace DDM.SalesInvoicePayments.Dtos
{
    public class GetAllSalesInvoicePaymentsInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }

        public string SalesInvoiceSalesInvoiceLineNamesFilter { get; set; }

        public string PaymentMethodNameFilter { get; set; }

        public string SalesOrderSalesOrderLineNamesFilter { get; set; }

    }
}