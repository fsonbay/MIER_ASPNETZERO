using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace DDM.Materials
{
    [Table("Material")]
    public class Material : Entity
    {

        [Required]
        [StringLength(MaterialConsts.MaxNameLength, MinimumLength = MaterialConsts.MinNameLength)]
        public virtual string Name { get; set; }

        public virtual string Description { get; set; }

    }
}