using System.Collections.Generic;
using DDM.Materials.Dtos;
using DDM.Dto;

namespace DDM.Materials.Exporting
{
    public interface IMaterialsExcelExporter
    {
        FileDto ExportToFile(List<GetMaterialForViewDto> materials);
    }
}