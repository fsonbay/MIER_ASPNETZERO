using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using DDM.PaymentMethods.Dtos;
using DDM.Dto;

namespace DDM.PaymentMethods
{
    public interface IPaymentMethodsAppService : IApplicationService
    {
        Task<PagedResultDto<GetPaymentMethodForViewDto>> GetAll(GetAllPaymentMethodsInput input);

        Task<GetPaymentMethodForViewDto> GetPaymentMethodForView(int id);

        Task<GetPaymentMethodForEditOutput> GetPaymentMethodForEdit(EntityDto input);

        Task CreateOrEdit(CreateOrEditPaymentMethodDto input);

        Task Delete(EntityDto input);

    }
}