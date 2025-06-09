using Livraria.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Livraria.Infra.Seeds
{
    public static class GeneroSeed
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Genero>().HasData(
                new Genero("Ficção Científica")
                {
                    Id = Guid.Parse("4fbd0572-d395-4dfb-ae7c-4e73887e1869")
                },
                new Genero("Fantasia")
                {
                    Id = Guid.Parse("2cb8b46e-6ad6-4b5e-9104-fbfe25b7d836")
                },
                new Genero("Distopia")
                {
                    Id = Guid.Parse("e77ebac7-9af4-4e9f-8e57-5367fefadcd2")
                }
            );
        }
    }
}