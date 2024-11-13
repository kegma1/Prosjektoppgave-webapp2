using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace prosjekt_webapp2.Migrations
{
    public partial class moreTestfolders : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Folder",
                columns: new[] { "Id", "Name", "ParentFolderId", "UserId" },
                values: new object[] { 4, "Homework", 1, 1 });

            migrationBuilder.InsertData(
                table: "Folder",
                columns: new[] { "Id", "Name", "ParentFolderId", "UserId" },
                values: new object[] { 5, "Homework", 2, 2 });

            migrationBuilder.InsertData(
                table: "Folder",
                columns: new[] { "Id", "Name", "ParentFolderId", "UserId" },
                values: new object[] { 6, "Homework", 3, 3 });

            migrationBuilder.InsertData(
                table: "Folder",
                columns: new[] { "Id", "Name", "ParentFolderId", "UserId" },
                values: new object[] { 7, "Photos", 3, 3 });

            migrationBuilder.InsertData(
                table: "Folder",
                columns: new[] { "Id", "Name", "ParentFolderId", "UserId" },
                values: new object[] { 8, "Totally_legal_movies:)", 1, 1 });

            migrationBuilder.InsertData(
                table: "Folder",
                columns: new[] { "Id", "Name", "ParentFolderId", "UserId" },
                values: new object[] { 9, "great_tits", 4, 1 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Folder",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Folder",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Folder",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Folder",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Folder",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Folder",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
