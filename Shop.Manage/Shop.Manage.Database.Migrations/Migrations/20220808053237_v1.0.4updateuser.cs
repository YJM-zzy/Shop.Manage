using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shop.Manage.Database.Migrations.Migrations
{
    public partial class v104updateuser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserMessage_Avart_AvartId",
                table: "UserMessage");

            migrationBuilder.DropIndex(
                name: "IX_UserMessage_AvartId",
                table: "UserMessage");

            migrationBuilder.DropColumn(
                name: "AvartId",
                table: "UserMessage");

            migrationBuilder.AddColumn<string>(
                name: "AvatarUrl",
                table: "UserMessage",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AvatarUrl",
                table: "UserMessage");

            migrationBuilder.AddColumn<int>(
                name: "AvartId",
                table: "UserMessage",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserMessage_AvartId",
                table: "UserMessage",
                column: "AvartId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserMessage_Avart_AvartId",
                table: "UserMessage",
                column: "AvartId",
                principalTable: "Avart",
                principalColumn: "Id");
        }
    }
}
