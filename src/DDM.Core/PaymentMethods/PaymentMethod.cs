using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace DDM.PaymentMethods
{
    [Table("PaymentMethod")]
    public class PaymentMethod : Entity
    {

        [Required]
        public virtual string Name { get; set; }

        public virtual string Code { get; set; }

    }
}