using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NoBolso.Domain.Entities;
using NoBolso.Domain.Enums;

namespace NoBolso.Infrastructure.Data.Configurations
{
    public class TransacaoConfiguration : IEntityTypeConfiguration<Transacao>
    {
        public void Configure(EntityTypeBuilder<Transacao> builder)
        {
            builder.ToTable("Transacoes");

            builder.HasKey(t => t.Id);

            builder.Property(t => t.Id)
                .IsRequired()
                .ValueGeneratedNever(); // Usaremos Guid gerado na entidade

            builder.Property(t => t.Descricao)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(t => t.Valor)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(t => t.TipoTransacao)
                .IsRequired()
                .HasConversion<int>(); // Enum para int

            builder.Property(t => t.DataTransacao)
                .IsRequired()
                .HasColumnType("timestamp with time zone");

            builder.Property(t => t.CarteiraId)
                .IsRequired();

            builder.Property(t => t.Ativo)
                .IsRequired()
                .HasDefaultValue(true);

            builder.Property(t => t.CriadoEm)
                .IsRequired()
                .HasColumnType("timestamp with time zone");

            builder.Property(t => t.AtualizadoEm)
                .HasColumnType("timestamp with time zone");

            // Relacionamento com Carteira
            builder.HasOne(t => t.Carteira)
                .WithMany(c => c.Transacoes)
                .HasForeignKey(t => t.CarteiraId)
                .OnDelete(DeleteBehavior.Cascade);

            // Índices
            builder.HasIndex(t => t.CarteiraId);
            builder.HasIndex(t => t.DataTransacao);
            builder.HasIndex(t => t.TipoTransacao);
            builder.HasIndex(t => new { t.CarteiraId, t.DataTransacao });
            builder.HasIndex(t => new { t.CarteiraId, t.Ativo });
        }
    }
}