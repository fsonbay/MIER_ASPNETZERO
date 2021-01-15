using Abp.Modules;
using Abp.Reflection.Extensions;

namespace DDM
{
    [DependsOn(typeof(DDMCoreSharedModule))]
    public class DDMApplicationSharedModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(DDMApplicationSharedModule).GetAssembly());
        }
    }
}