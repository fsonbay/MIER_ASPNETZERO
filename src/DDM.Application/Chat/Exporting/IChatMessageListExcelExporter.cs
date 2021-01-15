using System.Collections.Generic;
using Abp;
using DDM.Chat.Dto;
using DDM.Dto;

namespace DDM.Chat.Exporting
{
    public interface IChatMessageListExcelExporter
    {
        FileDto ExportToFile(UserIdentifier user, List<ChatMessageExportDto> messages);
    }
}
