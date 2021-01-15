using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using DDM.Customers.Dtos;
using DDM.Dto;
using System.Collections.Generic;

namespace DDM.Customers
{
    public interface ICustomersAppService : IApplicationService
    {
        Task<PagedResultDto<GetCustomerForViewDto>> GetAll(GetAllCustomersInput input);

        Task<GetCustomerForViewDto> GetCustomerForView(int id);

        Task<GetCustomerForEditOutput> GetCustomerForEdit(EntityDto input);

        Task CreateOrEdit(CreateOrEditCustomerDto input);

        Task Delete(EntityDto input);

        Task<List<CustomerCustomerCategoryLookupTableDto>> GetAllCustomerCategoryForTableDropdown();

    }
}