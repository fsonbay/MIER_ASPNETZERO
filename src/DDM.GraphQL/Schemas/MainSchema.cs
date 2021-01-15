using Abp.Dependency;
using GraphQL;
using GraphQL.Types;
using DDM.Queries.Container;

namespace DDM.Schemas
{
    public class MainSchema : Schema, ITransientDependency
    {
        public MainSchema(IDependencyResolver resolver) :
            base(resolver)
        {
            Query = resolver.Resolve<QueryContainer>();
        }
    }
}