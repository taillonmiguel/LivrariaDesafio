using Livraria.Application.Dtos;
using Livraria.Application.Interfaces;
using Livraria.Application.ViewModels;
using Livraria.Domain.Entities;
using Livraria.Domain.Repositories;
using Livraria.Shared.DomainValidation;

namespace Livraria.Application.Services
{
    public class AutorService : IAutorService
    {
        private readonly IAutorRepository _repository;
        private readonly IDomainValidation _domainValidation;
        private readonly IAutorValidator _validator;

        public AutorService(IAutorRepository repository, IDomainValidation domainValidation, IAutorValidator validator)
        {
            _repository = repository;
            _domainValidation = domainValidation;
            _validator = validator;
        }

        public async Task<IEnumerable<AutorViewModel>> GetAllAsync()
        {
            var autores = await _repository.GetAllAsync();

            return autores.Select(x => new AutorViewModel { Id = x.Id, Nome = x.Nome });
        }

        public async Task<AutorViewModel> GetByIdAsync(Guid id)
        {
            var autor = await _validator.ValidarExiste(id);

            if(autor is null)
            {
                return null!;
            }

            return new AutorViewModel { Id = autor.Id, Nome = autor.Nome };
        }

        public async Task<AutorViewModel> CreateAsync(AutorDto dto)
        {
            await _validator.ValidarCriar(dto);

            _domainValidation.EnsureValid();

            var autor = new Autor(dto.Nome);

            await _repository.AddAsync(autor);

            return new AutorViewModel { Id = autor.Id, Nome = autor.Nome };
        }

        public async Task<AutorViewModel> UpdateAsync(Guid id, AutorDto dto)
        {
            var autor = await _validator.ValidarExiste(id);

            _domainValidation.EnsureValid();

            autor.Atualizar(dto.Nome);
            _repository.Update(autor);

            return new AutorViewModel { Id = autor.Id, Nome = autor.Nome };
        }

        public async Task DeleteAsync(Guid id)
        {
            var autor = await _validator.ValidarExiste(id);

            _domainValidation.EnsureValid();

            _repository.Remove(autor);
        }
    }
}
