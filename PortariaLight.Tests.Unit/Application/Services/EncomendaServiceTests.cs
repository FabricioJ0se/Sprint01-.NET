using Xunit;
using FluentAssertions;
using Moq;
using PortariaLight.Application.Services;
using PortariaLight.Domain.Entities;
using PortariaLight.Domain.Repositories;

namespace PortariaLight.Tests.Unit.Application.Services;

public class EncomendaServiceTests
{
    private readonly Mock<IEncomendaRepository> _encomendaRepoMock;
    private readonly Mock<IMoradorRepository> _moradorRepoMock;
    private readonly Mock<IRetiradaRepository> _retiradaRepoMock;
    private readonly EncomendaService _sut;

    public EncomendaServiceTests()
    {
        _encomendaRepoMock = new Mock<IEncomendaRepository>();
        _moradorRepoMock = new Mock<IMoradorRepository>();
        _retiradaRepoMock = new Mock<IRetiradaRepository>();

        _sut = new EncomendaService(
            _encomendaRepoMock.Object,
            _moradorRepoMock.Object,
            _retiradaRepoMock.Object);
    }

    [Fact]
    public async Task GetAllEncomendasAsync_QuandoExistemEncomendas_RetornaListaCompleta()
    {
        // Arrange
        var encomendasEsperadas = new List<Encomenda>
        {
            new() { IdEncomenda = 1, Descricao = "Caixa Amazon", IdMorador = 1, DataRecebimento = DateTime.Today },
            new() { IdEncomenda = 2, Descricao = "Envelope Banco", IdMorador = 2, DataRecebimento = DateTime.Today }
        };

        _encomendaRepoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(encomendasEsperadas);

        // Act
        var resultado = await _sut.GetAllEncomendasAsync();

        // Assert
        resultado.Should().HaveCount(2);
        resultado.Should().BeEquivalentTo(encomendasEsperadas);
        _encomendaRepoMock.Verify(r => r.GetAllAsync(), Times.Once);
    }

    [Fact]
    public async Task GetAllEncomendasAsync_QuandoNaoExistemEncomendas_RetornaListaVazia()
    {
        // Arrange
        _encomendaRepoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(Enumerable.Empty<Encomenda>());

        // Act
        var resultado = await _sut.GetAllEncomendasAsync();

        // Assert
        resultado.Should().BeEmpty();
    }

    [Fact]
    public async Task GetEncomendaByIdAsync_QuandoEncomendaExiste_RetornaEncomenda()
    {
        // Arrange
        var encomenda = new Encomenda { IdEncomenda = 1, Descricao = "Caixa de livros", IdMorador = 1, DataRecebimento = DateTime.Today };
        _encomendaRepoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(encomenda);

        // Act
        var resultado = await _sut.GetEncomendaByIdAsync(1);

        // Assert
        resultado.Should().NotBeNull();
        resultado!.IdEncomenda.Should().Be(1);
    }

    [Fact]
    public async Task GetEncomendaByIdAsync_QuandoEncomendaNaoExiste_RetornaNull()
    {
        // Arrange
        _encomendaRepoMock.Setup(r => r.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Encomenda?)null);

        // Act
        var resultado = await _sut.GetEncomendaByIdAsync(999);

        // Assert
        resultado.Should().BeNull();
    }
}
