using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace prosjekt_webapp2.Migrations
{
    public partial class foldersCanBeRoot5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Folder",
                columns: new[] { "Id", "Name", "ParentFolderId", "UserId" },
                values: new object[] { 1, "a", null, 1 });

            migrationBuilder.InsertData(
                table: "Folder",
                columns: new[] { "Id", "Name", "ParentFolderId", "UserId" },
                values: new object[] { 2, "b", null, 2 });

            migrationBuilder.InsertData(
                table: "Folder",
                columns: new[] { "Id", "Name", "ParentFolderId", "UserId" },
                values: new object[] { 3, "c", null, 3 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Folder",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Folder",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Folder",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
