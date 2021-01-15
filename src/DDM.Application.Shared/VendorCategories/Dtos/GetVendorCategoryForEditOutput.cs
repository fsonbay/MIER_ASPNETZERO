using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace DDM.VendorCategories.Dtos
{
    public class GetVendorCategoryForEditOutput
    {
        public CreateOrEditVendorCategoryDto VendorCategory { get; set; }

    }
}