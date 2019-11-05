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
        
    }
}