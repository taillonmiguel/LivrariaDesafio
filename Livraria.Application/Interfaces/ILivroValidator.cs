using Livraria.Application.Dtos;
using Livraria.Domain.Entities;

namespace Livraria.Application.Interfaces
{
    public interface ILivroValidator
    {
        Task<Livro?> ValidarExiste(Guid id);
        Task ValidarCriar(LivroDto dto);
    }
}
