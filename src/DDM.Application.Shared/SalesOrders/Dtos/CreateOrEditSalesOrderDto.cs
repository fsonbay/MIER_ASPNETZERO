using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace DDM.SalesOrders.Dtos
{
    public class CreateOrEditSalesOrderDto : EntityDto<int?>
    {
        public string Number { get; set; }
        public bool ProcessedBySurabaya { get; set; }
        public DateTime Date { get; set; }
        public DateTime Deadline { get; set; }
        public string Amount { get; set; }

        public int CustomerId { get; set; }

        public List<CreateOrEditSalesOrderLineDto> SalesOrderLines { get; set; }

    }
}