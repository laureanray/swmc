using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using swmc.Data;
using swmc.Models;
using swmc.Models.FormModel;

namespace swmc.Controllers
{
        
    public class VesselController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VesselController(ApplicationDbContext context)
        {
            _context = context;
        }


        public IActionResult Index()
        {
            return View();
        }

      
        public IActionResult Archived()
        {
            return View();
        }
    }
}