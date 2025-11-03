using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DisasterReliefApp.Migrations
{
    /// <inheritdoc />
    public partial class FixTaskAssignmentForeignKey : Migration
    { 
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskAssignments_AspNetUsers_CreatedById",
                table: "TaskAssignments");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskAssignments_AspNetUsers_CreatedById",
                table: "TaskAssignments",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskAssignments_AspNetUsers_CreatedById",
                table: "TaskAssignments");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskAssignments_AspNetUsers_CreatedById",
                table: "TaskAssignments",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
