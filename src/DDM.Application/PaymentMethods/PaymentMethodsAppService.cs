using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using DDM.PaymentMethods.Dtos;
using DDM.Dto;
using Abp.Application.Services.Dto;
using DDM.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;
using Abp.UI;
using DDM.Storage;

namespace DDM.PaymentMethods
{
    [AbpAuthorize(AppPermissions.Pages_PaymentMethods)]
    public class PaymentMethodsAppService : DDMAppServiceBase, IPaymentMethodsAppService
    {
        private readonly IRepository<PaymentMethod> _paymentMethodRepository;

        public PaymentMethodsAppService(IRepository<PaymentMethod> paymentMethodRepository)
        {
            _paymentMethodRepository = paymentMethodRepository;

        }


        public List<PaymentMethodDto> GetAll()
        {
            var paymentMethods = _paymentMethodRepository.GetAll();
            var results = new List<PaymentMethodDto>();

            foreach (var o in paymentMethods)
            {
                var res = new PaymentMethodDto()
                {
                    Id = o.Id,
                    Name = o.Name
                };

                results.Add(res);
            }

            return results;

        }
        public async Task<PagedResultDto<GetPaymentMethodForViewDto>> GetAll(GetAllPaymentMethodsInput input)
        {

            var filteredPaymentMethods = _paymentMethodRepository.GetAll()
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.Name.Contains(input.Filter) || e.Code.Contains(input.Filter));

            var pagedAndFilteredPaymentMethods = filteredPaymentMethods
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var paymentMethods = from o in pagedAndFilteredPaymentMethods
                                 select new
                                 {
                                     Id = o.Id
                                 };

            var totalCount = await filteredPaymentMethods.CountAsync();

            var dbList = await paymentMethods.ToListAsync();
            var results = new List<GetPaymentMethodForViewDto>();

            foreach (var o in dbList)
            {
                var res = new GetPaymentMethodForViewDto()
                {
                    PaymentMethod = new PaymentMethodDto
                    {

                        Id = o.Id,
                    }
                };

                results.Add(res);
            }

            return new PagedResultDto<GetPaymentMethodForViewDto>(
                totalCount,
                results
            );

        }

        public async Task<GetPaymentMethodForViewDto> GetPaymentMethodForView(int id)
        {
            var paymentMethod = await _paymentMethodRepository.GetAsync(id);

            var output = new GetPaymentMethodForViewDto { PaymentMethod = ObjectMapper.Map<PaymentMethodDto>(paymentMethod) };

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_PaymentMethods_Edit)]
        public async Task<GetPaymentMethodForEditOutput> GetPaymentMethodForEdit(EntityDto input)
        {
            var paymentMethod = await _paymentMethodRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetPaymentMethodForEditOutput { PaymentMethod = ObjectMapper.Map<CreateOrEditPaymentMethodDto>(paymentMethod) };

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditPaymentMethodDto input)
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

        [AbpAuthorize(AppPermissions.Pages_PaymentMethods_Create)]
        protected virtual async Task Create(CreateOrEditPaymentMethodDto input)
        {
            var paymentMethod = ObjectMapper.Map<PaymentMethod>(input);

            await _paymentMethodRepository.InsertAsync(paymentMethod);

        }

        [AbpAuthorize(AppPermissions.Pages_PaymentMethods_Edit)]
        protected virtual async Task Update(CreateOrEditPaymentMethodDto input)
        {
            var paymentMethod = await _paymentMethodRepository.FirstOrDefaultAsync((int)input.Id);
            ObjectMapper.Map(input, paymentMethod);

        }

        [AbpAuthorize(AppPermissions.Pages_PaymentMethods_Delete)]
        public async Task Delete(EntityDto input)
        {
            await _paymentMethodRepository.DeleteAsync(input.Id);
        }

    }
}