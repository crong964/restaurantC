using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _netmvc.Migrations
{
    /// <inheritdoc />
    public partial class tablefiel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_OrderDetail_TableId",
                table: "OrderDetail",
                column: "TableId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetail_Table_TableId",
                table: "OrderDetail",
                column: "TableId",
                principalTable: "Table",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetail_Table_TableId",
                table: "OrderDetail");

            migrationBuilder.DropIndex(
                name: "IX_OrderDetail_TableId",
                table: "OrderDetail");
        }
    }
}
