using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace DDM.SalesInvoiceAdditionalCosts.Dtos
{
    public class GetSalesInvoiceAdditionalCostForEditOutput
    {
        public CreateOrEditSalesInvoiceAdditionalCostDto SalesInvoiceAdditionalCost { get; set; }

        public string SalesInvoiceSalesInvoiceLineNames { get; set; }

    }
}