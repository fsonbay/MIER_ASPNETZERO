using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using DDM.SalesInvoices.Dtos;
using DDM.Dto;

namespace DDM.SalesInvoices
{
    public interface ISalesInvoicesAppService : IApplicationService
    {
        Task<PagedResultDto<GetSalesInvoiceForViewDto>> GetAll(GetAllSalesInvoicesInput input);

        Task<GetSalesInvoiceForViewDto> GetSalesInvoiceForView(int id);

        Task<GetSalesInvoiceForEditOutput> GetSalesInvoiceForEdit(EntityDto input);

        Task CreateOrEdit(CreateOrEditSalesInvoiceDto input);

        Task Delete(EntityDto input);

        Task<PagedResultDto<SalesInvoiceSalesOrderLookupTableDto>> GetAllSalesOrderForLookupTable(GetAllForLookupTableInput input);

    }
}