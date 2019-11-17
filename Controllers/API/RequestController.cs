using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http;
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
        private readonly UserManager<ApplicationUser> _userManager;
        public RequestController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [Route("GetRequests")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Request>>> GetVessels()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var roles = await _userManager.GetRolesAsync(user);

            if (roles.Any(role => role.Equals("Admin")))
            {
                return await _context.Requests.Where(r => !r.IsArchived)
                    .Include(r => r.Requirements)
                    .ThenInclude(r => r.Skills)
                    .ThenInclude(r => r.SkillType)
                    .Include(r => r.Requirements)
                    .ThenInclude(r => r.Position)
                    .Include(r => r.Vessel).ToListAsync();
            }
                
           
            
            return await _context.Requests.Where(r => !r.IsArchived && r.ApplicationUser.Id.Equals(user.Id))
                .Include(r => r.Requirements)
                .ThenInclude(r => r.Skills)
                .ThenInclude(r => r.SkillType)
                .Include(r => r.Requirements)
                .ThenInclude(r => r.Position)
                .Include(r => r.Vessel).ToListAsync();
        }
        
        [Route("GetArchivedRequests")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Request>>> GetArchivedResults()
        {
            return await _context.Requests.Where(r => r.IsArchived).Include(r => r.Requirements).Include(r => r.Vessel)
                .ToListAsync();
        }
        
        
        [Route("AddRequest")]
        [HttpPost]
        public async Task<ActionResult<JsonResponse>> AddRequest(Request request)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            request.DateCreated = DateTime.Now;
            request.ApplicationUser = user;
            _context.Requests.Add(request);
            var res = await _context.SaveChangesAsync();            
            return new JsonResponse()
            {
                Message = "Success"
            };
        }

        [Route("ArchiveRequest")]
        [HttpGet]
        public async Task<ActionResult<JsonResponse>> ArchiveRequest(int requestId)
        {
            var request = await _context.Requests.FirstOrDefaultAsync(r => r.RequestId == requestId);

            if (request == null)
            {
                return NotFound();
            }

            request.IsArchived = true;

            _context.Entry(request).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return new JsonResponse()
            {
                Message =  "Success"
            };

        }

        
        
        [Route("RestoreRequest")]
        [HttpGet]
        public async Task<ActionResult<JsonResponse>> RestoreRequest(int requestId)
        {
            var request = await _context.Requests.FirstOrDefaultAsync(r => r.RequestId == requestId);

            if (request == null)
            {
                return NotFound();
            }

            request.IsArchived = false;

            _context.Entry(request).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return new JsonResponse()
            {
                Message =  "Success"
            };

        }
    }
}