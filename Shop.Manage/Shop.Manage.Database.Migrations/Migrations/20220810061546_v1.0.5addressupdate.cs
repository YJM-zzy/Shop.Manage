using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shop.Manage.Database.Migrations.Migrations
{
    public partial class v105addressupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDefault",
                table: "UserAddr",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsMunicipality",
                table: "UserAddr",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Tag",
                table: "UserAddr",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDefault",
                table: "UserAddr");

            migrationBuilder.DropColumn(
                name: "IsMunicipality",
                table: "UserAddr");

            migrationBuilder.DropColumn(
                name: "Tag",
                table: "UserAddr");
        }
    }
}
