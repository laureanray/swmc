using System;
using System.Collections.Generic;

namespace swmc.Models
{
    public class Request
    {
        public int RequestId { get; set; }
        public int VesselId { get; set; }
        public Vessel Vessel { get; set; }
        public DateTime OperationStartDate { get; set; }
        public DateTime OperationEndDate { get; set; }
        public string Destination { get; set; }
        public string Remarks { get; set; }
        public List<Requirement> Requirements { get; set; }
        public bool IsArchived { get; set; }
    }
}