namespace swmc.Models
{
    public class Skill
    {
        public int SkillID { get; set; }
        public int SkillTypeId { get; set; }
        public SkillType SkillType { get; set; }
    }
}