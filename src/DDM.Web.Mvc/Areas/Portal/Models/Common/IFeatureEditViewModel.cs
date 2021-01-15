using System.Collections.Generic;
using Abp.Application.Services.Dto;
using DDM.Editions.Dto;

namespace DDM.Web.Areas.Portal.Models.Common
{
    public interface IFeatureEditViewModel
    {
        List<NameValueDto> FeatureValues { get; set; }

        List<FlatFeatureDto> Features { get; set; }
    }
}