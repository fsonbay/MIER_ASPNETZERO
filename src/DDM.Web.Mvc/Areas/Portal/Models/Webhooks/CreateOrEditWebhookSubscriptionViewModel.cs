using Abp.Application.Services.Dto;
using Abp.Webhooks;
using DDM.WebHooks.Dto;

namespace DDM.Web.Areas.Portal.Models.Webhooks
{
    public class CreateOrEditWebhookSubscriptionViewModel
    {
        public WebhookSubscription WebhookSubscription { get; set; }

        public ListResultDto<GetAllAvailableWebhooksOutput> AvailableWebhookEvents { get; set; }
    }
}
