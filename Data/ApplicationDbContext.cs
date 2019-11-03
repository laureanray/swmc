using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using swmc.Models;

namespace swmc.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Applicant> Applicants { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<Family> Families { get; set; }
        public DbSet<Vessel> Vessels { get; set; }
        public DbSet<DocumentType> DocumentTypes { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Skill> Skills { get; set;  }
        public DbSet<Request> Requests { get; set; }
        public DbSet<Requirement> Requirements { get; set; }
    }
}
