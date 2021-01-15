using System.Threading.Tasks;
using Abp.Application.Services;

namespace DDM.MultiTenancy
{
    public interface ISubscriptionAppService : IApplicationService
    {
        Task DisableRecurringPayments();

        Task EnableRecurringPayments();
    }
}
