using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MySpecialVocabularyForIT.Migrations
{
    /// <inheritdoc />
    public partial class mssqllocal_migration_944 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Word",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Word_En = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Word_Rus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Word_Use_Case = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Poster = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Word", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Word");
        }
    }
}
