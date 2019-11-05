using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using swmc.Data;
using swmc.Models;

namespace swmc.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmbarkationController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public EmbarkationController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Route("GetEmbarkations")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Embarkation>>> GetEmbarkations()
        {
            var embarkations = await _context.Embarkations
                .Include(e => e.Applicants)
                .Include(e => e.Request).ThenInclude(e => e.Vessel)
                .Where(e => e.EmbarkationStatus.Equals(EmbarkationStatus.Embarked)).ToListAsync();

            return embarkations;
        }
        
        [Route("AddEmbarkation")]
        [HttpPost]
        public async Task<ActionResult<JsonResponse>> AddEmbarkation(Embarkation embarkation)
        {

            _context.Embarkations.Add(embarkation);

            for (var i = 0; i < embarkation.Applicants.Count; i++)
            {
                var applicant =
                    await _context.Applicants.FirstOrDefaultAsync(a =>
                        a.ApplicantId == embarkation.Applicants[i].ApplicantId);

                applicant.Status = Status.Embarked;
                _context.Entry(applicant).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            
            return new JsonResponse()
            {
                Message = "Success"
            };
        }

    }
}