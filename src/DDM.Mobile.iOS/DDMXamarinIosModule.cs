using Abp.Modules;
using Abp.Reflection.Extensions;

namespace DDM
{
    [DependsOn(typeof(DDMXamarinSharedModule))]
    public class DDMXamarinIosModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(DDMXamarinIosModule).GetAssembly());
        }
    }
}