using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace DDM.ProductionStatuses
{
    [Table("ProductionStatus")]
    public class ProductionStatus : Entity
    {

        [Required]
        [StringLength(ProductionStatusConsts.MaxNameLength, MinimumLength = ProductionStatusConsts.MinNameLength)]
        public virtual string Name { get; set; }

        public virtual string Description { get; set; }

    }
}