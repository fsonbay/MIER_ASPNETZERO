using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using DDM.Vendors.Dtos;
using DDM.Dto;
using System.Collections.Generic;

namespace DDM.Vendors
{
    public interface IVendorsAppService : IApplicationService
    {
        Task<PagedResultDto<GetVendorForViewDto>> GetAll(GetAllVendorsInput input);

        Task<GetVendorForViewDto> GetVendorForView(int id);

        Task<GetVendorForEditOutput> GetVendorForEdit(EntityDto input);

        Task CreateOrEdit(CreateOrEditVendorDto input);

        Task Delete(EntityDto input);

        Task<List<VendorVendorCategoryLookupTableDto>> GetAllVendorCategoryForTableDropdown();

    }
}