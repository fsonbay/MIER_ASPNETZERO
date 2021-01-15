using Abp.AutoMapper;
using DDM.MultiTenancy;
using DDM.MultiTenancy.Dto;
using DDM.Web.Areas.Portal.Models.Common;

namespace DDM.Web.Areas.Portal.Models.Tenants
{
    [AutoMapFrom(typeof (GetTenantFeaturesEditOutput))]
    public class TenantFeaturesEditViewModel : GetTenantFeaturesEditOutput, IFeatureEditViewModel
    {
        public Tenant Tenant { get; set; }
    }
}