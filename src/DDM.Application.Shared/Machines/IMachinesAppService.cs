using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using DDM.Machines.Dtos;
using DDM.Dto;

namespace DDM.Machines
{
    public interface IMachinesAppService : IApplicationService
    {
        Task<PagedResultDto<GetMachineForViewDto>> GetAll(GetAllMachinesInput input);

        Task<GetMachineForViewDto> GetMachineForView(int id);

        Task<GetMachineForEditOutput> GetMachineForEdit(EntityDto input);

        Task CreateOrEdit(CreateOrEditMachineDto input);

        Task Delete(EntityDto input);

    }
}