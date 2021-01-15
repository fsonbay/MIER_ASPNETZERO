using DDM.SalesOrders;
using DDM.Machines;
using DDM.Materials;
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

        public virtual decimal UnitPrice { get; set; }

        public virtual decimal LineAmount { get; set; }

        public virtual int SalesOrderId { get; set; }

        [ForeignKey("SalesOrderId")]
        public SalesOrder SalesOrderFk { get; set; }

        public virtual int MachineId { get; set; }

        [ForeignKey("MachineId")]
        public Machine MachineFk { get; set; }

        public virtual int MaterialId { get; set; }

        [ForeignKey("MaterialId")]
        public Material MaterialFk { get; set; }

    }
}