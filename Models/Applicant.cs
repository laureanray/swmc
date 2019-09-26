using System;

namespace swmc.Models
{
    public enum CivilStatus
    {
        Married,
        Single,
        Divorced,
        Widowed
    }

    public enum Gender
    {
        Male, 
        Female
    }

    public enum Position
    {
        EngineeringOfficer,
        Cadet
    }
    
    public class Applicant
    {
        public int ApplicantId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Suffix { get; set; }
        public CivilStatus CivilStatus { get; set; }
        public Gender Gender { get; set; }
        public int Age { get; set; }
        public string Address { get; set; }
        public string Religion { get; set; }
        public string Citizenship { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PlaceOfBirth { get; set; }
        public string Height { get; set; }
        public string Cellphone { get; set; }
        public string Telephone { get; set; }
        public bool IsActive { get; set; }
        public Position Position { get; set; }
        public Family Family { get; set; }
        public byte[] Photo { get; set; }
    }
}