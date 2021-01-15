using System.Linq;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Authorization;
using Abp.DynamicEntityParameters;
using Microsoft.AspNetCore.Mvc;
using DDM.Authorization;
using DDM.DynamicEntityParameters;
using DDM.Web.Areas.Portal.Models.EntityDynamicParameters;
using DDM.Web.Controllers;

namespace DDM.Web.Areas.Portal.Controllers
{
    [Area("Portal")]
    [AbpMvcAuthorize(AppPermissions.Pages_Administration_EntityDynamicParameters)]
    public class EntityDynamicParameterController : DDMControllerBase
    {
        private readonly IDynamicParameterAppService _dynamicParameterAppService;
        private readonly IDynamicEntityParameterDefinitionManager _dynamicEntityParameterDefinitionManager;

        public EntityDynamicParameterController(
            IDynamicParameterAppService dynamicParameterAppService,
            IDynamicEntityParameterDefinitionManager dynamicEntityParameterDefinitionManager
            )
        {
            _dynamicParameterAppService = dynamicParameterAppService;
            _dynamicEntityParameterDefinitionManager = dynamicEntityParameterDefinitionManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [AbpMvcAuthorize(AppPermissions.Pages_Administration_EntityDynamicParameters_Create)]
        public async Task<IActionResult> CreateModal()
        {
            var model = new CreateEntityDynamicParameterViewModel()
            {
                DynamicParameters = (await _dynamicParameterAppService.GetAll()).Items.ToList(),
                AllEntities = _dynamicEntityParameterDefinitionManager.GetAllEntities()
            };

            return PartialView("_CreateModal", model);
        }
    }
}