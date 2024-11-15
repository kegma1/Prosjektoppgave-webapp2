using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace prosjekt_webapp2.Migrations
{
    public partial class SeedImageDocument : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Document",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 11, 15, 10, 40, 52, 905, DateTimeKind.Local).AddTicks(821));

            migrationBuilder.InsertData(
                table: "Document",
                columns: new[] { "Id", "Content", "ContentTypeId", "CreatedDate", "ParentFolderId", "Title", "UserId" },
                values: new object[] { 2, "C:\\Users\\bjune\\OneDrive\\Documents\\Programmering\\Prosjektoppgave Webapi\\Prosjektoppgave-webapp2\\uploads\\Narvik-by-vinter-1440x900.jpg", 2, new DateTime(2024, 11, 15, 10, 40, 52, 905, DateTimeKind.Local).AddTicks(854), 7, "Narvik Winter Image", 1 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Document",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.UpdateData(
                table: "Document",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 11, 13, 17, 44, 49, 765, DateTimeKind.Local).AddTicks(7631));
        }
    }
}
