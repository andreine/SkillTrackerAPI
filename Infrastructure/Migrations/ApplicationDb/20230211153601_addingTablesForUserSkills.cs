using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations.ApplicationDb
{
    public partial class addingTablesForUserSkills : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Questions_QuestionCategorys_QuestionCategoryId",
                table: "Questions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_QuestionCategorys",
                table: "QuestionCategorys");

            migrationBuilder.RenameTable(
                name: "QuestionCategorys",
                newName: "QuestionCategories");

            migrationBuilder.AddPrimaryKey(
                name: "PK_QuestionCategories",
                table: "QuestionCategories",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "SessionSkillActivities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    IsCompleted = table.Column<int>(type: "int", nullable: false)
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
                    UserId = table.Column<int>(type: "int", nullable: false),
                    IsCorrect = table.Column<int>(type: "int", nullable: false),
                    QuestionId = table.Column<int>(type: "int", nullable: false),
                    SessionSkillActivityId = table.Column<int>(type: "int", nullable: false)
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
                    table.ForeignKey(
                        name: "FK_SessionSkillActivityQuestions_SessionSkillActivities_SessionSkillActivityId",
                        column: x => x.SessionSkillActivityId,
                        principalTable: "SessionSkillActivities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SessionSkillActivityQuestions_QuestionId",
                table: "SessionSkillActivityQuestions",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_SessionSkillActivityQuestions_SessionSkillActivityId",
                table: "SessionSkillActivityQuestions",
                column: "SessionSkillActivityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_QuestionCategories_QuestionCategoryId",
                table: "Questions",
                column: "QuestionCategoryId",
                principalTable: "QuestionCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Questions_QuestionCategories_QuestionCategoryId",
                table: "Questions");

            migrationBuilder.DropTable(
                name: "SessionSkillActivityQuestions");

            migrationBuilder.DropTable(
                name: "SessionSkillActivities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_QuestionCategories",
                table: "QuestionCategories");

            migrationBuilder.RenameTable(
                name: "QuestionCategories",
                newName: "QuestionCategorys");

            migrationBuilder.AddPrimaryKey(
                name: "PK_QuestionCategorys",
                table: "QuestionCategorys",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_QuestionCategorys_QuestionCategoryId",
                table: "Questions",
                column: "QuestionCategoryId",
                principalTable: "QuestionCategorys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
