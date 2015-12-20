using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Data.Entity;
using RPG.Models.SchemaModels;

namespace RPG.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Dev> Dev { get; set; }
        public DbSet<SystemData> SystemData { get; set; }
        public DbSet<Corporation> Corporations { get; set; } 
        public DbSet<ResearchNode> ResearchNodes { get; set; }
        public DbSet<LearnedResearchNode> LearnedResearchNodes { get; set; } 
        public DbSet<ActiveResearchNode> ActiveResearchNodes { get; set; } 
        public DbSet<LogMessage> LogMessages { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}
