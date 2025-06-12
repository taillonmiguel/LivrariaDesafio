using Livraria.Domain.Dto;
using Livraria.Domain.Entities;
using Livraria.Shared.Dto;

namespace Livraria.Domain.Repositories
{
    public interface ILivroRepository : IRepository<Livro>
    {
        Task<PagedResult<Livro>> SearchAsync(LivroFilter filtro);
    }
}