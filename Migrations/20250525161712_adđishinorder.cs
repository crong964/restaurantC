using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _netmvc.Migrations
{
    /// <inheritdoc />
    public partial class adđishinorder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "pathImage",
                table: "OrderDetail");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetail_DishId",
                table: "OrderDetail",
                column: "DishId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetail_Dish_DishId",
                table: "OrderDetail",
                column: "DishId",
                principalTable: "Dish",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetail_Dish_DishId",
                table: "OrderDetail");

            migrationBuilder.DropIndex(
                name: "IX_OrderDetail_DishId",
                table: "OrderDetail");

            migrationBuilder.AddColumn<string>(
                name: "pathImage",
                table: "OrderDetail",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}
