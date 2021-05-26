using Abp.Application.Services.Dto;

namespace DDM.PaymentMethods.Dtos
{
    public class GetAllForLookupTableInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}