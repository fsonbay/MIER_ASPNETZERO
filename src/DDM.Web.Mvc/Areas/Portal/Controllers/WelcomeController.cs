using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using DDM.Web.Controllers;

namespace DDM.Web.Areas.Portal.Controllers
{
    [Area("Portal")]
    [AbpMvcAuthorize]
    public class WelcomeController : DDMControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}