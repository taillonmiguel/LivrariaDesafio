using Livraria.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Livraria.Infra.Configurations
{
    public class LivroConfiguration : IEntityTypeConfiguration<Livro>
    {
        public void Configure(EntityTypeBuilder<Livro> builder)
        {
            builder.ToTable("Livros");

            builder.HasKey(l => l.Id);

            builder.Property(l => l.Titulo)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.HasOne(l => l.Genero)
                   .WithMany(g => g.Livros)
                   .HasForeignKey(l => l.GeneroId);

            builder.HasOne(l => l.Autor)
                   .WithMany(a => a.Livros)
                   .HasForeignKey(l => l.AutorId);
        }
    }
}
