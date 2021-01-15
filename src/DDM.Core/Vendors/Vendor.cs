using DDM.VendorCategories;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace DDM.Vendors
{
    [Table("Vendors")]
    public class Vendor : FullAuditedEntity
    {

        [Required]
        [StringLength(VendorConsts.MaxNameLength, MinimumLength = VendorConsts.MinNameLength)]
        public virtual string Name { get; set; }

        public virtual string Description { get; set; }

        public virtual int VendorCategoryId { get; set; }

        [ForeignKey("VendorCategoryId")]
        public VendorCategory VendorCategoryFk { get; set; }

    }
}