using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bachelor_API.Migrations
{
    /// <inheritdoc />
    public partial class mssqlazure_migration_681 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Lesson",
                columns: table => new
                {
                    LessonId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SharingCode = table.Column<int>(type: "int", nullable: true),
                    SharingTime = table.Column<int>(type: "int", nullable: true),
                    NumberOfPages = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lesson", x => x.LessonId);
                });

            migrationBuilder.CreateTable(
                name: "CodeBlock",
                columns: table => new
                {
                    CodeBlockId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Slot = table.Column<int>(type: "int", nullable: true),
                    PageNumber = table.Column<int>(type: "int", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AttributesNames = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AttributesValues = table.Column<int>(type: "int", nullable: true),
                    LessonId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CodeBlock", x => x.CodeBlockId);
                    table.ForeignKey(
                        name: "FK_CodeBlock_Lesson_LessonId",
                        column: x => x.LessonId,
                        principalTable: "Lesson",
                        principalColumn: "LessonId");
                });

            migrationBuilder.CreateTable(
                name: "Description",
                columns: table => new
                {
                    DescriptionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Slot = table.Column<int>(type: "int", nullable: true),
                    PageNumber = table.Column<int>(type: "int", nullable: true),
                    LessonId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Description", x => x.DescriptionId);
                    table.ForeignKey(
                        name: "FK_Description_Lesson_LessonId",
                        column: x => x.LessonId,
                        principalTable: "Lesson",
                        principalColumn: "LessonId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CodeBlock_LessonId",
                table: "CodeBlock",
                column: "LessonId");

            migrationBuilder.CreateIndex(
                name: "IX_Description_LessonId",
                table: "Description",
                column: "LessonId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CodeBlock");

            migrationBuilder.DropTable(
                name: "Description");

            migrationBuilder.DropTable(
                name: "Lesson");
        }
    }
}
