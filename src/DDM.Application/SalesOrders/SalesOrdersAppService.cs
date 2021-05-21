using DDM.Customers;
using DDM.Machines;
using DDM.Materials;

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using DDM.SalesOrders.Dtos;
using DDM.Dto;
using Abp.Application.Services.Dto;
using DDM.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;
using DDM.EntityFrameworkCore.Repositories;
using DDM.SalesOrderLines;
using DDM.SalesInvoices;
using System.Globalization;
using DDM.ProductionStatuses;

namespace DDM.SalesOrders
{
    [AbpAuthorize(AppPermissions.Pages_SalesOrders)]
    public class SalesOrdersAppService : DDMAppServiceBase, ISalesOrdersAppService
    {
        private readonly SalesOrderRepository _salesOrderRepository;
        private readonly SalesInvoiceRepository _salesInvoiceRepository;
        private readonly SalesOrderLineRepository _salesOrderLineRepository;


        private readonly IRepository<Customer, int> _lookup_customerRepository;
        private readonly IRepository<Machine, int> _lookup_machineRepository;
        private readonly IRepository<Material, int> _lookup_materialRepository;
        private readonly IRepository<ProductionStatus, int> _lookup_productionStatusRepository;

        public SalesOrdersAppService(SalesOrderRepository salesOrderRepository,
            SalesInvoiceRepository salesInvoiceRepository,
            SalesOrderLineRepository salesOrderLineRepository,
            IRepository<Customer, int> lookup_customerRepository,
            IRepository<Machine, int> lookup_machineRepository,
            IRepository<Material, int> lookup_materialRepository,
            IRepository<ProductionStatus, int> lookup_productionStatusRepository
            )
        {
            _salesOrderRepository = salesOrderRepository;
            _salesInvoiceRepository = salesInvoiceRepository;
            _salesOrderLineRepository = salesOrderLineRepository;
            _lookup_customerRepository = lookup_customerRepository;
            _lookup_machineRepository = lookup_machineRepository;
            _lookup_materialRepository = lookup_materialRepository;
            _lookup_productionStatusRepository = lookup_productionStatusRepository;
        }

        public async Task<PagedResultDto<GetSalesOrderForViewDto>> GetAll(GetAllSalesOrdersInput input)
        {
            var filteredSalesOrders = _salesOrderRepository.GetAll()
                        .Include(e => e.CustomerFk)
                        .Include(s => s.ProductionStatusFK)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.Number.Contains(input.Filter) || e.CustomerFk.Name.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.NumberFilter), e => e.Number == input.NumberFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.NotesFilter), e => e.Number == input.NotesFilter)
                        .WhereIf(input.MinDateFilter != null, e => e.Date >= input.MinDateFilter)
                        .WhereIf(input.MaxDateFilter != null, e => e.Date <= input.MaxDateFilter)
                        .WhereIf(input.MinDeadlineFilter != null, e => e.Deadline >= input.MinDeadlineFilter)
                        .WhereIf(input.MaxDeadlineFilter != null, e => e.Deadline <= input.MaxDeadlineFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.CustomerNameFilter), e => e.CustomerFk != null && e.CustomerFk.Name == input.CustomerNameFilter);

            var pagedAndFilteredSalesOrders = filteredSalesOrders
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var salesOrders = from o in pagedAndFilteredSalesOrders
                              select new GetSalesOrderForViewDto()
                              {
                                  SalesOrder = new SalesOrderDto
                                  {
                                      Id = o.Id,
                                      Number = o.Number,
                                      Date = o.Date,
                                      Deadline = o.Deadline,
                                      Notes = o.Notes
                                  },

                                  CustomerName = o.CustomerFk.Name,
                                  ProductionStatus = o.ProductionStatusFK.Name
                              };

            var totalCount = await filteredSalesOrders.CountAsync();

            return new PagedResultDto<GetSalesOrderForViewDto>(
                totalCount,
                await salesOrders.ToListAsync()
            );
        }

        [AbpAuthorize(AppPermissions.Pages_SalesOrders_Create, AppPermissions.Pages_SalesOrders_Edit)]
        public async Task<SalesOrderOutput> GetSalesOrderForEdit(NullableIdDto input)
        {
            SalesOrder salesOrder = null;
            var salesOrderLine = new SalesOrderLine();
            var salesOrderLineDto = new SalesOrderLineDto();
            var salesOrderOutput = new SalesOrderOutput();
            List<SalesOrderLineDto> salesOrderLineDtoList = new List<SalesOrderLineDto>();


            //Sales Order
            if (input.Id.HasValue)
            {
                salesOrder = await _salesOrderRepository
                    .GetAllIncluding(
                        s => s.SalesOrderLines
                    ).FirstOrDefaultAsync(s => s.Id.Equals(input.Id));

                foreach (var line in salesOrder.SalesOrderLines.ToList())
                {
                    salesOrderLineDto = new SalesOrderLineDto
                    {
                        Id = line.Id,
                        Name = line.Name,
                        Description = line.Description,
                        Quantity = line.Quantity,
                        UnitPrice = line.UnitPrice,
                        LineAmount = line.LineAmount
                    };

                    salesOrderLineDtoList.Add(salesOrderLineDto);
                }
                salesOrderOutput.SalesOrder = ObjectMapper.Map<SalesOrderDto>(salesOrder);
                salesOrderOutput.SalesOrderLines = salesOrderLineDtoList;
            }
            else
            {
                salesOrderLineDto = new SalesOrderLineDto
                {
                    Id = 0,
                    Name = "",
                    Description = "",
                    Quantity = 0,
                    UnitPrice = 0,
                    LineAmount = 0
                };

                salesOrderLineDtoList.Add(salesOrderLineDto);
                salesOrderOutput.SalesOrderLines = salesOrderLineDtoList;
            }


            //Sales Order
            salesOrderOutput.SalesOrder = salesOrder != null
                        ? ObjectMapper.Map<SalesOrderDto>(salesOrder)
                        : new SalesOrderDto();



            //salesOrderOutput.Customers

            var customers = _lookup_customerRepository
            .GetAll()
            .Select(c => new ComboboxItemDto(c.Id.ToString(), c.Name + " (" + c.Company + ")")
            {
                IsSelected = salesOrderOutput.SalesOrder.CustomerId == c.Id
            })
            .ToList();

            if (!input.Id.HasValue)
            {
                var def = new ComboboxItemDto("", "Please select ...")
                {
                    IsSelected = true
                };

                customers.Add(def);
            }
            salesOrderOutput.Customers = customers;

            //ProductionStatus
            salesOrderOutput.ProductionStatuses = _lookup_productionStatusRepository
                .GetAll()
                .Select(c => new ComboboxItemDto(c.Id.ToString(), c.Name)
                {
                    IsSelected = salesOrderOutput.SalesOrder.ProductionStatusId == c.Id
                })
                .ToList();




            return salesOrderOutput;
        }













        public async Task<GetSalesOrderForViewDto> GetSalesOrderForView(int id)
        {
            var salesOrder = await _salesOrderRepository.GetAsync(id);

            var output = new GetSalesOrderForViewDto { SalesOrder = ObjectMapper.Map<SalesOrderDto>(salesOrder) };

            if (output.SalesOrder.CustomerId != null)
            {
                var _lookupCustomer = await _lookup_customerRepository.FirstOrDefaultAsync((int)output.SalesOrder.CustomerId);
                output.CustomerName = _lookupCustomer?.Name?.ToString();
            }

            return output;
        }








        public async Task CreateOrEdit(CreateOrEditSalesOrderDto input)
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

        public async Task UpdateProductionStatus(EditSalesOrderProductionStatusDto input)
        {
            var salesOrder = await _salesOrderRepository.GetAsync((int)input.SalesOrderId);

            salesOrder.ProductionStatusId = input.ProductionStatusID;
            salesOrder.Notes = input.Notes;

        }

        private string CreateSalesOrderNumber(DateTime salesOrderDate, int customerId)
        {
            string prefix = "O" + salesOrderDate.ToString("yyyyMMdd");
            int customerOrderCount = _salesOrderRepository.CountCustomerOrder(customerId) + 1;
            return prefix + '-' + customerId + '-' + customerOrderCount;
        }



        [AbpAuthorize(AppPermissions.Pages_SalesOrders_Create)]
        protected virtual async Task Create(CreateOrEditSalesOrderDto input)
        {
            var salesOrder = new SalesOrder
            {
                //Id = 0,
                Number = CreateSalesOrderNumber(input.Date, input.CustomerId),
                ProcessedBySurabaya = input.ProcessedBySurabaya,
                CustomerId = input.CustomerId,
                Date = input.Date,
                Deadline = input.Deadline,
                Amount = decimal.Parse(input.Amount.Replace(".", "")),
                ProductionStatusId = 10, // 10 - New
                MarkForDelete = false
            };

            //Iterate Sales Order Lines
            var salesOrderLineList = new List<SalesOrderLine>();
            List<string> salesOrderLineNames = new List<string>();

            foreach (var item in input.SalesOrderLines.ToList())
            {
                //Create Sales Order Line
                var markForDelete = item.MarkForDelete;
                var name = item.Name;

                if (markForDelete)
                {
                    //Remove from collection
                    input.SalesOrderLines.Remove(item);
                }
                else
                {
                    var salesOrderLine = new SalesOrderLine
                    {
                        Id = 0,
                        Name = item.Name,
                        Description = item.Description,
                        Quantity = decimal.Parse(item.Quantity.Replace(".", "")),
                        UnitPrice = decimal.Parse(item.UnitPrice.Replace(".", "")),
                        LineAmount = decimal.Parse(item.LineAmount.Replace(".", "")),
                        MaterialId = item.MaterialId,
                        MachineId = item.MachineId
                    };

                    //Add item to collection
                    salesOrderLineList.Add(salesOrderLine);
                    salesOrderLineNames.Add(item.Name);

                }
            }

            //Add collection to parent
            salesOrder.SalesOrderLineNames = string.Join(", ", salesOrderLineNames);
            salesOrder.SalesOrderLines = salesOrderLineList;

            //Create Sales Order
            int newSalesOrderId = _salesOrderRepository.InsertAndGetId(salesOrder);

            //Create Sales Invoice
            var salesInvoice = new SalesInvoice
            {
                Number = salesOrder.Number.Substring(1),
                Date = salesOrder.Date,
                DueDate = salesOrder.Deadline,
                SalesOrderId = newSalesOrderId,
                Amount = salesOrder.Amount,
                Paid = 0,
                Outstanding = salesOrder.Amount,
                SalesInvoiceLineNames = salesOrder.SalesOrderLineNames,
                MarkForDelete = false
            };

            int newSalesInvoiceId = await _salesInvoiceRepository.InsertAndGetIdAsync(salesInvoice);
        }

        [AbpAuthorize(AppPermissions.Pages_SalesOrders_Edit)]
        protected virtual async Task Update(CreateOrEditSalesOrderDto input)
        {
            var salesOrder = _salesOrderRepository.Get((int)input.Id);

            //Iterate Sales Order Lines
            var salesOrderLineList = new List<SalesOrderLine>();
            List<string> salesOrderLineNames = new List<string>();

            foreach (var item in input.SalesOrderLines.ToList())
            {
                var id = (int)item.Id;
                var markForDelete = item.MarkForDelete;
                var name = item.Name;

                if (markForDelete)
                {
                    //Existing item
                    if (id != 0)
                    {
                        //Delete from database
                        var deleteItem = _salesOrderLineRepository.Get(id);
                        await _salesOrderLineRepository.DeleteAsync(id);
                    }

                    //Remove from collection
                    input.SalesOrderLines.Remove(item);
                }
                else
                {
                    var salesOrderLine = new SalesOrderLine
                    {
                        Id = (int)item.Id,
                        Name = item.Name,
                        Description = item.Description,
                        Quantity = decimal.Parse(item.Quantity.Replace(".", "")),
                        UnitPrice = decimal.Parse(item.UnitPrice.Replace(".", "")),
                        LineAmount = decimal.Parse(item.LineAmount.Replace(".", "")),
                        SalesOrderId = salesOrder.Id,
                        MaterialId = item.MaterialId,
                        MachineId = item.MachineId
                    };

                    //ADD SUB TO COLLECTION
                    salesOrderLineNames.Add(item.Name);
                    salesOrderLineList.Add(salesOrderLine);
                }
            }

            //ADD SUBS TO PARENT
            salesOrder.SalesOrderLineNames = string.Join(", ", salesOrderLineNames);
            salesOrder.SalesOrderLines = salesOrderLineList;

            salesOrder.Id = (int)input.Id;
            salesOrder.Number = input.Number;
            salesOrder.ProcessedBySurabaya = input.ProcessedBySurabaya;
            salesOrder.CustomerId = input.CustomerId;
            salesOrder.Date = input.Date;
            salesOrder.Deadline = input.Deadline;
            salesOrder.Amount = decimal.Parse(input.Amount.Replace(".", ""));
            salesOrder.ProductionStatusId = 10; // 10 - New
            salesOrder.MarkForDelete = false;


            ////Update
            //_salesOrderRepository.Update(salesOrder);

        }

        [AbpAuthorize(AppPermissions.Pages_SalesOrders_Delete)]
        public async Task Delete(EntityDto input)
        {
            await _salesOrderRepository.DeleteAsync(input.Id);
        }

        public async Task<List<SalesOrderCustomerLookupTableDto>> GetAllCustomerForTableDropdown()
        {
            return await _lookup_customerRepository.GetAll()
                .Select(customer => new SalesOrderCustomerLookupTableDto
                {
                    Id = customer.Id,
                    DisplayName = customer == null || customer.Name == null ? "" : customer.Name.ToString()
                }).ToListAsync();
        }

        public async Task<List<SalesOrderMachineLookupTableDto>> GetAllMachineForTableDropdown()
        {
            return await _lookup_machineRepository.GetAll()
                .Select(machine => new SalesOrderMachineLookupTableDto
                {
                    Id = machine.Id,
                    DisplayName = machine == null || machine.Name == null ? "" : machine.Name.ToString()
                }).ToListAsync();
        }

        public async Task<List<SalesOrderMaterialLookupTableDto>> GetAllMaterialForTableDropdown()
        {
            return await _lookup_materialRepository.GetAll()
                .Select(material => new SalesOrderMaterialLookupTableDto
                {
                    Id = material.Id,
                    DisplayName = material == null || material.Name == null ? "" : material.Name.ToString()
                }).ToListAsync();
        }

        public async Task<List<SalesOrderProductionStatusLookupTableDto>> GetAllProductionStatusForTableDropdown()
        {
            return await _lookup_productionStatusRepository.GetAll()
                .Select(productionStatus => new SalesOrderProductionStatusLookupTableDto
                {
                    Id = productionStatus.Id,
                    DisplayName = productionStatus == null || productionStatus.Name == null ? "" : productionStatus.Name.ToString()
                }).ToListAsync();
        }

    }
}