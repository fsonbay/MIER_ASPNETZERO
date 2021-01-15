using System.Collections.Generic;
using DDM.DynamicEntityParameters.Dto;

namespace DDM.Web.Areas.Portal.Models.EntityDynamicParameters
{
    public class CreateEntityDynamicParameterViewModel
    {
        public string EntityFullName { get; set; }

        public List<string> AllEntities { get; set; }

        public List<DynamicParameterDto> DynamicParameters { get; set; }
    }
}
