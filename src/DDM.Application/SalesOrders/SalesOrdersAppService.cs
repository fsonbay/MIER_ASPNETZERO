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

        [AbpAuthorize(AppPermissions.Pages_SalesOrders_Create, AppPermissions.Pages_SalesOrders_Edit)]
        public async Task<GetSalesOrderForEditOutput> GetSalesOrderForEdit(NullableIdDto input)
        {
            SalesOrder salesOrder = null;

            if (input.Id.HasValue)
            {
                salesOrder = await _salesOrderRepository.FirstOrDefaultAsync((int)input.Id);
            }

            var output = new GetSalesOrderForEditOutput();

            //Sales Order
            output.SalesOrder = salesOrder != null
                        ? ObjectMapper.Map<CreateOrEditSalesOrderDto>(salesOrder)
                        : new CreateOrEditSalesOrderDto();



            //SalesOrderLine
            var salesOrderLine = new SalesOrderLine();

            salesOrderLine.Id = 0;
            salesOrderLine.Name = "test name";
            salesOrderLine.Description = "test desc";

            var lineDto = salesOrderLine != null
                     ? ObjectMapper.Map<CreateOrEditSalesOrderLineDto>(salesOrderLine)
                     : new CreateOrEditSalesOrderLineDto();

            var outputLine = new GetSalesOrderLineForEditOutput();



            //Sales Order
            //output.SalesOrderLines = salesOrder != null
            //            ? ObjectMapper.Map<CreateOrEditSalesOrderDto>(salesOrder)
            //            : new CreateOrEditSalesOrderDto();


            //if (input.Id.HasValue)
            //{
            //    salesOrderLine = _salesOrderLineRepository.GetSalesOrderLines((int)input.Id);
            //}


            //var outputLine = new GetSalesOrderLineForEditOutput();
            //var outputLineList = new List<GetSalesOrderLineForEditOutput>();

            //outputLine.SalesOrderLine.Id = 0;
            //outputLine.SalesOrderLine.Name = "test name";
            //outputLine.SalesOrderLine.Name = "test desc";

            //outputLineList.Add(outputLine);



            //Customer
            output.Customers = _lookup_customerRepository
                .GetAll()
                .Select(c => new ComboboxItemDto(c.Id.ToString(), c.Name + " (" + c.Company + ")")
                { IsSelected = output.SalesOrder.CustomerId == c.Id })
                .ToList();

            //ProductionStatus
            output.ProductionStatuses = _lookup_productionStatusRepository
                .GetAll()
                        .Select(c => new ComboboxItemDto(c.Id.ToString(), c.Name)
                        { 
                            IsSelected = output.SalesOrder.ProductionStatusId == c.Id })
                        .ToList();

            //SalesOrderLine
          //  output.SalesOrderLines = outputLineList;

            return output;

            ////Empty model default value
            //orderLine.SalesOrderLineID = 0;
            //orderLine.Name = "";
            //orderLine.Description = "";
            //orderLine.MarkForDelete = false;
            //orderLineList.Add(orderLine);










            //EDIT MODE
            //if (input.Id.HasValue)
            //{


            //    var filteredSalesOrderLines = _salesOrderLineRepository.GetSalesOrderLines((int)input.Id);

            //    var s = from o in filteredSalesOrderLines
            //            select new GetSalesOrderLineForEditOutput()
            //            {
            //                SalesOrderLine = new CreateOrEditSalesOrderLineDto
            //                {
            //                    Id = o.Id,
            //                    Name = o.Name,
            //                    Description = o.Description,
            //                    SalesOrderId = o.SalesOrderId,
            //                }

            //};

            //    salesOrderlines = s.ToList();
            //}

            //INSERT MODE 
            //else
            //{
            //    //CUSTOMER NULL DEFAULT
            //    ComboboxItemDto DefaultSelected = new ComboboxItemDto
            //    {
            //        DisplayText = "Please select...",
            //        IsSelected = true,
            //        Value = ""
            //    };

            //    output.Customers.Add(DefaultSelected);

            //    //SALES ORDER LINES 
            //  //  var x = new CreateOrEditSalesOrderLineDto();

            //    var SalesOrderLineDefaultValue = new GetSalesOrderLineForEditOutput()
            //    {
            //        SalesOrderLine = new CreateOrEditSalesOrderLineDto
            //        {
            //            Id = 0,
            //            Name = "",
            //            Description = "",
            //            SalesOrderId = 0,
            //        }
            //    };

            //    salesOrderlines.Add(SalesOrderLineDefaultValue);
            //}






            //  var salesOrder = _salesOrderRepository.GetIncludes(input.Id);

            //var createOrEditSalesOrderDto = new CreateOrEditSalesOrderDto
            //{
            //    Id = salesOrder.Id,
            //    Number = salesOrder.Number,
            //    ProcessedBySurabaya = salesOrder.ProcessedBySurabaya,
            //    Date = salesOrder.Date,
            //    Deadline = salesOrder.Deadline,
            //    Amount = salesOrder.Amount.ToString("N0", new CultureInfo("id")),
            //    CustomerId = salesOrder.CustomerId,
            //    ProductionStatusId = salesOrder.ProductionStatusId,
            //    Notes = salesOrder.Notes
            //};

            //var createOrEditSalesOrderLineDtoList = new List<CreateOrEditSalesOrderLineDto>();

            //foreach(var salesOrderLine in salesOrder.SalesOrderLines.ToList())
            //{
            //    var createOrEditSalesOrderLineDto = new CreateOrEditSalesOrderLineDto
            //    {
            //        Id = salesOrderLine.Id,
            //        Name = salesOrderLine.Name,
            //        Description = salesOrderLine.Description,
            //        MaterialId = salesOrderLine.MaterialId,
            //        MachineId = salesOrderLine.MachineId,
            //        Quantity = salesOrderLine.Quantity.ToString("N0", new CultureInfo("id")),
            //        UnitPrice = salesOrderLine.UnitPrice.ToString("N0", new CultureInfo("id")),
            //        LineAmount = salesOrderLine.LineAmount.ToString("N0", new CultureInfo("id")),
            //        MarkForDelete = false
            //    };

            //    //Add createOrEditSalesOrderLineDto to list
            //    createOrEditSalesOrderLineDtoList.Add(createOrEditSalesOrderLineDto);
            //};

            ////Assign line list to nested SalesOrderLines 
            //createOrEditSalesOrderDto.SalesOrderLines = createOrEditSalesOrderLineDtoList;

            //var output = new GetSalesOrderForEditOutput { SalesOrder = createOrEditSalesOrderDto };


            //if (output.SalesOrder.CustomerId != null)
            //{
            //    var _lookupCustomer = await _lookup_customerRepository.FirstOrDefaultAsync((int)output.SalesOrder.CustomerId);
            //    output.CustomerName = _lookupCustomer?.Name?.ToString();
            //}

            //return output;

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