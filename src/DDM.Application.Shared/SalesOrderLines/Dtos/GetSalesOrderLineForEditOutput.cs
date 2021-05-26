using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace DDM.SalesOrderLines.Dtos
{
    public class GetSalesOrderLineForEditOutput
    {
        public CreateOrEditSalesOrderLineDto SalesOrderLine { get; set; }

        public string SalesOrderNumber { get; set; }

    }
}