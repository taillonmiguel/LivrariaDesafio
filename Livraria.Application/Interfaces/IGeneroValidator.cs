using Livraria.Application.Dtos;
using Livraria.Domain.Entities;

namespace Livraria.Application.Interfaces
{
    public interface IGeneroValidator
    {
        Task ValidarCriar(GeneroDto dto);
        Task<Genero?> ValidarExiste(Guid id);
    }
}
