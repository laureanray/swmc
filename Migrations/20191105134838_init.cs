using Microsoft.EntityFrameworkCore.Migrations;

namespace swmc.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Skills_Requirements_RequirementId",
                table: "Skills");

            migrationBuilder.AlterColumn<int>(
                name: "RequirementId",
                table: "Skills",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Skills_Requirements_RequirementId",
                table: "Skills",
                column: "RequirementId",
                principalTable: "Requirements",
                principalColumn: "RequirementId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Skills_Requirements_RequirementId",
                table: "Skills");

            migrationBuilder.AlterColumn<int>(
                name: "RequirementId",
                table: "Skills",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Skills_Requirements_RequirementId",
                table: "Skills",
                column: "RequirementId",
                principalTable: "Requirements",
                principalColumn: "RequirementId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
