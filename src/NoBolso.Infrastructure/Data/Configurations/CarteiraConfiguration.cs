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


            builder.Property(c => c.Ativo)
              .IsRequired()
              .HasDefaultValue(true);

            builder.HasQueryFilter(c => c.Ativo);

            // Relacionamento com Transações
            builder.HasMany(c => c.Transacoes)
                .WithOne(t => t.Carteira)
                .HasForeignKey(t => t.CarteiraId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(c => c.Usuario)
               .WithMany(u => u.Carteiras)
               .HasForeignKey(c => c.UsuarioId)
               .IsRequired()
               .OnDelete(DeleteBehavior.Restrict);

            // Índices
            builder.HasIndex(c => c.Nome);
            builder.HasIndex(c => c.UsuarioId);
            builder.HasIndex(c => c.CriadoEm);
        }
    }
}