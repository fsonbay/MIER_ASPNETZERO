using Abp.Domain.Services;

namespace DDM
{
    public abstract class DDMDomainServiceBase : DomainService
    {
        /* Add your common members for all your domain services. */

        protected DDMDomainServiceBase()
        {
            LocalizationSourceName = DDMConsts.LocalizationSourceName;
        }
    }
}
