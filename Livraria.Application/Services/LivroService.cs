using Livraria.Application.Dtos;
using Livraria.Application.Interfaces;
using Livraria.Application.ViewModels;
using Livraria.Domain.Entities;
using Livraria.Domain.Repositories;
using Livraria.Shared.DomainValidation;

namespace Livraria.Application.Services
{
    public class LivroService : ILivroService
    {
        private readonly ILivroRepository _repository;
        private readonly IDomainValidation _domainValidation;
        private readonly ILivroValidator _validator;

        public LivroService(ILivroRepository repository, IDomainValidation domainValidation, ILivroValidator validator)
        {
            _repository = repository;
            _domainValidation = domainValidation;
            _validator = validator;
        }

        public async Task<IEnumerable<LivroViewModel>> GetAllAsync()
        {
            var livros = await _repository.GetAllAsync();

            return livros.Select(x => new LivroViewModel
            {
                Id = x.Id,
                Titulo = x.Titulo,
                Descricao = x.Descricao,
                AutorId = x.AutorId,
                GeneroId = x.GeneroId
            });
        }

        public async Task<LivroViewModel> GetByIdAsync(Guid id)
        {
            var livro = await _validator.ValidarExiste(id);

            if (livro is null)
            {
                return null!;
            }

            return new LivroViewModel
            {
                Id = livro.Id,
                Titulo = livro.Titulo,
                Descricao = livro.Descricao,
                AutorId = livro.AutorId,
                GeneroId = livro.GeneroId
            };
        }

        public async Task<LivroViewModel> CreateAsync(LivroDto dto)
        {
            await _validator.ValidarCriar(dto);

            _domainValidation.EnsureValid();

            var livro = new Livro(dto.Titulo, dto.Descricao, dto.AutorId, dto.GeneroId);

            await _repository.AddAsync(livro);

            return new LivroViewModel
            {
                Id = livro.Id,
                Titulo = livro.Titulo,
                Descricao = livro.Descricao,
                AutorId = livro.AutorId,
                GeneroId = livro.GeneroId
            };
        }

        public async Task<LivroViewModel> UpdateAsync(Guid id, LivroDto dto)
        {
            var livro = await _validator.ValidarExiste(id);

            _domainValidation.EnsureValid();

            livro.Atualizar(dto.Titulo, dto.Descricao, dto.AutorId, dto.GeneroId);
            _repository.Update(livro);

            return new LivroViewModel
            {
                Id = livro.Id,
                Titulo = livro.Titulo,
                Descricao = livro.Descricao,
                AutorId = livro.AutorId,
                GeneroId = livro.GeneroId
            };
        }

        public async Task DeleteAsync(Guid id)
        {
            var livro = await _validator.ValidarExiste(id);

            _domainValidation.EnsureValid();

            _repository.Remove(livro);
        }
    }
}
