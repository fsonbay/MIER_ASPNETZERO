using System.Threading.Tasks;
using DDM.Authorization.Users;

namespace DDM.WebHooks
{
    public interface IAppWebhookPublisher
    {
        Task PublishTestWebhook();
    }
}
