using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using swmc.Data;
using swmc.Models;

namespace swmc.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public RequestController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Route("GetRequests")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Request>>> GetVessels()
        {
            return await _context.Requests.Where(r => !r.IsArchived).Include(r => r.Requirements).Include(r => r.Vessel).ToListAsync();
        }

        [Route("AddRequest")]
        [HttpPost]
        public async Task<ActionResult<JsonResponse>> AddRequest(Request request)
        {
            _context.Requests.Add(request);
            await _context.Requests.AddAsync(request);

            return new JsonResponse()
            {
                Message = "Success"
            };
        }

    }
}