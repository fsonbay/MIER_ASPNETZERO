using Microsoft.AspNetCore.Mvc;
using DDM.Web.Controllers;

namespace DDM.Web.Public.Controllers
{
    public class HomeController : DDMControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}