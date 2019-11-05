using System;
using System.Collections.Generic;

namespace swmc.Models
{
    public class  Requirement
    {
        public int RequirementId { get; set; }
        public List<Skill> Skills { get; set; }
        public int PositionId { get; set; }
        public Position Position { get; set; }
        public int Quantity { get; set; }
    }
}