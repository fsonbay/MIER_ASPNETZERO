using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace DDM.VendorCategories
{
    [Table("VendorCategory")]
    public class VendorCategory : Entity
    {

        [Required]
        [StringLength(VendorCategoryConsts.MaxNameLength, MinimumLength = VendorCategoryConsts.MinNameLength)]
        public virtual string Name { get; set; }

        public virtual string Description { get; set; }

    }
}