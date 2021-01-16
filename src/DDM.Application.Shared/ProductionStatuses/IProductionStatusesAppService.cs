using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using DDM.ProductionStatuses.Dtos;
using DDM.Dto;

namespace DDM.ProductionStatuses
{
    public interface IProductionStatusesAppService : IApplicationService
    {
        Task<PagedResultDto<GetProductionStatusForViewDto>> GetAll(GetAllProductionStatusesInput input);

        Task<GetProductionStatusForViewDto> GetProductionStatusForView(int id);

        Task<GetProductionStatusForEditOutput> GetProductionStatusForEdit(EntityDto input);

        Task CreateOrEdit(CreateOrEditProductionStatusDto input);

        Task Delete(EntityDto input);

    }
}