using Abp.Application.Services.Dto;

namespace DDM.SalesOrders.Dtos
{
    public class GetAllForLookupTableInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}