using Livraria.Application.Dtos;
using Livraria.Application.Services;
using Livraria.Application.Interfaces;
using Livraria.Domain.Entities;
using Livraria.Domain.Repositories;
using Livraria.Shared.DomainValidation;
using Moq;

namespace Livraria.Tests.Services;

public class AutorServiceTests
{
    private readonly Mock<IAutorRepository> _autorRepositoryMock = new();
    private readonly Mock<IDomainValidation> _domainValidationMock = new();
    private readonly Mock<IAutorValidator> _autorValidatorMock = new();

    [Fact]
    public async Task GetAllAsync_DeveRetornarListaDeAutores()
    {
        _autorRepositoryMock.Setup(r => r.GetAllAsync())
            .ReturnsAsync([new Autor("Autor A")]);

        var service = new AutorService(_autorRepositoryMock.Object, _domainValidationMock.Object, _autorValidatorMock.Object);

        var result = await service.GetAllAsync();

        Assert.Single(result);
    }

    [Fact]
    public async Task GetByIdAsync_DeveRetornarAutor()
    {
        var autor = new Autor("Autor B");
        _autorValidatorMock.Setup(v => v.ValidarExiste(It.IsAny<Guid>())).ReturnsAsync(autor);

        var service = new AutorService(_autorRepositoryMock.Object, _domainValidationMock.Object, _autorValidatorMock.Object);

        var result = await service.GetByIdAsync(Guid.NewGuid());

        Assert.Equal("Autor B", result.Nome);
    }

    [Fact]
    public async Task CreateAsync_DeveAdicionarAutor()
    {
        var dto = new AutorDto { Nome = "Autor C"};

        var service = new AutorService(_autorRepositoryMock.Object, _domainValidationMock.Object, _autorValidatorMock.Object);

        var result = await service.CreateAsync(dto);

        Assert.Equal("Autor C", result.Nome);
    }

    [Fact]
    public async Task UpdateAsync_DeveAtualizarAutor()
    {
        var autor = new Autor("Autor D");
        _autorValidatorMock.Setup(v => v.ValidarExiste(It.IsAny<Guid>())).ReturnsAsync(autor);

        var service = new AutorService(_autorRepositoryMock.Object, _domainValidationMock.Object, _autorValidatorMock.Object);

        var result = await service.UpdateAsync(Guid.NewGuid(), new AutorDto { Nome = "Autor D+" });

        Assert.Equal("Autor D+", result.Nome);
    }

    [Fact]
    public async Task DeleteAsync_DeveRemoverAutor()
    {
        var autor = new Autor("Autor E");
        _autorValidatorMock.Setup(v => v.ValidarExiste(It.IsAny<Guid>())).ReturnsAsync(autor);

        var service = new AutorService(_autorRepositoryMock.Object, _domainValidationMock.Object, _autorValidatorMock.Object);

        await service.DeleteAsync(Guid.NewGuid());

        _autorRepositoryMock.Verify(r => r.Remove(It.IsAny<Autor>()), Times.Once);
    }
}
