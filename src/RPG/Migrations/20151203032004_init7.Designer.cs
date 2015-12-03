using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations;
using RPG.Models;

namespace RPG.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20151203032004_init7")]
    partial class init7
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

            modelBuilder.Entity("RPG.Lib.Schema.SchemaDialogOption", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("Message");

                    b.Property<string>("Name");

                    b.Property<string>("Option1Text");

                    b.Property<int?>("Option1TriggerId");

                    b.Property<string>("Option2Text");

                    b.Property<int?>("Option2TriggerId");

                    b.Property<string>("ReferenceName");

                    b.HasKey("Id");
                });

            modelBuilder.Entity("RPG.Lib.Schema.SchemaLocation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.Property<string>("ReferenceName");

                    b.Property<int?>("RegionId");

                    b.HasKey("Id");
                });

            modelBuilder.Entity("RPG.Lib.Schema.SchemaQuest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("DiscoveryTeaser");

                    b.Property<int?>("EndingQuestStateId");

                    b.Property<string>("Name");

                    b.Property<string>("ReferenceName");

                    b.Property<int?>("StartingQuestStateId");

                    b.HasKey("Id");
                });

            modelBuilder.Entity("RPG.Lib.Schema.SchemaQuestState", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("BeginDialogOptionId");

                    b.Property<int?>("CompleteDialogOptionId");

                    b.Property<string>("CompletedNextQuestState");

                    b.Property<string>("Description");

                    b.Property<int?>("FailDialogOptionId");

                    b.Property<string>("FailedNextQuestState");

                    b.Property<string>("Name");

                    b.Property<int>("QuestProgress");

                    b.Property<string>("ReferenceName");

                    b.Property<int?>("SchemaQuestId");

                    b.HasKey("Id");
                });

            modelBuilder.Entity("RPG.Lib.Schema.SchemaRegion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.Property<string>("ReferenceName");

                    b.HasKey("Id");
                });

            modelBuilder.Entity("RPG.Lib.Schema.SchemaTrigger", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.Property<string>("ReferenceName");

                    b.Property<bool>("Repeatable");

                    b.Property<int?>("SchemaDialogOptionId");

                    b.Property<int?>("SchemaLocationId");

                    b.Property<int?>("SchemaQuestId");

                    b.Property<int?>("SchemaQuestStateId");

                    b.Property<int?>("SchemaRegionId");

                    b.Property<int?>("SchemaTriggerId");

                    b.Property<string>("TargetReferenceName");

                    b.Property<string>("TargetValue");

                    b.Property<int>("TriggerAction");

                    b.Property<int>("TriggerMethod");

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

            modelBuilder.Entity("RPG.Models.Character", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Level");

                    b.Property<int?>("LocationId");

                    b.Property<string>("Name");

                    b.Property<string>("PublicId");

                    b.Property<int?>("RegionId");

                    b.Property<string>("UserId");

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

            modelBuilder.Entity("RPG.Models.DialogOption", b =>
                {
                    b.Property<string>("Id");

                    b.Property<bool>("Active");

                    b.Property<int?>("CharacterId");

                    b.Property<string>("Message");

                    b.Property<int>("OptionChosen");

                    b.Property<int?>("SchemaDialogOptionId");

                    b.HasKey("Id");
                });

            modelBuilder.Entity("RPG.Models.Quest", b =>
                {
                    b.Property<string>("Id");

                    b.Property<bool>("Active");

                    b.Property<string>("CharacterId");

                    b.Property<bool>("Complete");

                    b.Property<bool>("IsCurrentQuest");

                    b.Property<int?>("SchemaQuestId");

                    b.Property<bool>("ShowInQuestLog");

                    b.HasKey("Id");
                });

            modelBuilder.Entity("RPG.Models.QuestState", b =>
                {
                    b.Property<string>("Id");

                    b.Property<bool>("Active");

                    b.Property<string>("CharacterId");

                    b.Property<string>("QuestId");

                    b.HasKey("Id");
                });

            modelBuilder.Entity("RPG.Models.SystemData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("MasterSchema");

                    b.HasKey("Id");
                });

            modelBuilder.Entity("RPG.Models.Trigger", b =>
                {
                    b.Property<string>("Id");

                    b.Property<int?>("CharacterId");

                    b.Property<int?>("SchemaTriggerId");

                    b.Property<bool>("Triggered");

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

            modelBuilder.Entity("RPG.Lib.Schema.SchemaDialogOption", b =>
                {
                    b.HasOne("RPG.Lib.Schema.SchemaTrigger")
                        .WithMany()
                        .HasForeignKey("Option1TriggerId");

                    b.HasOne("RPG.Lib.Schema.SchemaTrigger")
                        .WithMany()
                        .HasForeignKey("Option2TriggerId");
                });

            modelBuilder.Entity("RPG.Lib.Schema.SchemaLocation", b =>
                {
                    b.HasOne("RPG.Lib.Schema.SchemaRegion")
                        .WithMany()
                        .HasForeignKey("RegionId");
                });

            modelBuilder.Entity("RPG.Lib.Schema.SchemaQuest", b =>
                {
                    b.HasOne("RPG.Lib.Schema.SchemaQuestState")
                        .WithMany()
                        .HasForeignKey("EndingQuestStateId");

                    b.HasOne("RPG.Lib.Schema.SchemaQuestState")
                        .WithMany()
                        .HasForeignKey("StartingQuestStateId");
                });

            modelBuilder.Entity("RPG.Lib.Schema.SchemaQuestState", b =>
                {
                    b.HasOne("RPG.Lib.Schema.SchemaDialogOption")
                        .WithMany()
                        .HasForeignKey("BeginDialogOptionId");

                    b.HasOne("RPG.Lib.Schema.SchemaDialogOption")
                        .WithMany()
                        .HasForeignKey("CompleteDialogOptionId");

                    b.HasOne("RPG.Lib.Schema.SchemaDialogOption")
                        .WithMany()
                        .HasForeignKey("FailDialogOptionId");

                    b.HasOne("RPG.Lib.Schema.SchemaQuest")
                        .WithMany()
                        .HasForeignKey("SchemaQuestId");
                });

            modelBuilder.Entity("RPG.Lib.Schema.SchemaTrigger", b =>
                {
                    b.HasOne("RPG.Lib.Schema.SchemaDialogOption")
                        .WithMany()
                        .HasForeignKey("SchemaDialogOptionId");

                    b.HasOne("RPG.Lib.Schema.SchemaLocation")
                        .WithMany()
                        .HasForeignKey("SchemaLocationId");

                    b.HasOne("RPG.Lib.Schema.SchemaQuest")
                        .WithMany()
                        .HasForeignKey("SchemaQuestId");

                    b.HasOne("RPG.Lib.Schema.SchemaQuestState")
                        .WithMany()
                        .HasForeignKey("SchemaQuestStateId");

                    b.HasOne("RPG.Lib.Schema.SchemaRegion")
                        .WithMany()
                        .HasForeignKey("SchemaRegionId");

                    b.HasOne("RPG.Lib.Schema.SchemaTrigger")
                        .WithMany()
                        .HasForeignKey("SchemaTriggerId");
                });

            modelBuilder.Entity("RPG.Models.Character", b =>
                {
                    b.HasOne("RPG.Lib.Schema.SchemaRegion")
                        .WithMany()
                        .HasForeignKey("LocationId");

                    b.HasOne("RPG.Lib.Schema.SchemaRegion")
                        .WithMany()
                        .HasForeignKey("RegionId");

                    b.HasOne("RPG.Models.ApplicationUser")
                        .WithOne()
                        .HasForeignKey("RPG.Models.Character", "UserId");
                });

            modelBuilder.Entity("RPG.Models.DialogOption", b =>
                {
                    b.HasOne("RPG.Models.Character")
                        .WithMany()
                        .HasForeignKey("CharacterId");

                    b.HasOne("RPG.Lib.Schema.SchemaDialogOption")
                        .WithMany()
                        .HasForeignKey("SchemaDialogOptionId");
                });

            modelBuilder.Entity("RPG.Models.Quest", b =>
                {
                    b.HasOne("RPG.Lib.Schema.SchemaQuest")
                        .WithMany()
                        .HasForeignKey("SchemaQuestId");
                });

            modelBuilder.Entity("RPG.Models.QuestState", b =>
                {
                    b.HasOne("RPG.Models.Quest")
                        .WithMany()
                        .HasForeignKey("QuestId");
                });

            modelBuilder.Entity("RPG.Models.Trigger", b =>
                {
                    b.HasOne("RPG.Models.Character")
                        .WithMany()
                        .HasForeignKey("CharacterId");

                    b.HasOne("RPG.Lib.Schema.SchemaTrigger")
                        .WithMany()
                        .HasForeignKey("SchemaTriggerId");
                });
        }
    }
}
