using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;

namespace DDM
{
    [DependsOn(typeof(DDMClientModule), typeof(AbpAutoMapperModule))]
    public class DDMXamarinSharedModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Localization.IsEnabled = false;
            Configuration.BackgroundJobs.IsJobExecutionEnabled = false;
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(DDMXamarinSharedModule).GetAssembly());
        }
    }
}