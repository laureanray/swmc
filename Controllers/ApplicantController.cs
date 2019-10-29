using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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

        [HttpGet]
        public async Task<IActionResult> CreateApplicant()
        {
            CreateApplicantForm model = new CreateApplicantForm();

            var pos = await _context.Positions.Where(p => !p.IsArchived).ToListAsync();
            model.Positions = new List<Position>(pos);
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
                    MothersSuffix = model.Family.MothersSuffix,
                    MothersFirstName = model.Family.MothersFirstName,
                    MothersLastName = model.Family.MothersLastName,
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
                    Beneficiaries = model.Beneficiaries,
                    Dependents = model.Dependents,
                    Allottees = model.Allottees,
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

                applicant.DateCreated = DateTime.Now;
                applicant.Status = Status.Active;
                Console.WriteLine(applicant);
                _context.Applicants.Add(applicant);
                var res = await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Success));
            }
           
            CreateApplicantForm m = new CreateApplicantForm();

            var pos = await _context.Positions.Where(p => !p.IsArchived).ToListAsync();
            m.Positions = new List<Position>(pos);
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
    }
}