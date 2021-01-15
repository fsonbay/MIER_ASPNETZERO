using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using DDM.CustomerCategories.Dtos;
using DDM.Dto;
using Abp.Application.Services.Dto;
using DDM.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;

namespace DDM.CustomerCategories
{
    [AbpAuthorize(AppPermissions.Pages_CustomerCategories)]
    public class CustomerCategoriesAppService : DDMAppServiceBase, ICustomerCategoriesAppService
    {
        private readonly IRepository<CustomerCategory> _customerCategoryRepository;

        public CustomerCategoriesAppService(IRepository<CustomerCategory> customerCategoryRepository)
        {
            _customerCategoryRepository = customerCategoryRepository;

        }

        public async Task<PagedResultDto<GetCustomerCategoryForViewDto>> GetAll(GetAllCustomerCategoriesInput input)
        {

            var filteredCustomerCategories = _customerCategoryRepository.GetAll()
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.Name.Contains(input.Filter) || e.Description.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.NameFilter), e => e.Name == input.NameFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.DescriptionFilter), e => e.Description == input.DescriptionFilter);

            var pagedAndFilteredCustomerCategories = filteredCustomerCategories
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var customerCategories = from o in pagedAndFilteredCustomerCategories
                                     select new GetCustomerCategoryForViewDto()
                                     {
                                         CustomerCategory = new CustomerCategoryDto
                                         {
                                             Name = o.Name,
                                             Description = o.Description,
                                             Id = o.Id
                                         }
                                     };

            var totalCount = await filteredCustomerCategories.CountAsync();

            return new PagedResultDto<GetCustomerCategoryForViewDto>(
                totalCount,
                await customerCategories.ToListAsync()
            );
        }

        public async Task<GetCustomerCategoryForViewDto> GetCustomerCategoryForView(int id)
        {
            var customerCategory = await _customerCategoryRepository.GetAsync(id);

            var output = new GetCustomerCategoryForViewDto { CustomerCategory = ObjectMapper.Map<CustomerCategoryDto>(customerCategory) };

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_CustomerCategories_Edit)]
        public async Task<GetCustomerCategoryForEditOutput> GetCustomerCategoryForEdit(EntityDto input)
        {
            var customerCategory = await _customerCategoryRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetCustomerCategoryForEditOutput { CustomerCategory = ObjectMapper.Map<CreateOrEditCustomerCategoryDto>(customerCategory) };

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditCustomerCategoryDto input)
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

        [AbpAuthorize(AppPermissions.Pages_CustomerCategories_Create)]
        protected virtual async Task Create(CreateOrEditCustomerCategoryDto input)
        {
            var customerCategory = ObjectMapper.Map<CustomerCategory>(input);

            await _customerCategoryRepository.InsertAsync(customerCategory);
        }

        [AbpAuthorize(AppPermissions.Pages_CustomerCategories_Edit)]
        protected virtual async Task Update(CreateOrEditCustomerCategoryDto input)
        {
            var customerCategory = await _customerCategoryRepository.FirstOrDefaultAsync((int)input.Id);
            ObjectMapper.Map(input, customerCategory);
        }

        [AbpAuthorize(AppPermissions.Pages_CustomerCategories_Delete)]
        public async Task Delete(EntityDto input)
        {
            await _customerCategoryRepository.DeleteAsync(input.Id);
        }
    }
}