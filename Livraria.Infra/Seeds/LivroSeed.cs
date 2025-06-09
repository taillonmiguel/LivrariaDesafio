using Livraria.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Livraria.Infra.Seeds
{
    public static class LivroSeed
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Livro>().HasData(
                new Livro("1984", "Uma distopia totalitária",
                          Guid.Parse("0c1fa73a-91e9-456b-91ae-90e03c83d841"),
                          Guid.Parse("e77ebac7-9af4-4e9f-8e57-5367fefadcd2"))
                {
                    Id = Guid.Parse("ffb6cf11-9b86-4b86-8f23-d00ff6d04fd4")
                },

                new Livro("Harry Potter e a Pedra Filosofal", "Início da saga mágica",
                          Guid.Parse("14a3de89-71ea-47dc-bfd2-2139de3d2fd0"),
                          Guid.Parse("2cb8b46e-6ad6-4b5e-9104-fbfe25b7d836"))
                {
                    Id = Guid.Parse("2f8ea68e-f3d9-4d4a-9e87-ecab72a3fdf2")
                },

                new Livro("O Senhor dos Anéis", "Uma jornada épica pela Terra Média",
                          Guid.Parse("845b1ad1-9d84-4e56-bc1e-94eb55f71656"),
                          Guid.Parse("2cb8b46e-6ad6-4b5e-9104-fbfe25b7d836"))
                {
                    Id = Guid.Parse("a4fd96be-7607-41f0-a2c6-b30b338efcec")
                }
            );
        }
    }
}
