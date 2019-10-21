using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using swmc.Data;
using swmc.Models;

namespace swmc.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class VesselController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public VesselController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Vessel>>> GetVessels()
        {
            return await _context.Vessels.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Vessel>> GetVessel(long id)
        {
            var vessel = await _context.Vessels.FindAsync(id);

            if (vessel == null)
            {
                return NotFound();
            }

            return vessel;
        }

        [HttpPost]
        public async Task<ActionResult<Vessel>> AddVessel(Vessel vessel)
        {
            vessel.DateAdded = DateTime.Now;
            _context.Vessels.Add(vessel);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetVessel), new {id = vessel.VesselId}, vessel);
        }
    }
}