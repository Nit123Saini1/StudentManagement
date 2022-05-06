using Microsoft.EntityFrameworkCore.Migrations;

namespace StudentManagement.Migrations
{
    public partial class Addfkintable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StudentID",
                table: "tblStdSubjects",
                newName: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_tblStdSubjects_StudentId",
                table: "tblStdSubjects",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_tblStdSubjects_SubjectID",
                table: "tblStdSubjects",
                column: "SubjectID");

            migrationBuilder.AddForeignKey(
                name: "FK_tblStdSubjects_tblStudents_StudentId",
                table: "tblStdSubjects",
                column: "StudentId",
                principalTable: "tblStudents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tblStdSubjects_tblSubjects_SubjectID",
                table: "tblStdSubjects",
                column: "SubjectID",
                principalTable: "tblSubjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblStdSubjects_tblStudents_StudentId",
                table: "tblStdSubjects");

            migrationBuilder.DropForeignKey(
                name: "FK_tblStdSubjects_tblSubjects_SubjectID",
                table: "tblStdSubjects");

            migrationBuilder.DropIndex(
                name: "IX_tblStdSubjects_StudentId",
                table: "tblStdSubjects");

            migrationBuilder.DropIndex(
                name: "IX_tblStdSubjects_SubjectID",
                table: "tblStdSubjects");

            migrationBuilder.RenameColumn(
                name: "StudentId",
                table: "tblStdSubjects",
                newName: "StudentID");
        }
    }
}
