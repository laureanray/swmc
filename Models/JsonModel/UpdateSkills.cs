using System.Collections.Generic;

namespace swmc.Models.JsonModel
{
    public class UpdateSkills
    {
        public int ApplicantId { get; set; }
        public List<SkillType> SkillTypes { get; set; }
    }
}