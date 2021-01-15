using Abp.Application.Services;
using Abp.Application.Services.Dto;
using DDM.Authorization.Permissions.Dto;

namespace DDM.Authorization.Permissions
{
    public interface IPermissionAppService : IApplicationService
    {
        ListResultDto<FlatPermissionWithLevelDto> GetAllPermissions();
    }
}
