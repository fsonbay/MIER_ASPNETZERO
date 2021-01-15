using Abp.Modules;
using Abp.Reflection.Extensions;
using Castle.Windsor.MsDependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using DDM.Configure;
using DDM.Startup;
using DDM.Test.Base;

namespace DDM.GraphQL.Tests
{
    [DependsOn(
        typeof(DDMGraphQLModule),
        typeof(DDMTestBaseModule))]
    public class DDMGraphQLTestModule : AbpModule
    {
        public override void PreInitialize()
        {
            IServiceCollection services = new ServiceCollection();
            
            services.AddAndConfigureGraphQL();

            WindsorRegistrationHelper.CreateServiceProvider(IocManager.IocContainer, services);
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(DDMGraphQLTestModule).GetAssembly());
        }
    }
}