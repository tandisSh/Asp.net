using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ElinorStoreServer.Migrations
{
    /// <inheritdoc />
    public partial class editBasket2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                values: new object[] { "0e8c144e-03da-4e01-9e86-446dd8e3952f", null, "User", "USER" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2426167f-842e-4933-ae72-d8dfe34abf78",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "855e399c-6db9-4703-8025-ac1d8ac85762", "AQAAAAIAAYagAAAAENKn5PFWIOeIOp3CT8gkINlIK7zuF/Fm8jsJOlO+/LvOANa954VfCRp6QXgaUDp96A==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0e8c144e-03da-4e01-9e86-446dd8e3952f");

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
    }
}
