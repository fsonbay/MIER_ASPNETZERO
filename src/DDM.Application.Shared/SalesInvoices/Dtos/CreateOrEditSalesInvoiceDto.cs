using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace DDM.SalesInvoices.Dtos
{
    public class CreateOrEditSalesInvoiceDto : EntityDto<int?>
    {

        [Required]
        public string Number { get; set; }

        public DateTime Date { get; set; }

        public string DueDate { get; set; }

        public decimal Amount { get; set; }

        public decimal Paid { get; set; }

        public decimal Outstanding { get; set; }

        public int SalesOrderId { get; set; }

    }
}