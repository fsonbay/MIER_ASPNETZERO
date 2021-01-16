using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using DDM.ProductionStatuses.Dtos;
using DDM.Dto;
using Abp.Application.Services.Dto;
using DDM.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;

namespace DDM.ProductionStatuses
{
    [AbpAuthorize(AppPermissions.Pages_ProductionStatuses)]
    public class ProductionStatusesAppService : DDMAppServiceBase, IProductionStatusesAppService
    {
        private readonly IRepository<ProductionStatus> _productionStatusRepository;

        public ProductionStatusesAppService(IRepository<ProductionStatus> productionStatusRepository)
        {
            _productionStatusRepository = productionStatusRepository;

        }

        public async Task<PagedResultDto<GetProductionStatusForViewDto>> GetAll(GetAllProductionStatusesInput input)
        {

            var filteredProductionStatuses = _productionStatusRepository.GetAll()
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.Name.Contains(input.Filter) || e.Description.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.NameFilter), e => e.Name == input.NameFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.DescriptionFilter), e => e.Description == input.DescriptionFilter);

            var pagedAndFilteredProductionStatuses = filteredProductionStatuses
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var productionStatuses = from o in pagedAndFilteredProductionStatuses
                                     select new GetProductionStatusForViewDto()
                                     {
                                         ProductionStatus = new ProductionStatusDto
                                         {
                                             Name = o.Name,
                                             Description = o.Description,
                                             Id = o.Id
                                         }
                                     };

            var totalCount = await filteredProductionStatuses.CountAsync();

            return new PagedResultDto<GetProductionStatusForViewDto>(
                totalCount,
                await productionStatuses.ToListAsync()
            );
        }

        public async Task<GetProductionStatusForViewDto> GetProductionStatusForView(int id)
        {
            var productionStatus = await _productionStatusRepository.GetAsync(id);

            var output = new GetProductionStatusForViewDto { ProductionStatus = ObjectMapper.Map<ProductionStatusDto>(productionStatus) };

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_ProductionStatuses_Edit)]
        public async Task<GetProductionStatusForEditOutput> GetProductionStatusForEdit(EntityDto input)
        {
            var productionStatus = await _productionStatusRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetProductionStatusForEditOutput { ProductionStatus = ObjectMapper.Map<CreateOrEditProductionStatusDto>(productionStatus) };

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditProductionStatusDto input)
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

        [AbpAuthorize(AppPermissions.Pages_ProductionStatuses_Create)]
        protected virtual async Task Create(CreateOrEditProductionStatusDto input)
        {
            var productionStatus = ObjectMapper.Map<ProductionStatus>(input);

            await _productionStatusRepository.InsertAsync(productionStatus);
        }

        [AbpAuthorize(AppPermissions.Pages_ProductionStatuses_Edit)]
        protected virtual async Task Update(CreateOrEditProductionStatusDto input)
        {
            var productionStatus = await _productionStatusRepository.FirstOrDefaultAsync((int)input.Id);
            ObjectMapper.Map(input, productionStatus);
        }

        [AbpAuthorize(AppPermissions.Pages_ProductionStatuses_Delete)]
        public async Task Delete(EntityDto input)
        {
            await _productionStatusRepository.DeleteAsync(input.Id);
        }
    }
}