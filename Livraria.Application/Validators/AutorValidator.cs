using Livraria.Application.Dtos;
using Livraria.Application.Interfaces;
using Livraria.Domain.Entities;
using Livraria.Domain.Repositories;
using Livraria.Shared.DomainValidation;

namespace Livraria.Application.Validators
{
    public class AutorValidator : IAutorValidator
    {
        private readonly IDomainValidation _domainValidation;
        private readonly IAutorRepository _autorRepository;

        public AutorValidator(IDomainValidation domainValidation, IAutorRepository autorRepository)
        {
            _domainValidation = domainValidation;
            _autorRepository = autorRepository;
        }

        public async Task ValidarCriar(AutorDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Nome))
                _domainValidation.Add("nome", "O nome do autor é obrigatório.");
        }

        public async Task<Autor?> ValidarExiste(Guid id)
        {
            var autor = await _autorRepository.GetByIdAsync(id);

            if (autor is null)
            {
                _domainValidation.Add("Autor não encontrado.");
                return null;
            }

            return autor!;
        }
    }
}
