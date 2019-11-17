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
                System.IO.File.ReadAllBytes(hostingEnvironment.ContentRootPath + "/wwwroot/images/avatar.webp");


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
                DateCreated = DateTime.Now
            };

            var hr = new ApplicationUser()
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@swmc.com",
                UserName = "john",
                EmailConfirmed = true,
                DateCreated = DateTime.Now
            };

            var operations = new ApplicationUser()
            {
                FirstName = "Mark",
                LastName = "Alvarez",
                Email = "mark.alvarez@swmc.com",
                UserName = "markalvarez",
                EmailConfirmed = true,
                DateCreated = DateTime.Now
            };

            var principal = new ApplicationUser()
            {
                FirstName = "Juan",
                LastName = "Dela Cruz",
                Email = "juan.dc@swmc.com",
                UserName = "juan",
                EmailConfirmed = true,
                DateCreated = DateTime.Now
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

            var skillTypes = new List<SkillType>()
            {
                new SkillType()
                {
                    SkillName = "Firefighting",
                },
                new SkillType()
                {
                    SkillName = "Electronics"
                },
                new SkillType()
                {
                    SkillName = "Mechanical"
                },
                new SkillType()
                {
                    SkillName = "Telecommunications"
                }
            };

            if (!context.SkillTypes.Any())
            {
                foreach (var skillType in skillTypes)
                {
                    context.SkillTypes.Add(skillType);
                }

                await context.SaveChangesAsync();
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
                foreach (var po in positions)
                {
                    po.DateCreated = DateTime.Now;

                    context.Positions.Add(po);
                }

                await context.SaveChangesAsync();
            }


            var vessels = new List<Vessel>()
            {
                new Vessel()
                {
                    VesselName = "GRAND URANUS",
                    Principal = "CIDO SHIPPING (KOREA) CO., LTD",
                    Flag = "PANAMA",
                    GrossTonnage = "72654",
                    JSU = "0",
                    EngineMake = "HYUNDAI MAN B&W",
                    PortRegistry = "PANAMA",
                    OfficialNumber = "43497",
                    CBA = "IBF FKSU",
                    IMONumber = "9472206",
                    VesselAbr = "GRUR",
                    HorsePower = "19040",
                    Classification = "DNV",
                    Type = "PCC",
                    YearEnrolled = "2012",
                    YearBuilt = "2012"
                },
                new Vessel()
                {
                    VesselName = "DREAM ANGEL",
                    Principal = "CIDO SHIPPING (KOREA) CO., LTD",
                    Flag = "PANAMA",
                    GrossTonnage = "41678",
                    JSU = "40",
                    EngineMake = "MITSUI MAN B&W",
                    PortRegistry = "PANAMA",
                    OfficialNumber = "31564",
                    CBA = "IBF FKSU",
                    IMONumber = "41678",
                    VesselAbr = "GRUR",
                    HorsePower = "19040",
                    Classification = "DNV",
                    Type = "PCC",
                    YearEnrolled = "2012",
                    YearBuilt = "2012"
                }
            };

            if (!context.Vessels.Any())
            {
                foreach (var vessel in vessels)
                {
                    vessel.DateAdded = DateTime.Now;
                    context.Vessels.Add(vessel);
                }

                await context.SaveChangesAsync();
            }

            Applicant applicant = new Applicant();
            Applicant applicant2 = new Applicant();
            Applicant applicant3 = new Applicant();
            Applicant applicant4 = new Applicant();
            Applicant applicant5 = new Applicant();
            Applicant applicant6 = new Applicant();
            Applicant applicant7 = new Applicant();
            Applicant applicant8 = new Applicant();
            Applicant applicant9 = new Applicant();
            Applicant applicant10 = new Applicant();


            if (!context.Applicants.Any())
            {
                var family = new Family()
                {
                    FathersFirstName = "Fathers",
                    FathersMiddleName = "Middle",
                    FathersLastName = "Last",
                    MothersFirstName = "Mothers",
                    MothersMiddleName = "Middle",
                    MothersLastName = "Last",
                    SpouseFirstName = "Spouse",
                    SpouseMiddleName = "Middle",
                    SpouseLastName = "Last",
                    NumberOfChildren = "12"
                };

                var family2 = new Family()
                {
                    FathersFirstName = "Fathers",
                    FathersMiddleName = "Middle",
                    FathersLastName = "Last",
                    MothersFirstName = "Mothers",
                    MothersMiddleName = "Middle",
                    MothersLastName = "Last",
                    SpouseFirstName = "Spouse",
                    SpouseMiddleName = "Middle",
                    SpouseLastName = "Last",
                    NumberOfChildren = "12"
                };

                var family3 = new Family()
                {
                    FathersFirstName = "Fathers",
                    FathersMiddleName = "Middle",
                    FathersLastName = "Last",
                    MothersFirstName = "Mothers",
                    MothersMiddleName = "Middle",
                    MothersLastName = "Last",
                    SpouseFirstName = "Spouse",
                    SpouseMiddleName = "Middle",
                    SpouseLastName = "Last",
                    NumberOfChildren = "12"
                };

                var family4 = new Family()
                {
                    FathersFirstName = "Fathers",
                    FathersMiddleName = "Middle",
                    FathersLastName = "Last",
                    MothersFirstName = "Mothers",
                    MothersMiddleName = "Middle",
                    MothersLastName = "Last",
                    SpouseFirstName = "Spouse",
                    SpouseMiddleName = "Middle",
                    SpouseLastName = "Last",
                    NumberOfChildren = "12"
                };

                var family5 = new Family()
                {
                    FathersFirstName = "Fathers",
                    FathersMiddleName = "Middle",
                    FathersLastName = "Last",
                    MothersFirstName = "Mothers",
                    MothersMiddleName = "Middle",
                    MothersLastName = "Last",
                    SpouseFirstName = "Spouse",
                    SpouseMiddleName = "Middle",
                    SpouseLastName = "Last",
                    NumberOfChildren = "12"
                };

                var family6 = new Family()
                {
                    FathersFirstName = "Fathers",
                    FathersMiddleName = "Middle",
                    FathersLastName = "Last",
                    MothersFirstName = "Mothers",
                    MothersMiddleName = "Middle",
                    MothersLastName = "Last",
                    SpouseFirstName = "Spouse",
                    SpouseMiddleName = "Middle",
                    SpouseLastName = "Last",
                    NumberOfChildren = "12"
                };

                
                var family7 = new Family()
                {
                    FathersFirstName   = "Fathers",
                    FathersMiddleName = "Middle",
                    FathersLastName = "Last",
                    MothersFirstName = "Mothers",
                    MothersMiddleName = "Middle",
                    MothersLastName = "Last",
                    SpouseFirstName = "Spouse",
                    SpouseMiddleName = "Middle",
                    SpouseLastName = "Last",
                    NumberOfChildren = "12"
                };
                
                var family8 = new Family()
                {
                    FathersFirstName   = "Fathers",
                    FathersMiddleName = "Middle",
                    FathersLastName = "Last",
                    MothersFirstName = "Mothers",
                    MothersMiddleName = "Middle",
                    MothersLastName = "Last",
                    SpouseFirstName = "Spouse",
                    SpouseMiddleName = "Middle",
                    SpouseLastName = "Last",
                    NumberOfChildren = "12"
                };

                
                var family9 = new Family()
                {
                    FathersFirstName   = "Fathers",
                    FathersMiddleName = "Middle",
                    FathersLastName = "Last",
                    MothersFirstName = "Mothers",
                    MothersMiddleName = "Middle",
                    MothersLastName = "Last",
                    SpouseFirstName = "Spouse",
                    SpouseMiddleName = "Middle",
                    SpouseLastName = "Last",
                    NumberOfChildren = "12"
                };

                
                var family10 = new Family()
                {
                    FathersFirstName   = "Fathers",
                    FathersMiddleName = "Middle",
                    FathersLastName = "Last",
                    MothersFirstName = "Mothers",
                    MothersMiddleName = "Middle",
                    MothersLastName = "Last",
                    SpouseFirstName = "Spouse",
                    SpouseMiddleName = "Middle",
                    SpouseLastName = "Last",
                    NumberOfChildren = "12"
                };


                var dependents = new List<Dependent>()
                {
                    new Dependent()
                    {
                        Age = 19,
                        Name = "Maria Luna",
                        Relationship = "Wife"
                    },
                    new Dependent()
                    {
                        Age = 5,
                        Name = "Johnny Name",
                        Relationship = "Son"
                    }
                };

                var dependents2 = new List<Dependent>()
                {
                    new Dependent()
                    {
                        Age = 19,
                        Name = "Maria Luna",
                        Relationship = "Wife"
                    },
                    new Dependent()
                    {
                        Age = 5,
                        Name = "Johnny Name",
                        Relationship = "Son"
                    }
                };


                var documents = new List<Document>()
                {
                    new Document()
                    {
                        DocumentType = documentTypes[0],
                        DateExpiry = new DateTime(2017, 2, 2),
                        DateSubmitted = new DateTime(2016, 6, 2)
                    },
                    new Document()
                    {
                        DocumentType = documentTypes[1],
                        DateExpiry = new DateTime(2018, 2, 2),
                        DateSubmitted = new DateTime(2016, 6, 2)
                    },
                    new Document()
                    {
                        DocumentType = documentTypes[2],
                        DateExpiry = new DateTime(2018, 2, 2),
                        DateSubmitted = new DateTime(2016, 6, 2)
                    }
                };


                var documents2 = new List<Document>()
                {
                    new Document()
                    {
                        DocumentType = documentTypes[0],
                        DateExpiry = new DateTime(2017, 2, 2),
                        DateSubmitted = new DateTime(2016, 6, 2)
                    },
                    new Document()
                    {
                        DocumentType = documentTypes[1],
                        DateExpiry = new DateTime(2018, 2, 2),
                        DateSubmitted = new DateTime(2016, 6, 2)
                    },
                    new Document()
                    {
                        DocumentType = documentTypes[2],
                        DateExpiry = new DateTime(2018, 2, 2),
                        DateSubmitted = new DateTime(2016, 6, 2)
                    }
                };


                applicant = new Applicant()
                {
                    Address = "123 Address St., Manila, NCR 1016",
                    Age = 32,
                    Documents = new List<Document>(documents2),
                    Dependents = new List<Dependent>(dependents2),
                    Skills = new List<Skill>()
                    {
                        new Skill()
                        {
                            SkillType = skillTypes[0]
                        },
                        new Skill()
                        {
                            SkillType = skillTypes[1]
                        },
                        new Skill()
                        {
                            SkillType = skillTypes[2]
                        },
                        new Skill()
                        {
                            SkillType = skillTypes[3]
                        }
                    },
                    Cellphone = "09123456789",
                    Citizenship = "Filipino",
                    Family = family,
                    Gender = "Male",
                    Position = positions[0],
                    Height = 23,
                    Weight = 42,
                    Religion = "Christian",
                    Telephone = "1231231",
                    Status = Status.Active,
                    Suffix = "Jr",
                    SchoolFrom = "2005",
                    SchoolTo = "2010",
                    LastName = "Test",
                    FirstName = "Juan",
                    MiddleName = "Test",
                    DateOfBirth = new DateTime(2001, 2, 1),
                    PlaceOfBirth = "Quezon City",
                    CivilStatus = "Married",
                    DateCreated = DateTime.Now,
                    LastSchoolAttended = "School",
                    PositionId = 1,
                    Photo = defaultAvatar
                };

                applicant2 = new Applicant()
                {
                    Address = "123 Address St., Manila, NCR 1016",
                    Age = 32,
                    Documents = new List<Document>(documents),
                    Dependents = new List<Dependent>(dependents),
                    Skills = new List<Skill>()
                    {
                        new Skill()
                        {
                            SkillType = skillTypes[0]
                        },
                        new Skill()
                        {
                            SkillType = skillTypes[1]
                        },
                        new Skill()
                        {
                            SkillType = skillTypes[2]
                        },
                        new Skill()
                        {
                            SkillType = skillTypes[3]
                        }
                    },
                    Cellphone = "09123456789",
                    Citizenship = "Filipino",
                    Family = family2,
                    Gender = "Male",
                    Position = positions[2],
                    Height = 32,
                    Weight = 50,
                    Religion = "Roman Catholic",
                    Telephone = "1231231",
                    Status = Status.Active,
                    Suffix = "",
                    SchoolFrom = "2005",
                    SchoolTo = "2012",
                    LastName = "Cruiser",
                    FirstName = "Peter",
                    MiddleName = "David",
                    DateOfBirth = new DateTime(2001, 2, 1),
                    PlaceOfBirth = "Manila City",
                    CivilStatus = "Single",
                    DateCreated = DateTime.Now,
                    LastSchoolAttended = "School",
                    PositionId = 1,
                    Photo = defaultAvatar
                };

                

                applicant3 = new Applicant()
                {
                    Address = "123 Address St., Manila, NCR 1016",
                    Age = 32,
                    Documents = new List<Document>(documents),
                    Dependents = new List<Dependent>(dependents),
                    Skills = new List<Skill>()
                    {
                        new Skill()
                        {
                            SkillType = skillTypes[0]
                        },
                        new Skill()
                        {
                            SkillType = skillTypes[1]
                        },
                        new Skill()
                        {
                            SkillType = skillTypes[2]
                        },
                        new Skill()
                        {
                            SkillType = skillTypes[3]
                        }
                    },
                    Cellphone = "09123456789",
                    Citizenship = "Filipino",
                    Family = family3,
                    Gender = "Male",
                    Position = positions[2],
                    Height = 32,
                    Weight = 50,
                    Religion = "Roman Catholic",
                    Telephone = "1231231",
                    Status = Status.Active,
                    Suffix = "",
                    SchoolFrom = "2005",
                    SchoolTo = "2012",
                    LastName = "Cruiser",
                    FirstName = "Peter",
                    MiddleName = "David",
                    DateOfBirth = new DateTime(2001, 2, 1),
                    PlaceOfBirth = "Manila City",
                    CivilStatus = "Single",
                    DateCreated = DateTime.Now,
                    LastSchoolAttended = "School",
                    PositionId = 1,
                    Photo = defaultAvatar
                };


                applicant4 = new Applicant()
                {
                    Address = "123 Address St., Manila, NCR 1016",
                    Age = 32,
                    Documents = new List<Document>(documents),
                    Dependents = new List<Dependent>(dependents),
                    Skills = new List<Skill>()
                    {
                        new Skill()
                        {
                            SkillType = skillTypes[0]
                        },
                        new Skill()
                        {
                            SkillType = skillTypes[1]
                        },
                        new Skill()
                        {
                            SkillType = skillTypes[2]
                        },
                        new Skill()
                        {
                            SkillType = skillTypes[3]
                        }
                    },
                    Cellphone = "09123456789",
                    Citizenship = "Filipino",
                    Family = family4,
                    Gender = "Male",
                    Position = positions[2],
                    Height = 32,
                    Weight = 50,
                    Religion = "Roman Catholic",
                    Telephone = "1231231",
                    Status = Status.Active,
                    Suffix = "",
                    SchoolFrom = "2005",
                    SchoolTo = "2012",
                    LastName = "Cruiser",
                    FirstName = "Peter",
                    MiddleName = "David",
                    DateOfBirth = new DateTime(2001, 2, 1),
                    PlaceOfBirth = "Manila City",
                    CivilStatus = "Single",
                    DateCreated = DateTime.Now,
                    LastSchoolAttended = "School",
                    PositionId = 1,
                    Photo = defaultAvatar
                };


                applicant5 = new Applicant()
                {
                    Address = "123 Address St., Manila, NCR 1016",
                    Age = 32,
                    Documents = new List<Document>(documents),
                    Dependents = new List<Dependent>(dependents),
                    Skills = new List<Skill>()
                    {
                        new Skill()
                        {
                            SkillType = skillTypes[0]
                        },
                        new Skill()
                        {
                            SkillType = skillTypes[1]
                        },
                        new Skill()
                        {
                            SkillType = skillTypes[2]
                        },
                        new Skill()
                        {
                            SkillType = skillTypes[3]
                        }
                    },
                    Cellphone = "09123456789",
                    Citizenship = "Filipino",
                    Family = family5,
                    Gender = "Male",
                    Position = positions[2],
                    Height = 32,
                    Weight = 50,
                    Religion = "Roman Catholic",
                    Telephone = "1231231",
                    Status = Status.Active,
                    Suffix = "",
                    SchoolFrom = "2005",
                    SchoolTo = "2012",
                    LastName = "Cruiser",
                    FirstName = "Peter",
                    MiddleName = "David",
                    DateOfBirth = new DateTime(2001, 2, 1),
                    PlaceOfBirth = "Manila City",
                    CivilStatus = "Single",
                    DateCreated = DateTime.Now,
                    LastSchoolAttended = "School",
                    PositionId = 1,
                    Photo = defaultAvatar
                };

                applicant6 = new Applicant()
                {
                    Address = "123 Address St., Manila, NCR 1016",
                    Age = 32,
                    Documents = new List<Document>(documents),
                    Dependents = new List<Dependent>(dependents),
                    Skills = new List<Skill>()
                    {
                        new Skill()
                        {
                            SkillType = skillTypes[0]
                        },
                        new Skill()
                        {
                            SkillType = skillTypes[1]
                        },
                        new Skill()
                        {
                            SkillType = skillTypes[2]
                        },
                        new Skill()
                        {
                            SkillType = skillTypes[3]
                        }
                    },
                    Cellphone = "09123456789",
                    Citizenship = "Filipino",
                    Family = family6,
                    Gender = "Male",
                    Position = positions[2],
                    Height = 32,
                    Weight = 50,
                    Religion = "Roman Catholic",
                    Telephone = "1231231",
                    Status = Status.Active,
                    Suffix = "",
                    SchoolFrom = "2005",
                    SchoolTo = "2012",
                    LastName = "Cruiser",
                    FirstName = "Peter",
                    MiddleName = "David",
                    DateOfBirth = new DateTime(2001, 2, 1),
                    PlaceOfBirth = "Manila City",
                    CivilStatus = "Single",
                    DateCreated = DateTime.Now,
                    LastSchoolAttended = "School",
                    PositionId = 1,
                    Photo = defaultAvatar
                };

                applicant7 = new Applicant()
                {
                    Address = "123 Address St., Manila, NCR 1016",
                    Age = 32,
                    Documents = new List<Document>(documents),
                    Dependents = new List<Dependent>(dependents),
                    Skills = new List<Skill>()
                    {
                        new Skill()
                        {
                            SkillType = skillTypes[0]
                        },
                        new Skill()
                        {
                            SkillType = skillTypes[1]
                        },
                        new Skill()
                        {
                            SkillType = skillTypes[2]
                        },
                        new Skill()
                        {
                            SkillType = skillTypes[3]
                        }
                    },
                    Cellphone = "09123456789",
                    Citizenship = "Filipino",
                    Family = family7,
                    Gender = "Male",
                    Position = positions[2],
                    Height = 32,
                    Weight = 50,
                    Religion = "Roman Catholic",
                    Telephone = "1231231",
                    Status = Status.Active,
                    Suffix = "",
                    SchoolFrom = "2005",
                    SchoolTo = "2012",
                    LastName = "Cruiser",
                    FirstName = "Peter",
                    MiddleName = "David",
                    DateOfBirth = new DateTime(2001, 2, 1),
                    PlaceOfBirth = "Manila City",
                    CivilStatus = "Single",
                    DateCreated = DateTime.Now,
                    LastSchoolAttended = "School",
                    PositionId = 1,
                    Photo = defaultAvatar
                };

                applicant8 = new Applicant()
                {
                    Address = "123 Address St., Manila, NCR 1016",
                    Age = 32,
                    Documents = new List<Document>(documents),
                    Dependents = new List<Dependent>(dependents),
                    Skills = new List<Skill>()
                    {
                        new Skill()
                        {
                            SkillType = skillTypes[0]
                        },
                        new Skill()
                        {
                            SkillType = skillTypes[1]
                        },
                        new Skill()
                        {
                            SkillType = skillTypes[2]
                        },
                        new Skill()
                        {
                            SkillType = skillTypes[3]
                        }
                    },
                    Cellphone = "09123456789",
                    Citizenship = "Filipino",
                    Family = family8,
                    Gender = "Male",
                    Position = positions[2],
                    Height = 32,
                    Weight = 50,
                    Religion = "Roman Catholic",
                    Telephone = "1231231",
                    Status = Status.Active,
                    Suffix = "",
                    SchoolFrom = "2005",
                    SchoolTo = "2012",
                    LastName = "Cruiser",
                    FirstName = "Peter",
                    MiddleName = "David",
                    DateOfBirth = new DateTime(2001, 2, 1),
                    PlaceOfBirth = "Manila City",
                    CivilStatus = "Single",
                    DateCreated = DateTime.Now,
                    LastSchoolAttended = "School",
                    PositionId = 1,
                    Photo = defaultAvatar
                };

                applicant9 = new Applicant()
                {
                    Address = "123 Address St., Manila, NCR 1016",
                    Age = 32,
                    Documents = new List<Document>(documents),
                    Dependents = new List<Dependent>(dependents),
                    Skills = new List<Skill>()
                    {
                        new Skill()
                        {
                            SkillType = skillTypes[0]
                        },
                        new Skill()
                        {
                            SkillType = skillTypes[1]
                        },
                        new Skill()
                        {
                            SkillType = skillTypes[2]
                        },
                        new Skill()
                        {
                            SkillType = skillTypes[3]
                        }
                    },
                    Cellphone = "09123456789",
                    Citizenship = "Filipino",
                    Family = family9,
                    Gender = "Male",
                    Position = positions[2],
                    Height = 32,
                    Weight = 50,
                    Religion = "Roman Catholic",
                    Telephone = "1231231",
                    Status = Status.Active,
                    Suffix = "",
                    SchoolFrom = "2005",
                    SchoolTo = "2012",
                    LastName = "Cruiser",
                    FirstName = "Peter",
                    MiddleName = "David",
                    DateOfBirth = new DateTime(2001, 2, 1),
                    PlaceOfBirth = "Manila City",
                    CivilStatus = "Single",
                    DateCreated = DateTime.Now,
                    LastSchoolAttended = "School",
                    PositionId = 1,
                    Photo = defaultAvatar
                };


                applicant10 = new Applicant()
                {
                    Address = "123 Address St., Manila, NCR 1016",
                    Age = 32,
                    Documents = new List<Document>(documents),
                    Dependents = new List<Dependent>(dependents),
                    Skills = new List<Skill>()
                    {
                        new Skill()
                        {
                            SkillType = skillTypes[0]
                        },
                        new Skill()
                        {
                            SkillType = skillTypes[1]
                        },
                        new Skill()
                        {
                            SkillType = skillTypes[2]
                        },
                        new Skill()
                        {
                            SkillType = skillTypes[3]
                        }
                    },
                    Cellphone = "09123456789",
                    Citizenship = "Filipino",
                    Family = family10,
                    Gender = "Male",
                    Position = positions[2],
                    Height = 32,
                    Weight = 50,
                    Religion = "Roman Catholic",
                    Telephone = "1231231",
                    Status = Status.Active,
                    Suffix = "",
                    SchoolFrom = "2005",
                    SchoolTo = "2012",
                    LastName = "Cruiser",
                    FirstName = "Peter",
                    MiddleName = "David",
                    DateOfBirth = new DateTime(2001, 2, 1),
                    PlaceOfBirth = "Manila City",
                    CivilStatus = "Single",
                    DateCreated = DateTime.Now,
                    LastSchoolAttended = "School",
                    PositionId = 1,
                    Photo = defaultAvatar
                };

                context.Applicants.Add(applicant);
                context.Applicants.Add(applicant2);
                context.Applicants.Add(applicant3);
                context.Applicants.Add(applicant4);
                context.Applicants.Add(applicant5);
                context.Applicants.Add(applicant6);
                context.Applicants.Add(applicant7);
                context.Applicants.Add(applicant8);
                context.Applicants.Add(applicant9);
                context.Applicants.Add(applicant10);

                await context.SaveChangesAsync();
            }

            var requirements = new List<Requirement>()
            {
                new Requirement()
                {
                    Position = positions[0],
                    Quantity = 2,
                    Skills = new List<Skill>()
                    {
                        new Skill()
                        {
                            SkillType = skillTypes[0]
                        },
                        new Skill()
                        {
                            SkillType = skillTypes[1]
                        },
                        new Skill()
                        {
                            SkillType = skillTypes[2]
                        },
                        new Skill()
                        {
                            SkillType = skillTypes[3]
                        }
                    }
                },
                new Requirement()
                {
                    Position = positions[1],
                    Quantity = 10,
                    Skills = new List<Skill>()
                    {
                        new Skill()
                        {
                            SkillType = skillTypes[0]
                        },
                        new Skill()
                        {
                            SkillType = skillTypes[1]
                        },
                        new Skill()
                        {
                            SkillType = skillTypes[2]
                        },
                        new Skill()
                        {
                            SkillType = skillTypes[3]
                        }
                    }
                },
                new Requirement()
                {
                    Position = positions[2],
                    Quantity = 5,
                    Skills = new List<Skill>()
                    {
                        new Skill()
                        {
                            SkillType = skillTypes[0]
                        },
                        new Skill()
                        {
                            SkillType = skillTypes[1]
                        }
                    }
                },
                new Requirement()
                {
                    Position = positions[3],
                    Quantity = 4,
                    Skills = new List<Skill>()
                    {
                        new Skill()
                        {
                            SkillType = skillTypes[0]
                        },
                        new Skill()
                        {
                            SkillType = skillTypes[1]
                        },
                        new Skill()
                        {
                            SkillType = skillTypes[2]
                        }
                    }
                },
                new Requirement()
                {
                    Position = positions[4],
                    Quantity = 6,
                    Skills = new List<Skill>()
                    {
                        new Skill()
                        {
                            SkillType = skillTypes[0]
                        },
                        new Skill()
                        {
                            SkillType = skillTypes[1]
                        },
                        new Skill()
                        {
                            SkillType = skillTypes[2]
                        },
                        new Skill()
                        {
                            SkillType = skillTypes[3]
                        }
                    }
                },
                new Requirement()
                {
                    Position = positions[5],
                    Quantity = 2,
                    Skills = new List<Skill>()
                    {
                        new Skill()
                        {
                            SkillType = skillTypes[0]
                        }
                    }
                }
            };

            var request = new Request()
            {
                Vessel = vessels[0],
                Destination = "South Korea",
                Remarks = "Remarks",
                Requirements = new List<Requirement>(requirements),
                StartDate = new DateTime(2014, 2, 5),
                EndDate = new DateTime(2014, 5, 5),
                DateCreated = DateTime.Now,
                ApplicationUser = principal
            };


            if (!context.Requests.Any())
            {
                context.Requests.Add(request);
                await context.SaveChangesAsync();
            }


            var embarkation = new Embarkation()
            {
                Request = request,
                EmbarkationStatus = EmbarkationStatus.Embarked,
                Applicants = new List<ApplicantEmbarkation>()
                {
                    new ApplicantEmbarkation()
                    {
                        Applicant = applicant
                    }
                }
            };

            if (!context.Embarkations.Any())
            {
                context.Embarkations.Add(embarkation);
                await context.SaveChangesAsync();
            }
        }
    }
}