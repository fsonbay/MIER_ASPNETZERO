using Abp.Application.Services.Dto;

namespace DDM.VendorCategories.Dtos
{
    public class GetAllForLookupTableInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}