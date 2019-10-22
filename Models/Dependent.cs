using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace swmc.Models
{
    public class Dependent
    {
        public int DependentId { get; set; }
        public int ApplicantId { get; set; }
        public Applicant Applicant { get; set; }
        public string Name { get; set; }
        public string Relationship { get; set; }
        public int Age { get; set; }
    }
}