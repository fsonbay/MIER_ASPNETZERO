using DDM.SalesInvoices;
using DDM.PaymentMethods;
using DDM.SalesOrders;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace DDM.SalesInvoicePayments
{
    [Table("SalesInvoicePayment")]
    public class SalesInvoicePayment : Entity
    {

        public virtual DateTime Date { get; set; }

        public virtual decimal Amount { get; set; }

        public virtual int SalesInvoiceId { get; set; }

        [ForeignKey("SalesInvoiceId")]
        public SalesInvoice SalesInvoiceFk { get; set; }

        public virtual int PaymentMethodId { get; set; }

        [ForeignKey("PaymentMethodId")]
        public PaymentMethod PaymentMethodFk { get; set; }

        public virtual int SalesOrderId { get; set; }

        [ForeignKey("SalesOrderId")]
        public SalesOrder SalesOrderFk { get; set; }

    }
}