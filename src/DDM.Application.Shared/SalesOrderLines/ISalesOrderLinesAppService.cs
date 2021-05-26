using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using DDM.SalesOrderLines.Dtos;
using DDM.Dto;

namespace DDM.SalesOrderLines
{
    public interface ISalesOrderLinesAppService : IApplicationService
    {
        Task<PagedResultDto<GetSalesOrderLineForViewDto>> GetAll(GetAllSalesOrderLinesInput input);

        Task<GetSalesOrderLineForEditOutput> GetSalesOrderLineForEdit(EntityDto input);

        Task CreateOrEdit(CreateOrEditSalesOrderLineDto input);

        Task Delete(EntityDto input);

        Task<PagedResultDto<SalesOrderLineSalesOrderLookupTableDto>> GetAllSalesOrderForLookupTable(GetAllForLookupTableInput input);

    }
}