using Abp.Application.Services.Dto;

namespace DDM.SalesInvoices.Dtos
{
    public class GetAllForLookupTableInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}