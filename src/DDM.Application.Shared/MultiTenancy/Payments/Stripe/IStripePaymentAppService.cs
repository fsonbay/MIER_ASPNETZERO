using System.Threading.Tasks;
using Abp.Application.Services;
using DDM.MultiTenancy.Payments.Dto;
using DDM.MultiTenancy.Payments.Stripe.Dto;

namespace DDM.MultiTenancy.Payments.Stripe
{
    public interface IStripePaymentAppService : IApplicationService
    {
        Task ConfirmPayment(StripeConfirmPaymentInput input);

        StripeConfigurationDto GetConfiguration();

        Task<SubscriptionPaymentDto> GetPaymentAsync(StripeGetPaymentInput input);

        Task<string> CreatePaymentSession(StripeCreatePaymentSessionInput input);
    }
}