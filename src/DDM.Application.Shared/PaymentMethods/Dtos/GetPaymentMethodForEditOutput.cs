using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace DDM.PaymentMethods.Dtos
{
    public class GetPaymentMethodForEditOutput
    {
        public CreateOrEditPaymentMethodDto PaymentMethod { get; set; }

    }
}