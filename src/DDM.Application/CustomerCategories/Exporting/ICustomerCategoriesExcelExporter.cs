using System.Collections.Generic;
using DDM.CustomerCategories.Dtos;
using DDM.Dto;

namespace DDM.CustomerCategories.Exporting
{
    public interface ICustomerCategoriesExcelExporter
    {
        FileDto ExportToFile(List<GetCustomerCategoryForViewDto> customerCategories);
    }
}