using System;
using swmc.Models;

namespace swmc.ViewModels
{
    public class ApplicantReport
    {
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public Status ApplicantStatus { get; set; }
    }
}