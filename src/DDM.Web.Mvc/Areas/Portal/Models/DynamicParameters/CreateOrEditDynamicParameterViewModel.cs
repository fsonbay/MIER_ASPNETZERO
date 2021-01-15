using System.Collections.Generic;
using DDM.DynamicEntityParameters.Dto;

namespace DDM.Web.Areas.Portal.Models.DynamicParameters
{
    public class CreateOrEditDynamicParameterViewModel
    {
        public DynamicParameterDto DynamicParameterDto { get; set; }

        public List<string> AllowedInputTypes { get; set; }
    }
}
