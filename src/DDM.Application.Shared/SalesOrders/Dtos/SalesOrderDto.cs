using System;
using System.Collections.Generic;
using Abp.Application.Services.Dto;

namespace DDM.SalesOrders.Dtos
{
    public class SalesOrderDto : EntityDto
    {
        public string Number { get; set; }

        public DateTime Date { get; set; }

        public DateTime Deadline { get; set; }

        public int CustomerId { get; set; }


    }
}