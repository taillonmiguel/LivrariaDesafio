using Livraria.Application.Dtos;
using Livraria.Application.ViewModels;

namespace Livraria.Application.Interfaces
{
    public interface IAutorService
    {
        Task<IEnumerable<AutorViewModel>> GetAllAsync();
        Task<AutorViewModel> GetByIdAsync(Guid id);
        Task<AutorViewModel> CreateAsync(AutorDto dto);
        Task<AutorViewModel> UpdateAsync(Guid id, AutorDto dto);
        Task DeleteAsync(Guid id);
    }
}
