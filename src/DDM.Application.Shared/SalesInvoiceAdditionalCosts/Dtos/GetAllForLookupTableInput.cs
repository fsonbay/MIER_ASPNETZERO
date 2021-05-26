using Abp.Application.Services.Dto;

namespace DDM.SalesInvoiceAdditionalCosts.Dtos
{
    public class GetAllForLookupTableInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}