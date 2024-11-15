using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace prosjekt_webapp2.Migrations
{
    public partial class UpdateDocumentSeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Document",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 11, 15, 10, 49, 7, 68, DateTimeKind.Local).AddTicks(3541));

            migrationBuilder.UpdateData(
                table: "Document",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Content", "CreatedDate" },
                values: new object[] { "uploads\\Narvik-by-vinter-1440x900.jpg", new DateTime(2024, 11, 15, 10, 49, 7, 68, DateTimeKind.Local).AddTicks(3569) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Document",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 11, 15, 10, 40, 52, 905, DateTimeKind.Local).AddTicks(821));

            migrationBuilder.UpdateData(
                table: "Document",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Content", "CreatedDate" },
                values: new object[] { "C:\\Users\\bjune\\OneDrive\\Documents\\Programmering\\Prosjektoppgave Webapi\\Prosjektoppgave-webapp2\\uploads\\Narvik-by-vinter-1440x900.jpg", new DateTime(2024, 11, 15, 10, 40, 52, 905, DateTimeKind.Local).AddTicks(854) });
        }
    }
}
