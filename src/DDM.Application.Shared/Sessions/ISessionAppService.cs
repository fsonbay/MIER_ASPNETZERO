using System.Threading.Tasks;
using Abp.Application.Services;
using DDM.Sessions.Dto;

namespace DDM.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();

        Task<UpdateUserSignInTokenOutput> UpdateUserSignInToken();
    }
}
