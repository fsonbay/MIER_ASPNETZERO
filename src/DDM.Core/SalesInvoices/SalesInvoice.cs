using DDM.SalesOrders;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace DDM.SalesInvoices
{
    [Table("SalesInvoice")]
    public class SalesInvoice : Entity
    {

        [Required]
        public virtual string Number { get; set; }

        public virtual DateTime Date { get; set; }

        public virtual DateTime DueDate { get; set; }

        public virtual decimal Amount { get; set; }

        public virtual decimal Paid { get; set; }

        public virtual decimal Outstanding { get; set; }
        public string SalesInvoiceLineNames { get; set; }
        public bool MarkForDelete { get; set; }

        public virtual int SalesOrderId { get; set; }

        [ForeignKey("SalesOrderId")]
        public SalesOrder SalesOrderFk { get; set; }

    }
}