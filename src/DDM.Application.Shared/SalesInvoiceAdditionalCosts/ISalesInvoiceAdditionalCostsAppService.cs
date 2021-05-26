using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using DDM.SalesInvoiceAdditionalCosts.Dtos;
using DDM.Dto;

namespace DDM.SalesInvoiceAdditionalCosts
{
    public interface ISalesInvoiceAdditionalCostsAppService : IApplicationService
    {
        Task<PagedResultDto<GetSalesInvoiceAdditionalCostForViewDto>> GetAll(GetAllSalesInvoiceAdditionalCostsInput input);

        Task<GetSalesInvoiceAdditionalCostForEditOutput> GetSalesInvoiceAdditionalCostForEdit(EntityDto input);

        Task CreateOrEdit(CreateOrEditSalesInvoiceAdditionalCostDto input);

        Task Delete(EntityDto input);

        Task<PagedResultDto<SalesInvoiceAdditionalCostSalesInvoiceLookupTableDto>> GetAllSalesInvoiceForLookupTable(GetAllForLookupTableInput input);

    }
}