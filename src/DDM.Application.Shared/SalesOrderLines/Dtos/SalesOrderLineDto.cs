using System;
using Abp.Application.Services.Dto;

namespace DDM.SalesOrderLines.Dtos
{
    public class SalesOrderLineDto : EntityDto
    {
        public int SalesOrderId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal? Quantity { get; set; }
        public decimal? Price { get; set; }
        public decimal? Amount { get; set; }
        public bool MarkForDelete { get; set; }

    }
}