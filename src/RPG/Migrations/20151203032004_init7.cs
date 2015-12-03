using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;
using Microsoft.Data.Entity.Metadata;

namespace RPG.Migrations
{
    public partial class init7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    NormalizedName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityRole", x => x.Id);
                });
            migrationBuilder.CreateTable(
                name: "SchemaRegion",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    ReferenceName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchemaRegion", x => x.Id);
                });
            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    NormalizedEmail = table.Column<string>(nullable: true),
                    NormalizedUserName = table.Column<string>(nullable: true),
                    PasswordHash = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    SecurityStamp = table.Column<string>(nullable: true),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    UserName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUser", x => x.Id);
                });
            migrationBuilder.CreateTable(
                name: "Dev",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Author = table.Column<string>(nullable: true),
                    ProductName = table.Column<string>(nullable: true),
                    Version = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dev", x => x.Id);
                });
            migrationBuilder.CreateTable(
                name: "SystemData",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MasterSchema = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemData", x => x.Id);
                });
            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityRoleClaim<string>", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IdentityRoleClaim<string>_IdentityRole_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
            migrationBuilder.CreateTable(
                name: "SchemaLocation",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    ReferenceName = table.Column<string>(nullable: true),
                    RegionId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchemaLocation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SchemaLocation_SchemaRegion_RegionId",
                        column: x => x.RegionId,
                        principalTable: "SchemaRegion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });
            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityUserClaim<string>", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IdentityUserClaim<string>_ApplicationUser_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityUserLogin<string>", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_IdentityUserLogin<string>_ApplicationUser_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityUserRole<string>", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_IdentityUserRole<string>_IdentityRole_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IdentityUserRole<string>_ApplicationUser_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
            migrationBuilder.CreateTable(
                name: "Character",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Level = table.Column<int>(nullable: false),
                    LocationId = table.Column<int>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    PublicId = table.Column<string>(nullable: true),
                    RegionId = table.Column<int>(nullable: true),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Character", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Character_SchemaRegion_LocationId",
                        column: x => x.LocationId,
                        principalTable: "SchemaRegion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Character_SchemaRegion_RegionId",
                        column: x => x.RegionId,
                        principalTable: "SchemaRegion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Character_ApplicationUser_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });
            migrationBuilder.CreateTable(
                name: "SchemaDialogOption",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true),
                    Message = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Option1Text = table.Column<string>(nullable: true),
                    Option1TriggerId = table.Column<int>(nullable: true),
                    Option2Text = table.Column<string>(nullable: true),
                    Option2TriggerId = table.Column<int>(nullable: true),
                    ReferenceName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchemaDialogOption", x => x.Id);
                });
            migrationBuilder.CreateTable(
                name: "DialogOption",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    CharacterId = table.Column<int>(nullable: true),
                    Message = table.Column<string>(nullable: true),
                    OptionChosen = table.Column<int>(nullable: false),
                    SchemaDialogOptionId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DialogOption", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DialogOption_Character_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "Character",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DialogOption_SchemaDialogOption_SchemaDialogOptionId",
                        column: x => x.SchemaDialogOptionId,
                        principalTable: "SchemaDialogOption",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });
            migrationBuilder.CreateTable(
                name: "SchemaQuest",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true),
                    DiscoveryTeaser = table.Column<string>(nullable: true),
                    EndingQuestStateId = table.Column<int>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    ReferenceName = table.Column<string>(nullable: true),
                    StartingQuestStateId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchemaQuest", x => x.Id);
                });
            migrationBuilder.CreateTable(
                name: "SchemaQuestState",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BeginDialogOptionId = table.Column<int>(nullable: true),
                    CompleteDialogOptionId = table.Column<int>(nullable: true),
                    CompletedNextQuestState = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    FailDialogOptionId = table.Column<int>(nullable: true),
                    FailedNextQuestState = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    QuestProgress = table.Column<int>(nullable: false),
                    ReferenceName = table.Column<string>(nullable: true),
                    SchemaQuestId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchemaQuestState", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SchemaQuestState_SchemaDialogOption_BeginDialogOptionId",
                        column: x => x.BeginDialogOptionId,
                        principalTable: "SchemaDialogOption",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SchemaQuestState_SchemaDialogOption_CompleteDialogOptionId",
                        column: x => x.CompleteDialogOptionId,
                        principalTable: "SchemaDialogOption",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SchemaQuestState_SchemaDialogOption_FailDialogOptionId",
                        column: x => x.FailDialogOptionId,
                        principalTable: "SchemaDialogOption",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SchemaQuestState_SchemaQuest_SchemaQuestId",
                        column: x => x.SchemaQuestId,
                        principalTable: "SchemaQuest",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });
            migrationBuilder.CreateTable(
                name: "Quest",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    CharacterId = table.Column<string>(nullable: true),
                    Complete = table.Column<bool>(nullable: false),
                    IsCurrentQuest = table.Column<bool>(nullable: false),
                    SchemaQuestId = table.Column<int>(nullable: true),
                    ShowInQuestLog = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quest", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Quest_SchemaQuest_SchemaQuestId",
                        column: x => x.SchemaQuestId,
                        principalTable: "SchemaQuest",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });
            migrationBuilder.CreateTable(
                name: "SchemaTrigger",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    ReferenceName = table.Column<string>(nullable: true),
                    Repeatable = table.Column<bool>(nullable: false),
                    SchemaDialogOptionId = table.Column<int>(nullable: true),
                    SchemaLocationId = table.Column<int>(nullable: true),
                    SchemaQuestId = table.Column<int>(nullable: true),
                    SchemaQuestStateId = table.Column<int>(nullable: true),
                    SchemaRegionId = table.Column<int>(nullable: true),
                    SchemaTriggerId = table.Column<int>(nullable: true),
                    TargetReferenceName = table.Column<string>(nullable: true),
                    TargetValue = table.Column<string>(nullable: true),
                    TriggerAction = table.Column<int>(nullable: false),
                    TriggerMethod = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchemaTrigger", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SchemaTrigger_SchemaDialogOption_SchemaDialogOptionId",
                        column: x => x.SchemaDialogOptionId,
                        principalTable: "SchemaDialogOption",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SchemaTrigger_SchemaLocation_SchemaLocationId",
                        column: x => x.SchemaLocationId,
                        principalTable: "SchemaLocation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SchemaTrigger_SchemaQuest_SchemaQuestId",
                        column: x => x.SchemaQuestId,
                        principalTable: "SchemaQuest",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SchemaTrigger_SchemaQuestState_SchemaQuestStateId",
                        column: x => x.SchemaQuestStateId,
                        principalTable: "SchemaQuestState",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SchemaTrigger_SchemaRegion_SchemaRegionId",
                        column: x => x.SchemaRegionId,
                        principalTable: "SchemaRegion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SchemaTrigger_SchemaTrigger_SchemaTriggerId",
                        column: x => x.SchemaTriggerId,
                        principalTable: "SchemaTrigger",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });
            migrationBuilder.CreateTable(
                name: "QuestState",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    CharacterId = table.Column<string>(nullable: true),
                    QuestId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestState", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuestState_Quest_QuestId",
                        column: x => x.QuestId,
                        principalTable: "Quest",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });
            migrationBuilder.CreateTable(
                name: "Trigger",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CharacterId = table.Column<int>(nullable: true),
                    SchemaTriggerId = table.Column<int>(nullable: true),
                    Triggered = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trigger", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Trigger_Character_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "Character",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Trigger_SchemaTrigger_SchemaTriggerId",
                        column: x => x.SchemaTriggerId,
                        principalTable: "SchemaTrigger",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });
            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName");
            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");
            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName");
            migrationBuilder.AddForeignKey(
                name: "FK_SchemaDialogOption_SchemaTrigger_Option1TriggerId",
                table: "SchemaDialogOption",
                column: "Option1TriggerId",
                principalTable: "SchemaTrigger",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_SchemaDialogOption_SchemaTrigger_Option2TriggerId",
                table: "SchemaDialogOption",
                column: "Option2TriggerId",
                principalTable: "SchemaTrigger",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_SchemaQuest_SchemaQuestState_EndingQuestStateId",
                table: "SchemaQuest",
                column: "EndingQuestStateId",
                principalTable: "SchemaQuestState",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_SchemaQuest_SchemaQuestState_StartingQuestStateId",
                table: "SchemaQuest",
                column: "StartingQuestStateId",
                principalTable: "SchemaQuestState",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(name: "FK_SchemaQuestState_SchemaDialogOption_BeginDialogOptionId", table: "SchemaQuestState");
            migrationBuilder.DropForeignKey(name: "FK_SchemaQuestState_SchemaDialogOption_CompleteDialogOptionId", table: "SchemaQuestState");
            migrationBuilder.DropForeignKey(name: "FK_SchemaQuestState_SchemaDialogOption_FailDialogOptionId", table: "SchemaQuestState");
            migrationBuilder.DropForeignKey(name: "FK_SchemaTrigger_SchemaDialogOption_SchemaDialogOptionId", table: "SchemaTrigger");
            migrationBuilder.DropForeignKey(name: "FK_SchemaQuestState_SchemaQuest_SchemaQuestId", table: "SchemaQuestState");
            migrationBuilder.DropTable("AspNetRoleClaims");
            migrationBuilder.DropTable("AspNetUserClaims");
            migrationBuilder.DropTable("AspNetUserLogins");
            migrationBuilder.DropTable("AspNetUserRoles");
            migrationBuilder.DropTable("Dev");
            migrationBuilder.DropTable("DialogOption");
            migrationBuilder.DropTable("QuestState");
            migrationBuilder.DropTable("SystemData");
            migrationBuilder.DropTable("Trigger");
            migrationBuilder.DropTable("AspNetRoles");
            migrationBuilder.DropTable("Quest");
            migrationBuilder.DropTable("Character");
            migrationBuilder.DropTable("AspNetUsers");
            migrationBuilder.DropTable("SchemaDialogOption");
            migrationBuilder.DropTable("SchemaTrigger");
            migrationBuilder.DropTable("SchemaLocation");
            migrationBuilder.DropTable("SchemaRegion");
            migrationBuilder.DropTable("SchemaQuest");
            migrationBuilder.DropTable("SchemaQuestState");
        }
    }
}
