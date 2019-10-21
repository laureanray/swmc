using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace swmc.Migrations
{
    public partial class @new : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ApplicantId",
                table: "Documents",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Telephone",
                table: "Applicants",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Religion",
                table: "Applicants",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Position",
                table: "Applicants",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "PlaceOfBirth",
                table: "Applicants",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Applicants",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Height",
                table: "Applicants",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Gender",
                table: "Applicants",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Applicants",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CivilStatus",
                table: "Applicants",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "Citizenship",
                table: "Applicants",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Cellphone",
                table: "Applicants",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Applicants",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MiddleName",
                table: "Applicants",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Weight",
                table: "Applicants",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Vessels",
                columns: table => new
                {
                    VesselId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    VesselName = table.Column<string>(nullable: false),
                    Principal = table.Column<string>(nullable: false),
                    Flag = table.Column<string>(nullable: false),
                    GrossTonnage = table.Column<string>(nullable: false),
                    JSU = table.Column<string>(nullable: false),
                    EngineMake = table.Column<string>(nullable: false),
                    PortRegistry = table.Column<string>(nullable: true),
                    OfficialNumber = table.Column<string>(nullable: true),
                    CBA = table.Column<string>(nullable: true),
                    IMONumber = table.Column<string>(nullable: true),
                    VesselAbr = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    HorsePower = table.Column<string>(nullable: true),
                    Classification = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    YearBuilt = table.Column<string>(nullable: true),
                    DateEnrolled = table.Column<DateTime>(nullable: false),
                    DateAdded = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vessels", x => x.VesselId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Documents_ApplicantId",
                table: "Documents",
                column: "ApplicantId");

            migrationBuilder.AddForeignKey(
                name: "FK_Documents_Applicants_ApplicantId",
                table: "Documents",
                column: "ApplicantId",
                principalTable: "Applicants",
                principalColumn: "ApplicantId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Documents_Applicants_ApplicantId",
                table: "Documents");

            migrationBuilder.DropTable(
                name: "Vessels");

            migrationBuilder.DropIndex(
                name: "IX_Documents_ApplicantId",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "ApplicantId",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "MiddleName",
                table: "Applicants");

            migrationBuilder.DropColumn(
                name: "Weight",
                table: "Applicants");

            migrationBuilder.AlterColumn<string>(
                name: "Telephone",
                table: "Applicants",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Religion",
                table: "Applicants",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<int>(
                name: "Position",
                table: "Applicants",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "PlaceOfBirth",
                table: "Applicants",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Applicants",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Height",
                table: "Applicants",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<int>(
                name: "Gender",
                table: "Applicants",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Applicants",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<int>(
                name: "CivilStatus",
                table: "Applicants",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Citizenship",
                table: "Applicants",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Cellphone",
                table: "Applicants",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Applicants",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
