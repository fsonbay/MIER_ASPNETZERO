namespace DDM.SalesInvoicePayments.Dtos
{
    public class GetSalesInvoicePaymentForViewDto
    {
        public SalesInvoicePaymentDto SalesInvoicePayment { get; set; }

        public string SalesInvoiceSalesInvoiceLineNames { get; set; }

        public string PaymentMethodName { get; set; }

        public string SalesOrderSalesOrderLineNames { get; set; }

    }
}