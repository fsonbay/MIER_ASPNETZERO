using DDM.CustomerCategories;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace DDM.Customers
{
    [Table("Customer")]
    public class Customer : FullAuditedEntity
    {

        [Required]
        [StringLength(CustomerConsts.MaxNameLength, MinimumLength = CustomerConsts.MinNameLength)]
        public virtual string Name { get; set; }

        public virtual string Company { get; set; }

        public virtual string Phone { get; set; }

        public virtual string Email { get; set; }

        public virtual string Address { get; set; }
        public override DateTime CreationTime { get; set; }

        public virtual int CustomerCategoryId { get; set; }

        [ForeignKey("CustomerCategoryId")]
        public CustomerCategory CustomerCategoryFk { get; set; }

    }
}