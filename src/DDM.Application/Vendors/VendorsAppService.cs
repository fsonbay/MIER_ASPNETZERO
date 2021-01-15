using DDM.VendorCategories;

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using DDM.Vendors.Dtos;
using DDM.Dto;
using Abp.Application.Services.Dto;
using DDM.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;

namespace DDM.Vendors
{
    [AbpAuthorize(AppPermissions.Pages_Vendors)]
    public class VendorsAppService : DDMAppServiceBase, IVendorsAppService
    {
        private readonly IRepository<Vendor> _vendorRepository;
        private readonly IRepository<VendorCategory, int> _lookup_vendorCategoryRepository;

        public VendorsAppService(IRepository<Vendor> vendorRepository, IRepository<VendorCategory, int> lookup_vendorCategoryRepository)
        {
            _vendorRepository = vendorRepository;
            _lookup_vendorCategoryRepository = lookup_vendorCategoryRepository;

        }

        public async Task<PagedResultDto<GetVendorForViewDto>> GetAll(GetAllVendorsInput input)
        {

            var filteredVendors = _vendorRepository.GetAll()
                        .Include(e => e.VendorCategoryFk)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.Name.Contains(input.Filter) || e.Description.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.NameFilter), e => e.Name == input.NameFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.DescriptionFilter), e => e.Description == input.DescriptionFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.VendorCategoryNameFilter), e => e.VendorCategoryFk != null && e.VendorCategoryFk.Name == input.VendorCategoryNameFilter);

            var pagedAndFilteredVendors = filteredVendors
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var vendors = from o in pagedAndFilteredVendors
                          join o1 in _lookup_vendorCategoryRepository.GetAll() on o.VendorCategoryId equals o1.Id into j1
                          from s1 in j1.DefaultIfEmpty()

                          select new GetVendorForViewDto()
                          {
                              Vendor = new VendorDto
                              {
                                  Name = o.Name,
                                  Description = o.Description,
                                  Id = o.Id
                              },
                              VendorCategoryName = s1 == null || s1.Name == null ? "" : s1.Name.ToString()
                          };

            var totalCount = await filteredVendors.CountAsync();

            return new PagedResultDto<GetVendorForViewDto>(
                totalCount,
                await vendors.ToListAsync()
            );
        }

        public async Task<GetVendorForViewDto> GetVendorForView(int id)
        {
            var vendor = await _vendorRepository.GetAsync(id);

            var output = new GetVendorForViewDto { Vendor = ObjectMapper.Map<VendorDto>(vendor) };

            if (output.Vendor.VendorCategoryId != null)
            {
                var _lookupVendorCategory = await _lookup_vendorCategoryRepository.FirstOrDefaultAsync((int)output.Vendor.VendorCategoryId);
                output.VendorCategoryName = _lookupVendorCategory?.Name?.ToString();
            }

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_Vendors_Edit)]
        public async Task<GetVendorForEditOutput> GetVendorForEdit(EntityDto input)
        {
            var vendor = await _vendorRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetVendorForEditOutput { Vendor = ObjectMapper.Map<CreateOrEditVendorDto>(vendor) };

            if (output.Vendor.VendorCategoryId != null)
            {
                var _lookupVendorCategory = await _lookup_vendorCategoryRepository.FirstOrDefaultAsync((int)output.Vendor.VendorCategoryId);
                output.VendorCategoryName = _lookupVendorCategory?.Name?.ToString();
            }

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditVendorDto input)
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

        [AbpAuthorize(AppPermissions.Pages_Vendors_Create)]
        protected virtual async Task Create(CreateOrEditVendorDto input)
        {
            var vendor = ObjectMapper.Map<Vendor>(input);

            await _vendorRepository.InsertAsync(vendor);
        }

        [AbpAuthorize(AppPermissions.Pages_Vendors_Edit)]
        protected virtual async Task Update(CreateOrEditVendorDto input)
        {
            var vendor = await _vendorRepository.FirstOrDefaultAsync((int)input.Id);
            ObjectMapper.Map(input, vendor);
        }

        [AbpAuthorize(AppPermissions.Pages_Vendors_Delete)]
        public async Task Delete(EntityDto input)
        {
            await _vendorRepository.DeleteAsync(input.Id);
        }
        [AbpAuthorize(AppPermissions.Pages_Vendors)]
        public async Task<List<VendorVendorCategoryLookupTableDto>> GetAllVendorCategoryForTableDropdown()
        {
            return await _lookup_vendorCategoryRepository.GetAll()
                .Select(vendorCategory => new VendorVendorCategoryLookupTableDto
                {
                    Id = vendorCategory.Id,
                    DisplayName = vendorCategory == null || vendorCategory.Name == null ? "" : vendorCategory.Name.ToString()
                }).ToListAsync();
        }

    }
}