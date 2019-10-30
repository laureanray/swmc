using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using swmc.Data;
using swmc.Models;

namespace swmc.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public UserController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [Route("GetUsers")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            var users = await _context.Users.Where(u => !u.IsArchived).ToListAsync();
            var usersToReturn = new List<User>();
            
            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                var u = new User()
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Role = roles[0],
                    DateCreated = user.DateCreated,
                    DateUpdated = user.DateCreated,
                    IsArchived = user.IsArchived,
                    Email = user.Email
                };

                usersToReturn.Add(u);
            }

            return usersToReturn;
        }
        
        [Route("GetArchivedUsers")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ApplicationUser>>> GetArchivedUsers()
        {
            var users = await _context.Users.Where(u => u.IsArchived).ToListAsync();

            return users;
        }

//        [Route("AddUser")]
//        [HttpPost]
//        public async Task<ActionResult<JsonResponse>> AddUser(ApplicationUser user)
//        {
//            var result = await _userManager.CreateAsync(user);
//            if (result.Succeeded)
//            {
//                
//            }
//        } 
    }
}