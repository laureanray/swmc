using System;
using System.Collections.Generic;
using swmc.Models;

namespace swmc.ViewModels
{
    public class EmbarkationViewModel
    {
        public List<Embarkation> Embarkations { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
    }
}