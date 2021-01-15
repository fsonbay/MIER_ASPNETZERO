using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using DDM.SalesOrders.Dtos;
using DDM.Dto;
using System.Collections.Generic;

namespace DDM.SalesOrders
{
    public interface ISalesOrdersAppService : IApplicationService
    {
        Task<PagedResultDto<GetSalesOrderForViewDto>> GetAll(GetAllSalesOrdersInput input);

        Task<GetSalesOrderForViewDto> GetSalesOrderForView(int id);

        Task<GetSalesOrderForEditOutput> GetSalesOrderForEdit(EntityDto input);

        Task CreateOrEdit(CreateOrEditSalesOrderDto input);

        Task Delete(EntityDto input);

        Task<List<SalesOrderCustomerLookupTableDto>> GetAllCustomerForTableDropdown();
        Task<List<SalesOrderMachineLookupTableDto>> GetAllMachineForTableDropdown();
        Task<List<SalesOrderMaterialLookupTableDto>> GetAllMaterialForTableDropdown();
    }
}