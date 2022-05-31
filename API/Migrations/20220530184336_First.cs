using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class First : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Kandidat",
                columns: table => new
                {
                    KandidatId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Ime = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Prezime = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    JMBG = table.Column<string>(type: "varchar(13)", maxLength: 13, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    GodinaRodjenja = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Telefon = table.Column<string>(type: "varchar(33)", maxLength: 33, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Napomena = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    KandidatZaposlen = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DatumIzmjenePodataka = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kandidat", x => x.KandidatId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Kandidat");
        }
    }
}
