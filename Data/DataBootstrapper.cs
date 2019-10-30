using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using swmc.Models;

namespace swmc.Data
{
    public class DataBootstrapper
    {
        public static byte[] defaultAvatar;
        public static Dictionary<string, string> RoleDetails = new Dictionary<string, string>();

        public static async Task Initialize(ApplicationDbContext context, UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager,
            IHostingEnvironment hostingEnvironment)
        {
            context.Database.EnsureCreated();
            
            // Create default admin if not already in the database

            defaultAvatar =
                System.IO.File.ReadAllBytes(hostingEnvironment.ContentRootPath + "/wwwroot/images/avatar.png");
            
            
            // Add the roles in the dictionary
            RoleDetails.Add("Admin", "Admin Details");
            RoleDetails.Add("HR", "HR Details");
            RoleDetails.Add("Operations", "Operations Details");
            RoleDetails.Add("Principal", "Principal Details");
            // Iterate through the array then save and check
            foreach (var role in RoleDetails)
            {
                if (await roleManager.FindByNameAsync(role.Key) == null)
                {
                    await roleManager.CreateAsync(new ApplicationRole(role.Key, role.Value, DateTime.Now));
                }
            }

            var admin = new ApplicationUser()
            {
                FirstName = "Administrator",
                LastName = "Root",
                Email = "admin@swmc.com",
                UserName = "admin",
                EmailConfirmed = true,
                DateCreated =  DateTime.Now
            };
            
            var hr = new ApplicationUser()
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@swmc.com",
                UserName = "john",
                EmailConfirmed = true,
                DateCreated =  DateTime.Now
            };

            var operations = new ApplicationUser()
            {
                FirstName = "Mark",
                LastName = "Alvarez",
                Email = "mark.alvarez@swmc.com",
                UserName = "markalvarez",
                EmailConfirmed = true,
                DateCreated =  DateTime.Now
            };

            var principal = new ApplicationUser()
            {
                FirstName = "Juan",
                LastName = "Dela Cruz",
                Email = "juan.dc@swmc.com",
                UserName = "juan",
                EmailConfirmed = true,
                DateCreated =  DateTime.Now
            };

            if (await userManager.FindByEmailAsync(admin.Email) == null)
            {
                var result = await userManager.CreateAsync(admin);

                if (result.Succeeded)
                {
                    await userManager.AddPasswordAsync(admin, "P@$$w0rd");
                    await userManager.AddToRoleAsync(admin, "Admin");
                }
            }
            
            if (await userManager.FindByEmailAsync(hr.Email) == null)
            {
                var result = await userManager.CreateAsync(hr);

                if (result.Succeeded)
                {
                    await userManager.AddPasswordAsync(hr, "P@$$w0rd");
                    await userManager.AddToRoleAsync(hr, "HR");
                }
            }
            
            if (await userManager.FindByEmailAsync(operations.Email) == null)
            {
                var result = await userManager.CreateAsync(operations);

                if (result.Succeeded)
                {
                    await userManager.AddPasswordAsync(operations, "P@$$w0rd");
                    await userManager.AddToRoleAsync(operations, "Operations");
                }
            }
            
            if (await userManager.FindByEmailAsync(principal.Email) == null)
            {
                var result = await userManager.CreateAsync(principal);

                if (result.Succeeded)
                {
                    await userManager.AddPasswordAsync(principal, "P@$$w0rd");
                    await userManager.AddToRoleAsync(principal, "Principal");
                }
            }

            var documentTypes = new List<DocumentType>()
            {
                new DocumentType()
                {
                    DocumentTypeName = "Birth Certificate",
                    Issuer = "NSO"
                },
                new DocumentType()
                {
                    DocumentTypeName = "Passport",
                    Issuer = "DFA"
                },
                new DocumentType()
                {
                    DocumentTypeName = "BST",
                    Issuer = "IDK"
                },
                new DocumentType()
                {
                    DocumentTypeName = "Seaman's Book",
                    Issuer = "IDK"
                },
                new DocumentType()
                {
                    DocumentTypeName = "Seafarers Registration Certificate",
                    Issuer = "IDK"
                },
                new DocumentType()
                {
                    DocumentTypeName = "Visa",
                    Issuer = "DFA"
                },
                new DocumentType()
                {
                    DocumentTypeName = "Medical Certificate",
                    Issuer = "NSO"
                }
            };

            if (!context.DocumentTypes.Any())
            {
                foreach (var dt in documentTypes)
                {
                    dt.DateCreated = DateTime.Now;
                    context.DocumentTypes.Add(dt);
                }

                await context.SaveChangesAsync();
            }


            var positions = new List<Position>()
            {
                new Position()
                {
                    PositionName = "Captain",
                    PositionCode = "CT"

                },
                new Position()
                {
                    PositionName = "Deck Officer",
                    PositionCode = "DC"
                },
                new Position()
                {
                    PositionName = "Engineering Officer",
                    PositionCode = "EO"
                },
                new Position()
                {
                    PositionName = "Electro-Technical",
                    PositionCode = "ET"
                },
                new Position()
                {
                    PositionName = "Cadet",
                    PositionCode = "CD"
                },
                new Position()
                {
                    PositionName = "Chief Cook",
                    PositionCode = "CC"
                }
            };

            if (!context.Positions.Any())
            {
                foreach(var po in positions)
                {
                    po.DateCreated = DateTime.Now;

                    context.Positions.Add(po);
                }

                await context.SaveChangesAsync();
            }

        }
    }
}