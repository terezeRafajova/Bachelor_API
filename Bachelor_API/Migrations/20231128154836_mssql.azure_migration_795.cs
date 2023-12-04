using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bachelor_API.Migrations
{
    /// <inheritdoc />
    public partial class mssqlazure_migration_795 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AttributesNames",
                table: "CodeBlock");

            migrationBuilder.DropColumn(
                name: "AttributesValues",
                table: "CodeBlock");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "CodeBlock",
                newName: "JsonBlocks");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "JsonBlocks",
                table: "CodeBlock",
                newName: "Type");

            migrationBuilder.AddColumn<string>(
                name: "AttributesNames",
                table: "CodeBlock",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AttributesValues",
                table: "CodeBlock",
                type: "int",
                nullable: true);
        }
    }
}
