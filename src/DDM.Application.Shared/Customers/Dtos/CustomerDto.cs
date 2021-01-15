using System;
using Abp.Application.Services.Dto;

namespace DDM.Customers.Dtos
{
    public class CustomerDto : EntityDto
    {
        public string Name { get; set; }

        public string Company { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }
        public DateTime CreationTime { get; set; }

        public int CustomerCategoryId { get; set; }


    }
}