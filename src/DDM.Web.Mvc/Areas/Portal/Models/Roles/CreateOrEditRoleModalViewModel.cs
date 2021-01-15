using Abp.AutoMapper;
using DDM.Authorization.Roles.Dto;
using DDM.Web.Areas.Portal.Models.Common;

namespace DDM.Web.Areas.Portal.Models.Roles
{
    [AutoMapFrom(typeof(GetRoleForEditOutput))]
    public class CreateOrEditRoleModalViewModel : GetRoleForEditOutput, IPermissionsEditViewModel
    {
        public bool IsEditMode => Role.Id.HasValue;
    }
}