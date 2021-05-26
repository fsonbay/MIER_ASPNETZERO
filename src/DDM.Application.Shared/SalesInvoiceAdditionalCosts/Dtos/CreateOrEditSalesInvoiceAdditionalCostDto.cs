using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace DDM.SalesInvoiceAdditionalCosts.Dtos
{
    public class CreateOrEditSalesInvoiceAdditionalCostDto : EntityDto<int?>
    {

        public int SalesInvoiceId { get; set; }

    }
}