using Livraria.Application.Dtos;
using Livraria.Application.Services;
using Livraria.Application.Interfaces;
using Livraria.Domain.Entities;
using Livraria.Domain.Repositories;
using Livraria.Shared.DomainValidation;
using Moq;

namespace Livraria.Tests.Services;

public class LivroServiceTests
{
    private readonly Mock<ILivroRepository> _livroRepositoryMock = new();
    private readonly Mock<IDomainValidation> _domainValidationMock = new();
    private readonly Mock<ILivroValidator> _livroValidatorMock = new();

    [Fact]
    public async Task GetAllAsync_DeveRetornarListaDeLivros()
    {
        _livroRepositoryMock.Setup(r => r.GetAllAsync())
            .ReturnsAsync([new Livro("Livro X", "Desc", Guid.NewGuid(), Guid.NewGuid())]);

        var service = new LivroService(_livroRepositoryMock.Object, _domainValidationMock.Object, _livroValidatorMock.Object);

        var result = await service.GetAllAsync();

        Assert.Single(result);
    }

    [Fact]
    public async Task GetByIdAsync_DeveRetornarLivro()
    {
        var livro = new Livro("Livro Y", "Desc", Guid.NewGuid(), Guid.NewGuid());
        _livroValidatorMock.Setup(v => v.ValidarExiste(It.IsAny<Guid>())).ReturnsAsync(livro);

        var service = new LivroService(_livroRepositoryMock.Object, _domainValidationMock.Object, _livroValidatorMock.Object);

        var result = await service.GetByIdAsync(Guid.NewGuid());

        Assert.Equal("Livro Y", result.Titulo);
    }

    [Fact]
    public async Task CreateAsync_DeveAdicionarLivro()
    {
        var dto = new LivroDto
        {
            Titulo = "Livro Z",
            Descricao = "Desc",
            AutorId = Guid.NewGuid(),
            GeneroId = Guid.NewGuid()
        };

        var service = new LivroService(_livroRepositoryMock.Object, _domainValidationMock.Object, _livroValidatorMock.Object);

        var result = await service.CreateAsync(dto);

        Assert.Equal("Livro Z", result.Titulo);
    }

    [Fact]
    public async Task UpdateAsync_DeveAtualizarLivro()
    {
        var livro = new Livro("Livro A", "Desc", Guid.NewGuid(), Guid.NewGuid());
        _livroValidatorMock.Setup(v => v.ValidarExiste(It.IsAny<Guid>())).ReturnsAsync(livro);

        var service = new LivroService(_livroRepositoryMock.Object, _domainValidationMock.Object, _livroValidatorMock.Object);

        var result = await service.UpdateAsync(Guid.NewGuid(), new LivroDto
        {
            Titulo = "Livro A+",
            Descricao = "Nova Desc",
            AutorId = Guid.NewGuid(),
            GeneroId = Guid.NewGuid()
        });

        Assert.Equal("Livro A+", result.Titulo);
    }

    [Fact]
    public async Task DeleteAsync_DeveRemoverLivro()
    {
        var livro = new Livro("Livro B", "Desc", Guid.NewGuid(), Guid.NewGuid());
        _livroValidatorMock.Setup(v => v.ValidarExiste(It.IsAny<Guid>())).ReturnsAsync(livro);

        var service = new LivroService(_livroRepositoryMock.Object, _domainValidationMock.Object, _livroValidatorMock.Object);

        await service.DeleteAsync(Guid.NewGuid());

        _livroRepositoryMock.Verify(r => r.Remove(It.IsAny<Livro>()), Times.Once);
    }
}
