using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _netmvc.Migrations
{
    /// <inheritdoc />
#pragma warning disable CS8981 // The type name only contains lower-cased ascii characters. Such names may become reserved for the language.
    public partial class key : Migration
#pragma warning restore CS8981 // The type name only contains lower-cased ascii characters. Such names may become reserved for the language.
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DishTypeId",
                table: "Dish",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Dish_DishTypeId",
                table: "Dish",
                column: "DishTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Dish_DishType_DishTypeId",
                table: "Dish",
                column: "DishTypeId",
                principalTable: "DishType",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dish_DishType_DishTypeId",
                table: "Dish");

            migrationBuilder.DropIndex(
                name: "IX_Dish_DishTypeId",
                table: "Dish");

            migrationBuilder.DropColumn(
                name: "DishTypeId",
                table: "Dish");
        }
    }
}
