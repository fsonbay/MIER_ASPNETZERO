using System.Collections.Generic;
using DDM.Editions;
using DDM.Editions.Dto;
using DDM.MultiTenancy.Payments;
using DDM.MultiTenancy.Payments.Dto;

namespace DDM.Web.Models.Payment
{
    public class BuyEditionViewModel
    {
        public SubscriptionStartType? SubscriptionStartType { get; set; }

        public EditionSelectDto Edition { get; set; }

        public decimal? AdditionalPrice { get; set; }

        public EditionPaymentType EditionPaymentType { get; set; }

        public List<PaymentGatewayModel> PaymentGateways { get; set; }
    }
}
