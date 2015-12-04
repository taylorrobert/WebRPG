using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Data.Entity;
using RPG.Lib.Schema;
using RPG.ViewModels.Action;

namespace RPG.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Dev> Dev { get; set; }
        public DbSet<Character> Characters { get; set; }
        public DbSet<SystemData> SystemData { get; set; }
        public DbSet<Trigger> Triggers { get; set; }
        public DbSet<DialogOption> DialogOptions { get; set; }
        public DbSet<Quest> Quests { get; set; }


        public DbSet<SchemaRegion> SchemaRegions { get; set; }
        public DbSet<SchemaLocation> SchemaLocations { get; set; }
        public DbSet<SchemaQuest> SchemaQuests { get; set; }
        public DbSet<SchemaQuestState> SchemaQuestStates { get; set; }
        public DbSet<SchemaDialogOption> SchemaDialogOptions { get; set; }
        public DbSet<SchemaTrigger> SchemaTriggers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

        public DbSet<ActionModel> ActionModel { get; set; }
    }
}
