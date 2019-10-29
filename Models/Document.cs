using System;

namespace swmc.Models
{
    public class Document
    {
        public int DocumentId { get; set; }
        public int ApplicantId { get; set; }
        public string DocumentName { get; set; }
        public DateTime DateSubmitted { get; set; }
        public DateTime DateExpiry { get; set; }
    }
}