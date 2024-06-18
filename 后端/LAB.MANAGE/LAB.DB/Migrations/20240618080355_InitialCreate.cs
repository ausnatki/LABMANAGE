using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LAB.DB.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Academy",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDel = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Academy", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDel = table.Column<bool>(type: "bit", nullable: false),
                    RoleName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Semesters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDel = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Semesters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SysUser",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LoginName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sex = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SysUser", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Laboratories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDel = table.Column<bool>(type: "bit", nullable: false),
                    AcademyId = table.Column<int>(type: "int", nullable: false),
                    LabNumber = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Laboratories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Laboratories_Academy_AcademyId",
                        column: x => x.AcademyId,
                        principalTable: "Academy",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RID = table.Column<int>(type: "int", nullable: false),
                    UID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RID",
                        column: x => x.RID,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_SysUser_UID",
                        column: x => x.UID,
                        principalTable: "SysUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LabAssignments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LabID = table.Column<int>(type: "int", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LabAssignments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LabAssignments_Laboratories_LabID",
                        column: x => x.LabID,
                        principalTable: "Laboratories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LabAssignments_SysUser_UserID",
                        column: x => x.UserID,
                        principalTable: "SysUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TB_DailySafetyCheck",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LabID = table.Column<int>(type: "int", nullable: false),
                    SemesterID = table.Column<int>(type: "int", nullable: false),
                    UID = table.Column<int>(type: "int", nullable: false),
                    CheckDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Cleanliness = table.Column<bool>(type: "bit", nullable: false),
                    DoorsAndWindows = table.Column<bool>(type: "bit", nullable: false),
                    ElectricalLines = table.Column<bool>(type: "bit", nullable: false),
                    FireSafetyEquipmentAvailable = table.Column<bool>(type: "bit", nullable: false),
                    FireSafetyEquipmentExpiry = table.Column<bool>(type: "bit", nullable: false),
                    InstrumentEquipmentIntact = table.Column<bool>(type: "bit", nullable: false),
                    InstrumentEquipmentWorking = table.Column<bool>(type: "bit", nullable: false),
                    InstrumentWarningLabelsIntact = table.Column<bool>(type: "bit", nullable: false),
                    OtherHazards = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OtherSafetyHazards = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDel = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_DailySafetyCheck", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TB_DailySafetyCheck_Laboratories_LabID",
                        column: x => x.LabID,
                        principalTable: "Laboratories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TB_DailySafetyCheck_Semesters_SemesterID",
                        column: x => x.SemesterID,
                        principalTable: "Semesters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TB_DailySafetyCheck_SysUser_UID",
                        column: x => x.UID,
                        principalTable: "SysUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LabEquipmentRepairs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DLabID = table.Column<int>(type: "int", nullable: false),
                    RepairDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IssuesFound = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Suggestions = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RepairPersonnelID = table.Column<int>(type: "int", nullable: false),
                    ComletionStatus = table.Column<bool>(type: "bit", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDel = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LabEquipmentRepairs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LabEquipmentRepairs_TB_DailySafetyCheck_DLabID",
                        column: x => x.DLabID,
                        principalTable: "TB_DailySafetyCheck",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LabInclidentHanding",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DLabID = table.Column<int>(type: "int", nullable: false),
                    IncidentDetails = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RepairPersonnelID = table.Column<int>(type: "int", nullable: false),
                    ReportedByID = table.Column<int>(type: "int", nullable: false),
                    InclidentTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDel = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LabInclidentHanding", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LabInclidentHanding_TB_DailySafetyCheck_DLabID",
                        column: x => x.DLabID,
                        principalTable: "TB_DailySafetyCheck",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LabAssignments_LabID",
                table: "LabAssignments",
                column: "LabID");

            migrationBuilder.CreateIndex(
                name: "IX_LabAssignments_UserID",
                table: "LabAssignments",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_LabEquipmentRepairs_DLabID",
                table: "LabEquipmentRepairs",
                column: "DLabID");

            migrationBuilder.CreateIndex(
                name: "IX_LabInclidentHanding_DLabID",
                table: "LabInclidentHanding",
                column: "DLabID");

            migrationBuilder.CreateIndex(
                name: "IX_Laboratories_AcademyId",
                table: "Laboratories",
                column: "AcademyId");

            migrationBuilder.CreateIndex(
                name: "IX_TB_DailySafetyCheck_LabID",
                table: "TB_DailySafetyCheck",
                column: "LabID");

            migrationBuilder.CreateIndex(
                name: "IX_TB_DailySafetyCheck_SemesterID",
                table: "TB_DailySafetyCheck",
                column: "SemesterID");

            migrationBuilder.CreateIndex(
                name: "IX_TB_DailySafetyCheck_UID",
                table: "TB_DailySafetyCheck",
                column: "UID");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RID",
                table: "UserRoles",
                column: "RID");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_UID",
                table: "UserRoles",
                column: "UID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LabAssignments");

            migrationBuilder.DropTable(
                name: "LabEquipmentRepairs");

            migrationBuilder.DropTable(
                name: "LabInclidentHanding");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "TB_DailySafetyCheck");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Laboratories");

            migrationBuilder.DropTable(
                name: "Semesters");

            migrationBuilder.DropTable(
                name: "SysUser");

            migrationBuilder.DropTable(
                name: "Academy");
        }
    }
}
