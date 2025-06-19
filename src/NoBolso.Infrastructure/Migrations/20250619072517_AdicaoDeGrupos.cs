using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NoBolso.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AdicaoDeGrupos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carteiras_Usuario_UsuarioId",
                table: "Carteiras");

            migrationBuilder.AddColumn<Guid>(
                name: "CriadoPorUsuarioId",
                table: "Transacoes",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<Guid>(
                name: "UsuarioId",
                table: "Carteiras",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddColumn<Guid>(
                name: "GrupoId",
                table: "Carteiras",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Grupos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Nome = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    CriadoEm = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    AtualizadoEm = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grupos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GastosRecorrentes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Descricao = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Valor = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    Tipo = table.Column<int>(type: "integer", nullable: false),
                    Categoria = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Frequencia = table.Column<int>(type: "integer", nullable: false),
                    DataInicio = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DataFim = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Ativo = table.Column<bool>(type: "boolean", nullable: false),
                    CarteiraId = table.Column<Guid>(type: "uuid", nullable: false),
                    GrupoId = table.Column<Guid>(type: "uuid", nullable: false),
                    CriadoPorUsuarioId = table.Column<Guid>(type: "uuid", nullable: false),
                    CriadoEm = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    AtualizadoEm = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GastosRecorrentes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GastosRecorrentes_Carteiras_CarteiraId",
                        column: x => x.CarteiraId,
                        principalTable: "Carteiras",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GastosRecorrentes_Grupos_GrupoId",
                        column: x => x.GrupoId,
                        principalTable: "Grupos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GastosRecorrentes_Usuario_CriadoPorUsuarioId",
                        column: x => x.CriadoPorUsuarioId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GrupoUsuario",
                columns: table => new
                {
                    GruposId = table.Column<Guid>(type: "uuid", nullable: false),
                    UsuariosId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GrupoUsuario", x => new { x.GruposId, x.UsuariosId });
                    table.ForeignKey(
                        name: "FK_GrupoUsuario_Grupos_GruposId",
                        column: x => x.GruposId,
                        principalTable: "Grupos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GrupoUsuario_Usuario_UsuariosId",
                        column: x => x.UsuariosId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Carteiras_GrupoId",
                table: "Carteiras",
                column: "GrupoId");

            migrationBuilder.CreateIndex(
                name: "IX_GastosRecorrentes_CarteiraId",
                table: "GastosRecorrentes",
                column: "CarteiraId");

            migrationBuilder.CreateIndex(
                name: "IX_GastosRecorrentes_CriadoPorUsuarioId",
                table: "GastosRecorrentes",
                column: "CriadoPorUsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_GastosRecorrentes_GrupoId",
                table: "GastosRecorrentes",
                column: "GrupoId");

            migrationBuilder.CreateIndex(
                name: "IX_GrupoUsuario_UsuariosId",
                table: "GrupoUsuario",
                column: "UsuariosId");

            migrationBuilder.AddForeignKey(
                name: "FK_Carteiras_Grupos_GrupoId",
                table: "Carteiras",
                column: "GrupoId",
                principalTable: "Grupos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Carteiras_Usuario_UsuarioId",
                table: "Carteiras",
                column: "UsuarioId",
                principalTable: "Usuario",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carteiras_Grupos_GrupoId",
                table: "Carteiras");

            migrationBuilder.DropForeignKey(
                name: "FK_Carteiras_Usuario_UsuarioId",
                table: "Carteiras");

            migrationBuilder.DropTable(
                name: "GastosRecorrentes");

            migrationBuilder.DropTable(
                name: "GrupoUsuario");

            migrationBuilder.DropTable(
                name: "Grupos");

            migrationBuilder.DropIndex(
                name: "IX_Carteiras_GrupoId",
                table: "Carteiras");

            migrationBuilder.DropColumn(
                name: "CriadoPorUsuarioId",
                table: "Transacoes");

            migrationBuilder.DropColumn(
                name: "GrupoId",
                table: "Carteiras");

            migrationBuilder.AlterColumn<Guid>(
                name: "UsuarioId",
                table: "Carteiras",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Carteiras_Usuario_UsuarioId",
                table: "Carteiras",
                column: "UsuarioId",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
