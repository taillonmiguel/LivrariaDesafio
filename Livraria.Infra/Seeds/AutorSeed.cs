using Livraria.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Livraria.Infra.Seeds
{
    public static class AutorSeed
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Autor>().HasData(
                new Autor("George Orwell")
                {
                    Id = Guid.Parse("0c1fa73a-91e9-456b-91ae-90e03c83d841")
                },
                new Autor("J.K. Rowling")
                {
                    Id = Guid.Parse("14a3de89-71ea-47dc-bfd2-2139de3d2fd0")
                },
                new Autor("J.R.R. Tolkien")
                {
                    Id = Guid.Parse("845b1ad1-9d84-4e56-bc1e-94eb55f71656")
                }
            );
        }
    }
}
