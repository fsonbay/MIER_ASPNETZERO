using System.Threading.Tasks;
using Abp.Application.Services;
using DDM.MultiTenancy.Payments.PayPal.Dto;

namespace DDM.MultiTenancy.Payments.PayPal
{
    public interface IPayPalPaymentAppService : IApplicationService
    {
        Task ConfirmPayment(long paymentId, string paypalOrderId);

        PayPalConfigurationDto GetConfiguration();
    }
}
