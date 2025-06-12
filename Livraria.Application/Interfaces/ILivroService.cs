using Livraria.Application.Dtos;
using Livraria.Application.ViewModels;
using Livraria.Shared.Dto;

namespace Livraria.Application.Interfaces
{
    public interface ILivroService
    {
        Task<IEnumerable<LivroViewModel>> GetAllAsync();
        Task<LivroViewModel> GetByIdAsync(Guid id);
        Task<LivroViewModel> CreateAsync(LivroDto dto);
        Task<LivroViewModel> UpdateAsync(Guid id, LivroDto dto);
        Task DeleteAsync(Guid id);
        Task<PagedResult<LivroViewModel>> SearchAsync(LivroFilterDto dto);
    }
}