using System;
using System.Collections.Generic;
using swmc.Models;

namespace swmc.ViewModels
{
    public class ApplicantViewModel
    {
        public List<Applicant> Applicants { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
    }
}