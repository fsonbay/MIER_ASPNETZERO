using System.Threading.Tasks;
using Abp.Application.Services;
using DDM.Configuration.Tenants.Dto;

namespace DDM.Configuration.Tenants
{
    public interface ITenantSettingsAppService : IApplicationService
    {
        Task<TenantSettingsEditDto> GetAllSettings();

        Task UpdateAllSettings(TenantSettingsEditDto input);

        Task ClearLogo();

        Task ClearCustomCss();
    }
}
