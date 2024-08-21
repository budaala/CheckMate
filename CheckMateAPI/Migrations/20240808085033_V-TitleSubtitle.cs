using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CheckMateAPI.Migrations
{
    /// <inheritdoc />
    public partial class VTitleSubtitle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Todos",
                newName: "Subtitle");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Todos",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "TEXT")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Todos",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Title",
                table: "Todos");

            migrationBuilder.RenameColumn(
                name: "Subtitle",
                table: "Todos",
                newName: "Description");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Todos",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);
        }
    }
}
