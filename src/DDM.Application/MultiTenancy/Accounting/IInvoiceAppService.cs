using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using DDM.MultiTenancy.Accounting.Dto;

namespace DDM.MultiTenancy.Accounting
{
    public interface IInvoiceAppService
    {
        Task<InvoiceDto> GetInvoiceInfo(EntityDto<long> input);

        Task CreateInvoice(CreateInvoiceDto input);
    }
}
