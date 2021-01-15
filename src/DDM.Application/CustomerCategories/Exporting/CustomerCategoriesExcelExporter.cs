using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using DDM.DataExporting.Excel.NPOI;
using DDM.CustomerCategories.Dtos;
using DDM.Dto;
using DDM.Storage;

namespace DDM.CustomerCategories.Exporting
{
    public class CustomerCategoriesExcelExporter : NpoiExcelExporterBase, ICustomerCategoriesExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public CustomerCategoriesExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
            ITempFileCacheManager tempFileCacheManager) :
    base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetCustomerCategoryForViewDto> customerCategories)
        {
            return CreateExcelPackage(
                "CustomerCategories.xlsx",
                excelPackage =>
                {

                    var sheet = excelPackage.CreateSheet(L("CustomerCategories"));

                    AddHeader(
                        sheet,
                        L("Name"),
                        L("Description")
                        );

                    AddObjects(
                        sheet, 2, customerCategories,
                        _ => _.CustomerCategory.Name,
                        _ => _.CustomerCategory.Description
                        );

                });
        }
    }
}