using System.Collections.Generic;

namespace swmc.Models
{
    public enum EmbarkationStatus
    {
        Embarked,
        Disembarked
    }
    public class Embarkation
    {
        public int EmbarkationId { get; set; }
        public int RequestId { get; set; }
        public Request Request { get; set; }
        public IEnumerable<ApplicantEmbarkation> Applicants { get; set; }
        public EmbarkationStatus EmbarkationStatus { get; set; }
    }
}