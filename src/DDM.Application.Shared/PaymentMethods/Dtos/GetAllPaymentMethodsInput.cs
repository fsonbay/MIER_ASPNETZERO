using Abp.Application.Services.Dto;
using System;

namespace DDM.PaymentMethods.Dtos
{
    public class GetAllPaymentMethodsInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }

    }
}