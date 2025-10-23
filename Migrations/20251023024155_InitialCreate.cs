using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace StudentTest.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MstSubjects",
                columns: table => new
                {
                    SubjectKey = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SubjectName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MstSubjects", x => x.SubjectKey);
                });

            migrationBuilder.CreateTable(
                name: "MstStudents",
                columns: table => new
                {
                    StudentKey = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StudentName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    SubjectKey = table.Column<int>(type: "integer", nullable: false),
                    Grade = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MstStudents", x => x.StudentKey);
                    table.ForeignKey(
                        name: "FK_MstStudents_MstSubjects_SubjectKey",
                        column: x => x.SubjectKey,
                        principalTable: "MstSubjects",
                        principalColumn: "SubjectKey",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MstStudents_SubjectKey",
                table: "MstStudents",
                column: "SubjectKey");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MstStudents");

            migrationBuilder.DropTable(
                name: "MstSubjects");
        }
    }
}
