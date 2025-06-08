using Livraria.Application.Dtos;
using Livraria.Domain.Entities;

namespace Livraria.Application.Interfaces
{
    public interface IAutorValidator
    {
        Task ValidarCriar(AutorDto dto);
        Task<Autor?> ValidarExiste(Guid id);
    }
}
