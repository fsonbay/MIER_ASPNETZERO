using System;
using Abp.Application.Services.Dto;

namespace DDM.SalesInvoices.Dtos
{
    public class SalesInvoiceDto : EntityDto
    {
        public string Number { get; set; }

        public DateTime Date { get; set; }

        public DateTime DueDate { get; set; }

        public decimal Amount { get; set; }

        public decimal Paid { get; set; }

        public decimal Outstanding { get; set; }

        public int SalesOrderId { get; set; }

    }
}