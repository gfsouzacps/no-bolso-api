using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NoBolso.Domain.Entities;

namespace NoBolso.Infrastructure.Data.Configurations
{
    public class CarteiraConfiguration : IEntityTypeConfiguration<Carteira>
    {
        public void Configure(EntityTypeBuilder<Carteira> builder)
        {
            builder.ToTable("Carteiras");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id)
                .IsRequired()
                .ValueGeneratedNever(); // Usaremos Guid gerado na entidade

            builder.Property(c => c.Nome)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(c => c.CriadoEm)
                .IsRequired()
                .HasColumnType("timestamp with time zone");

            builder.Property(c => c.AtualizadoEm)
                .HasColumnType("timestamp with time zone");

            // Propriedade computada para o saldo
            builder.Property(c => c.Saldo)
                .HasComputedColumnSql("(SELECT COALESCE(SUM(CASE WHEN t.\"TipoTransacao\" = 1 THEN t.\"Valor\" ELSE -t.\"Valor\" END), 0) FROM \"Transacoes\" t WHERE t.\"CarteiraId\" = \"Id\" AND t.\"Ativo\" = true)", stored: false);

            builder.Property(c => c.Ativo) // Adicionar esta configuração
              .IsRequired()
              .HasDefaultValue(true);

            builder.HasQueryFilter(c => c.Ativo);

            // Relacionamento com Transações
            builder.HasMany(c => c.Transacoes)
                .WithOne(t => t.Carteira)
                .HasForeignKey(t => t.CarteiraId)
                .OnDelete(DeleteBehavior.Cascade);

            // Índices
            builder.HasIndex(c => c.Nome);
            builder.HasIndex(c => c.CriadoEm);
        }
    }
}