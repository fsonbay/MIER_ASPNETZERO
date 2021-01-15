using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using DDM.Materials.Dtos;
using DDM.Dto;
using Abp.Application.Services.Dto;
using DDM.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;

namespace DDM.Materials
{
    [AbpAuthorize(AppPermissions.Pages_Materials)]
    public class MaterialsAppService : DDMAppServiceBase, IMaterialsAppService
    {
        private readonly IRepository<Material> _materialRepository;

        public MaterialsAppService(IRepository<Material> materialRepository)
        {
            _materialRepository = materialRepository;

        }

        public async Task<PagedResultDto<GetMaterialForViewDto>> GetAll(GetAllMaterialsInput input)
        {

            var filteredMaterials = _materialRepository.GetAll()
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.Name.Contains(input.Filter) || e.Description.Contains(input.Filter));

            var pagedAndFilteredMaterials = filteredMaterials
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var materials = from o in pagedAndFilteredMaterials
                            select new GetMaterialForViewDto()
                            {
                                Material = new MaterialDto
                                {
                                    Name = o.Name,
                                    Description = o.Description,
                                    Id = o.Id
                                }
                            };

            var totalCount = await filteredMaterials.CountAsync();

            return new PagedResultDto<GetMaterialForViewDto>(
                totalCount,
                await materials.ToListAsync()
            );
        }

        public async Task<GetMaterialForViewDto> GetMaterialForView(int id)
        {
            var material = await _materialRepository.GetAsync(id);

            var output = new GetMaterialForViewDto { Material = ObjectMapper.Map<MaterialDto>(material) };

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_Materials_Edit)]
        public async Task<GetMaterialForEditOutput> GetMaterialForEdit(EntityDto input)
        {
            var material = await _materialRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetMaterialForEditOutput { Material = ObjectMapper.Map<CreateOrEditMaterialDto>(material) };

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditMaterialDto input)
        {
            if (input.Id == null)
            {
                await Create(input);
            }
            else
            {
                await Update(input);
            }
        }

        [AbpAuthorize(AppPermissions.Pages_Materials_Create)]
        protected virtual async Task Create(CreateOrEditMaterialDto input)
        {
            var material = ObjectMapper.Map<Material>(input);

            await _materialRepository.InsertAsync(material);
        }

        [AbpAuthorize(AppPermissions.Pages_Materials_Edit)]
        protected virtual async Task Update(CreateOrEditMaterialDto input)
        {
            var material = await _materialRepository.FirstOrDefaultAsync((int)input.Id);
            ObjectMapper.Map(input, material);
        }

        [AbpAuthorize(AppPermissions.Pages_Materials_Delete)]
        public async Task Delete(EntityDto input)
        {
            await _materialRepository.DeleteAsync(input.Id);
        }
    }
}