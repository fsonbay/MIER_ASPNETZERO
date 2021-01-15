using System;
using Abp.AspNetCore.Mvc.Authorization;
using Abp.DynamicEntityParameters;
using Abp.Extensions;
using Abp.UI;
using Microsoft.AspNetCore.Mvc;
using DDM.Authorization;
using DDM.Web.Areas.Portal.Models.EntityDynamicParameterValues;
using DDM.Web.Controllers;

namespace DDM.Web.Areas.Portal.Controllers
{
    [Area("Portal")]
    [AbpMvcAuthorize(AppPermissions.Pages_Administration_EntityDynamicParameterValue)]
    public class EntityDynamicParameterValueController : DDMControllerBase
    {
        private readonly IDynamicEntityParameterDefinitionManager _dynamicEntityParameterDefinitionManager;

        public EntityDynamicParameterValueController(IDynamicEntityParameterDefinitionManager dynamicEntityParameterDefinitionManager)
        {
            _dynamicEntityParameterDefinitionManager = dynamicEntityParameterDefinitionManager;
        }

        [AbpMvcAuthorize(AppPermissions.Pages_Administration_EntityDynamicParameterValue_Edit)]
        [AbpMvcAuthorize(AppPermissions.Pages_Administration_EntityDynamicParameterValue_Create)]
        [HttpGet("/Portal/EntityDynamicParameterValue/ManageAll/{entityFullName}/{entityId}")]
        public IActionResult ManageAll(string entityFullName, string entityId)
        {
            if (entityFullName.IsNullOrWhiteSpace())
            {
                throw new ArgumentNullException(nameof(entityFullName));
            }

            if (entityId.IsNullOrWhiteSpace())
            {
                throw new ArgumentNullException(nameof(entityId));
            }

            if (!_dynamicEntityParameterDefinitionManager.ContainsEntity(entityFullName))
            {
                throw new UserFriendlyException(L("UnknownEntityType", entityFullName));
            }

            return View(new EntityDynamicParameterValueManageAllViewModel
            {
                EntityFullName = entityFullName,
                EntityId = entityId
            });
        }

        [AbpMvcAuthorize(AppPermissions.Pages_Administration_EntityDynamicParameterValue_Edit)]
        [AbpMvcAuthorize(AppPermissions.Pages_Administration_EntityDynamicParameterValue_Create)]
        public IActionResult ManageEntityDynamicParameterValuesModal(string entityFullName, string rowId)
        {
            if (entityFullName.IsNullOrWhiteSpace())
            {
                throw new ArgumentNullException(nameof(entityFullName));
            }

            if (rowId.IsNullOrWhiteSpace())
            {
                throw new ArgumentNullException(nameof(rowId));
            }

            if (!_dynamicEntityParameterDefinitionManager.ContainsEntity(entityFullName))
            {
                throw new UserFriendlyException(L("UnknownEntityType", entityFullName));
            }

            return PartialView("_ManageEntityDynamicParameterValuesModal", new EntityDynamicParameterValueManageAllViewModel
            {
                EntityFullName = entityFullName,
                EntityId = rowId
            });
        }
    }
}