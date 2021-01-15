using System;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using DDM.Web.Areas.Portal.Models.VendorCategories;
using DDM.Web.Controllers;
using DDM.Authorization;
using DDM.VendorCategories;
using DDM.VendorCategories.Dtos;
using Abp.Application.Services.Dto;
using Abp.Extensions;

namespace DDM.Web.Areas.Portal.Controllers
{
    [Area("Portal")]
    [AbpMvcAuthorize(AppPermissions.Pages_VendorCategories)]
    public class VendorCategoriesController : DDMControllerBase
    {
        private readonly IVendorCategoriesAppService _vendorCategoriesAppService;

        public VendorCategoriesController(IVendorCategoriesAppService vendorCategoriesAppService)
        {
            _vendorCategoriesAppService = vendorCategoriesAppService;
        }

        public ActionResult Index()
        {
            var model = new VendorCategoriesViewModel
            {
                FilterText = ""
            };

            return View(model);
        }

        [AbpMvcAuthorize(AppPermissions.Pages_VendorCategories_Create, AppPermissions.Pages_VendorCategories_Edit)]
        public async Task<PartialViewResult> CreateOrEditModal(int? id)
        {
            GetVendorCategoryForEditOutput getVendorCategoryForEditOutput;

            if (id.HasValue)
            {
                getVendorCategoryForEditOutput = await _vendorCategoriesAppService.GetVendorCategoryForEdit(new EntityDto { Id = (int)id });
            }
            else
            {
                getVendorCategoryForEditOutput = new GetVendorCategoryForEditOutput
                {
                    VendorCategory = new CreateOrEditVendorCategoryDto()
                };
            }

            var viewModel = new CreateOrEditVendorCategoryModalViewModel()
            {
                VendorCategory = getVendorCategoryForEditOutput.VendorCategory,
            };

            return PartialView("_CreateOrEditModal", viewModel);
        }

        public async Task<PartialViewResult> ViewVendorCategoryModal(int id)
        {
            var getVendorCategoryForViewDto = await _vendorCategoriesAppService.GetVendorCategoryForView(id);

            var model = new VendorCategoryViewModel()
            {
                VendorCategory = getVendorCategoryForViewDto.VendorCategory
            };

            return PartialView("_ViewVendorCategoryModal", model);
        }

    }
}