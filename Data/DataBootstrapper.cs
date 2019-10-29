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