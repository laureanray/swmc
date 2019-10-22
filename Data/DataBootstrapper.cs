using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using swmc.Models;

namespace swmc.Data
{
    public class DataBootstrapper
    {
        public static byte[] defaultAvatar;
        public static async Task Initialize(ApplicationDbContext context, UserManager<ApplicationUser> userManager,
            IHostingEnvironment hostingEnvironment)
        {
            context.Database.EnsureCreated();
            
            // Create default admin if not already in the database

            defaultAvatar =
                System.IO.File.ReadAllBytes(hostingEnvironment.ContentRootPath + "/wwwroot/images/avatar.png");

            ApplicationUser admin = new ApplicationUser()
            {
                FirstName = "Administrator",
                LastName = "Root",
                Email = "admin@swmc.com",
                UserName = "admin",
                EmailConfirmed = true
            };

            if (await userManager.FindByEmailAsync(admin.Email) == null)
            {
                var result = await userManager.CreateAsync(admin);

                if (result.Succeeded)
                {
                    await userManager.AddPasswordAsync(admin, "P@$$w0rd");
                }
            }
            
        }
    }
}