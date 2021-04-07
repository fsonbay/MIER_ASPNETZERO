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

namespace DDM.Web.Areas.Portal.Controllers
{
    [Area("Portal")]
    [AbpMvcAuthorize(AppPermissions.Pages_SalesOrders)]
    public class SalesOrdersController : DDMControllerBase
    {
        private readonly ISalesOrdersAppService _salesOrdersAppService;

        public SalesOrdersController(ISalesOrdersAppService salesOrdersAppService)
        {
            _salesOrdersAppService = salesOrdersAppService;
        }

        public ActionResult Index()
        {
            var model = new SalesOrdersViewModel
            {
                FilterText = ""
            };

            return View(model);
        }

        [AbpMvcAuthorize(AppPermissions.Pages_SalesOrders_Create, AppPermissions.Pages_SalesOrders_Edit)]
        public async Task<ActionResult> CreateOrEdit(int? id)
        {

            GetSalesOrderForEditOutput getSalesOrderForEditOutput;

            if (id.HasValue)
            {
                getSalesOrderForEditOutput = await _salesOrdersAppService.GetSalesOrderForEdit(new EntityDto { Id = (int)id });

                //TODO : GET LINES BASED ON ORDER ID
                //getSalesOrderLineForEditOutput

                //getSalesOrderLineForEditOutput = new GetSalesOrderLineForEditOutput
                //{
                //    SalesOrderLines = new List<CreateOrEditSalesOrderLineDto>()
                //};

            }
            else
            {
                //Empty models
                var salesOrder = new CreateOrEditSalesOrderDto();
                var templateLine = new CreateOrEditSalesOrderLineDto
                {
                    Name = "",
                    Description = "",
                    SalesOrderId = 0,
                    MachineId = 0,
                    MaterialId = 0
                };

                var salesOrderLines = new List<CreateOrEditSalesOrderLineDto>();
                salesOrderLines.Add(templateLine);

                salesOrder.SalesOrderLines = salesOrderLines;

                getSalesOrderForEditOutput = new GetSalesOrderForEditOutput
                {
                    SalesOrder = salesOrder
                };

            }

            var viewModel = new CreateOrEditSalesOrderModalViewModel()
            {
                SalesOrder = getSalesOrderForEditOutput.SalesOrder,
                CustomerName = getSalesOrderForEditOutput.CustomerName,

                SalesOrderCustomerList = await _salesOrdersAppService.GetAllCustomerForTableDropdown(),
                SalesOrderMachineList = await _salesOrdersAppService.GetAllMachineForTableDropdown(),
                SalesOrderMaterialList = await _salesOrdersAppService.GetAllMaterialForTableDropdown()
            };

            return View(viewModel);
        }

        [AbpMvcAuthorize(AppPermissions.Pages_SalesOrders_Create, AppPermissions.Pages_SalesOrders_Edit)]
        public async Task<PartialViewResult> CreateOrEditModal(int? id)
        {
            GetSalesOrderForEditOutput getSalesOrderForEditOutput;

            if (id.HasValue)
            {
                getSalesOrderForEditOutput = await _salesOrdersAppService.GetSalesOrderForEdit(new EntityDto { Id = (int)id });

                //TODO : GET LINES BASED ON ORDER ID
                //getSalesOrderLineForEditOutput

                //getSalesOrderLineForEditOutput = new GetSalesOrderLineForEditOutput
                //{
                //    SalesOrderLines = new List<CreateOrEditSalesOrderLineDto>()
                //};

            }
            else
            {
                //Empty models
                var salesOrder = new CreateOrEditSalesOrderDto();
                var templateLine = new CreateOrEditSalesOrderLineDto
                {
                    Name = "",
                    Description = "",
                    SalesOrderId = 0,
                    MachineId = 0,
                    MaterialId = 0
                };

                var salesOrderLines = new List<CreateOrEditSalesOrderLineDto>();
                salesOrderLines.Add(templateLine);

                salesOrder.SalesOrderLines = salesOrderLines;

                getSalesOrderForEditOutput = new GetSalesOrderForEditOutput
                {
                    SalesOrder = salesOrder
                };

            }

            var viewModel = new CreateOrEditSalesOrderModalViewModel()
            {
                SalesOrder = getSalesOrderForEditOutput.SalesOrder,
                CustomerName = getSalesOrderForEditOutput.CustomerName,

                SalesOrderCustomerList = await _salesOrdersAppService.GetAllCustomerForTableDropdown(),
                SalesOrderMachineList = await _salesOrdersAppService.GetAllMachineForTableDropdown(),
                SalesOrderMaterialList = await _salesOrdersAppService.GetAllMaterialForTableDropdown()
            };

            return PartialView("_CreateOrEditModal", viewModel);
        }

        public async Task<PartialViewResult> ViewSalesOrderModal(int id)
        {
            var getSalesOrderForViewDto = await _salesOrdersAppService.GetSalesOrderForView(id);

            var model = new SalesOrderViewModel()
            {
                SalesOrder = getSalesOrderForViewDto.SalesOrder,
                CustomerName = getSalesOrderForViewDto.CustomerName

            };

            return PartialView("_ViewSalesOrderModal", model);
        }

        public async Task<PartialViewResult> EditProductionStatusModal(int id)
        {
            GetSalesOrderForEditOutput getSalesOrderForEditOutput;
            getSalesOrderForEditOutput = await _salesOrdersAppService.GetSalesOrderForEdit(new EntityDto { Id = (int)id });

            var viewModel = new EditProductionStatusViewModel()
            {
                SalesOrderId = (int)getSalesOrderForEditOutput.SalesOrder.Id,
                ProductionStatusId = getSalesOrderForEditOutput.SalesOrder.ProductionStatusId,
                Notes = getSalesOrderForEditOutput.SalesOrder.Notes,

                ProductionStatusList = await _salesOrdersAppService.GetAllProductionStatusForTableDropdown()
            };

            return PartialView("_EditProductionStatusModal", viewModel);

        }
    }

}