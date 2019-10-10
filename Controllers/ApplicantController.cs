using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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
        public IActionResult CreateApplicant()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateApplicant(CreateApplicantForm model)
        {
            // Create applicant from model

            if (ModelState.IsValid)
            {
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
                    IsActive = true
                };

                var res = await _context.Applicants.AddAsync(applicant);
                
            }
            else
            {
                return View();
            }
         
            Console.WriteLine(model.Applicant.FirstName);
            return View();
        }
    }
}