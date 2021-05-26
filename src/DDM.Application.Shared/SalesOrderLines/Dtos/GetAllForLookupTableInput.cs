using Abp.Application.Services.Dto;

namespace DDM.SalesOrderLines.Dtos
{
    public class GetAllForLookupTableInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}