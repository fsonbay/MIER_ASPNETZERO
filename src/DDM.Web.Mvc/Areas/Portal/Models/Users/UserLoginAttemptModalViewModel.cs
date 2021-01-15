using System.Collections.Generic;
using DDM.Authorization.Users.Dto;

namespace DDM.Web.Areas.Portal.Models.Users
{
    public class UserLoginAttemptModalViewModel
    {
        public List<UserLoginAttemptDto> LoginAttempts { get; set; }
    }
}