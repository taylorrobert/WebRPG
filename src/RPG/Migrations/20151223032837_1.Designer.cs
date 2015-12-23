using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations;
using RPG.Models;

namespace RPG.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20151223032837_1")]
    partial class _1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0-rc1-16348")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityRole", b =>
                {
                    b.Property<string>("Id");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("NormalizedName")
                        .HasAnnotation("MaxLength", 256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .HasAnnotation("Relational:Name", "RoleNameIndex");

                    b.HasAnnotation("Relational:TableName", "AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasAnnotation("Relational:TableName", "AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasAnnotation("Relational:TableName", "AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasAnnotation("Relational:TableName", "AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasAnnotation("Relational:TableName", "AspNetUserRoles");
                });

            modelBuilder.Entity("RPG.Models.ActiveResearchNode", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Active");

                    b.Property<int?>("CorporationId");

                    b.Property<int>("RDInvested");

                    b.Property<int?>("ResearchNodeId");

                    b.HasKey("Id");
                });

            modelBuilder.Entity("RPG.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id");

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("IsAdmin");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("NormalizedUserName")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasAnnotation("MaxLength", 256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasAnnotation("Relational:Name", "EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .HasAnnotation("Relational:Name", "UserNameIndex");

                    b.HasAnnotation("Relational:TableName", "AspNetUsers");
                });

            modelBuilder.Entity("RPG.Models.Corporation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<double>("BusinessMultiplier");

                    b.Property<long>("Cash");

                    b.Property<string>("Name");

                    b.Property<int>("PublicInterest");

                    b.Property<int>("RD");

                    b.Property<int>("Readiness");

                    b.Property<int>("Reputation");

                    b.Property<long>("TurnCount");

                    b.Property<int>("TurnsRemaining");

                    b.Property<string>("UserId");

                    b.HasKey("Id");
                });

            modelBuilder.Entity("RPG.Models.CorporationPerson", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("CorporationId");

                    b.Property<bool>("Hired");

                    b.Property<int?>("PersonId");

                    b.HasKey("Id");
                });

            modelBuilder.Entity("RPG.Models.Dev", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Author");

                    b.Property<string>("ProductName");

                    b.Property<string>("Version");

                    b.HasKey("Id");
                });

            modelBuilder.Entity("RPG.Models.LogMessage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("CorporationId");

                    b.Property<string>("Message");

                    b.Property<DateTime>("TimeStamp");

                    b.Property<long>("TurnCount");

                    b.HasKey("Id");
                });

            modelBuilder.Entity("RPG.Models.Person", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Business");

                    b.Property<bool>("Celebrity");

                    b.Property<string>("Description");

                    b.Property<int>("Experience");

                    b.Property<int>("Fitness");

                    b.Property<int>("Intelligence");

                    b.Property<string>("Name");

                    b.Property<string>("Position");

                    b.Property<int>("SeverancePayout");

                    b.Property<int>("SigningBonus");

                    b.Property<int>("TurnSalary");

                    b.HasKey("Id");
                });

            modelBuilder.Entity("RPG.Models.SchemaModels.LearnedResearchNode", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("CorporationId");

                    b.Property<int?>("ResearchNodeId");

                    b.HasKey("Id");
                });

            modelBuilder.Entity("RPG.Models.SchemaModels.ResearchNode", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("CashCost");

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.Property<int>("RDCost");

                    b.Property<string>("Script");

                    b.HasKey("Id");
                });

            modelBuilder.Entity("RPG.Models.SystemData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("MasterSchema");

                    b.HasKey("Id");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNet.Identity.EntityFramework.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("RPG.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("RPG.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNet.Identity.EntityFramework.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId");

                    b.HasOne("RPG.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("RPG.Models.ActiveResearchNode", b =>
                {
                    b.HasOne("RPG.Models.Corporation")
                        .WithMany()
                        .HasForeignKey("CorporationId");

                    b.HasOne("RPG.Models.SchemaModels.ResearchNode")
                        .WithMany()
                        .HasForeignKey("ResearchNodeId");
                });

            modelBuilder.Entity("RPG.Models.Corporation", b =>
                {
                    b.HasOne("RPG.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("RPG.Models.CorporationPerson", b =>
                {
                    b.HasOne("RPG.Models.Corporation")
                        .WithMany()
                        .HasForeignKey("CorporationId");

                    b.HasOne("RPG.Models.Person")
                        .WithMany()
                        .HasForeignKey("PersonId");
                });

            modelBuilder.Entity("RPG.Models.LogMessage", b =>
                {
                    b.HasOne("RPG.Models.Corporation")
                        .WithMany()
                        .HasForeignKey("CorporationId");
                });

            modelBuilder.Entity("RPG.Models.SchemaModels.LearnedResearchNode", b =>
                {
                    b.HasOne("RPG.Models.Corporation")
                        .WithMany()
                        .HasForeignKey("CorporationId");

                    b.HasOne("RPG.Models.SchemaModels.ResearchNode")
                        .WithMany()
                        .HasForeignKey("ResearchNodeId");
                });
        }
    }
}
