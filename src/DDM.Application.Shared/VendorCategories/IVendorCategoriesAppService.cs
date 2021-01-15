using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using DDM.VendorCategories.Dtos;
using DDM.Dto;

namespace DDM.VendorCategories
{
    public interface IVendorCategoriesAppService : IApplicationService
    {
        Task<PagedResultDto<GetVendorCategoryForViewDto>> GetAll(GetAllVendorCategoriesInput input);

        Task<GetVendorCategoryForViewDto> GetVendorCategoryForView(int id);

        Task<GetVendorCategoryForEditOutput> GetVendorCategoryForEdit(EntityDto input);

        Task CreateOrEdit(CreateOrEditVendorCategoryDto input);

        Task Delete(EntityDto input);

    }
}