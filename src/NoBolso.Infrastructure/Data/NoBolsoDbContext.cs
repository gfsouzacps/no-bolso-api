using Microsoft.EntityFrameworkCore;
using NoBolso.Domain.Entities;
using NoBolso.Infrastructure.Data.Configurations;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;

namespace NoBolso.Infrastructure.Data
{
    public class NoBolsoDbContext : DbContext
    {
        public DbSet<Carteira> Carteiras { get; set; }
        public DbSet<Transacao> Transacoes { get; set; }
        public DbSet<GastoRecorrente> GastosRecorrentes { get; set; }

        public NoBolsoDbContext(DbContextOptions<NoBolsoDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Aplicar todas as configurações automaticamente
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            // Ou aplicar manualmente
            modelBuilder.ApplyConfiguration(new CarteiraConfiguration());
            modelBuilder.ApplyConfiguration(new TransacaoConfiguration());
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            // Aqui você pode adicionar lógica para auditoria, eventos, etc.
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}