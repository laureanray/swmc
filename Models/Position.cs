﻿using System;
using System.ComponentModel.DataAnnotations;

namespace swmc.Models
{
    public class Position
    {
        public int PositionId { get; set; }
        [Required] 
        public string PositionName { get; set; }
        public string PositionCode { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public bool IsArchived { get; set; }
    }    
}