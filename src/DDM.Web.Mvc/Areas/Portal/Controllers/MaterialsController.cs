using System;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using DDM.Web.Areas.Portal.Models.Materials;
using DDM.Web.Controllers;
using DDM.Authorization;
using DDM.Materials;
using DDM.Materials.Dtos;
using Abp.Application.Services.Dto;
using Abp.Extensions;

namespace DDM.Web.Areas.Portal.Controllers
{
    [Area("Portal")]
    [AbpMvcAuthorize(AppPermissions.Pages_Materials)]
    public class MaterialsController : DDMControllerBase
    {
        private readonly IMaterialsAppService _materialsAppService;

        public MaterialsController(IMaterialsAppService materialsAppService)
        {
            _materialsAppService = materialsAppService;
        }

        public ActionResult Index()
        {
            var model = new MaterialsViewModel
            {
                FilterText = ""
            };

            return View(model);
        }

        [AbpMvcAuthorize(AppPermissions.Pages_Materials_Create, AppPermissions.Pages_Materials_Edit)]
        public async Task<PartialViewResult> CreateOrEditModal(int? id)
        {
            GetMaterialForEditOutput getMaterialForEditOutput;

            if (id.HasValue)
            {
                getMaterialForEditOutput = await _materialsAppService.GetMaterialForEdit(new EntityDto { Id = (int)id });
            }
            else
            {
                getMaterialForEditOutput = new GetMaterialForEditOutput
                {
                    Material = new CreateOrEditMaterialDto()
                };
            }

            var viewModel = new CreateOrEditMaterialModalViewModel()
            {
                Material = getMaterialForEditOutput.Material,
            };

            return PartialView("_CreateOrEditModal", viewModel);
        }

        public async Task<PartialViewResult> ViewMaterialModal(int id)
        {
            var getMaterialForViewDto = await _materialsAppService.GetMaterialForView(id);

            var model = new MaterialViewModel()
            {
                Material = getMaterialForViewDto.Material
            };

            return PartialView("_ViewMaterialModal", model);
        }

    }
}