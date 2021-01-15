using Abp.Application.Services.Dto;

namespace DDM.CustomerCategories.Dtos
{
    public class GetAllForLookupTableInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}