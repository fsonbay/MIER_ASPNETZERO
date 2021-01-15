using Abp.AspNetCore.Mvc.Authorization;
using DDM.Authorization;
using DDM.Storage;
using Abp.BackgroundJobs;

namespace DDM.Web.Controllers
{
    [AbpMvcAuthorize(AppPermissions.Pages_Administration_Users)]
    public class UsersController : UsersControllerBase
    {
        public UsersController(IBinaryObjectManager binaryObjectManager, IBackgroundJobManager backgroundJobManager)
            : base(binaryObjectManager, backgroundJobManager)
        {
        }
    }
}