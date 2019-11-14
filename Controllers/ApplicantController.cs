using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using swmc.Data;
using swmc.Models;
using swmc.Models.FormModel;

namespace swmc.Controllers
{
    public class ApplicantController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ApplicantController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> UpdateApplicant(int applicantId)
        {
            var applicant = await _context.Applicants
                .Include(a => a.Family)
                .Include(a => a.Dependents)
                .Include(a => a.Documents)
                .FirstOrDefaultAsync(a => a.ApplicantId == applicantId);

            var positions = await _context.Positions.Where(p => !p.IsArchived).ToListAsync();
            
            if (applicant == null) return NotFound();
            
            
            UpdateApplicantForm model = new UpdateApplicantForm();
            
            model.Applicant = applicant;
            model.Family = applicant.Family;
            model.Dependents = new List<Dependent>(applicant.Dependents);
            model.Documents = new List<Document>(applicant.Documents);
            model.Positions = new List<Position>(positions);
            model.ApplicantId = applicantId;

            return View(model);
        }
        
        [HttpGet]
        public async Task<IActionResult> CreateApplicant()
        {
            CreateApplicantForm model = new CreateApplicantForm();
            var dts = await _context.DocumentTypes.Where(dt => !dt.IsArchived).ToListAsync();
            var pos = await _context.Positions.Where(p => !p.IsArchived).ToListAsync();
            model.Positions = new List<Position>(pos);
            model.DocumentTypes = new List<DocumentType>(dts);
            return View(model);
        }
        
        
           
        [HttpPost]
        public async Task<IActionResult> CreateApplicant(CreateApplicantForm model)
        {
            // Create applicant from model

            if (ModelState.IsValid)
            {
                var position =
                    await _context.Positions.FirstOrDefaultAsync(po => po.PositionId == model.Applicant.PositionId);
                
                
                var family = new Family()
                {
                    FathersSuffix = model.Family.FathersSuffix,
                    FathersFirstName = model.Family.FathersFirstName,
                    FathersLastName = model.Family.FathersLastName,
                    FathersMiddleName = model.Family.FathersMiddleName,
                    MothersSuffix = model.Family.MothersSuffix,
                    MothersFirstName = model.Family.MothersFirstName,
                    MothersLastName = model.Family.MothersLastName,
                    MothersMiddleName = model.Family.MothersMiddleName,
                    SpouseSuffix = model.Family.SpouseSuffix,
                    SpouseFirstName = model.Family.SpouseFirstName,
                    SpouseLastName = model.Family.SpouseLastName,
                    SpouseMiddleName = model.Family.SpouseMiddleName,
                    NumberOfChildren = model.Family.NumberOfChildren
                };
                
                var applicant = new Applicant()
                {
                    FirstName = model.Applicant.FirstName,
                    LastName = model.Applicant.LastName,
                    MiddleName = model.Applicant.MiddleName,
                    Suffix = model.Applicant.Suffix,
                    CivilStatus = model.Applicant.CivilStatus,
                    Address = model.Applicant.Address,
                    Age = model.Applicant.Age,
                    Religion = model.Applicant.Religion,
                    Gender = model.Applicant.Gender,
                    Cellphone = model.Applicant.Cellphone,
                    Citizenship = model.Applicant.Citizenship,
                    Height = model.Applicant.Height,
                    Weight = model.Applicant.Weight,
                    DateOfBirth = model.Applicant.DateOfBirth,
                    PlaceOfBirth = model.Applicant.PlaceOfBirth,
                    Telephone = model.Applicant.Telephone,
                    IsActive = true,
                    Family = family,
                    LastSchoolAttended = model.Applicant.LastSchoolAttended,
                    SchoolFrom = model.Applicant.SchoolFrom,
                    SchoolTo = model.Applicant.SchoolTo,
                    Dependents = model.Dependents,
                    Documents = model.Documents,
                    Position = position
                };

                if (model.ApplicantPhoto != null)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await model.ApplicantPhoto.CopyToAsync(memoryStream);
                        applicant.Photo = memoryStream.ToArray();
                    }
                }
                else
                {
                    applicant.Photo = DataBootstrapper.defaultAvatar;
                }

                applicant.DateCreated = DateTime.Now;
                applicant.Status = Status.Active;
                Console.WriteLine(applicant);
                _context.Applicants.Add(applicant);
                var res = await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Success));
            }
           
            CreateApplicantForm m = new CreateApplicantForm();

            var pos = await _context.Positions.Where(p => !p.IsArchived).ToListAsync();
            var dts = await _context.DocumentTypes.Where(dt => !dt.IsArchived).ToListAsync();
            m.Positions = new List<Position>(pos);
            m.DocumentTypes = new List<DocumentType>(dts);
            return View(m);
        }
        
        
        [HttpPost]
        public async Task<IActionResult> UpdateApplicant(UpdateApplicantForm model)
        {
            // Create applicant from model

            if (ModelState.IsValid)
            {
                var position =
                    await _context.Positions.FirstOrDefaultAsync(po => po.PositionId == model.Applicant.PositionId);


                var applicantToUpdate =
                    await _context.Applicants.FirstOrDefaultAsync(a => a.ApplicantId == model.ApplicantId);
                
                var family = new Family()
                {
                    FathersSuffix = model.Family.FathersSuffix,
                    FathersFirstName = model.Family.FathersFirstName,
                    FathersLastName = model.Family.FathersLastName,
                    FathersMiddleName = model.Family.FathersMiddleName,
                    MothersSuffix = model.Family.MothersSuffix,
                    MothersFirstName = model.Family.MothersFirstName,
                    MothersLastName = model.Family.MothersLastName,
                    MothersMiddleName = model.Family.MothersMiddleName,
                    SpouseSuffix = model.Family.SpouseSuffix,
                    SpouseFirstName = model.Family.SpouseFirstName,
                    SpouseLastName = model.Family.SpouseLastName,
                    SpouseMiddleName = model.Family.SpouseMiddleName,
                    NumberOfChildren = model.Family.NumberOfChildren
                };

                if (applicantToUpdate != null)
                {
                    applicantToUpdate.FirstName = model.Applicant.FirstName;
                    applicantToUpdate.LastName = model.Applicant.LastName;
                    applicantToUpdate.MiddleName = model.Applicant.MiddleName;
                    applicantToUpdate.Suffix = model.Applicant.Suffix;
                    applicantToUpdate.CivilStatus = model.Applicant.CivilStatus;
                    applicantToUpdate.Address = model.Applicant.Address;
                    applicantToUpdate.Age = model.Applicant.Age;
                    applicantToUpdate.Religion = model.Applicant.Religion;
                    applicantToUpdate.Gender = model.Applicant.Gender;
                    applicantToUpdate.Cellphone = model.Applicant.Cellphone;
                    applicantToUpdate.Citizenship = model.Applicant.Citizenship;
                    applicantToUpdate.Height = model.Applicant.Height;
                    applicantToUpdate.Weight = model.Applicant.Weight;
                    applicantToUpdate.DateOfBirth = model.Applicant.DateOfBirth;
                    applicantToUpdate.PlaceOfBirth = model.Applicant.PlaceOfBirth;
                    applicantToUpdate.Telephone = model.Applicant.Telephone;
                    applicantToUpdate.IsActive = true;
                    applicantToUpdate.Family = family;
                    applicantToUpdate.LastSchoolAttended = model.Applicant.LastSchoolAttended;
                    applicantToUpdate.SchoolFrom = model.Applicant.SchoolFrom;
                    applicantToUpdate.SchoolTo = model.Applicant.SchoolTo;
                    applicantToUpdate.Dependents = model.Dependents;
                    applicantToUpdate.Documents = model.Documents;
                    applicantToUpdate.Position = position;
                    
                    if (model.ApplicantPhoto != null)
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            await model.ApplicantPhoto.CopyToAsync(memoryStream);
                            applicantToUpdate.Photo = memoryStream.ToArray();
                        }
                    }
                   
                    
                    applicantToUpdate.DateUpdated = DateTime.Now;
                    applicantToUpdate.Status = Status.Active;
                    _context.Entry(applicantToUpdate).State = EntityState.Modified;
                }

                 var res = await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Success));
            }
           
            UpdateApplicantForm m = new UpdateApplicantForm();

            var pos = await _context.Positions.Where(p => !p.IsArchived).ToListAsync();
            var dts = await _context.DocumentTypes.Where(dt => !dt.IsArchived).ToListAsync();
            m.Positions = new List<Position>(pos);
            m.DocumentTypes = new List<DocumentType>(dts);
            m.ApplicantId = model.ApplicantId;
            return View(m);
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Archived()
        {
            return View();
        }

        public IActionResult DocumentTypes()
        {
            return View();
        }

        public IActionResult Success()
        {
            return View();
        }
        
        public IActionResult Positions()
        {
            return View();
        }

        public IActionResult Documents()
        {
            return View();
        }

        public IActionResult Skills()
        {
            return View();
        }
    }
}