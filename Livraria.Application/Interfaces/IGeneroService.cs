using Livraria.Application.Dtos;
using Livraria.Application.ViewModels;

namespace Livraria.Application.Interfaces
{
    public interface IGeneroService
    {
        Task<IEnumerable<GeneroViewModel>> GetAllAsync();
        Task<GeneroViewModel?> GetByIdAsync(Guid id);
        Task<GeneroViewModel> CreateAsync(GeneroDto dto);
        Task<GeneroViewModel> UpdateAsync(Guid id, GeneroDto dto);
        Task DeleteAsync(Guid id);
    }
}