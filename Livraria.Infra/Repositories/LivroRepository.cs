using Livraria.Domain.Entities;
using Livraria.Domain.Repositories;
using Livraria.Infra.Context;

namespace Livraria.Infra.Repositories
{
    public class LivroRepository : Repository<Livro>, ILivroRepository
    {
        public LivroRepository(AppDbContext context) : base(context) { }
    }
}