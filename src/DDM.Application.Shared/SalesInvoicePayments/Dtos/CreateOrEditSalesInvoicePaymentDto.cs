using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace DDM.SalesInvoicePayments.Dtos
{
    public class CreateOrEditSalesInvoicePaymentDto : EntityDto<int?>
    {

        public int SalesInvoiceId { get; set; }

        public int PaymentMethodId { get; set; }

        public int SalesOrderId { get; set; }

    }
}