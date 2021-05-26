using Abp.Application.Services.Dto;

namespace DDM.SalesInvoicePayments.Dtos
{
    public class GetAllForLookupTableInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}