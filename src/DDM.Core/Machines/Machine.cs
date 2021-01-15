using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace DDM.Machines
{
    [Table("Machine")]
    public class Machine : Entity
    {

        [Required]
        [StringLength(MachineConsts.MaxNameLength, MinimumLength = MachineConsts.MinNameLength)]
        public virtual string Name { get; set; }

        public virtual string Description { get; set; }

    }
}