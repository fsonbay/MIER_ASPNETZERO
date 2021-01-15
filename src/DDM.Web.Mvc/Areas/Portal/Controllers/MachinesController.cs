using System;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using DDM.Web.Areas.Portal.Models.Machines;
using DDM.Web.Controllers;
using DDM.Authorization;
using DDM.Machines;
using DDM.Machines.Dtos;
using Abp.Application.Services.Dto;
using Abp.Extensions;

namespace DDM.Web.Areas.Portal.Controllers
{
    [Area("Portal")]
    [AbpMvcAuthorize(AppPermissions.Pages_Machines)]
    public class MachinesController : DDMControllerBase
    {
        private readonly IMachinesAppService _machinesAppService;

        public MachinesController(IMachinesAppService machinesAppService)
        {
            _machinesAppService = machinesAppService;
        }

        public ActionResult Index()
        {
            var model = new MachinesViewModel
            {
                FilterText = ""
            };

            return View(model);
        }

        [AbpMvcAuthorize(AppPermissions.Pages_Machines_Create, AppPermissions.Pages_Machines_Edit)]
        public async Task<PartialViewResult> CreateOrEditModal(int? id)
        {
            GetMachineForEditOutput getMachineForEditOutput;

            if (id.HasValue)
            {
                getMachineForEditOutput = await _machinesAppService.GetMachineForEdit(new EntityDto { Id = (int)id });
            }
            else
            {
                getMachineForEditOutput = new GetMachineForEditOutput
                {
                    Machine = new CreateOrEditMachineDto()
                };
            }

            var viewModel = new CreateOrEditMachineModalViewModel()
            {
                Machine = getMachineForEditOutput.Machine,
            };

            return PartialView("_CreateOrEditModal", viewModel);
        }

        public async Task<PartialViewResult> ViewMachineModal(int id)
        {
            var getMachineForViewDto = await _machinesAppService.GetMachineForView(id);

            var model = new MachineViewModel()
            {
                Machine = getMachineForViewDto.Machine
            };

            return PartialView("_ViewMachineModal", model);
        }

    }
}