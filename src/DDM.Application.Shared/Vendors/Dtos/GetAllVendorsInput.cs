using Abp.Application.Services.Dto;
using System;

namespace DDM.Vendors.Dtos
{
    public class GetAllVendorsInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }

        public string NameFilter { get; set; }

        public string DescriptionFilter { get; set; }

        public string VendorCategoryNameFilter { get; set; }

    }
}