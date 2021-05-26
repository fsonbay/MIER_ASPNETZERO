using DDM.SalesInvoices;

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using DDM.SalesInvoiceAdditionalCosts.Dtos;
using DDM.Dto;
using Abp.Application.Services.Dto;
using DDM.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;
using Abp.UI;
using DDM.Storage;

namespace DDM.SalesInvoiceAdditionalCosts
{
    [AbpAuthorize(AppPermissions.Pages_SalesInvoiceAdditionalCosts)]
    public class SalesInvoiceAdditionalCostsAppService : DDMAppServiceBase, ISalesInvoiceAdditionalCostsAppService
    {
        private readonly IRepository<SalesInvoiceAdditionalCost> _salesInvoiceAdditionalCostRepository;
        private readonly IRepository<SalesInvoice, int> _lookup_salesInvoiceRepository;

        public SalesInvoiceAdditionalCostsAppService(IRepository<SalesInvoiceAdditionalCost> salesInvoiceAdditionalCostRepository, IRepository<SalesInvoice, int> lookup_salesInvoiceRepository)
        {
            _salesInvoiceAdditionalCostRepository = salesInvoiceAdditionalCostRepository;
            _lookup_salesInvoiceRepository = lookup_salesInvoiceRepository;

        }

        public async Task<PagedResultDto<GetSalesInvoiceAdditionalCostForViewDto>> GetAll(GetAllSalesInvoiceAdditionalCostsInput input)
        {

            var filteredSalesInvoiceAdditionalCosts = _salesInvoiceAdditionalCostRepository.GetAll()
                        .Include(e => e.SalesInvoiceFk)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.Name.Contains(input.Filter) || e.Description.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.SalesInvoiceSalesInvoiceLineNamesFilter), e => e.SalesInvoiceFk != null && e.SalesInvoiceFk.SalesInvoiceLineNames == input.SalesInvoiceSalesInvoiceLineNamesFilter);

            var pagedAndFilteredSalesInvoiceAdditionalCosts = filteredSalesInvoiceAdditionalCosts
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var salesInvoiceAdditionalCosts = from o in pagedAndFilteredSalesInvoiceAdditionalCosts
                                              join o1 in _lookup_salesInvoiceRepository.GetAll() on o.SalesInvoiceId equals o1.Id into j1
                                              from s1 in j1.DefaultIfEmpty()

                                              select new
                                              {

                                                  Id = o.Id,
                                                  SalesInvoiceSalesInvoiceLineNames = s1 == null || s1.SalesInvoiceLineNames == null ? "" : s1.SalesInvoiceLineNames.ToString()
                                              };

            var totalCount = await filteredSalesInvoiceAdditionalCosts.CountAsync();

            var dbList = await salesInvoiceAdditionalCosts.ToListAsync();
            var results = new List<GetSalesInvoiceAdditionalCostForViewDto>();

            foreach (var o in dbList)
            {
                var res = new GetSalesInvoiceAdditionalCostForViewDto()
                {
                    SalesInvoiceAdditionalCost = new SalesInvoiceAdditionalCostDto
                    {

                        Id = o.Id,
                    },
                    SalesInvoiceSalesInvoiceLineNames = o.SalesInvoiceSalesInvoiceLineNames
                };

                results.Add(res);
            }

            return new PagedResultDto<GetSalesInvoiceAdditionalCostForViewDto>(
                totalCount,
                results
            );

        }

        [AbpAuthorize(AppPermissions.Pages_SalesInvoiceAdditionalCosts_Edit)]
        public async Task<GetSalesInvoiceAdditionalCostForEditOutput> GetSalesInvoiceAdditionalCostForEdit(EntityDto input)
        {
            var salesInvoiceAdditionalCost = await _salesInvoiceAdditionalCostRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetSalesInvoiceAdditionalCostForEditOutput { SalesInvoiceAdditionalCost = ObjectMapper.Map<CreateOrEditSalesInvoiceAdditionalCostDto>(salesInvoiceAdditionalCost) };

            if (output.SalesInvoiceAdditionalCost.SalesInvoiceId != null)
            {
                var _lookupSalesInvoice = await _lookup_salesInvoiceRepository.FirstOrDefaultAsync((int)output.SalesInvoiceAdditionalCost.SalesInvoiceId);
                output.SalesInvoiceSalesInvoiceLineNames = _lookupSalesInvoice?.SalesInvoiceLineNames?.ToString();
            }

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditSalesInvoiceAdditionalCostDto input)
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

        [AbpAuthorize(AppPermissions.Pages_SalesInvoiceAdditionalCosts_Create)]
        protected virtual async Task Create(CreateOrEditSalesInvoiceAdditionalCostDto input)
        {
            var salesInvoiceAdditionalCost = ObjectMapper.Map<SalesInvoiceAdditionalCost>(input);

            await _salesInvoiceAdditionalCostRepository.InsertAsync(salesInvoiceAdditionalCost);

        }

        [AbpAuthorize(AppPermissions.Pages_SalesInvoiceAdditionalCosts_Edit)]
        protected virtual async Task Update(CreateOrEditSalesInvoiceAdditionalCostDto input)
        {
            var salesInvoiceAdditionalCost = await _salesInvoiceAdditionalCostRepository.FirstOrDefaultAsync((int)input.Id);
            ObjectMapper.Map(input, salesInvoiceAdditionalCost);

        }

        [AbpAuthorize(AppPermissions.Pages_SalesInvoiceAdditionalCosts_Delete)]
        public async Task Delete(EntityDto input)
        {
            await _salesInvoiceAdditionalCostRepository.DeleteAsync(input.Id);
        }

        [AbpAuthorize(AppPermissions.Pages_SalesInvoiceAdditionalCosts)]
        public async Task<PagedResultDto<SalesInvoiceAdditionalCostSalesInvoiceLookupTableDto>> GetAllSalesInvoiceForLookupTable(GetAllForLookupTableInput input)
        {
            var query = _lookup_salesInvoiceRepository.GetAll().WhereIf(
                   !string.IsNullOrWhiteSpace(input.Filter),
                  e => e.SalesInvoiceLineNames != null && e.SalesInvoiceLineNames.Contains(input.Filter)
               );

            var totalCount = await query.CountAsync();

            var salesInvoiceList = await query
                .PageBy(input)
                .ToListAsync();

            var lookupTableDtoList = new List<SalesInvoiceAdditionalCostSalesInvoiceLookupTableDto>();
            foreach (var salesInvoice in salesInvoiceList)
            {
                lookupTableDtoList.Add(new SalesInvoiceAdditionalCostSalesInvoiceLookupTableDto
                {
                    Id = salesInvoice.Id,
                    DisplayName = salesInvoice.SalesInvoiceLineNames?.ToString()
                });
            }

            return new PagedResultDto<SalesInvoiceAdditionalCostSalesInvoiceLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
        }

    }
}