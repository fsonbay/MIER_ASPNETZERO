using DDM.CustomerCategories;

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using DDM.Customers.Dtos;
using DDM.Dto;
using Abp.Application.Services.Dto;
using DDM.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;

namespace DDM.Customers
{
    [AbpAuthorize(AppPermissions.Pages_Customers)]
    public class CustomersAppService : DDMAppServiceBase, ICustomersAppService
    {
        private readonly IRepository<Customer> _customerRepository;
        private readonly IRepository<CustomerCategory, int> _lookup_customerCategoryRepository;

        public CustomersAppService(IRepository<Customer> customerRepository, IRepository<CustomerCategory, int> lookup_customerCategoryRepository)
        {
            _customerRepository = customerRepository;
            _lookup_customerCategoryRepository = lookup_customerCategoryRepository;

        }

        public async Task<PagedResultDto<GetCustomerForViewDto>> GetAll(GetAllCustomersInput input)
        {

            var filteredCustomers = _customerRepository.GetAll()
                        .Include(e => e.CustomerCategoryFk)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.Name.Contains(input.Filter) || e.Company.Contains(input.Filter) || e.Phone.Contains(input.Filter) || e.Email.Contains(input.Filter) || e.Address.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.NameFilter), e => e.Name == input.NameFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.CompanyFilter), e => e.Company == input.CompanyFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.PhoneFilter), e => e.Phone == input.PhoneFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.EmailFilter), e => e.Email == input.EmailFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.AddressFilter), e => e.Address == input.AddressFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.CustomerCategoryNameFilter), e => e.CustomerCategoryFk != null && e.CustomerCategoryFk.Name == input.CustomerCategoryNameFilter);

            var pagedAndFilteredCustomers = filteredCustomers
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var customers = from o in pagedAndFilteredCustomers
                            join o1 in _lookup_customerCategoryRepository.GetAll() on o.CustomerCategoryId equals o1.Id into j1
                            from s1 in j1.DefaultIfEmpty()

                            select new GetCustomerForViewDto()
                            {
                                Customer = new CustomerDto
                                {
                                    Name = o.Name,
                                    Company = o.Company,
                                    Phone = o.Phone,
                                    Email = o.Email,
                                    Address = o.Address,
                                    Id = o.Id,
                                    CreationTime = o.CreationTime
                                },
                                CustomerCategoryName = s1 == null || s1.Name == null ? "" : s1.Name.ToString()
                            };

            var totalCount = await filteredCustomers.CountAsync();

            return new PagedResultDto<GetCustomerForViewDto>(
                totalCount,
                await customers.ToListAsync()
            );
        }

        public async Task<GetCustomerForViewDto> GetCustomerForView(int id)
        {
            var customer = await _customerRepository.GetAsync(id);

            var output = new GetCustomerForViewDto { Customer = ObjectMapper.Map<CustomerDto>(customer) };

            if (output.Customer.CustomerCategoryId != null)
            {
                var _lookupCustomerCategory = await _lookup_customerCategoryRepository.FirstOrDefaultAsync((int)output.Customer.CustomerCategoryId);
                output.CustomerCategoryName = _lookupCustomerCategory?.Name?.ToString();
            }

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_Customers_Create, AppPermissions.Pages_Customers_Edit)]
        public async Task<GetCustomerForEditOutput> GetCustomerForEdit(NullableIdDto input)
        {
            Customer customer = null;
            if (input.Id.HasValue)
            {
                customer = await _customerRepository.FirstOrDefaultAsync((int)input.Id);
            }

            var output = new GetCustomerForEditOutput();

            output = new GetCustomerForEditOutput { Customer = ObjectMapper.Map<CreateOrEditCustomerDto>(customer) };

            //Customer
            output.Customer = customer != null
                ? ObjectMapper.Map<CreateOrEditCustomerDto>(customer)
                : new CreateOrEditCustomerDto();

            //Customer categories
            output.CustomerCategories = _lookup_customerCategoryRepository
                .GetAll()
                .Select(c => new ComboboxItemDto(c.Id.ToString(), c.Name + " (" + c.Description + ")") { IsSelected = output.Customer.CustomerCategoryId == c.Id })
                .ToList();

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditCustomerDto input)
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

        public int CreateNewCustomer(CreateOrEditCustomerDto input)
        {
            var i = CreateAndGetId(input);
            return i;
        }

        [AbpAuthorize(AppPermissions.Pages_Customers_Create)]
        protected virtual int CreateAndGetId(CreateOrEditCustomerDto input)
        {
            var customer = ObjectMapper.Map<Customer>(input);
            int id =  _customerRepository.InsertAndGetId(customer);
            return id;
        }

        [AbpAuthorize(AppPermissions.Pages_Customers_Create)]
        protected virtual async Task Create(CreateOrEditCustomerDto input)
        {
            var customer = ObjectMapper.Map<Customer>(input);

            await _customerRepository.InsertAsync(customer);
        }

        [AbpAuthorize(AppPermissions.Pages_Customers_Edit)]
        protected virtual async Task Update(CreateOrEditCustomerDto input)
        {
            var customer = await _customerRepository.FirstOrDefaultAsync((int)input.Id);
            ObjectMapper.Map(input, customer);
        }

        [AbpAuthorize(AppPermissions.Pages_Customers_Delete)]
        public async Task Delete(EntityDto input)
        {
            await _customerRepository.DeleteAsync(input.Id);
        }
        [AbpAuthorize(AppPermissions.Pages_Customers)]
        public async Task<List<CustomerCustomerCategoryLookupTableDto>> GetAllCustomerCategoryForTableDropdown()
        {
            return await _lookup_customerCategoryRepository.GetAll()
                .Select(customerCategory => new CustomerCustomerCategoryLookupTableDto
                {
                    Id = customerCategory.Id,
                    DisplayName = customerCategory == null || customerCategory.Name == null ? "" : customerCategory.Name.ToString()
                }).ToListAsync();
        }

    }
}