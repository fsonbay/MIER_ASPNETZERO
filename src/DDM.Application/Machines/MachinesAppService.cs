using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using DDM.Machines.Dtos;
using DDM.Dto;
using Abp.Application.Services.Dto;
using DDM.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;

namespace DDM.Machines
{
    [AbpAuthorize(AppPermissions.Pages_Machines)]
    public class MachinesAppService : DDMAppServiceBase, IMachinesAppService
    {
        private readonly IRepository<Machine> _machineRepository;

        public MachinesAppService(IRepository<Machine> machineRepository)
        {
            _machineRepository = machineRepository;

        }

        public async Task<PagedResultDto<GetMachineForViewDto>> GetAll(GetAllMachinesInput input)
        {

            var filteredMachines = _machineRepository.GetAll()
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.Name.Contains(input.Filter) || e.Description.Contains(input.Filter));

            var pagedAndFilteredMachines = filteredMachines
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var machines = from o in pagedAndFilteredMachines
                           select new GetMachineForViewDto()
                           {
                               Machine = new MachineDto
                               {
                                   Name = o.Name,
                                   Description = o.Description,
                                   Id = o.Id
                               }
                           };

            var totalCount = await filteredMachines.CountAsync();

            return new PagedResultDto<GetMachineForViewDto>(
                totalCount,
                await machines.ToListAsync()
            );
        }

        public async Task<GetMachineForViewDto> GetMachineForView(int id)
        {
            var machine = await _machineRepository.GetAsync(id);

            var output = new GetMachineForViewDto { Machine = ObjectMapper.Map<MachineDto>(machine) };

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_Machines_Edit)]
        public async Task<GetMachineForEditOutput> GetMachineForEdit(EntityDto input)
        {
            var machine = await _machineRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetMachineForEditOutput { Machine = ObjectMapper.Map<CreateOrEditMachineDto>(machine) };

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditMachineDto input)
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

        [AbpAuthorize(AppPermissions.Pages_Machines_Create)]
        protected virtual async Task Create(CreateOrEditMachineDto input)
        {
            var machine = ObjectMapper.Map<Machine>(input);

            await _machineRepository.InsertAsync(machine);
        }

        [AbpAuthorize(AppPermissions.Pages_Machines_Edit)]
        protected virtual async Task Update(CreateOrEditMachineDto input)
        {
            var machine = await _machineRepository.FirstOrDefaultAsync((int)input.Id);
            ObjectMapper.Map(input, machine);
        }

        [AbpAuthorize(AppPermissions.Pages_Machines_Delete)]
        public async Task Delete(EntityDto input)
        {
            await _machineRepository.DeleteAsync(input.Id);
        }
    }
}