using System;
using Abp.Application.Services.Dto;

namespace DDM.SalesInvoicePayments.Dtos
{
    public class SalesInvoicePaymentDto : EntityDto
    {
        public int SalesInvoiceId { get; set; }

        public int PaymentMethodId { get; set; }

        public int SalesOrderId { get; set; }

    }
}