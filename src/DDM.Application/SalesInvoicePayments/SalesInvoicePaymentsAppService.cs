using DDM.SalesInvoices;
using DDM.PaymentMethods;
using DDM.SalesOrders;

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using DDM.SalesInvoicePayments.Dtos;
using DDM.Dto;
using Abp.Application.Services.Dto;
using DDM.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;
using Abp.UI;
using DDM.Storage;

namespace DDM.SalesInvoicePayments
{
    [AbpAuthorize(AppPermissions.Pages_SalesInvoicePayments)]
    public class SalesInvoicePaymentsAppService : DDMAppServiceBase, ISalesInvoicePaymentsAppService
    {
        private readonly IRepository<SalesInvoicePayment> _salesInvoicePaymentRepository;
        private readonly IRepository<SalesInvoice, int> _lookup_salesInvoiceRepository;
        private readonly IRepository<PaymentMethod, int> _lookup_paymentMethodRepository;
        private readonly IRepository<SalesOrder, int> _lookup_salesOrderRepository;

        public SalesInvoicePaymentsAppService(IRepository<SalesInvoicePayment> salesInvoicePaymentRepository, IRepository<SalesInvoice, int> lookup_salesInvoiceRepository, IRepository<PaymentMethod, int> lookup_paymentMethodRepository, IRepository<SalesOrder, int> lookup_salesOrderRepository)
        {
            _salesInvoicePaymentRepository = salesInvoicePaymentRepository;
            _lookup_salesInvoiceRepository = lookup_salesInvoiceRepository;
            _lookup_paymentMethodRepository = lookup_paymentMethodRepository;
            _lookup_salesOrderRepository = lookup_salesOrderRepository;

        }

        public async Task<PagedResultDto<GetSalesInvoicePaymentForViewDto>> GetAll(GetAllSalesInvoicePaymentsInput input)
        {

            var filteredSalesInvoicePayments = _salesInvoicePaymentRepository.GetAll()
                        .Include(e => e.SalesInvoiceFk)
                        .Include(e => e.PaymentMethodFk)
                        .Include(e => e.SalesOrderFk)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.SalesInvoiceSalesInvoiceLineNamesFilter), e => e.SalesInvoiceFk != null && e.SalesInvoiceFk.SalesInvoiceLineNames == input.SalesInvoiceSalesInvoiceLineNamesFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.PaymentMethodNameFilter), e => e.PaymentMethodFk != null && e.PaymentMethodFk.Name == input.PaymentMethodNameFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.SalesOrderSalesOrderLineNamesFilter), e => e.SalesOrderFk != null && e.SalesOrderFk.SalesOrderLineNames == input.SalesOrderSalesOrderLineNamesFilter);

            var pagedAndFilteredSalesInvoicePayments = filteredSalesInvoicePayments
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var salesInvoicePayments = from o in pagedAndFilteredSalesInvoicePayments
                                       join o1 in _lookup_salesInvoiceRepository.GetAll() on o.SalesInvoiceId equals o1.Id into j1
                                       from s1 in j1.DefaultIfEmpty()

                                       join o2 in _lookup_paymentMethodRepository.GetAll() on o.PaymentMethodId equals o2.Id into j2
                                       from s2 in j2.DefaultIfEmpty()

                                       join o3 in _lookup_salesOrderRepository.GetAll() on o.SalesOrderId equals o3.Id into j3
                                       from s3 in j3.DefaultIfEmpty()

                                       select new
                                       {

                                           Id = o.Id,
                                           SalesInvoiceSalesInvoiceLineNames = s1 == null || s1.SalesInvoiceLineNames == null ? "" : s1.SalesInvoiceLineNames.ToString(),
                                           PaymentMethodName = s2 == null || s2.Name == null ? "" : s2.Name.ToString(),
                                           SalesOrderSalesOrderLineNames = s3 == null || s3.SalesOrderLineNames == null ? "" : s3.SalesOrderLineNames.ToString()
                                       };

            var totalCount = await filteredSalesInvoicePayments.CountAsync();

            var dbList = await salesInvoicePayments.ToListAsync();
            var results = new List<GetSalesInvoicePaymentForViewDto>();

            foreach (var o in dbList)
            {
                var res = new GetSalesInvoicePaymentForViewDto()
                {
                    SalesInvoicePayment = new SalesInvoicePaymentDto
                    {

                        Id = o.Id,
                    },
                    SalesInvoiceSalesInvoiceLineNames = o.SalesInvoiceSalesInvoiceLineNames,
                    PaymentMethodName = o.PaymentMethodName,
                    SalesOrderSalesOrderLineNames = o.SalesOrderSalesOrderLineNames
                };

                results.Add(res);
            }

            return new PagedResultDto<GetSalesInvoicePaymentForViewDto>(
                totalCount,
                results
            );

        }

        [AbpAuthorize(AppPermissions.Pages_SalesInvoicePayments_Edit)]
        public async Task<GetSalesInvoicePaymentForEditOutput> GetSalesInvoicePaymentForEdit(EntityDto input)
        {
            var salesInvoicePayment = await _salesInvoicePaymentRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetSalesInvoicePaymentForEditOutput { SalesInvoicePayment = ObjectMapper.Map<CreateOrEditSalesInvoicePaymentDto>(salesInvoicePayment) };

            if (output.SalesInvoicePayment.SalesInvoiceId != null)
            {
                var _lookupSalesInvoice = await _lookup_salesInvoiceRepository.FirstOrDefaultAsync((int)output.SalesInvoicePayment.SalesInvoiceId);
                output.SalesInvoiceSalesInvoiceLineNames = _lookupSalesInvoice?.SalesInvoiceLineNames?.ToString();
            }

            if (output.SalesInvoicePayment.PaymentMethodId != null)
            {
                var _lookupPaymentMethod = await _lookup_paymentMethodRepository.FirstOrDefaultAsync((int)output.SalesInvoicePayment.PaymentMethodId);
                output.PaymentMethodName = _lookupPaymentMethod?.Name?.ToString();
            }

            if (output.SalesInvoicePayment.SalesOrderId != null)
            {
                var _lookupSalesOrder = await _lookup_salesOrderRepository.FirstOrDefaultAsync((int)output.SalesInvoicePayment.SalesOrderId);
                output.SalesOrderSalesOrderLineNames = _lookupSalesOrder?.SalesOrderLineNames?.ToString();
            }

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditSalesInvoicePaymentDto input)
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

        [AbpAuthorize(AppPermissions.Pages_SalesInvoicePayments_Create)]
        protected virtual async Task Create(CreateOrEditSalesInvoicePaymentDto input)
        {
            var salesInvoicePayment = ObjectMapper.Map<SalesInvoicePayment>(input);

            await _salesInvoicePaymentRepository.InsertAsync(salesInvoicePayment);

        }

        [AbpAuthorize(AppPermissions.Pages_SalesInvoicePayments_Edit)]
        protected virtual async Task Update(CreateOrEditSalesInvoicePaymentDto input)
        {
            var salesInvoicePayment = await _salesInvoicePaymentRepository.FirstOrDefaultAsync((int)input.Id);
            ObjectMapper.Map(input, salesInvoicePayment);

        }

        [AbpAuthorize(AppPermissions.Pages_SalesInvoicePayments_Delete)]
        public async Task Delete(EntityDto input)
        {
            await _salesInvoicePaymentRepository.DeleteAsync(input.Id);
        }
        [AbpAuthorize(AppPermissions.Pages_SalesInvoicePayments)]
        public async Task<List<SalesInvoicePaymentSalesInvoiceLookupTableDto>> GetAllSalesInvoiceForTableDropdown()
        {
            return await _lookup_salesInvoiceRepository.GetAll()
                .Select(salesInvoice => new SalesInvoicePaymentSalesInvoiceLookupTableDto
                {
                    Id = salesInvoice.Id,
                    DisplayName = salesInvoice == null || salesInvoice.SalesInvoiceLineNames == null ? "" : salesInvoice.SalesInvoiceLineNames.ToString()
                }).ToListAsync();
        }

        [AbpAuthorize(AppPermissions.Pages_SalesInvoicePayments)]
        public async Task<PagedResultDto<SalesInvoicePaymentPaymentMethodLookupTableDto>> GetAllPaymentMethodForLookupTable(GetAllForLookupTableInput input)
        {
            var query = _lookup_paymentMethodRepository.GetAll().WhereIf(
                   !string.IsNullOrWhiteSpace(input.Filter),
                  e => e.Name != null && e.Name.Contains(input.Filter)
               );

            var totalCount = await query.CountAsync();

            var paymentMethodList = await query
                .PageBy(input)
                .ToListAsync();

            var lookupTableDtoList = new List<SalesInvoicePaymentPaymentMethodLookupTableDto>();
            foreach (var paymentMethod in paymentMethodList)
            {
                lookupTableDtoList.Add(new SalesInvoicePaymentPaymentMethodLookupTableDto
                {
                    Id = paymentMethod.Id,
                    DisplayName = paymentMethod.Name?.ToString()
                });
            }

            return new PagedResultDto<SalesInvoicePaymentPaymentMethodLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
        }

        [AbpAuthorize(AppPermissions.Pages_SalesInvoicePayments)]
        public async Task<PagedResultDto<SalesInvoicePaymentSalesOrderLookupTableDto>> GetAllSalesOrderForLookupTable(GetAllForLookupTableInput input)
        {
            var query = _lookup_salesOrderRepository.GetAll().WhereIf(
                   !string.IsNullOrWhiteSpace(input.Filter),
                  e => e.SalesOrderLineNames != null && e.SalesOrderLineNames.Contains(input.Filter)
               );

            var totalCount = await query.CountAsync();

            var salesOrderList = await query
                .PageBy(input)
                .ToListAsync();

            var lookupTableDtoList = new List<SalesInvoicePaymentSalesOrderLookupTableDto>();
            foreach (var salesOrder in salesOrderList)
            {
                lookupTableDtoList.Add(new SalesInvoicePaymentSalesOrderLookupTableDto
                {
                    Id = salesOrder.Id,
                    DisplayName = salesOrder.SalesOrderLineNames?.ToString()
                });
            }

            return new PagedResultDto<SalesInvoicePaymentSalesOrderLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
        }

    }
}