using Abp.Application.Services.Dto;

namespace DDM.Vendors.Dtos
{
    public class GetAllForLookupTableInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}