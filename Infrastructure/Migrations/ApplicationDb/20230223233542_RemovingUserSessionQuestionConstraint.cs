using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations.ApplicationDb
{
    public partial class RemovingUserSessionQuestionConstraint : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserSessionQuestions_Questions_QuestionId",
                table: "UserSessionQuestions");

            migrationBuilder.DropIndex(
                name: "IX_UserSessionQuestions_QuestionId",
                table: "UserSessionQuestions");

            migrationBuilder.AddColumn<int>(
                name: "UserSessionId",
                table: "UserSessionQuestions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_UserSessionQuestions_UserSessionId",
                table: "UserSessionQuestions",
                column: "UserSessionId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserSessionQuestions_UserSessions_UserSessionId",
                table: "UserSessionQuestions",
                column: "UserSessionId",
                principalTable: "UserSessions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserSessionQuestions_UserSessions_UserSessionId",
                table: "UserSessionQuestions");

            migrationBuilder.DropIndex(
                name: "IX_UserSessionQuestions_UserSessionId",
                table: "UserSessionQuestions");

            migrationBuilder.DropColumn(
                name: "UserSessionId",
                table: "UserSessionQuestions");

            migrationBuilder.CreateIndex(
                name: "IX_UserSessionQuestions_QuestionId",
                table: "UserSessionQuestions",
                column: "QuestionId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserSessionQuestions_Questions_QuestionId",
                table: "UserSessionQuestions",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
