using DDM.SalesOrders;

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using DDM.SalesInvoices.Dtos;
using DDM.Dto;
using Abp.Application.Services.Dto;
using DDM.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;

namespace DDM.SalesInvoices
{
    [AbpAuthorize(AppPermissions.Pages_SalesInvoices)]
    public class SalesInvoicesAppService : DDMAppServiceBase, ISalesInvoicesAppService
    {
        private readonly IRepository<SalesInvoice> _salesInvoiceRepository;
        private readonly IRepository<SalesOrder, int> _lookup_salesOrderRepository;

        public SalesInvoicesAppService(IRepository<SalesInvoice> salesInvoiceRepository, IRepository<SalesOrder, int> lookup_salesOrderRepository)
        {
            _salesInvoiceRepository = salesInvoiceRepository;
            _lookup_salesOrderRepository = lookup_salesOrderRepository;

        }

        public async Task<PagedResultDto<GetSalesInvoiceForViewDto>> GetAll(GetAllSalesInvoicesInput input)
        {

            var filteredSalesInvoices = _salesInvoiceRepository.GetAll()
                        .Include(e => e.SalesOrderFk)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.Number.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.NumberFilter), e => e.Number == input.NumberFilter)
                        .WhereIf(input.MinDateFilter != null, e => e.Date >= input.MinDateFilter)
                        .WhereIf(input.MaxDateFilter != null, e => e.Date <= input.MaxDateFilter)

                        .WhereIf(input.MinDueDateFilter != null, e => e.Date >= input.MinDueDateFilter)
                        .WhereIf(input.MaxDueDateFilter != null, e => e.Date <= input.MaxDueDateFilter)

                        .WhereIf(input.MinAmountFilter != null, e => e.Amount >= input.MinAmountFilter)
                        .WhereIf(input.MaxAmountFilter != null, e => e.Amount <= input.MaxAmountFilter)
                        .WhereIf(input.MinPaidFilter != null, e => e.Paid >= input.MinPaidFilter)
                        .WhereIf(input.MaxPaidFilter != null, e => e.Paid <= input.MaxPaidFilter)
                        .WhereIf(input.MinOutstandingFilter != null, e => e.Outstanding >= input.MinOutstandingFilter)
                        .WhereIf(input.MaxOutstandingFilter != null, e => e.Outstanding <= input.MaxOutstandingFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.SalesOrderNumberFilter), e => e.SalesOrderFk != null && e.SalesOrderFk.Number == input.SalesOrderNumberFilter);

            var pagedAndFilteredSalesInvoices = filteredSalesInvoices
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var salesInvoices = from o in pagedAndFilteredSalesInvoices
                                join o1 in _lookup_salesOrderRepository.GetAll() on o.SalesOrderId equals o1.Id into j1
                                from s1 in j1.DefaultIfEmpty()

                                select new GetSalesInvoiceForViewDto()
                                {
                                    SalesInvoice = new SalesInvoiceDto
                                    {
                                        Number = o.Number,
                                        Date = o.Date,
                                        DueDate = o.DueDate,
                                        Amount = o.Amount,
                                        Paid = o.Paid,
                                        Outstanding = o.Outstanding,
                                        Id = o.Id
                                    },
                                    SalesOrderNumber = s1 == null || s1.Number == null ? "" : s1.Number.ToString()
                                };

            var totalCount = await filteredSalesInvoices.CountAsync();

            return new PagedResultDto<GetSalesInvoiceForViewDto>(
                totalCount,
                await salesInvoices.ToListAsync()
            );
        }

        public async Task<GetSalesInvoiceForViewDto> GetSalesInvoiceForView(int id)
        {
            var salesInvoice = await _salesInvoiceRepository.GetAsync(id);

            var output = new GetSalesInvoiceForViewDto { SalesInvoice = ObjectMapper.Map<SalesInvoiceDto>(salesInvoice) };

            if (output.SalesInvoice.SalesOrderId != null)
            {
                var _lookupSalesOrder = await _lookup_salesOrderRepository.FirstOrDefaultAsync((int)output.SalesInvoice.SalesOrderId);
                output.SalesOrderNumber = _lookupSalesOrder?.Number?.ToString();
            }

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_SalesInvoices_Edit)]
        public async Task<GetSalesInvoiceForEditOutput> GetSalesInvoiceForEdit(EntityDto input)
        {
            var salesInvoice = await _salesInvoiceRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetSalesInvoiceForEditOutput { SalesInvoice = ObjectMapper.Map<CreateOrEditSalesInvoiceDto>(salesInvoice) };

            if (output.SalesInvoice.SalesOrderId != null)
            {
                var _lookupSalesOrder = await _lookup_salesOrderRepository.FirstOrDefaultAsync((int)output.SalesInvoice.SalesOrderId);
                output.SalesOrderNumber = _lookupSalesOrder?.Number?.ToString();
            }

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditSalesInvoiceDto input)
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

        [AbpAuthorize(AppPermissions.Pages_SalesInvoices_Create)]
        protected virtual async Task Create(CreateOrEditSalesInvoiceDto input)
        {
            var salesInvoice = ObjectMapper.Map<SalesInvoice>(input);

            await _salesInvoiceRepository.InsertAsync(salesInvoice);
        }

        [AbpAuthorize(AppPermissions.Pages_SalesInvoices_Edit)]
        protected virtual async Task Update(CreateOrEditSalesInvoiceDto input)
        {
            var salesInvoice = await _salesInvoiceRepository.FirstOrDefaultAsync((int)input.Id);
            ObjectMapper.Map(input, salesInvoice);
        }

        [AbpAuthorize(AppPermissions.Pages_SalesInvoices_Delete)]
        public async Task Delete(EntityDto input)
        {
            await _salesInvoiceRepository.DeleteAsync(input.Id);
        }

        [AbpAuthorize(AppPermissions.Pages_SalesInvoices)]
        public async Task<PagedResultDto<SalesInvoiceSalesOrderLookupTableDto>> GetAllSalesOrderForLookupTable(GetAllForLookupTableInput input)
        {
            var query = _lookup_salesOrderRepository.GetAll().WhereIf(
                   !string.IsNullOrWhiteSpace(input.Filter),
                  e => e.Number != null && e.Number.Contains(input.Filter)
               );

            var totalCount = await query.CountAsync();

            var salesOrderList = await query
                .PageBy(input)
                .ToListAsync();

            var lookupTableDtoList = new List<SalesInvoiceSalesOrderLookupTableDto>();
            foreach (var salesOrder in salesOrderList)
            {
                lookupTableDtoList.Add(new SalesInvoiceSalesOrderLookupTableDto
                {
                    Id = salesOrder.Id,
                    DisplayName = salesOrder.Number?.ToString()
                });
            }

            return new PagedResultDto<SalesInvoiceSalesOrderLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
        }
    }
}