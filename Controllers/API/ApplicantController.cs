using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using swmc.Data;
using swmc.Models;
using swmc.Models.JsonModel;

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

        [Route("GetSkillTypes")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SkillType>>> GetSkillTypes()
        {
            return await _context.SkillTypes.ToListAsync();
        }
        
        [Route("GetApplicants")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Applicant>>> GetApplicants()
        {
            var applicants = await _context.Applicants
                .Include(a => a.Position)
                .Include(a => a.Family)
                .Include(a => a.Dependents)
                .Include(a => a.Documents)
                .Include( a => a.Skills)
                .ThenInclude(a => a.SkillType)
                .Where(a => !a.Status.Equals(Status.Archived)).ToListAsync();
            return applicants;
        }

        [Route("GetApplicant")]
        [HttpGet]
        public async Task<ActionResult<Applicant>> GetApplicant(int applicantId)
        {
            var applicant = await _context.Applicants.Include(a => a.Skills).ThenInclude(a => a.SkillType).FirstOrDefaultAsync(a => a.ApplicantId == applicantId);

            if (applicant == null)
            {
                return NotFound();
            }
            
            return applicant;
        }
        
        [Route("GetArchivedApplicants")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Applicant>>> GetArchivedApplicants()
        {
            var applicants = await _context.Applicants.Include(a => a.Family)
                .Include(a => a.Dependents)
                .Include( a => a.Skills)
                .Include( a => a.Position)
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

        [Route("GetDocuments")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<JsonDocument>>> GetDocuments()
        {
            var documents = new List<JsonDocument>();
            
            var docs =  await _context.Documents.Include(d => d.DocumentType).ToListAsync();

            if (docs.Any())
            {
                foreach (var doc in docs)
                {
                    var applicant = await _context.Applicants.FirstOrDefaultAsync(a => a.ApplicantId == doc.ApplicantId);
                    documents.Add(new JsonDocument()
                    {
                        Document = doc,
                        Applicant = applicant
                    });
                }
            }

            return documents;
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

        [Route("GetEmbarkedApplicants")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Applicant>>> GetEmbarkedApplicants()
        {
            return await _context.Applicants.Where(a => a.Status.Equals(Status.Embarked)).ToListAsync();
        }
 
        [Route("UpdateApplicantSkills")]
        [HttpPost]
        public async Task<ActionResult<JsonResponse>> UpdateApplicantSkills(UpdateSkills model)
        {
            var applicant = await _context.Applicants.Include(a => a.Skills).FirstOrDefaultAsync(a => a.ApplicantId == model.ApplicantId);

            if (applicant == null) return NotFound();

            var skills = new List<Skill>();
            for (int i = 0; i < model.SkillTypes.Count; i++)
            {
                var skillType = await _context.SkillTypes.FirstOrDefaultAsync(s => s.SkillTypeId == model.SkillTypes[i].SkillTypeId);
                
                skills.Add(new Skill()
                {
                    SkillType = skillType
                });
            }

            applicant.Skills = new List<Skill>(skills);
            applicant.DateUpdated = DateTime.Now;
            _context.Entry(applicant).State = EntityState.Modified;

            await _context.SaveChangesAsync();
            
            return new JsonResponse()
            {
                Message = "Success"
            };
        }


        [Route("GetApplicantsThatMatches")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Applicant>>> GetApplicantsThatMatches(int requirementId)
        {
            var requirement = await _context.Requirements.Include(r => r.Position).Include(r => r.Skills).FirstOrDefaultAsync(r => r.RequirementId == requirementId);

            if (requirement == null) return NotFound();
            
            // add query to or weth embarked applicants with less than a month
            var applicants = await _context.Applicants.Include(a => a.Skills).Include(a => a.Position).Where(a => a.Status.Equals(Status.Active)).ToListAsync();
            var applicantsToReturn = new List<Applicant>();
   
            foreach(var applicant in applicants)
            {
                int count = 0;
//                var count = applicant.Skills.Interssct(requirement.Skills).Count();

                for (int i = 0; i < requirement.Skills.Count; i++)
                {
                    for (int j = 0; j < applicant.Skills.Count; j++)
                    {
                        if (requirement.Skills[i].SkillTypeId == applicant.Skills[j].SkillTypeId)
                        {
                            count++;
                        }
                    }
                   
                }

                var a = applicant.Position.Equals(requirement.Position);
                
                if (count == requirement.Skills.Count &&
                    applicant.Position.Equals(requirement.Position) &&
                    applicant.Status.Equals(Status.Active))
                {
                    applicantsToReturn.Add(applicant);
                }
            }


            return applicantsToReturn;
        }
        

      
    }
}