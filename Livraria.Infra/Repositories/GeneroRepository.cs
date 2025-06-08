using Livraria.Domain.Entities;
using Livraria.Domain.Repositories;
using Livraria.Infra.Context;

namespace Livraria.Infra.Repositories
{
    public class GeneroRepository(AppDbContext _dbContext) : Repository<Genero>(_dbContext), IGeneroRepository
    {
    }
}