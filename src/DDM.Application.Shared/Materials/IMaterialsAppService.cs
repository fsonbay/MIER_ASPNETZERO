using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using DDM.Materials.Dtos;
using DDM.Dto;

namespace DDM.Materials
{
    public interface IMaterialsAppService : IApplicationService
    {
        Task<PagedResultDto<GetMaterialForViewDto>> GetAll(GetAllMaterialsInput input);

        Task<GetMaterialForViewDto> GetMaterialForView(int id);

        Task<GetMaterialForEditOutput> GetMaterialForEdit(EntityDto input);

        Task CreateOrEdit(CreateOrEditMaterialDto input);

        Task Delete(EntityDto input);

    }
}