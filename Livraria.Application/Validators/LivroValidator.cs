using Livraria.Application.Dtos;
using Livraria.Application.Interfaces;
using Livraria.Domain.Entities;
using Livraria.Domain.Repositories;
using Livraria.Shared.DomainValidation;

namespace Livraria.Application.Validators
{
    public class LivroValidator : ILivroValidator
    {
        private readonly IDomainValidation _domainValidation;
        private readonly ILivroRepository _livroRepository;

        public LivroValidator(IDomainValidation domainValidation, ILivroRepository livroRepository)
        {
            _domainValidation = domainValidation;
            _livroRepository = livroRepository;
        }

        public async Task ValidarCriar(LivroDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Titulo))
                _domainValidation.Add("titulo", "O título do livro é obrigatório.");

            if (string.IsNullOrWhiteSpace(dto.Descricao))
                _domainValidation.Add("descricao", "A descrição do livro é obrigatória.");

            if (dto.AutorId == Guid.Empty)
                _domainValidation.Add("autorId", "O autor do livro é obrigatório.");

            if (dto.GeneroId == Guid.Empty)
                _domainValidation.Add("generoId", "O gênero do livro é obrigatório.");
        }

        public async Task<Livro?> ValidarExiste(Guid id)
        {
            var livro = await _livroRepository.GetByIdAsync(id);

            if (livro is null)
            {
                _domainValidation.Add("Livro não encontrado.");
                return null;
            }

            return livro!;
        }
    }
}
