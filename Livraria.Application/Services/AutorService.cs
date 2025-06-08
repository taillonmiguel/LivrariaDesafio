using Livraria.Application.Dtos;
using Livraria.Application.Interfaces;
using Livraria.Application.ViewModels;
using Livraria.Domain.Entities;
using Livraria.Domain.Repositories;

namespace Livraria.Application.Services
{
    public class AutorService : IAutorService
    {
        private readonly IAutorRepository _repository;

        public AutorService(IAutorRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<AutorViewModel>> GetAllAsync()
        {
            var autores = await _repository.GetAllAsync();
            return autores.Select(x => new AutorViewModel { Id = x.Id, Nome = x.Nome });
        }

        public async Task<AutorViewModel> GetByIdAsync(Guid id)
        {
            var autor = await _repository.GetByIdAsync(id);
            return new AutorViewModel { Id = autor.Id, Nome = autor.Nome };
        }

        public async Task<AutorViewModel> CreateAsync(AutorDto dto)
        {
            var autor = new Autor(dto.Nome);
            await _repository.AddAsync(autor);
            await _repository.SaveChangesAsync();
            return new AutorViewModel { Id = autor.Id, Nome = autor.Nome };
        }

        public async Task<AutorViewModel> UpdateAsync(Guid id, AutorDto dto)
        {
            var autor = await _repository.GetByIdAsync(id);
            autor.Atualizar(dto.Nome);
            _repository.Update(autor);
            await _repository.SaveChangesAsync();
            return new AutorViewModel { Id = autor.Id, Nome = autor.Nome };
        }

        public async Task DeleteAsync(Guid id)
        {
            var autor = await _repository.GetByIdAsync(id);
            _repository.Remove(autor);
            await _repository.SaveChangesAsync();
        }
    }
}