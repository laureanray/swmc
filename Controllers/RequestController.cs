using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using swmc.Models;

namespace swmc.Controllers
{
    public class RequestController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
