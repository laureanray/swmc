using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
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

        [Route("GetVessels")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Vessel>>> GetVessels()
        {
            return await _context.Vessels.Where(v => !v.IsArchived).ToListAsync();
        }
        
        [Route("GetArchivedVessels")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Vessel>>> GetArchivedVessels()
        {
            return await _context.Vessels.Where(v => v.IsArchived).ToListAsync();
        }


        [Route("ArchiveVessel")]
        [HttpGet]
        public async Task<ActionResult<JsonResponse>> ArchiveVessel(int vesselId)
        {
            var vessel = await _context.Vessels.FirstOrDefaultAsync(v => v.VesselId == vesselId);

            if (vessel == null)
            {
                return NotFound();
            }

            vessel.IsArchived = true;
            vessel.DateUpdated = DateTime.Now;

            _context.Entry(vessel).State = EntityState.Modified;

            await _context.SaveChangesAsync();
            
            return new JsonResponse()
            {
                Message = "Success"
            };
        }
        
        [Route("RestoreVessel")]
        [HttpGet]
        public async Task<ActionResult<JsonResponse>> RestoreVessel(int vesselId)
        {
            var vessel = await _context.Vessels.FirstOrDefaultAsync(v => v.VesselId == vesselId);

            if (vessel == null)
            {
                return NotFound();
            }

            vessel.IsArchived = false;
            vessel.DateUpdated = DateTime.Now;

            _context.Entry(vessel).State = EntityState.Modified;

            await _context.SaveChangesAsync();
            
            return new JsonResponse()
            {
                Message = "Success"
            };
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Vessel>> GetVessel(int id)
        {
            var vessel = await _context.Vessels.FindAsync(id);

            if (vessel == null)
            {
                return NotFound();
            }

            return vessel;
        }

        [Route("AddVessel")]
        [HttpPost]
        public async Task<ActionResult<JsonResponse>> AddVessel(Vessel vessel)
        {
            vessel.DateAdded = DateTime.Now;
            _context.Vessels.Add(vessel);
            await _context.SaveChangesAsync();

            return new JsonResponse()
            {
                Message = "Success"
            };
        }


        [Route("UpdateVessel")]
        [HttpPost]
        public async Task<ActionResult<JsonResponse>> UpdateVessel(Vessel vessel)
        {
            var vesselToUpdate = await _context.Vessels.FirstOrDefaultAsync(v => v.VesselId == vessel.VesselId);

            vesselToUpdate.Classification = vessel.Classification;
            vesselToUpdate.CBA = vessel.CBA;
            vesselToUpdate.Principal = vessel.Principal;
            vesselToUpdate.JSU = vessel.JSU;
            vesselToUpdate.IMONumber = vessel.IMONumber;
            vesselToUpdate.VesselName = vessel.VesselName;
            vesselToUpdate.VesselAbr = vessel.VesselAbr;
            vesselToUpdate.Flag = vessel.Flag;
            vesselToUpdate.PortRegistry = vessel.PortRegistry;
            vesselToUpdate.GrossTonnage = vessel.GrossTonnage;
            vesselToUpdate.HorsePower = vessel.HorsePower;
            vesselToUpdate.YearBuilt = vessel.YearBuilt;
            vesselToUpdate.YearEnrolled = vessel.YearEnrolled;
            vesselToUpdate.EngineMake = vessel.EngineMake;
            vesselToUpdate.Type = vessel.Type;
            vesselToUpdate.OfficialNumber = vessel.OfficialNumber;
            vesselToUpdate.DateUpdated = DateTime.Now;
            vesselToUpdate.Owner = vessel.Owner;

            _context.Entry(vesselToUpdate).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return new JsonResponse()
            {
                Message = "Success"
            };
        }

    }
}