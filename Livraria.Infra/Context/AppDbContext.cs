using Livraria.Domain.Entities;
using Livraria.Infra.Seeds;
using Microsoft.EntityFrameworkCore;

namespace Livraria.Infra.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Genero> Generos { get; set; }
        public DbSet<Autor> Autores { get; set; }
        public DbSet<Livro> Livros { get; set; }

        public DbContext Instance => this;

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<DefaultEntity>())
            {
                if (entry.State == EntityState.Modified)
                {
                    entry.Entity.DataAtualizacao = DateTime.UtcNow;
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
            base.OnModelCreating(modelBuilder);

            AutorSeed.Seed(modelBuilder);
            GeneroSeed.Seed(modelBuilder);
            LivroSeed.Seed(modelBuilder);
        }
    }
}
