using System.Threading.Tasks;
using DDM.Sessions.Dto;

namespace DDM.Web.Session
{
    public interface IPerRequestSessionCache
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformationsAsync();
    }
}
