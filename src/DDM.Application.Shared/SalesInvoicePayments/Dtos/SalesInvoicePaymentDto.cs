using System;
using System.Collections.Generic;
using Abp.Application.Services.Dto;

namespace DDM.SalesInvoicePayments.Dtos
{
    public class SalesInvoicePaymentDto : EntityDto
    {
        public int SalesInvoiceId { get; set; }
        public int SalesOrderId { get; set; }
        public int PaymentMethodId { get; set; }
        public DateTime Date { get; set; }
        public decimal? Amount { get; set; }
        public bool MarkForDelete { get; set; }
    }
}