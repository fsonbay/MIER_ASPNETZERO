using DDM.SalesOrders;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace DDM.SalesOrderLines
{
    [Table("SalesOrderLine")]
    public class SalesOrderLine : Entity
    {

        [Required]
        public virtual string Name { get; set; }

        public virtual string Description { get; set; }

        public virtual decimal Quantity { get; set; }

        public virtual decimal Price { get; set; }

        public virtual decimal Amount { get; set; }

        public virtual int SalesOrderId { get; set; }

        [ForeignKey("SalesOrderId")]
        public SalesOrder SalesOrderFk { get; set; }

    }
}