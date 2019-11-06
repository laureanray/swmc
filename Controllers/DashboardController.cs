using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using swmc.Data;
using swmc.Models;

namespace swmc.Controllers
{
    [Authorize(Roles = "Admin, Operations, HR, Principal")]
    public class Dashboard : Controller
    {
        private readonly ApplicationDbContext _context;

        public Dashboard(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var embarkations = await _context.Embarkations.Include(e => e.Request).Include(e => e.Applicants).ThenInclude(e => e.Applicant).Where(
                e => e.Request.EndDate == DateTime.Today
            ).ToListAsync();

            if (embarkations.Any())
            {
                foreach (var e in embarkations)
                {
                    foreach (var applicant in e.Applicants)
                    {
                        applicant.Applicant.Status = Status.Active;
                        _context.Entry(applicant).State = EntityState.Modified;
                    }

                    e.EmbarkationStatus = EmbarkationStatus.Disembarked;
                    _context.Entry(e).State = EntityState.Modified;
                }

                await _context.SaveChangesAsync();
            }
            
            return View();
        }
    }
}
