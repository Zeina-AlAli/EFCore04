using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EFCore04.Migrations
{
    /// <inheritdoc />
    public partial class SeedingData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Branches",
                columns: new[] { "Code", "Address", "Name", "PhoneNumber" },
                values: new object[,]
                {
                    { "CAI-01", "Cairo", "NBE", 1016556892 },
                    { "CAI-02", "Cairo", "AlexBank", 1233434099 },
                    { "GZ-01", "Giza", "CIB", 112882308 }
                });

            migrationBuilder.InsertData(
                table: "Managers",
                columns: new[] { "Id", "BranchCode", "Email", "FullName", "HireDate", "PhoneNumber" },
                values: new object[,]
                {
                    { 1, "CAI-01", "Ahmed@gmail.com", "Ahmed Ali", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1016998790 },
                    { 2, "CAI-02", "Ahmed@gmail.com", "Samer Ali", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1016998790 },
                    { 3, "GZ-01", "Mohammed@gmail.com", "Mohammed Adel", new DateTime(2025, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1016886308 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Managers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Managers",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Managers",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Branches",
                keyColumn: "Code",
                keyValue: "CAI-01");

            migrationBuilder.DeleteData(
                table: "Branches",
                keyColumn: "Code",
                keyValue: "CAI-02");

            migrationBuilder.DeleteData(
                table: "Branches",
                keyColumn: "Code",
                keyValue: "GZ-01");
        }
    }
}
