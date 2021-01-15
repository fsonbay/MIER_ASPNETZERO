using Microsoft.Extensions.Configuration;

namespace DDM.Configuration
{
    public interface IAppConfigurationAccessor
    {
        IConfigurationRoot Configuration { get; }
    }
}
