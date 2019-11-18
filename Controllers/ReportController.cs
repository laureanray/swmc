using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<ApplicationUser> _userManager;
        
        public ReportController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult ApplicantReport()
        {
            return View();
        }

        public IActionResult EmbarkationReport()
        {
            return View();
        }


        [HttpPost]        
        public async Task<ActionResult> _ApplicantReport(ApplicantReport model)
        {
            DateTime startDateTime = model.DateFrom;
            DateTime endDateTime = model.DateTo.AddDays(1).AddTicks(-1);

            var user = await _userManager.GetUserAsync(HttpContext.User);

            var applicants = await _context.Applicants.Include(a => a.Position).Where(a => a.Status.Equals(model.ApplicantStatus))
                .Where(a => a.DateCreated >= startDateTime && a.DateCreated <= endDateTime).ToListAsync();
            
            
            
            ApplicantViewModel m = new ApplicantViewModel();
            m.Applicants = new List<Applicant>(applicants);
            m.DateFrom = model.DateFrom;
            m.DateTo = model.DateTo;
            m.GeneratedBy = user.FirstName + " " + user.LastName;

            return new ViewAsPdf("_ApplicantReport", m)
            {
                PageOrientation = Orientation.Landscape,
                FileName = "APPLICANT_REP_" + DateTime.Now + ".pdf"
            };

//            return View(m);

        }
        
        [HttpPost]        
        public async Task<ActionResult> _RequestReport(RequestReport model)
        {
            DateTime startDateTime = model.DateFrom;
            DateTime endDateTime = model.DateTo.AddDays(1).AddTicks(-1);

            var requests = await _context.Requests.Include(a => a.Vessel)
                .Where(a => a.DateCreated >= startDateTime && a.DateCreated <= endDateTime).ToListAsync();
            
            RequestViewModel m = new RequestViewModel();
            m.Requests = new List<Request>(requests);
            m.DateFrom = model.DateFrom;
            m.DateTo = model.DateTo;

            return new ViewAsPdf("_RequestReport", m)
            {
                PageOrientation = Orientation.Landscape,
                FileName = "REQUEST_REP_" + DateTime.Now + ".pdf"
            };
        }
        
        [HttpPost]        
        public async Task<ActionResult> _EmbarkationReport(EmbarkationReport model)
        {
            DateTime startDateTime = model.DateFrom;
            DateTime endDateTime = model.DateTo.AddDays(1).AddTicks(-1);

            var embarkations = await _context.Embarkations.Include(a => a.Request).ThenInclude(a => a.Vessel)
                .Where(a => a.DateCreated >= startDateTime && a.DateCreated <= endDateTime).ToListAsync();
            
            EmbarkationViewModel m = new EmbarkationViewModel();
            m.Embarkations = new List<Embarkation>(embarkations);
            m.DateFrom = model.DateFrom;
            m.DateTo = model.DateTo;

            return new ViewAsPdf("_EmbarkationReport", m)
            {
                PageOrientation = Orientation.Landscape,
                FileName = "EMBARKATION_REP_" + DateTime.Now + ".pdf"
            };
        }
    }
}
