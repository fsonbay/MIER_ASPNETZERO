using System.Collections.Generic;
using Abp.DynamicEntityParameters;

namespace DDM.DynamicEntityParameters
{
    public class DynamicEntityParameterDefinitionAppService : DDMAppServiceBase, IDynamicEntityParameterDefinitionAppService
    {
        private readonly IDynamicEntityParameterDefinitionManager _dynamicEntityParameterDefinitionManager;

        public DynamicEntityParameterDefinitionAppService(IDynamicEntityParameterDefinitionManager dynamicEntityParameterDefinitionManager)
        {
            _dynamicEntityParameterDefinitionManager = dynamicEntityParameterDefinitionManager;
        }

        public List<string> GetAllAllowedInputTypeNames()
        {
            return _dynamicEntityParameterDefinitionManager.GetAllAllowedInputTypeNames();
        }

        public List<string> GetAllEntities()
        {
            return _dynamicEntityParameterDefinitionManager.GetAllEntities();
        }
    }
}
