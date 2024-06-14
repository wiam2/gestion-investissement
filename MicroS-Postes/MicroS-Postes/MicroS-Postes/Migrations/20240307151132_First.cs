using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MicroS_Postes.Migrations
{
    /// <inheritdoc />
    public partial class First : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Likes",
                columns: table => new
                {
                    IdLike = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdLOwner = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdPoste = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Likes", x => x.IdLike);
                });

            migrationBuilder.CreateTable(
                name: "Postes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdOwner = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Titre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DatePoste = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Montant = table.Column<float>(type: "real", nullable: false),
                    Secteur = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: true),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumLikes = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Postes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PostesInvestisseurs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    TypeInvestissement = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostesInvestisseurs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PostesInvestisseurs_Postes_Id",
                        column: x => x.Id,
                        principalTable: "Postes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PostesStartups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    EtapeDev = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostesStartups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PostesStartups_Postes_Id",
                        column: x => x.Id,
                        principalTable: "Postes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Likes");

            migrationBuilder.DropTable(
                name: "PostesInvestisseurs");

            migrationBuilder.DropTable(
                name: "PostesStartups");

            migrationBuilder.DropTable(
                name: "Postes");
        }
    }
}
