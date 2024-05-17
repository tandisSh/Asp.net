using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ElinorStoreServer.Migrations
{
    /// <inheritdoc />
    public partial class deleteUsertId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UsertId",
                table: "Baskets");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UsertId",
                table: "Baskets",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }
    }
}
