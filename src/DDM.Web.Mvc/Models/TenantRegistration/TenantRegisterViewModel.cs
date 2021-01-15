using DDM.Editions;
using DDM.Editions.Dto;
using DDM.MultiTenancy.Payments;
using DDM.Security;
using DDM.MultiTenancy.Payments.Dto;

namespace DDM.Web.Models.TenantRegistration
{
    public class TenantRegisterViewModel
    {
        public PasswordComplexitySetting PasswordComplexitySetting { get; set; }

        public int? EditionId { get; set; }

        public SubscriptionStartType? SubscriptionStartType { get; set; }

        public EditionSelectDto Edition { get; set; }

        public EditionPaymentType EditionPaymentType { get; set; }
    }
}
