using System;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using DDM.Web.Areas.Portal.Models.SalesOrders;
using DDM.Web.Controllers;
using DDM.Authorization;
using DDM.SalesOrders;
using DDM.SalesOrders.Dtos;
using Abp.Application.Services.Dto;
using Abp.Extensions;
using System.Collections.Generic;
using DDM.SalesOrderLines;
using DDM.Customers;
using DDM.Customers.Dtos;
using DDM.Web.Areas.Portal.Models.Customers;
using Abp.Notifications;


namespace DDM.Web.Areas.Portal.Controllers
{
    [Area("Portal")]
    [AbpMvcAuthorize(AppPermissions.Pages_SalesOrders)]
    public class SalesOrdersController : DDMControllerBase
    {
        private readonly ISalesOrdersAppService _salesOrdersAppService;
        private readonly ICustomersAppService _customersAppService;


        public SalesOrdersController(ISalesOrdersAppService salesOrdersAppService,
            ICustomersAppService customersAppService)
        {
            _salesOrdersAppService = salesOrdersAppService;
            _customersAppService = customersAppService;
        }

        public ActionResult Index()
        {
            var model = new SalesOrdersViewModel
            {
                FilterText = ""
            };

            return View(model);
        }

        [AbpMvcAuthorize(AppPermissions.Pages_SalesOrders_Create)]
        public async Task<ActionResult> Create(int? customerId)
        {
            SalesOrderOutput output;
            output = await _salesOrdersAppService.GetSalesOrderForCreate();
            var viewModel = ObjectMapper.Map<SalesOrderViewModel>(output);

            //HANDLE ORDER CREATION FROM CUSTOMER PAGE
            if (customerId != null)
            {
                viewModel.CustomerId = (int)customerId;
            }

            viewModel.SalesOrder.Id = 0;

            return View(viewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Create(SalesOrderViewModel viewModel, string submit)
        {

            return null;

        }

       


        [AbpMvcAuthorize(AppPermissions.Pages_SalesOrders_Create, AppPermissions.Pages_SalesOrders_Edit)]
        public async Task<ActionResult> CreateOrEdit(int? id)
        {
            SalesOrderOutput output;
            output = await _salesOrdersAppService.GetSalesOrderForEdit(new NullableIdDto { Id = id });
            var viewModel = ObjectMapper.Map<SalesOrderViewModel>(output);
            return View(viewModel);

            //GetCustomerForEditOutput output;
            //output = await _customersAppService.GetCustomerForEdit(new NullableIdDto { Id = id });
            //var viewModel = ObjectMapper.Map<CreateOrEditCustomerModalViewModel>(output);
            //return PartialView("_CreateOrEditModal", viewModel);


            //GetSalesOrderForEditOutput getSalesOrderForEditOutput;

            //if (id.HasValue)
            //{
            //    getSalesOrderForEditOutput = await _salesOrdersAppService.GetSalesOrderForEdit(new EntityDto { Id = (int)id });

            //    //TODO : GET LINES BASED ON ORDER ID
            //    //getSalesOrderLineForEditOutput

            //    //getSalesOrderLineForEditOutput = new GetSalesOrderLineForEditOutput
            //    //{
            //    //    SalesOrderLines = new List<CreateOrEditSalesOrderLineDto>()
            //    //};

            //}
            //else
            //{
            //    //Empty models
            //    var salesOrder = new CreateOrEditSalesOrderDto();
            //    var templateLine = new CreateOrEditSalesOrderLineDto
            //    {
            //        Name = "",
            //        Description = "",
            //        SalesOrderId = 0,
            //        MachineId = 0,
            //        MaterialId = 0
            //    };

            //    var salesOrderLines = new List<CreateOrEditSalesOrderLineDto>();
            //    salesOrderLines.Add(templateLine);

            //    salesOrder.SalesOrderLines = salesOrderLines;

            //    getSalesOrderForEditOutput = new GetSalesOrderForEditOutput
            //    {
            //        SalesOrder = salesOrder
            //    };

            //}

            //var viewModel = new CreateOrEditSalesOrderViewModel()
            //{
            //    SalesOrder = getSalesOrderForEditOutput.SalesOrder,
            //    CustomerName = getSalesOrderForEditOutput.CustomerName,

            //    SalesOrderCustomerList = await _salesOrdersAppService.GetAllCustomerForTableDropdown(),
            //    SalesOrderMachineList = await _salesOrdersAppService.GetAllMachineForTableDropdown(),
            //    SalesOrderMaterialList = await _salesOrdersAppService.GetAllMaterialForTableDropdown()
            //};

            //return View(viewModel);
        }

        [AbpMvcAuthorize(AppPermissions.Pages_Customers_Create, AppPermissions.Pages_Customers_Edit)]
        public async Task<PartialViewResult> CreateCustomerModal(int? id)
        {
            GetCustomerForEditOutput output;
            output = await _customersAppService.GetCustomerForEdit(new NullableIdDto { Id = id });
            var viewModel = ObjectMapper.Map<CreateOrEditCustomerModalViewModel>(output);
            return PartialView("_CreateOrEditModal", viewModel);
        }












        [AbpMvcAuthorize(AppPermissions.Pages_SalesOrders_Create, AppPermissions.Pages_SalesOrders_Edit)]
        public async Task<PartialViewResult> CreateOrEditModal(int? id)
        {
            return PartialView();

            //GetSalesOrderForEditOutput getSalesOrderForEditOutput;

            //if (id.HasValue)
            //{
            //    getSalesOrderForEditOutput = await _salesOrdersAppService.GetSalesOrderForEdit(new EntityDto { Id = (int)id });

            //    //TODO : GET LINES BASED ON ORDER ID
            //    //getSalesOrderLineForEditOutput

            //    //getSalesOrderLineForEditOutput = new GetSalesOrderLineForEditOutput
            //    //{
            //    //    SalesOrderLines = new List<CreateOrEditSalesOrderLineDto>()
            //    //};

            //}
            //else
            //{
            //    //Empty models
            //    var salesOrder = new CreateOrEditSalesOrderDto();
            //    var templateLine = new CreateOrEditSalesOrderLineDto
            //    {
            //        Name = "",
            //        Description = "",
            //        SalesOrderId = 0,
            //        MachineId = 0,
            //        MaterialId = 0
            //    };

            //    var salesOrderLines = new List<CreateOrEditSalesOrderLineDto>();
            //    salesOrderLines.Add(templateLine);

            //    salesOrder.SalesOrderLines = salesOrderLines;

            //    getSalesOrderForEditOutput = new GetSalesOrderForEditOutput
            //    {
            //        SalesOrder = salesOrder
            //    };

            //}

            //var viewModel = new CreateOrEditSalesOrderViewModel()
            //{
            //    SalesOrder = getSalesOrderForEditOutput.SalesOrder,
            //    CustomerName = getSalesOrderForEditOutput.CustomerName,

            //    SalesOrderCustomerList = await _salesOrdersAppService.GetAllCustomerForTableDropdown(),
            //    SalesOrderMachineList = await _salesOrdersAppService.GetAllMachineForTableDropdown(),
            //    SalesOrderMaterialList = await _salesOrdersAppService.GetAllMaterialForTableDropdown()
            //};

            //return PartialView("_CreateOrEditModal", viewModel);
        }

        public async Task<PartialViewResult> ViewSalesOrderModal(int id)
        {
            var getSalesOrderForViewDto = await _salesOrdersAppService.GetSalesOrderForView(id);

            var model = new SalesOrderViewModel()
            {
                SalesOrder = getSalesOrderForViewDto.SalesOrder
            };

            return PartialView("_ViewSalesOrderModal", model);
        }

        public async Task<PartialViewResult> EditProductionStatusModal(int id)
        {
            return PartialView();

            //GetSalesOrderForEditOutput getSalesOrderForEditOutput;
            //getSalesOrderForEditOutput = await _salesOrdersAppService.GetSalesOrderForEdit(new EntityDto { Id = (int)id });

            //var viewModel = new EditProductionStatusViewModel()
            //{
            //    SalesOrderId = (int)getSalesOrderForEditOutput.SalesOrder.Id,
            //    ProductionStatusId = getSalesOrderForEditOutput.SalesOrder.ProductionStatusId,
            //    Notes = getSalesOrderForEditOutput.SalesOrder.Notes,

            //    ProductionStatusList = await _salesOrdersAppService.GetAllProductionStatusForTableDropdown()
            //};

            //return PartialView("_EditProductionStatusModal", viewModel);

        }
    }

}