using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace DDM.CustomerCategories
{
    [Table("CustomerCategory")]
    public class CustomerCategory : Entity
    {

        [Required]
        [StringLength(CustomerCategoryConsts.MaxNameLength, MinimumLength = CustomerCategoryConsts.MinNameLength)]
        public virtual string Name { get; set; }

        public virtual string Description { get; set; }

    }
}