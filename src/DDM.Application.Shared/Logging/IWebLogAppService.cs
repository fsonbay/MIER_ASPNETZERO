using Abp.Application.Services;
using DDM.Dto;
using DDM.Logging.Dto;

namespace DDM.Logging
{
    public interface IWebLogAppService : IApplicationService
    {
        GetLatestWebLogsOutput GetLatestWebLogs();

        FileDto DownloadWebLogs();
    }
}
