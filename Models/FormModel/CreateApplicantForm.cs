using System.Collections.Generic;

namespace swmc.Models.FormModel
{
    public class CreateApplicantForm
    {
        public Applicant Applicant { get; set; }
        public Family Family { get; set; }
        public List<Beneficiary> Beneficiaries { get; set; }
    }
}