using System.Collections.Generic;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using DDM.Editions.Dto;
using DDM.Web.Areas.Portal.Models.Common;

namespace DDM.Web.Areas.Portal.Models.Editions
{
    [AutoMapFrom(typeof(GetEditionEditOutput))]
    public class CreateEditionModalViewModel : GetEditionEditOutput, IFeatureEditViewModel
    {
        public IReadOnlyList<ComboboxItemDto> EditionItems { get; set; }

        public IReadOnlyList<ComboboxItemDto> FreeEditionItems { get; set; }
    }
}