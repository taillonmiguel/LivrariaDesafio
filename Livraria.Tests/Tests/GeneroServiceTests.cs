using Livraria.Application.Dtos;
using Livraria.Application.Interfaces;
using Livraria.Application.Services;
using Livraria.Domain.Entities;
using Livraria.Domain.Repositories;
using Livraria.Shared.DomainValidation;
using Moq;
using Xunit;

namespace Livraria.Tests.Tests
{
    public class GeneroServiceTests
    {
        private readonly Mock<IGeneroRepository> _repositoryMock;
        private readonly Mock<IDomainValidation> _domainValidationMock;
        private readonly Mock<IGeneroValidator> _validatorMock;
        private readonly GeneroService _service;

        public GeneroServiceTests()
        {
            _repositoryMock = new Mock<IGeneroRepository>();
            _domainValidationMock = new Mock<IDomainValidation>();
            _validatorMock = new Mock<IGeneroValidator>();
            _service = new GeneroService(_repositoryMock.Object, _domainValidationMock.Object, _validatorMock.Object);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsList()
        {
            var data = new List<Genero> { new("Romance"), new("Aventura") };
            _repositoryMock.Setup(r => r.GetAllAsync()).ReturnsAsync(data);

            var result = await _service.GetAllAsync();

            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsGenero()
        {
            var genero = new Genero("Terror");
            _validatorMock.Setup(v => v.ValidarExiste(It.IsAny<Guid>())).ReturnsAsync(genero);

            var result = await _service.GetByIdAsync(Guid.NewGuid());

            Assert.Equal("Terror", result.Nome);
        }

        [Fact]
        public async Task CreateAsync_AddsGenero()
        {
            var dto = new GeneroDto { Nome = "Fantasia" };

            var result = await _service.CreateAsync(dto);

            _repositoryMock.Verify(r => r.AddAsync(It.IsAny<Genero>()), Times.Once);
            Assert.Equal("Fantasia", result.Nome);
        }

        [Fact]
        public async Task UpdateAsync_UpdatesGenero()
        {
            var genero = new Genero("Inicial");
            _validatorMock.Setup(v => v.ValidarExiste(It.IsAny<Guid>())).ReturnsAsync(genero);

            var dto = new GeneroDto { Nome = "Alterado" };

            var result = await _service.UpdateAsync(Guid.NewGuid(), dto);

            Assert.Equal("Alterado", result.Nome);
            _repositoryMock.Verify(r => r.Update(It.IsAny<Genero>()), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_RemovesGenero()
        {
            var genero = new Genero("Excluir");
            _validatorMock.Setup(v => v.ValidarExiste(It.IsAny<Guid>())).ReturnsAsync(genero);

            await _service.DeleteAsync(Guid.NewGuid());

            _repositoryMock.Verify(r => r.Remove(It.IsAny<Genero>()), Times.Once);
        }
    }

}
