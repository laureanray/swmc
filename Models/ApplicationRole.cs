using System;
using Microsoft.AspNetCore.Identity;

namespace swmc.Models
{
    public class ApplicationRole : IdentityRole
    {
        public ApplicationRole() : base()
        {
            
        }

        public ApplicationRole(string roleName, string description, DateTime dateCreated) : base(roleName)
        {
            Description = description;
            DateCreated = dateCreated;
        }

        public string Description { get; set; }
        public DateTime DateCreated { get; set; }
    }
}