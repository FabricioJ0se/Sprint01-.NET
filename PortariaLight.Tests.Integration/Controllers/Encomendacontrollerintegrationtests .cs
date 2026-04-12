using Xunit;
using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using PortariaLight.Domain.Entities;
using PortariaLight.Tests.Integration.Fixtures;

namespace PortariaLight.Tests.Integration.Controllers;

[Collection("PortariaLight Integration Tests")]
public class EncomendaControllerIntegrationTests : IClassFixture<PortariaLightWebApplicationFactory>
{
	private readonly HttpClient _client;

	public EncomendaControllerIntegrationTests(PortariaLightWebApplicationFactory factory)
	{
		_client = factory.CreateClient();
	}

	// ── GET /api/Encomenda ────────────────────────────────────────────────────

	[Fact]
	public async Task GetAll_QuandoRequisicaoValida_RetornaStatusOk()
	{
		// Arrange
		// (banco seed foi aplicado na factory)

		// Act
		var response = await _client.GetAsync("/api/Encomenda");

		// Assert
		response.StatusCode.Should().Be(HttpStatusCode.OK);
	}

	[Fact]
	public async Task GetAll_QuandoRequisicaoValida_RetornaListaDeEncomendas()
	{
		// Arrange
		// Act
		var encomendas = await _client.GetFromJsonAsync<List<Encomenda>>("/api/Encomenda");

		// Assert
		encomendas.Should().NotBeNull();
		encomendas.Should().HaveCountGreaterThan(0);
	}

	// ── GET /api/Encomenda/{id} ───────────────────────────────────────────────

	[Fact]
	public async Task GetById_QuandoEncomendaExiste_RetornaStatusOk()
	{
		// Arrange
		var id = 1; // seed inseriu id=1

		// Act
		var response = await _client.GetAsync($"/api/Encomenda/{id}");

		// Assert
		response.StatusCode.Should().Be(HttpStatusCode.OK);
	}

	[Fact]
	public async Task GetById_QuandoEncomendaNaoExiste_RetornaNotFound()
	{
		// Arrange
		var idInexistente = 99999;

		// Act
		var response = await _client.GetAsync($"/api/Encomenda/{idInexistente}");

		// Assert
		response.StatusCode.Should().Be(HttpStatusCode.NotFound);
	}

	// ── POST /api/Encomenda ───────────────────────────────────────────────────

	[Fact]
	public async Task Create_ComDadosValidos_RetornaStatusCreated()
	{
		// Arrange
		var novaEncomenda = new Encomenda
		{
			Descricao = "Encomenda de Integração",
			IdMorador = 1,
			IdRetirada = 0,
			DataRecebimento = DateTime.Today
		};

		// Act
		var response = await _client.PostAsJsonAsync("/api/Encomenda", novaEncomenda);

		// Assert
		response.StatusCode.Should().Be(HttpStatusCode.Created);
	}

	[Fact]
	public async Task Create_ComMoradorInexistente_RetornaBadRequestOuErro()
	{
		// Arrange
		var encomendaInvalida = new Encomenda
		{
			Descricao = "Encomenda inválida",
			IdMorador = 99999, // morador não existe
			DataRecebimento = DateTime.Today
		};

		// Act
		var response = await _client.PostAsJsonAsync("/api/Encomenda", encomendaInvalida);

		// Assert — deve ser 400 (BadRequest) ou 500 com regra de negócio
		response.StatusCode.Should().BeOneOf(
			HttpStatusCode.BadRequest,
			HttpStatusCode.InternalServerError);
	}

	// ── DELETE /api/Encomenda/{id} ────────────────────────────────────────────

	[Fact]
	public async Task Delete_QuandoEncomendaNaoExiste_RetornaNotFound()
	{
		// Arrange
		var idInexistente = 99999;

		// Act
		var response = await _client.DeleteAsync($"/api/Encomenda/{idInexistente}");

		// Assert
		response.StatusCode.Should().Be(HttpStatusCode.NotFound);
	}

	// ── GET /api/Encomenda/nao-retiradas ─────────────────────────────────────

	[Fact]
	public async Task GetNaoRetiradas_QuandoRequisicaoValida_RetornaStatusOk()
	{
		// Arrange
		// Act
		var response = await _client.GetAsync("/api/Encomenda/nao-retiradas");

		// Assert
		response.StatusCode.Should().Be(HttpStatusCode.OK);
	}

	// ── GET /health ───────────────────────────────────────────────────────────

	[Fact]
	public async Task HealthCheck_QuandoApiEstaAtiva_RetornaStatusOkOuHealthy()
	{
		// Arrange
		// Act
		var response = await _client.GetAsync("/health");

		// Assert
		response.StatusCode.Should().BeOneOf(HttpStatusCode.OK, HttpStatusCode.ServiceUnavailable);
		// ServiceUnavailable é aceitável em CI sem Oracle;
		// o importante é que o endpoint responde.
	}
}