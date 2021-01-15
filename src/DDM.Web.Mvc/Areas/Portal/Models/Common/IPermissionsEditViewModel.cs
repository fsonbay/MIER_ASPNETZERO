using System.Collections.Generic;
using DDM.Authorization.Permissions.Dto;

namespace DDM.Web.Areas.Portal.Models.Common
{
    public interface IPermissionsEditViewModel
    {
        List<FlatPermissionDto> Permissions { get; set; }

        List<string> GrantedPermissionNames { get; set; }
    }
}