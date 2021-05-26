using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using DDM.SalesInvoicePayments.Dtos;
using DDM.Dto;
using System.Collections.Generic;

namespace DDM.SalesInvoicePayments
{
    public interface ISalesInvoicePaymentsAppService : IApplicationService
    {
        Task<PagedResultDto<GetSalesInvoicePaymentForViewDto>> GetAll(GetAllSalesInvoicePaymentsInput input);

        Task<GetSalesInvoicePaymentForEditOutput> GetSalesInvoicePaymentForEdit(EntityDto input);

        Task CreateOrEdit(CreateOrEditSalesInvoicePaymentDto input);

        Task Delete(EntityDto input);

        Task<List<SalesInvoicePaymentSalesInvoiceLookupTableDto>> GetAllSalesInvoiceForTableDropdown();

        Task<PagedResultDto<SalesInvoicePaymentPaymentMethodLookupTableDto>> GetAllPaymentMethodForLookupTable(GetAllForLookupTableInput input);

        Task<PagedResultDto<SalesInvoicePaymentSalesOrderLookupTableDto>> GetAllSalesOrderForLookupTable(GetAllForLookupTableInput input);

    }
}