using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace swmc.Models.FormModel
{
    public class UpdateApplicantForm
    {
        public IFormFile ApplicantPhoto { get; set; }
        public Applicant Applicant { get; set; }
        public Family Family { get; set; }
        public List<Beneficiary> Beneficiaries { get; set; }
        public List<Dependent> Dependents { get; set; }
        public List<Document> Documents { get; set; }
        public List<DocumentType> DocumentTypes { get; set; }
        public List<Position> Positions { get; set; }
    }
}