using Abp.Modules;
using Abp.Reflection.Extensions;

namespace DDM
{
    [DependsOn(typeof(DDMXamarinSharedModule))]
    public class DDMXamarinAndroidModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(DDMXamarinAndroidModule).GetAssembly());
        }
    }
}