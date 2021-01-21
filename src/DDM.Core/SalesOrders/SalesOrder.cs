using DDM.Customers;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using System.Collections.Generic;
using DDM.SalesOrderLines;
using DDM.ProductionStatuses;

namespace DDM.SalesOrders
{
    [Table("SalesOrder")]
    public class SalesOrder : FullAuditedEntity
    {
        [Required]
        [StringLength(SalesOrderConsts.MaxNumberLength, MinimumLength = SalesOrderConsts.MinNumberLength)]
        public virtual string Number { get; set; }
        public bool ProcessedBySurabaya { get; set; }
        public virtual DateTime Date { get; set; }
        public virtual DateTime Deadline { get; set; }
        public decimal Amount { get; set; }
        public bool? MarkForDelete { get; set; }
        public string Notes { get; set; }

        public string SalesOrderLineNames { get; set; }
        public List<SalesOrderLine> SalesOrderLines { get; set; }

        public int ProductionStatusId { get; set; }
        [ForeignKey("ProductionStatusId")]
        public ProductionStatus ProductionStatusFK { get; set; }

        public virtual int CustomerId { get; set; }
        [ForeignKey("CustomerId")]
        public Customer CustomerFk { get; set; }




    }
}