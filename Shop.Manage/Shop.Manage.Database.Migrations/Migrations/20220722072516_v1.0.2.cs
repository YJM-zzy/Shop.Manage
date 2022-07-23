using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shop.Manage.Database.Migrations.Migrations
{
    public partial class v102 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BusinessMessage_Avart_AvartId",
                table: "BusinessMessage");

            migrationBuilder.DropForeignKey(
                name: "FK_UserMessage_Avart_AvartId",
                table: "UserMessage");

            migrationBuilder.AlterColumn<int>(
                name: "AvartId",
                table: "UserMessage",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "AvartId",
                table: "BusinessMessage",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessMessage_Avart_AvartId",
                table: "BusinessMessage",
                column: "AvartId",
                principalTable: "Avart",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserMessage_Avart_AvartId",
                table: "UserMessage",
                column: "AvartId",
                principalTable: "Avart",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BusinessMessage_Avart_AvartId",
                table: "BusinessMessage");

            migrationBuilder.DropForeignKey(
                name: "FK_UserMessage_Avart_AvartId",
                table: "UserMessage");

            migrationBuilder.AlterColumn<int>(
                name: "AvartId",
                table: "UserMessage",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AvartId",
                table: "BusinessMessage",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessMessage_Avart_AvartId",
                table: "BusinessMessage",
                column: "AvartId",
                principalTable: "Avart",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserMessage_Avart_AvartId",
                table: "UserMessage",
                column: "AvartId",
                principalTable: "Avart",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
