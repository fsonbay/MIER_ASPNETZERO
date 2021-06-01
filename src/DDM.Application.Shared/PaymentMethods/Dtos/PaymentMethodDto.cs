using System;
using Abp.Application.Services.Dto;

namespace DDM.PaymentMethods.Dtos
{
    public class PaymentMethodDto : EntityDto
    {
        public string Name { get; set; }
    }
}