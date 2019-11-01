using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace swmc.Models
{
    public enum Status
    {
        Active,
        Embarked,
        Archived
    }
    public class Applicant
    {
        
        
        public int ApplicantId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string MiddleName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string Suffix { get; set; }
        [Required]
        public string CivilStatus { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        public int Age { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string Religion { get; set; }
        [Required]
        public string Citizenship { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        [Required]
        public string PlaceOfBirth { get; set; }
        [Required]
        public int Height { get; set; }
        [Required]
        public int Weight { get; set; }
        [Required]
        public string Cellphone { get; set; }
        [Required]
        public string Telephone { get; set; }
        [Required]
        public bool IsActive { get; set; }
        [Required]
        public int PositionId { get; set; }
        public Position Position { get; set; }
        [Required]
        public string LastSchoolAttended { get; set; }
        [Required]
        public string SchoolFrom { get; set; }
        [Required]
        public string SchoolTo { get; set; }
        public Status Status { get; set; }
        public Family Family { get; set; }
        public byte[] Photo { get; set; }
        public List<Document> Documents { get; set; }
//        public List<Beneficiary> Beneficiaries { get; set; }
//        public List<Allottee> Allottees { get; set; }
        public List<Dependent> Dependents { get; set; }
        public List<Skill> Skills { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
    }
}