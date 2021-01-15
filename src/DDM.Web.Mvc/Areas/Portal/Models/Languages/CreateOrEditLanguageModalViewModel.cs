using Abp.AutoMapper;
using DDM.Localization.Dto;

namespace DDM.Web.Areas.Portal.Models.Languages
{
    [AutoMapFrom(typeof(GetLanguageForEditOutput))]
    public class CreateOrEditLanguageModalViewModel : GetLanguageForEditOutput
    {
        public bool IsEditMode => Language.Id.HasValue;
    }
}