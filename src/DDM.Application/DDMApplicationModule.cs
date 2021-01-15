using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using DDM.Authorization;

namespace DDM
{
    /// <summary>
    /// Application layer module of the application.
    /// </summary>
    [DependsOn(
        typeof(DDMApplicationSharedModule),
        typeof(DDMCoreModule)
        )]
    public class DDMApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            //Adding authorization providers
            Configuration.Authorization.Providers.Add<AppAuthorizationProvider>();

            //Adding custom AutoMapper configuration
            Configuration.Modules.AbpAutoMapper().Configurators.Add(CustomDtoMapper.CreateMappings);
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(DDMApplicationModule).GetAssembly());
        }
    }
}