using System.Collections.Generic;
using Abp.Application.Services.Dto;
using DDM.Editions.Dto;

namespace DDM.MultiTenancy.Dto
{
    public class GetTenantFeaturesEditOutput
    {
        public List<NameValueDto> FeatureValues { get; set; }

        public List<FlatFeatureDto> Features { get; set; }
    }
}