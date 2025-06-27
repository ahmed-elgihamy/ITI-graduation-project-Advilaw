using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdviLaw.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class v2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LawyerCardID",
                table: "Lawyers");

            migrationBuilder.RenameColumn(
                name: "barCardImagePath",
                table: "Lawyers",
                newName: "BarCardImagePath");

            migrationBuilder.RenameColumn(
                name: "barAssociationCardNumber",
                table: "Lawyers",
                newName: "BarAssociationCardNumber");

            migrationBuilder.AlterColumn<string>(
                name: "Bio",
                table: "Lawyers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "NationalIDImagePath",
                table: "Lawyers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NationalIDImagePath",
                table: "Lawyers");

            migrationBuilder.RenameColumn(
                name: "BarCardImagePath",
                table: "Lawyers",
                newName: "barCardImagePath");

            migrationBuilder.RenameColumn(
                name: "BarAssociationCardNumber",
                table: "Lawyers",
                newName: "barAssociationCardNumber");

            migrationBuilder.AlterColumn<string>(
                name: "Bio",
                table: "Lawyers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LawyerCardID",
                table: "Lawyers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
