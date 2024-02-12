using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ADMS.Migrations
{
    /// <inheritdoc />
    public partial class AddOrdersAndStatements2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Groups_Orders_OrderId",
                table: "Groups");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Orders_OrderId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_OrderId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Groups_OrderId",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "Groups");

            migrationBuilder.AddColumn<int[]>(
                name: "Groups",
                table: "Orders",
                type: "integer[]",
                nullable: true);

            migrationBuilder.AddColumn<int[]>(
                name: "Students",
                table: "Orders",
                type: "integer[]",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Groups",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Students",
                table: "Orders");

            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "Students",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "Groups",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Students_OrderId",
                table: "Students",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Groups_OrderId",
                table: "Groups",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_Orders_OrderId",
                table: "Groups",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Orders_OrderId",
                table: "Students",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id");
        }
    }
}
