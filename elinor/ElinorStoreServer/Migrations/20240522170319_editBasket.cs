using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ElinorStoreServer.Migrations
{
    /// <inheritdoc />
    public partial class editBasket : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3d8a6345-4e13-4005-8996-1f19a7c9d887");

            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "Baskets",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ProductName",
                table: "Baskets",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "8d3a80db-65f6-4c17-b594-a2e3a46f8054", null, "User", "USER" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2426167f-842e-4933-ae72-d8dfe34abf78",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "d5aba49f-0799-4ec5-976d-8eceea8543cd", "AQAAAAIAAYagAAAAEFz72uuLDtGNqOLw+LOUWs7X6pEQIraYJ8i3MgvKSfcNOPBiZ+N0iYlV8aeIC1qsfQ==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8d3a80db-65f6-4c17-b594-a2e3a46f8054");

            migrationBuilder.DropColumn(
                name: "Category",
                table: "Baskets");

            migrationBuilder.DropColumn(
                name: "ProductName",
                table: "Baskets");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "3d8a6345-4e13-4005-8996-1f19a7c9d887", null, "User", "USER" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2426167f-842e-4933-ae72-d8dfe34abf78",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "fd08b0e4-c509-4643-9c08-c970a9b3f9ab", "AQAAAAIAAYagAAAAEEoX9UpeFx7+wxUOk47TXsVHnaFlJwsNDi5Pd/E4CUNpo5eDU4/8qiXG+TBhbPHIIw==" });
        }
    }
}
