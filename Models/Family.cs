namespace swmc.Models
{
    public class Family
    {
        public int FamilyId { get; set; }
        public int ApplicantId { get; set; }
        public string SpouseFirstName { get; set; }
        public string SpouseLastName { get; set; }
        public string SpouseMiddleName { get; set; }
        public string SpouseSuffix { get; set; }
        public int NumberOfChildren { get; set; }
        public string FathersFirstName { get; set; }
        public string FathersMiddleName { get; set; }
        public string FathersLastName { get; set; }
        public string FathersSuffix { get; set; }
        public string MothersFirstName { get; set; }
        public string MothersMiddleName { get; set; }
        public string MothersLastName { get; set; }
        public string MothersSuffix { get; set; }
    }
}