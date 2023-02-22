using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations.ApplicationDb
{
    public partial class AddedQuestionConstrantOnActivityConstraint : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SessionSkillActivityQuestions_SessionSkillActivities_SessionSkillActivityId",
                table: "SessionSkillActivityQuestions");

            migrationBuilder.DropIndex(
                name: "IX_SessionSkillActivityQuestions_SessionSkillActivityId",
                table: "SessionSkillActivityQuestions");

            migrationBuilder.DropColumn(
                name: "SessionSkillActivityId",
                table: "SessionSkillActivityQuestions");

            migrationBuilder.RenameColumn(
                name: "SessionId",
                table: "Questions",
                newName: "SessionSkillActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_SessionSkillActivityId",
                table: "Questions",
                column: "SessionSkillActivityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_SessionSkillActivities_SessionSkillActivityId",
                table: "Questions",
                column: "SessionSkillActivityId",
                principalTable: "SessionSkillActivities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Questions_SessionSkillActivities_SessionSkillActivityId",
                table: "Questions");

            migrationBuilder.DropIndex(
                name: "IX_Questions_SessionSkillActivityId",
                table: "Questions");

            migrationBuilder.RenameColumn(
                name: "SessionSkillActivityId",
                table: "Questions",
                newName: "SessionId");

            migrationBuilder.AddColumn<int>(
                name: "SessionSkillActivityId",
                table: "SessionSkillActivityQuestions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_SessionSkillActivityQuestions_SessionSkillActivityId",
                table: "SessionSkillActivityQuestions",
                column: "SessionSkillActivityId");

            migrationBuilder.AddForeignKey(
                name: "FK_SessionSkillActivityQuestions_SessionSkillActivities_SessionSkillActivityId",
                table: "SessionSkillActivityQuestions",
                column: "SessionSkillActivityId",
                principalTable: "SessionSkillActivities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
