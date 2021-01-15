using Microsoft.AspNetCore.Mvc;
using DDM.Web.Controllers;

namespace DDM.Web.Public.Controllers
{
    public class AboutController : DDMControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}