using System.Collections.Generic;
using DDM.Authorization.Users.Importing.Dto;
using Abp.Dependency;

namespace DDM.Authorization.Users.Importing
{
    public interface IUserListExcelDataReader: ITransientDependency
    {
        List<ImportUserDto> GetUsersFromExcel(byte[] fileBytes);
    }
}
