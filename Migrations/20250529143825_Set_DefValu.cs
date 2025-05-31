using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _netmvc.Migrations
{
    /// <inheritdoc />
    public partial class Set_DefValu : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Served",
                table: "OrderDetail",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Served",
                table: "OrderDetail");
        }
    }
}
