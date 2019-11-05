using System.Collections.Generic;

namespace swmc.Models.JsonModel
{
    public class UpdateSkills
    {
        public int ApplicantId { get; set; }
        public List<Skill> Skills { get; set; }
    }
}