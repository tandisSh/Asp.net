using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ElinorStoreServer.Migrations
{
    /// <inheritdoc />
    public partial class addDateToBasket : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0e8c144e-03da-4e01-9e86-446dd8e3952f");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Baskets",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "5b5fb939-f5b4-4d1f-99aa-73e5995fcdc6", null, "User", "USER" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2426167f-842e-4933-ae72-d8dfe34abf78",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "1b8f99e5-0d58-46b0-8853-17563e2fa253", "AQAAAAIAAYagAAAAEFvql+QghbSW2yST4VcQaQ5jE/LpkTWFeWdV8wJSRWHb6lb2PQIXr0S/hPRylvjaPw==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5b5fb939-f5b4-4d1f-99aa-73e5995fcdc6");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
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
    }
}
