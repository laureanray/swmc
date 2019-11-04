using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using swmc.Models;

namespace swmc.Controllers
{
    [Authorize(Roles = "Admin")]
    public class EmbarkationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Embarked()
        {
            return View();
        }

        public IActionResult Disembarked()
        {
            return View();
        }
    }
}
