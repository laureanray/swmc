using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rotativa.AspNetCore;
using Rotativa.AspNetCore.Options;
using swmc.Data;
using swmc.Models;
using swmc.ViewModels;

namespace swmc.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ReportController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReportController(ApplicationDbContext context)
        {
            _context = context;
        }

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

        [HttpPost]        
        public async Task<ActionResult> _ApplicantReport(ApplicantReport model)
        {
            DateTime startDateTime = model.DateFrom;
            DateTime endDateTime = model.DateTo.AddDays(1).AddTicks(-1);

            var applicants = await _context.Applicants.Include(a => a.Position).Where(a => a.Status.Equals(model.ApplicantStatus))
                .Where(a => a.DateCreated >= startDateTime && a.DateCreated <= endDateTime).ToListAsync();
            
            
            
            ApplicantViewModel m = new ApplicantViewModel();
            m.Applicants = new List<Applicant>(applicants);
            m.DateFrom = model.DateFrom;
            m.DateTo = model.DateTo;

            return new ViewAsPdf("_ApplicantReport", m)
            {
                PageOrientation = Orientation.Landscape,
                FileName = "APPLICANT_REP_" + DateTime.Now + ".pdf"
            };

     
            return View(m);
        }
        
    }
}
