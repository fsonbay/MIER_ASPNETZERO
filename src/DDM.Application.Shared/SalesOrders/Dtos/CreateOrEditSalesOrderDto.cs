using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace DDM.SalesOrders.Dtos
{
    public class CreateOrEditSalesOrderDto : EntityDto<int?>
    {

        public string Number { get; set; }

        public DateTime Date { get; set; }

        public DateTime DueDate { get; set; }

        public int CustomerId { get; set; }

        public List<SalesOrderLineDto> SalesOrderLines { get; set; }

    }
}