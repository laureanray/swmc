using System.ComponentModel.DataAnnotations;

namespace swmc.Models
{
    public class Family
    {
        public int FamilyId { get; set; }
        public int ApplicantId { get; set; }
        [Required]
        public string SpouseFirstName { get; set; }
        [Required]
        public string SpouseLastName { get; set; }
        [Required]
        public string SpouseMiddleName { get; set; }
        public string SpouseSuffix { get; set; }
        public string NumberOfChildren { get; set; }
        [Required]
        public string FathersFirstName { get; set; }
        [Required]
        public string FathersMiddleName { get; set; }
        [Required]
        public string FathersLastName { get; set; }
        public string FathersSuffix { get; set; }
        [Required]
        public string MothersFirstName { get; set; }
        [Required]
        public string MothersMiddleName { get; set; }
        [Required]
        public string MothersLastName { get; set; }
        public string MothersSuffix { get; set; }
    }
}