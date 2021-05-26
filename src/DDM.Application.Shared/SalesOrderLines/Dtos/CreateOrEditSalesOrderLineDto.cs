using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace DDM.SalesOrderLines.Dtos
{
    public class CreateOrEditSalesOrderLineDto : EntityDto<int?>
    {

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Quantity { get; set; }

        public int SalesOrderId { get; set; }

    }
}