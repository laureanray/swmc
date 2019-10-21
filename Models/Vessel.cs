using System;
using System.ComponentModel.DataAnnotations;

namespace swmc.Models
{
    public class Vessel
    {
        public int VesselId { get; set; }
        [Required]
        public string VesselName { get; set; }
        [Required]
        public string Principal { get; set; }
        [Required]
        public string Flag { get; set; }
        [Required]
        public string GrossTonnage { get; set; }
        [Required]
        public string JSU { get; set; }
        [Required]
        public string EngineMake { get; set; }
        public string PortRegistry { get; set; }
        public string OfficialNumber { get; set; }
        public string CBA { get; set; }
        public string IMONumber { get; set; }
        public string VesselAbr { get; set; }
        public string Status { get; set; }
        public string HorsePower { get; set; }
        public string Classification { get; set; }
        public string Type { get; set; }
        public string YearBuilt{ get; set; }
        public DateTime DateEnrolled { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime DateModified { get; set; }
        public bool IsArchived { get; set; }
    }    
}