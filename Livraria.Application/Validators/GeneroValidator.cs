using Livraria.Application.Dtos;
using Livraria.Application.Interfaces;
using Livraria.Domain.Entities;
using Livraria.Domain.Repositories;
using Livraria.Shared.DomainValidation;

namespace Livraria.Application.Validators
{
    internal class GeneroValidator : IGeneroValidator
    {
        private readonly IDomainValidation _domainValidation;
        private readonly IGeneroRepository _generoRepository;

        public GeneroValidator(IDomainValidation domainValidation, IGeneroRepository generoRepository)
        {
            _domainValidation = domainValidation;
            _generoRepository = generoRepository;
        }

        public async Task ValidarCriar(GeneroDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Nome))
                _domainValidation.Add("nome", "O nome do gênero é obrigatório.");

            var genero = await _generoRepository.ExistsAsync(x => x.Nome, dto.Nome);

            if (genero)
                _domainValidation.Add("nome", "Já existe um gênero com esse nome.");
        }

        public async Task<Genero?> ValidarExiste(Guid id)
        {
            var genero = await _generoRepository.GetByIdAsync(id);

            if (genero is null)
            {
                _domainValidation.Add("Gênero não encontrado.");
                return null;
            }

            return genero!;
        }
    }
}
