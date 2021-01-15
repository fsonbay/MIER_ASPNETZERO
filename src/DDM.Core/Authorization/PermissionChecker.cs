using Abp.Authorization;
using DDM.Authorization.Roles;
using DDM.Authorization.Users;

namespace DDM.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {

        }
    }
}
