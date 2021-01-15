using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using DDM.CustomerCategories.Dtos;
using DDM.Dto;

namespace DDM.CustomerCategories
{
    public interface ICustomerCategoriesAppService : IApplicationService
    {
        Task<PagedResultDto<GetCustomerCategoryForViewDto>> GetAll(GetAllCustomerCategoriesInput input);

        Task<GetCustomerCategoryForViewDto> GetCustomerCategoryForView(int id);

        Task<GetCustomerCategoryForEditOutput> GetCustomerCategoryForEdit(EntityDto input);

        Task CreateOrEdit(CreateOrEditCustomerCategoryDto input);

        Task Delete(EntityDto input);

    }
}