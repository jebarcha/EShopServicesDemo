using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace EShopServices.Api.Author.Migrations
{
    /// <inheritdoc />
    public partial class MigrationPostgresInitial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AuthorBook",
                columns: table => new
                {
                    AuthorBookId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    Birthday = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    AuthorBookGuid = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthorBook", x => x.AuthorBookId);
                });

            migrationBuilder.CreateTable(
                name: "AcademicGrade",
                columns: table => new
                {
                    AcademidGradeId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    AcademicCenter = table.Column<string>(type: "text", nullable: false),
                    GradeDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    AuthorBookId = table.Column<int>(type: "integer", nullable: false),
                    AcademicGradeGuid = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AcademicGrade", x => x.AcademidGradeId);
                    table.ForeignKey(
                        name: "FK_AcademicGrade_AuthorBook_AuthorBookId",
                        column: x => x.AuthorBookId,
                        principalTable: "AuthorBook",
                        principalColumn: "AuthorBookId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AcademicGrade_AuthorBookId",
                table: "AcademicGrade",
                column: "AuthorBookId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AcademicGrade");

            migrationBuilder.DropTable(
                name: "AuthorBook");
        }
    }
}
