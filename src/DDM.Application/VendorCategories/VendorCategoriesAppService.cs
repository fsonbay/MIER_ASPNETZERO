using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using DDM.VendorCategories.Dtos;
using DDM.Dto;
using Abp.Application.Services.Dto;
using DDM.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;

namespace DDM.VendorCategories
{
    [AbpAuthorize(AppPermissions.Pages_VendorCategories)]
    public class VendorCategoriesAppService : DDMAppServiceBase, IVendorCategoriesAppService
    {
        private readonly IRepository<VendorCategory> _vendorCategoryRepository;

        public VendorCategoriesAppService(IRepository<VendorCategory> vendorCategoryRepository)
        {
            _vendorCategoryRepository = vendorCategoryRepository;

        }

        public async Task<PagedResultDto<GetVendorCategoryForViewDto>> GetAll(GetAllVendorCategoriesInput input)
        {

            var filteredVendorCategories = _vendorCategoryRepository.GetAll()
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.Name.Contains(input.Filter) || e.Description.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.NameFilter), e => e.Name == input.NameFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.DescriptionFilter), e => e.Description == input.DescriptionFilter);

            var pagedAndFilteredVendorCategories = filteredVendorCategories
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var vendorCategories = from o in pagedAndFilteredVendorCategories
                                   select new GetVendorCategoryForViewDto()
                                   {
                                       VendorCategory = new VendorCategoryDto
                                       {
                                           Name = o.Name,
                                           Description = o.Description,
                                           Id = o.Id
                                       }
                                   };

            var totalCount = await filteredVendorCategories.CountAsync();

            return new PagedResultDto<GetVendorCategoryForViewDto>(
                totalCount,
                await vendorCategories.ToListAsync()
            );
        }

        public async Task<GetVendorCategoryForViewDto> GetVendorCategoryForView(int id)
        {
            var vendorCategory = await _vendorCategoryRepository.GetAsync(id);

            var output = new GetVendorCategoryForViewDto { VendorCategory = ObjectMapper.Map<VendorCategoryDto>(vendorCategory) };

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_VendorCategories_Edit)]
        public async Task<GetVendorCategoryForEditOutput> GetVendorCategoryForEdit(EntityDto input)
        {
            var vendorCategory = await _vendorCategoryRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetVendorCategoryForEditOutput { VendorCategory = ObjectMapper.Map<CreateOrEditVendorCategoryDto>(vendorCategory) };

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditVendorCategoryDto input)
        {
            if (input.Id == null)
            {
                await Create(input);
            }
            else
            {
                await Update(input);
            }
        }

        [AbpAuthorize(AppPermissions.Pages_VendorCategories_Create)]
        protected virtual async Task Create(CreateOrEditVendorCategoryDto input)
        {
            var vendorCategory = ObjectMapper.Map<VendorCategory>(input);

            await _vendorCategoryRepository.InsertAsync(vendorCategory);
        }

        [AbpAuthorize(AppPermissions.Pages_VendorCategories_Edit)]
        protected virtual async Task Update(CreateOrEditVendorCategoryDto input)
        {
            var vendorCategory = await _vendorCategoryRepository.FirstOrDefaultAsync((int)input.Id);
            ObjectMapper.Map(input, vendorCategory);
        }

        [AbpAuthorize(AppPermissions.Pages_VendorCategories_Delete)]
        public async Task Delete(EntityDto input)
        {
            await _vendorCategoryRepository.DeleteAsync(input.Id);
        }
    }
}