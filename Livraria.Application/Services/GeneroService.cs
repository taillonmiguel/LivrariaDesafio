using Livraria.Application.Dtos;
using Livraria.Application.Interfaces;
using Livraria.Application.Validators;
using Livraria.Application.ViewModels;
using Livraria.Domain.Entities;
using Livraria.Domain.Repositories;
using Livraria.Shared.DomainValidation;

namespace Livraria.Application.Services
{
    public class GeneroService : IGeneroService
    {
        private readonly IGeneroRepository _repository;
        private readonly IDomainValidation _domainValidation;
        private readonly IGeneroValidator _generoValidator;

        public GeneroService(IGeneroRepository repository, 
            IDomainValidation domainValidation, IGeneroValidator generoValidator)
        {
            _repository = repository;
            _domainValidation = domainValidation;
            _generoValidator = generoValidator;
        }

        public async Task<IEnumerable<GeneroViewModel>> GetAllAsync()
        {
            var generos = await _repository.GetAllAsync();
            return generos.Select(x => new GeneroViewModel { Id = x.Id, Nome = x.Nome });
        }

        public async Task<GeneroViewModel?> GetByIdAsync(Guid id)
        {
            var genero = await _repository.GetByIdAsync(id);

            if (genero is null) 
            {
                return null;
            }

            return new GeneroViewModel { Id = genero.Id, Nome = genero.Nome };
        }

        public async Task<GeneroViewModel> CreateAsync(GeneroDto dto)
        {
            await _generoValidator.ValidarCriar(dto);

            _domainValidation.EnsureValid();

            var genero = new Genero(dto.Nome);
            await _repository.AddAsync(genero);

            return new GeneroViewModel { Id = genero.Id, Nome = genero.Nome };
        }

        public async Task<GeneroViewModel> UpdateAsync(Guid id, GeneroDto dto)
        {
            var genero = await _generoValidator.ValidarExiste(id);

            _domainValidation.EnsureValid();

            genero.Atualizar(dto.Nome);
            _repository.Update(genero);

            return new GeneroViewModel { Id = genero.Id, Nome = genero.Nome };
        }

        public async Task DeleteAsync(Guid id)
        {
            var genero = await _generoValidator.ValidarExiste(id);

            _domainValidation.EnsureValid();

            _repository.Remove(genero);
        }
    }
}