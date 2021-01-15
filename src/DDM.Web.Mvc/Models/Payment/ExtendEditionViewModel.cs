using System.Collections.Generic;
using DDM.Editions.Dto;
using DDM.MultiTenancy.Payments;

namespace DDM.Web.Models.Payment
{
    public class ExtendEditionViewModel
    {
        public EditionSelectDto Edition { get; set; }

        public List<PaymentGatewayModel> PaymentGateways { get; set; }
    }
}