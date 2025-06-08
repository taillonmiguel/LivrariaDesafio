using Livraria.Application.Dtos;
using Livraria.Application.Interfaces;
using Livraria.Application.ViewModels;
using Livraria.Domain.Entities;
using Livraria.Domain.Repositories;

namespace Livraria.Application.Services
{
    public class GeneroService : IGeneroService
    {
        private readonly IGeneroRepository _repository;

        public GeneroService(IGeneroRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<GeneroViewModel>> GetAllAsync()
        {
            var generos = await _repository.GetAllAsync();
            return generos.Select(x => new GeneroViewModel { Id = x.Id, Nome = x.Nome });
        }

        public async Task<GeneroViewModel> GetByIdAsync(Guid id)
        {
            var genero = await _repository.GetByIdAsync(id);
            return new GeneroViewModel { Id = genero.Id, Nome = genero.Nome };
        }

        public async Task<GeneroViewModel> CreateAsync(GeneroDto dto)
        {
            var genero = new Genero(dto.Nome);
            await _repository.AddAsync(genero);
            await _repository.SaveChangesAsync();
            return new GeneroViewModel { Id = genero.Id, Nome = genero.Nome };
        }

        public async Task<GeneroViewModel> UpdateAsync(Guid id, GeneroDto dto)
        {
            var genero = await _repository.GetByIdAsync(id);
            genero.Atualizar(dto.Nome);
            _repository.Update(genero);
            await _repository.SaveChangesAsync();
            return new GeneroViewModel { Id = genero.Id, Nome = genero.Nome };
        }

        public async Task DeleteAsync(Guid id)
        {
            var genero = await _repository.GetByIdAsync(id);
            _repository.Remove(genero);
            await _repository.SaveChangesAsync();
        }
    }
}