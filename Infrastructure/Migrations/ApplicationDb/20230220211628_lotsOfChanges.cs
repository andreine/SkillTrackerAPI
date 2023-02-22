using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations.ApplicationDb
{
    public partial class lotsOfChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Questions_SessionSkillActivities_SessionSkillActivityId",
                table: "Questions");

            migrationBuilder.DropTable(
                name: "SessionSkillActivities");

            migrationBuilder.DropTable(
                name: "SessionSkillActivityQuestions");

            migrationBuilder.RenameColumn(
                name: "SessionSkillActivityId",
                table: "Questions",
                newName: "SessionId");

            migrationBuilder.RenameIndex(
                name: "IX_Questions_SessionSkillActivityId",
                table: "Questions",
                newName: "IX_Questions_SessionId");

            migrationBuilder.CreateTable(
                name: "Sessions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sessions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserSessionQuestions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsCorrect = table.Column<int>(type: "int", nullable: false),
                    QuestionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSessionQuestions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserSessionQuestions_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserSessions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsCompleted = table.Column<int>(type: "int", nullable: false),
                    ActivationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SessionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSessions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserSessions_Sessions_SessionId",
                        column: x => x.SessionId,
                        principalTable: "Sessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserSessionQuestions_QuestionId",
                table: "UserSessionQuestions",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSessions_SessionId",
                table: "UserSessions",
                column: "SessionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_Sessions_SessionId",
                table: "Questions",
                column: "SessionId",
                principalTable: "Sessions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Sessions_SessionId",
                table: "Questions");

            migrationBuilder.DropTable(
                name: "UserSessionQuestions");

            migrationBuilder.DropTable(
                name: "UserSessions");

            migrationBuilder.DropTable(
                name: "Sessions");

            migrationBuilder.RenameColumn(
                name: "SessionId",
                table: "Questions",
                newName: "SessionSkillActivityId");

            migrationBuilder.RenameIndex(
                name: "IX_Questions_SessionId",
                table: "Questions",
                newName: "IX_Questions_SessionSkillActivityId");

            migrationBuilder.CreateTable(
                name: "SessionSkillActivities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActivityDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsCompleted = table.Column<int>(type: "int", nullable: false),
                    LastCompleted = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SessionSkillActivities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SessionSkillActivityQuestions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuestionId = table.Column<int>(type: "int", nullable: false),
                    IsCorrect = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SessionSkillActivityQuestions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SessionSkillActivityQuestions_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SessionSkillActivityQuestions_QuestionId",
                table: "SessionSkillActivityQuestions",
                column: "QuestionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_SessionSkillActivities_SessionSkillActivityId",
                table: "Questions",
                column: "SessionSkillActivityId",
                principalTable: "SessionSkillActivities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
