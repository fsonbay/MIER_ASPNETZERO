using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace DDM.SalesInvoicePayments.Dtos
{
    public class GetSalesInvoicePaymentForEditOutput
    {
        public CreateOrEditSalesInvoicePaymentDto SalesInvoicePayment { get; set; }

        public string SalesInvoiceSalesInvoiceLineNames { get; set; }

        public string PaymentMethodName { get; set; }

        public string SalesOrderSalesOrderLineNames { get; set; }

    }
}