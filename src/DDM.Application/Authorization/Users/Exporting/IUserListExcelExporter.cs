using System.Collections.Generic;
using DDM.Authorization.Users.Dto;
using DDM.Dto;

namespace DDM.Authorization.Users.Exporting
{
    public interface IUserListExcelExporter
    {
        FileDto ExportToFile(List<UserListDto> userListDtos);
    }
}