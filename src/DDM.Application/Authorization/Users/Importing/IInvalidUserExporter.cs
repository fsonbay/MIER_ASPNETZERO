using System.Collections.Generic;
using DDM.Authorization.Users.Importing.Dto;
using DDM.Dto;

namespace DDM.Authorization.Users.Importing
{
    public interface IInvalidUserExporter
    {
        FileDto ExportToFile(List<ImportUserDto> userListDtos);
    }
}
