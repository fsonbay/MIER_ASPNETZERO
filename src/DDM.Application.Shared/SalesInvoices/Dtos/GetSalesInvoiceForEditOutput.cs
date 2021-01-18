using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace DDM.SalesInvoices.Dtos
{
    public class GetSalesInvoiceForEditOutput
    {
        public CreateOrEditSalesInvoiceDto SalesInvoice { get; set; }

        public string SalesOrderNumber { get; set; }

    }
}