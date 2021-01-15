using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;

namespace DDM.Startup
{
    [DependsOn(typeof(DDMCoreModule))]
    public class DDMGraphQLModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(DDMGraphQLModule).GetAssembly());
        }

        public override void PreInitialize()
        {
            base.PreInitialize();

            //Adding custom AutoMapper configuration
            Configuration.Modules.AbpAutoMapper().Configurators.Add(CustomDtoMapper.CreateMappings);
        }
    }
}