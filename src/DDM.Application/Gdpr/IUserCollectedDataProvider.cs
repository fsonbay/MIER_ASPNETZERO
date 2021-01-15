using System.Collections.Generic;
using System.Threading.Tasks;
using Abp;
using DDM.Dto;

namespace DDM.Gdpr
{
    public interface IUserCollectedDataProvider
    {
        Task<List<FileDto>> GetFiles(UserIdentifier user);
    }
}
