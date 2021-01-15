using System.Collections.Generic;
using Abp.Application.Services.Dto;
using DDM.Authorization.Permissions.Dto;
using DDM.Web.Areas.Portal.Models.Common;

namespace DDM.Web.Areas.Portal.Models.Roles
{
    public class RoleListViewModel : IPermissionsEditViewModel
    {
        public List<FlatPermissionDto> Permissions { get; set; }

        public List<string> GrantedPermissionNames { get; set; }
    }
}