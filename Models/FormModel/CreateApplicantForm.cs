using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace swmc.Models.FormModel
{
    public class CreateApplicantForm
    {
        public IFormFile ApplicantPhoto { get; set; }
        public Applicant Applicant { get; set; }
        public Family Family { get; set; }
        public List<Beneficiary> Beneficiaries { get; set; }
        public List<Dependent> Dependents { get; set; }
        public List<Document> Documents { get; set; }
        public List<Allottee> Allottees { get; set; }
    }
}