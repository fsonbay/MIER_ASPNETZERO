using System;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using DDM.Web.Areas.Portal.Models.ProductionStatuses;
using DDM.Web.Controllers;
using DDM.Authorization;
using DDM.ProductionStatuses;
using DDM.ProductionStatuses.Dtos;
using Abp.Application.Services.Dto;
using Abp.Extensions;

namespace DDM.Web.Areas.Portal.Controllers
{
    [Area("Portal")]
    [AbpMvcAuthorize(AppPermissions.Pages_ProductionStatuses)]
    public class ProductionStatusesController : DDMControllerBase
    {
        private readonly IProductionStatusesAppService _productionStatusesAppService;

        public ProductionStatusesController(IProductionStatusesAppService productionStatusesAppService)
        {
            _productionStatusesAppService = productionStatusesAppService;
        }

        public ActionResult Index()
        {
            var model = new ProductionStatusesViewModel
            {
                FilterText = ""
            };

            return View(model);
        }

        [AbpMvcAuthorize(AppPermissions.Pages_ProductionStatuses_Create, AppPermissions.Pages_ProductionStatuses_Edit)]
        public async Task<PartialViewResult> CreateOrEditModal(int? id)
        {
            GetProductionStatusForEditOutput getProductionStatusForEditOutput;

            if (id.HasValue)
            {
                getProductionStatusForEditOutput = await _productionStatusesAppService.GetProductionStatusForEdit(new EntityDto { Id = (int)id });
            }
            else
            {
                getProductionStatusForEditOutput = new GetProductionStatusForEditOutput
                {
                    ProductionStatus = new CreateOrEditProductionStatusDto()
                };
            }

            var viewModel = new CreateOrEditProductionStatusModalViewModel()
            {
                ProductionStatus = getProductionStatusForEditOutput.ProductionStatus,
            };

            return PartialView("_CreateOrEditModal", viewModel);
        }

        public async Task<PartialViewResult> ViewProductionStatusModal(int id)
        {
            var getProductionStatusForViewDto = await _productionStatusesAppService.GetProductionStatusForView(id);

            var model = new ProductionStatusViewModel()
            {
                ProductionStatus = getProductionStatusForViewDto.ProductionStatus
            };

            return PartialView("_ViewProductionStatusModal", model);
        }

    }
}