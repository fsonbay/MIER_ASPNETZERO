using Abp.Application.Services.Dto;

namespace DDM.Materials.Dtos
{
    public class GetAllForLookupTableInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}