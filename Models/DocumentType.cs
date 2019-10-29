using System;
using System.ComponentModel.DataAnnotations;

namespace swmc.Models
{
    public class DocumentType
    {
        public int DocumentTypeId { get; set; }
        [Required]
        public string DocumentTypeName { get; set; }
        [Required]
        public string Issuer { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public bool IsArchived { get; set; }
    }
    
}