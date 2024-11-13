using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace prosjekt_webapp2.Migrations
{
    public partial class moreTestfolders1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Document_ContentType_ContentTypeId",
                table: "Document");

            migrationBuilder.AlterColumn<int>(
                name: "ContentTypeId",
                table: "Document",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "ContentType",
                columns: new[] { "Id", "Type" },
                values: new object[] { 1, 0 });

            migrationBuilder.InsertData(
                table: "ContentType",
                columns: new[] { "Id", "Type" },
                values: new object[] { 2, 1 });

            migrationBuilder.InsertData(
                table: "Document",
                columns: new[] { "Id", "Content", "ContentTypeId", "CreatedDate", "ParentFolderId", "Title", "UserId" },
                values: new object[] { 1, "Hallo,\nDette er en test", 1, new DateTime(2024, 11, 13, 17, 44, 49, 765, DateTimeKind.Local).AddTicks(7631), 1, "importante shit", 1 });

            migrationBuilder.AddForeignKey(
                name: "FK_Document_ContentType_ContentTypeId",
                table: "Document",
                column: "ContentTypeId",
                principalTable: "ContentType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Document_ContentType_ContentTypeId",
                table: "Document");

            migrationBuilder.DeleteData(
                table: "ContentType",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Document",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ContentType",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.AlterColumn<int>(
                name: "ContentTypeId",
                table: "Document",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_Document_ContentType_ContentTypeId",
                table: "Document",
                column: "ContentTypeId",
                principalTable: "ContentType",
                principalColumn: "Id");
        }
    }
}
