using System.Threading.Tasks;
using Abp.Dependency;

namespace DDM.MultiTenancy.Accounting
{
    public interface IInvoiceNumberGenerator : ITransientDependency
    {
        Task<string> GetNewInvoiceNumber();
    }
}