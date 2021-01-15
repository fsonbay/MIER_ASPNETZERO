using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DDM.Authorization.Permissions.Dto;

namespace DDM.Web.Areas.Portal.Models.Common.Modals
{
    public class PermissionTreeModalViewModel : IPermissionsEditViewModel
    {
        public List<FlatPermissionDto> Permissions { get; set; }
        public List<string> GrantedPermissionNames { get; set; }
    }
}
