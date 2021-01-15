using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace DDM.SalesOrders.Dtos
{
    public class GetSalesOrderForEditOutput
    {
        public CreateOrEditSalesOrderDto SalesOrder { get; set; }

        public string CustomerName { get; set; }

    }
}