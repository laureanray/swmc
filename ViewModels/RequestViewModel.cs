using System;
using System.Collections.Generic;
using swmc.Models;

namespace swmc.ViewModels
{
    public class RequestViewModel
    {
        public List<Request> Requests { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
    }
}