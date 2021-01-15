using System.Threading.Tasks;
using Abp.Application.Services;
using DDM.Editions.Dto;
using DDM.MultiTenancy.Dto;

namespace DDM.MultiTenancy
{
    public interface ITenantRegistrationAppService: IApplicationService
    {
        Task<RegisterTenantOutput> RegisterTenant(RegisterTenantInput input);

        Task<EditionsSelectOutput> GetEditionsForSelect();

        Task<EditionSelectDto> GetEdition(int editionId);
    }
}