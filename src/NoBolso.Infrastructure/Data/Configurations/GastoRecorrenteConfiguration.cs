using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NoBolso.Domain.Entities;

namespace NoBolso.Infrastructure.Data.Configurations;

public class GastoRecorrenteConfiguration : IEntityTypeConfiguration<GastoRecorrente>
{
    public void Configure(EntityTypeBuilder<GastoRecorrente> builder)
    {
        builder.ToTable("GastosRecorrentes");

        builder.HasKey(g => g.Id);

        builder.Property(g => g.Descricao)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(g => g.Valor)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.Property(g => g.Categoria)
            .HasMaxLength(100);

        // Relacionamento com Carteira
        builder.HasOne(g => g.Carteira)
            .WithMany() // Uma carteira pode ter vários gastos recorrentes associados
            .HasForeignKey(g => g.CarteiraId)
            .OnDelete(DeleteBehavior.Restrict); // Não deixa deletar a carteira se houver um gasto recorrente nela

        // Relacionamento com Usuario
        builder.HasOne(g => g.Usuario)
            .WithMany() // Um usuário pode ter vários gastos recorrentes
            .HasForeignKey(g => g.UsuarioId)
            .OnDelete(DeleteBehavior.Cascade); // Se o usuário for deletado, seus gastos recorrentes também são

        builder.HasQueryFilter(g => g.Ativo);
    }
}
