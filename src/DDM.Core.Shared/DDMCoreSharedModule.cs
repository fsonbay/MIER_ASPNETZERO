using Abp.Modules;
using Abp.Reflection.Extensions;

namespace DDM
{
    public class DDMCoreSharedModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(DDMCoreSharedModule).GetAssembly());
        }
    }
}