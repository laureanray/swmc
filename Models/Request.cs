﻿using System;
using System.Collections.Generic;

namespace swmc.Models
{
    
    public class Request
    {
        public int RequestId { get; set; }
        public int VesselId { get; set; }
        public Vessel Vessel { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Destination { get; set; }
        public string Remarks { get; set; }
        public List<Requirement> Requirements { get; set; }
        public bool IsArchived { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}    