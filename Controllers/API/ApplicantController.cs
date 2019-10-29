using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using swmc.Data;
using swmc.Models;

namespace swmc.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicantController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ApplicantController(ApplicationDbContext context)
        {
            _context = context;
        }
 
        [Route("ArchiveApplicant")]
        [HttpGet]
        public async Task<ActionResult> ArchiveApplicant([FromQuery] int applicantId)
        {
            var applicant = await _context.Applicants.FirstOrDefaultAsync(a => a.ApplicantId == applicantId);

            if (applicant == null)
            {
                return NotFound();
            }

            applicant.Status = Status.Archived;
            applicant.DateUpdated = DateTime.Now;
            _context.Entry(applicant).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return Ok(new JsonResponse()
            {
                Message = "Success"
            });
        }
        
        [Route("RestoreApplicant")]
        [HttpGet]
        public async Task<ActionResult> RestoreApplicant([FromQuery] int applicantId)
        {
            var applicant = await _context.Applicants.FirstOrDefaultAsync(a => a.ApplicantId == applicantId);

            if (applicant == null)
            {
                return NotFound();
            }

            applicant.Status = Status.Active;
            applicant.DateUpdated = DateTime.Now;
            _context.Entry(applicant).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return Ok(new JsonResponse()
            {
                Message = "Success"
            });
        }

        [Route("GetApplicants")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Applicant>>> GetApplicants()
        {
            var applicants = await _context.Applicants
                .Include(a => a.Position)
                .Include(a => a.Family)
                .Include(a => a.Beneficiaries)
                .Include(a => a.Dependents)
                .Include(a => a.Allottees)
                .Include(a => a.Documents)
                .Where(a => !a.Status.Equals(Status.Archived)).ToListAsync();
            return applicants;
        }
        
        [Route("GetArchivedApplicants")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Applicant>>> GetArchivedApplicants()
        {
            var applicants = await _context.Applicants.Include(a => a.Family).Include(a => a.Beneficiaries)
                .Include(a => a.Dependents).Include(a => a.Allottees)
                .Include(a => a.Documents).Where(a => a.Status.Equals(Status.Archived)).ToListAsync();
            return applicants;
        }

        [Route("ArchiveDocumentType")]
        [HttpGet]
        public async Task<ActionResult> ArchiveDocumentType([FromQuery] int documentTypeId)
        {
            var dt = await _context.DocumentTypes.FirstOrDefaultAsync(d => d.DocumentTypeId == documentTypeId);

            if (dt == null)
            {
                return NotFound();
            }
            
            dt.DateUpdated = DateTime.Now;
            dt.IsArchived = true;

            _context.Entry(dt).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return Ok(new JsonResponse()
            {
                Message = "Success"
            });
        }

        [Route("RestoreDocumentType")]
        [HttpGet]
        public async Task<ActionResult> RestoreDocumentType([FromQuery] int documentTypeId)
        {
            var dt = await _context.DocumentTypes.FirstOrDefaultAsync(d => d.DocumentTypeId == documentTypeId);

            if (dt == null)
            {
                return NotFound();
            }
            
            dt.DateUpdated = DateTime.Now;
            dt.IsArchived = false;

            _context.Entry(dt).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return Ok(new JsonResponse()
            {
                Message = "Success"
            });
        }

        [Route("GetDocumentTypes")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DocumentType>>> GetDocumentTypes()
        {
            var documentTypes = await _context.DocumentTypes.Where(dt => !dt.IsArchived).ToListAsync();

            return documentTypes;
        }

        [Route("AddDocumentType")]
        [HttpPost]
        public async Task<ActionResult<JsonResponse>> AddDocumentType(DocumentType dt)
        {
            dt.DateCreated = DateTime.Now;

            _context.DocumentTypes.Add(dt);

            await _context.SaveChangesAsync();

            return new JsonResponse()
            {
                Message = "Success"
            };
        }
        
        [Route("GetArchivedDocumentTypes")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DocumentType>>> GetArchivedDocumentTypes()
        {
            var documentTypes = await _context.DocumentTypes.Where(dt => dt.IsArchived).ToListAsync();

            return documentTypes;
        }

        [Route("GetPositions")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Position>>> GetPositions()
        {
            var positions = await _context.Positions.Where(p => !p.IsArchived).ToListAsync();

            return positions;
        }

        
        [Route("GetArchivedPositions")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Position>>> GetArchivedPositions()
        {
            var positions = await _context.Positions.Where(p => p.IsArchived).ToListAsync();

            return positions;
        }

      
    }
}