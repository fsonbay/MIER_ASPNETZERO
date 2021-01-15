using System;
using Abp.Application.Services.Dto;

namespace DDM.Vendors.Dtos
{
    public class VendorDto : EntityDto
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public int VendorCategoryId { get; set; }

    }
}