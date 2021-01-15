using System.Collections.Generic;

namespace DDM.DynamicEntityParameters
{
    public interface IDynamicEntityParameterDefinitionAppService
    {
        List<string> GetAllAllowedInputTypeNames();

        List<string> GetAllEntities();
    }
}
