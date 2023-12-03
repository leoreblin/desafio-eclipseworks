using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DesafioEclipseworks.WebAPI.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddingTasksToProject : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Project",
                table: "Tasks");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Project",
                table: "Tasks",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Project",
                table: "Tasks");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Project",
                table: "Tasks",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
