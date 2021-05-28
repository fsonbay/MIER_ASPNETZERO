using System;
using Abp.Application.Services.Dto;

namespace DDM.SalesInvoiceAdditionalCosts.Dtos
{
    public class SalesInvoiceAdditionalCostDto : EntityDto
    {

        public int SalesInvoiceId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal? Quantity { get; set; }
        public decimal? Price { get; set; }
        public decimal? Amount { get; set; }
        public bool MarkForDelete { get; set; }
    }
}