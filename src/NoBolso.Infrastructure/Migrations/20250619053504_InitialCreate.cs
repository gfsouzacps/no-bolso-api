using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NoBolso.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Nome = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    CriadoEm = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    AtualizadoEm = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Carteiras",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Nome = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Ativo = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    UsuarioId = table.Column<Guid>(type: "uuid", nullable: false),
                    CriadoEm = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    AtualizadoEm = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carteiras", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Carteiras_Usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Transacoes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Descricao = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    Valor = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    TipoTransacao = table.Column<int>(type: "integer", nullable: false),
                    DataTransacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CarteiraId = table.Column<Guid>(type: "uuid", nullable: false),
                    Ativo = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    CriadoEm = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    AtualizadoEm = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transacoes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transacoes_Carteiras_CarteiraId",
                        column: x => x.CarteiraId,
                        principalTable: "Carteiras",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Carteiras_CriadoEm",
                table: "Carteiras",
                column: "CriadoEm");

            migrationBuilder.CreateIndex(
                name: "IX_Carteiras_Nome",
                table: "Carteiras",
                column: "Nome");

            migrationBuilder.CreateIndex(
                name: "IX_Carteiras_UsuarioId",
                table: "Carteiras",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Transacoes_CarteiraId",
                table: "Transacoes",
                column: "CarteiraId");

            migrationBuilder.CreateIndex(
                name: "IX_Transacoes_CarteiraId_Ativo",
                table: "Transacoes",
                columns: new[] { "CarteiraId", "Ativo" });

            migrationBuilder.CreateIndex(
                name: "IX_Transacoes_CarteiraId_DataTransacao",
                table: "Transacoes",
                columns: new[] { "CarteiraId", "DataTransacao" });

            migrationBuilder.CreateIndex(
                name: "IX_Transacoes_DataTransacao",
                table: "Transacoes",
                column: "DataTransacao");

            migrationBuilder.CreateIndex(
                name: "IX_Transacoes_TipoTransacao",
                table: "Transacoes",
                column: "TipoTransacao");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transacoes");

            migrationBuilder.DropTable(
                name: "Carteiras");

            migrationBuilder.DropTable(
                name: "Usuario");
        }
    }
}
