using System.Threading.Tasks;
using Abp.Application.Services;
using DDM.Install.Dto;

namespace DDM.Install
{
    public interface IInstallAppService : IApplicationService
    {
        Task Setup(InstallDto input);

        AppSettingsJsonDto GetAppSettingsJson();

        CheckDatabaseOutput CheckDatabase();
    }
}