using System.Collections.Generic;

namespace swmc.Models
{
    public class ApplicantEmbarkation
    {
        public int ApplicantId { get; set; }
        public Applicant Applicant { get; set; }
        public int EmbarkationId { get; set; }
        public Embarkation Embarkation { get; set; }
    }
}