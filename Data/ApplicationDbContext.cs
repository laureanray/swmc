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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<ApplicantEmbarkation>()
                .HasKey(ae => new {ae.ApplicantId, ae.EmbarkationId});

            builder.Entity<ApplicantEmbarkation>()
                .HasOne(ae => ae.Applicant)
                .WithMany(ae => ae.Embarkations)
                .HasForeignKey(ae => ae.ApplicantId);
            
            builder.Entity<ApplicantEmbarkation>()
                .HasOne(ae => ae.Embarkation)
                .WithMany(ae => ae.Applicants)
                .HasForeignKey(ae => ae.EmbarkationId);
        }


        public DbSet<Applicant> Applicants { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<Family> Families { get; set; }
        public DbSet<Vessel> Vessels { get; set; }
        public DbSet<DocumentType> DocumentTypes { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<SkillType> SkillTypes { get; set; }
        public DbSet<Skill> Skills { get; set;  }
        public DbSet<Request> Requests { get; set; }
        public DbSet<Requirement> Requirements { get; set; }
        public DbSet<Embarkation> Embarkations { get; set; }
    }
}
