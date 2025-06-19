using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NoBolso.Domain.Entities;

namespace NoBolso.Infrastructure.Data.Configurations;

    public class GrupoConfiguration : IEntityTypeConfiguration<Grupo>
    {
        public void Configure(EntityTypeBuilder<Grupo> builder)
        {
            builder.ToTable("Grupos");
            builder.HasKey(g => g.Id);
            builder.Property(g => g.Nome).IsRequired().HasMaxLength(100);

            // Configura a relação muitos-para-muitos entre Grupo e Usuario.
            // O EF Core criará a tabela de junção "GrupoUsuario" automaticamente.
            builder.HasMany(g => g.Usuarios)
                   .WithMany(u => u.Grupos);
        }
    }