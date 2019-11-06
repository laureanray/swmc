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
    public class ReportController : Controller
    {
        public IActionResult ApplicantReport()
        {
            return View();
        }

        public IActionResult EmbarkationReport()
        {
            return View();
        }

        public IActionResult RequestReport()
        {
            return View();
        }

        
//        public async Task<ActionResult> _ApplicantReport()
//        {
//
//        }
        
    }
}
