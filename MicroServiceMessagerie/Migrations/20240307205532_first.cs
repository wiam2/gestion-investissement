using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MicroServiceMessagerie.Migrations
{
    /// <inheritdoc />
    public partial class first : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Conversation",
                columns: table => new
                {
                    idconversation = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Conversation", x => x.idconversation);
                });

            migrationBuilder.CreateTable(
                name: "Message",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Emeteur = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Recepteur = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Contenu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    conversationidconversation = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Message", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Message_Conversation_conversationidconversation",
                        column: x => x.conversationidconversation,
                        principalTable: "Conversation",
                        principalColumn: "idconversation");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Message_conversationidconversation",
                table: "Message",
                column: "conversationidconversation");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Message");

            migrationBuilder.DropTable(
                name: "Conversation");
        }
    }
}
