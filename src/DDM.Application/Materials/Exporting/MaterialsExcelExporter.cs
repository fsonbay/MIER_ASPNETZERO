using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using DDM.DataExporting.Excel.NPOI;
using DDM.Materials.Dtos;
using DDM.Dto;
using DDM.Storage;

namespace DDM.Materials.Exporting
{
    public class MaterialsExcelExporter : NpoiExcelExporterBase, IMaterialsExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public MaterialsExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
            ITempFileCacheManager tempFileCacheManager) :
    base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetMaterialForViewDto> materials)
        {
            return CreateExcelPackage(
                "Materials.xlsx",
                excelPackage =>
                {

                    var sheet = excelPackage.CreateSheet(L("Materials"));

                    AddHeader(
                        sheet,
                        L("Name"),
                        L("Description")
                        );

                    AddObjects(
                        sheet, 2, materials,
                        _ => _.Material.Name,
                        _ => _.Material.Description
                        );

                });
        }
    }
}