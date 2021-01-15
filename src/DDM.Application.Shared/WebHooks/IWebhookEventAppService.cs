using System.Threading.Tasks;
using Abp.Webhooks;

namespace DDM.WebHooks
{
    public interface IWebhookEventAppService
    {
        Task<WebhookEvent> Get(string id);
    }
}
