using System;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using DDM.Web.Areas.Portal.Models.Vendors;
using DDM.Web.Controllers;
using DDM.Authorization;
using DDM.Vendors;
using DDM.Vendors.Dtos;
using Abp.Application.Services.Dto;
using Abp.Extensions;

namespace DDM.Web.Areas.Portal.Controllers
{
    [Area("Portal")]
    [AbpMvcAuthorize(AppPermissions.Pages_Vendors)]
    public class VendorsController : DDMControllerBase
    {
        private readonly IVendorsAppService _vendorsAppService;

        public VendorsController(IVendorsAppService vendorsAppService)
        {
            _vendorsAppService = vendorsAppService;
        }

        public ActionResult Index()
        {
            var model = new VendorsViewModel
            {
                FilterText = ""
            };

            return View(model);
        }

        [AbpMvcAuthorize(AppPermissions.Pages_Vendors_Create, AppPermissions.Pages_Vendors_Edit)]
        public async Task<PartialViewResult> CreateOrEditModal(int? id)
        {
            GetVendorForEditOutput getVendorForEditOutput;

            if (id.HasValue)
            {
                getVendorForEditOutput = await _vendorsAppService.GetVendorForEdit(new EntityDto { Id = (int)id });
            }
            else
            {
                getVendorForEditOutput = new GetVendorForEditOutput
                {
                    Vendor = new CreateOrEditVendorDto()
                };
            }

            var viewModel = new CreateOrEditVendorModalViewModel()
            {
                Vendor = getVendorForEditOutput.Vendor,
                VendorCategoryName = getVendorForEditOutput.VendorCategoryName,
                VendorVendorCategoryList = await _vendorsAppService.GetAllVendorCategoryForTableDropdown(),
            };

            return PartialView("_CreateOrEditModal", viewModel);
        }

        public async Task<PartialViewResult> ViewVendorModal(int id)
        {
            var getVendorForViewDto = await _vendorsAppService.GetVendorForView(id);

            var model = new VendorViewModel()
            {
                Vendor = getVendorForViewDto.Vendor
                ,
                VendorCategoryName = getVendorForViewDto.VendorCategoryName

            };

            return PartialView("_ViewVendorModal", model);
        }

    }
}