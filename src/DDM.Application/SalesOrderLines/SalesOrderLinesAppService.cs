using DDM.SalesOrders;

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using DDM.SalesOrderLines.Dtos;
using DDM.Dto;
using Abp.Application.Services.Dto;
using DDM.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;
using Abp.UI;
using DDM.Storage;

namespace DDM.SalesOrderLines
{
    [AbpAuthorize(AppPermissions.Pages_SalesOrderLines)]
    public class SalesOrderLinesAppService : DDMAppServiceBase, ISalesOrderLinesAppService
    {
        private readonly IRepository<SalesOrderLine> _salesOrderLineRepository;
        private readonly IRepository<SalesOrder, int> _lookup_salesOrderRepository;

        public SalesOrderLinesAppService(IRepository<SalesOrderLine> salesOrderLineRepository, IRepository<SalesOrder, int> lookup_salesOrderRepository)
        {
            _salesOrderLineRepository = salesOrderLineRepository;
            _lookup_salesOrderRepository = lookup_salesOrderRepository;

        }

        public async Task<PagedResultDto<GetSalesOrderLineForViewDto>> GetAll(GetAllSalesOrderLinesInput input)
        {

            var filteredSalesOrderLines = _salesOrderLineRepository.GetAll()
                        .Include(e => e.SalesOrderFk)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.Name.Contains(input.Filter) || e.Description.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.NameFilter), e => e.Name == input.NameFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.DescriptionFilter), e => e.Description == input.DescriptionFilter)
                        .WhereIf(input.MinQuantityFilter != null, e => e.Quantity >= input.MinQuantityFilter)
                        .WhereIf(input.MaxQuantityFilter != null, e => e.Quantity <= input.MaxQuantityFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.SalesOrderNumberFilter), e => e.SalesOrderFk != null && e.SalesOrderFk.Number == input.SalesOrderNumberFilter);

            var pagedAndFilteredSalesOrderLines = filteredSalesOrderLines
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var salesOrderLines = from o in pagedAndFilteredSalesOrderLines
                                  join o1 in _lookup_salesOrderRepository.GetAll() on o.SalesOrderId equals o1.Id into j1
                                  from s1 in j1.DefaultIfEmpty()

                                  select new
                                  {

                                      o.Name,
                                      o.Description,
                                      o.Quantity,
                                      Id = o.Id,
                                      SalesOrderNumber = s1 == null || s1.Number == null ? "" : s1.Number.ToString()
                                  };

            var totalCount = await filteredSalesOrderLines.CountAsync();

            var dbList = await salesOrderLines.ToListAsync();
            var results = new List<GetSalesOrderLineForViewDto>();

            foreach (var o in dbList)
            {
                var res = new GetSalesOrderLineForViewDto()
                {
                    SalesOrderLine = new SalesOrderLineDto
                    {

                        Name = o.Name,
                        Description = o.Description,
                        Quantity = o.Quantity,
                        Id = o.Id,
                    },
                    SalesOrderNumber = o.SalesOrderNumber
                };

                results.Add(res);
            }

            return new PagedResultDto<GetSalesOrderLineForViewDto>(
                totalCount,
                results
            );

        }

        [AbpAuthorize(AppPermissions.Pages_SalesOrderLines_Edit)]
        public async Task<GetSalesOrderLineForEditOutput> GetSalesOrderLineForEdit(EntityDto input)
        {
            var salesOrderLine = await _salesOrderLineRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetSalesOrderLineForEditOutput { SalesOrderLine = ObjectMapper.Map<CreateOrEditSalesOrderLineDto>(salesOrderLine) };

            if (output.SalesOrderLine.SalesOrderId != null)
            {
                var _lookupSalesOrder = await _lookup_salesOrderRepository.FirstOrDefaultAsync((int)output.SalesOrderLine.SalesOrderId);
                output.SalesOrderNumber = _lookupSalesOrder?.Number?.ToString();
            }

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditSalesOrderLineDto input)
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

        [AbpAuthorize(AppPermissions.Pages_SalesOrderLines_Create)]
        protected virtual async Task Create(CreateOrEditSalesOrderLineDto input)
        {
            var salesOrderLine = ObjectMapper.Map<SalesOrderLine>(input);

            await _salesOrderLineRepository.InsertAsync(salesOrderLine);

        }

        [AbpAuthorize(AppPermissions.Pages_SalesOrderLines_Edit)]
        protected virtual async Task Update(CreateOrEditSalesOrderLineDto input)
        {
            var salesOrderLine = await _salesOrderLineRepository.FirstOrDefaultAsync((int)input.Id);
            ObjectMapper.Map(input, salesOrderLine);

        }

        [AbpAuthorize(AppPermissions.Pages_SalesOrderLines_Delete)]
        public async Task Delete(EntityDto input)
        {
            await _salesOrderLineRepository.DeleteAsync(input.Id);
        }

        [AbpAuthorize(AppPermissions.Pages_SalesOrderLines)]
        public async Task<PagedResultDto<SalesOrderLineSalesOrderLookupTableDto>> GetAllSalesOrderForLookupTable(GetAllForLookupTableInput input)
        {
            var query = _lookup_salesOrderRepository.GetAll().WhereIf(
                   !string.IsNullOrWhiteSpace(input.Filter),
                  e => e.Number != null && e.Number.Contains(input.Filter)
               );

            var totalCount = await query.CountAsync();

            var salesOrderList = await query
                .PageBy(input)
                .ToListAsync();

            var lookupTableDtoList = new List<SalesOrderLineSalesOrderLookupTableDto>();
            foreach (var salesOrder in salesOrderList)
            {
                lookupTableDtoList.Add(new SalesOrderLineSalesOrderLookupTableDto
                {
                    Id = salesOrder.Id,
                    DisplayName = salesOrder.Number?.ToString()
                });
            }

            return new PagedResultDto<SalesOrderLineSalesOrderLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
        }

    }
}