using Livraria.Domain.Entities;
using Livraria.Domain.Repositories;
using Livraria.Infra.Context;

namespace Livraria.Infra.Repositories
{
    public class AutorRepository : Repository<Autor>, IAutorRepository
    {
        public AutorRepository(AppDbContext context) : base(context) { }
    }
}