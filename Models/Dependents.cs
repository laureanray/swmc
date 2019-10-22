using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace swmc.Models
{
    public class Dependents
    {
        public int DependentsId { get; set; }
        public string Name { get; set; }
        public string Relationship { get; set; }
        public int Age { get; set; }
    }
}