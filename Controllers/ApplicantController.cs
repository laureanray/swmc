using Microsoft.AspNetCore.Mvc;

namespace swmc.Controllers
{
    public class ApplicantController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}