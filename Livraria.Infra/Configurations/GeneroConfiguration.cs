using Livraria.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Livraria.Infra.Configurations
{
    public class GeneroConfiguration : IEntityTypeConfiguration<Genero>
    {
        public void Configure(EntityTypeBuilder<Genero> builder)
        {
            builder.ToTable("Generos"); 

            builder.HasKey(g => g.Id);

            builder.Property(g => g.Nome)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.HasMany(g => g.Livros)
                   .WithOne(l => l.Genero)
                   .HasForeignKey(l => l.GeneroId);
        }
    }
}
