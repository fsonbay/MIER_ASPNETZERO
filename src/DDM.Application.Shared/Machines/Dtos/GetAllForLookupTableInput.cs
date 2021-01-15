using Abp.Application.Services.Dto;

namespace DDM.Machines.Dtos
{
    public class GetAllForLookupTableInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}