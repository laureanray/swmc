using System;
using System.ComponentModel.DataAnnotations;

namespace swmc.Models
{
    public class Document
    {
        public int DocumentId { get; set; }
        public int ApplicantId { get; set; }
        [Required]
        public int DocumentTypeId { get; set; }
        public DocumentType DocumentType { get; set; }
        public DateTime DateSubmitted { get; set; }
        public DateTime DateExpiry { get; set; }
    }
}