using Livraria.Domain.Entities;
using Livraria.Domain.Repositories;
using Livraria.Infra.Context;

namespace Livraria.Infra.Repositories
{
    public class GeneroRepository : Repository<Genero>, IGeneroRepository
    {
        public GeneroRepository(AppDbContext context) : base(context) { }
    }
}