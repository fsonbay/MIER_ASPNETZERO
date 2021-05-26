using DDM.SalesInvoices;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace DDM.SalesInvoiceAdditionalCosts
{
    [Table("SalesInvoiceAdditionalCosts")]
    public class SalesInvoiceAdditionalCost : Entity
    {

        public virtual string Name { get; set; }

        public virtual string Description { get; set; }

        public virtual decimal Quantity { get; set; }

        public virtual decimal Amount { get; set; }

        public virtual decimal Price { get; set; }

        public virtual int SalesInvoiceId { get; set; }

        [ForeignKey("SalesInvoiceId")]
        public SalesInvoice SalesInvoiceFk { get; set; }

    }
}