using Xunit;
using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using PortariaLight.Domain.Entities;
using PortariaLight.Tests.Integration.Fixtures;

namespace PortariaLight.Tests.Integration.Controllers;

[Collection("PortariaLight Integration Tests")]
public class MoradorControllerIntegrationTests : IClassFixture<PortariaLightWebApplicationFactory>
{
	private readonly HttpClient _client;

	public MoradorControllerIntegrationTests(PortariaLightWebApplicationFactory factory)
	{
		_client = factory.CreateClient();
	}

	// ── GET /api/Morador ──────────────────────────────────────────────────────

	[Fact]
	public async Task GetAll_QuandoRequisicaoValida_RetornaStatusOk()
	{
		// Arrange
		// Act
		var response = await _client.GetAsync("/api/Morador");

		// Assert
		response.StatusCode.Should().Be(HttpStatusCode.OK);
	}

	[Fact]
	public async Task GetAll_QuandoRequisicaoValida_RetornaContentTypeJson()
	{
		// Arrange
		// Act
		var response = await _client.GetAsync("/api/Morador");

		// Assert
		response.Content.Headers.ContentType?.MediaType
			.Should().Be("application/json");
	}

	// ── GET /api/Morador/{id} ─────────────────────────────────────────────────

	[Fact]
	public async Task GetById_QuandoMoradorExiste_RetornaDadosCorretamente()
	{
		// Arrange
		var id = 1; // seed inseriu id=1

		// Act
		var morador = await _client.GetFromJsonAsync<Morador>($"/api/Morador/{id}");

		// Assert
		morador.Should().NotBeNull();
		morador!.IdMorador.Should().Be(1);
		morador.Nome.Should().Be("João Teste");
	}

	[Fact]
	public async Task GetById_QuandoMoradorNaoExiste_RetornaNotFound()
	{
		// Arrange
		// Act
		var response = await _client.GetAsync("/api/Morador/99999");

		// Assert
		response.StatusCode.Should().Be(HttpStatusCode.NotFound);
	}

	// ── POST /api/Morador ─────────────────────────────────────────────────────

	[Fact]
	public async Task Create_ComDadosValidos_RetornaStatusCreated()
	{
		// Arrange
		var novoMorador = new Morador
		{
			Nome = "Novo Morador Integração",
			Contato = "11900001111",
			IdApartamento = 1
		};

		// Act
		var response = await _client.PostAsJsonAsync("/api/Morador", novoMorador);

		// Assert
		response.StatusCode.Should().Be(HttpStatusCode.Created);
	}

	[Fact]
	public async Task Create_ComDadosValidos_RetornaMoradorComIdAtribuido()
	{
		// Arrange
		var novoMorador = new Morador
		{
			Nome = "Morador ID Test",
			Contato = "11900002222",
			IdApartamento = 2
		};

		// Act
		var response = await _client.PostAsJsonAsync("/api/Morador", novoMorador);
		var criado = await response.Content.ReadFromJsonAsync<Morador>();

		// Assert
		criado.Should().NotBeNull();
		criado!.IdMorador.Should().BeGreaterThan(0);
	}

	// ── DELETE /api/Morador/{id} ──────────────────────────────────────────────

	[Fact]
	public async Task Delete_QuandoMoradorNaoExiste_RetornaNotFound()
	{
		// Arrange
		// Act
		var response = await _client.DeleteAsync("/api/Morador/99999");

		// Assert
		response.StatusCode.Should().Be(HttpStatusCode.NotFound);
	}
}