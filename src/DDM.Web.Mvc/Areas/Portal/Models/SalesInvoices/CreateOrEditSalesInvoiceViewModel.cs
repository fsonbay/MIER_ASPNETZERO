using DDM.SalesInvoices.Dtos;

using Abp.Extensions;

namespace DDM.Web.Areas.Portal.Models.SalesInvoices
{
    public class CreateOrEditSalesInvoiceModalViewModel
    {
        public CreateOrEditSalesInvoiceDto SalesInvoice { get; set; }

        public string SalesOrderNumber { get; set; }

        public bool IsEditMode => SalesInvoice.Id.HasValue;
    }
}