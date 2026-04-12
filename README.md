# PortariaLight API

Sistema de gerenciamento de portaria de condomínio desenvolvido com **ASP.NET Core 8**, **Oracle Database** e arquitetura em camadas (Domain, Application, Infrastructure, API).

---

## 🏗️ Arquitetura

```
PortariaLight.sln
├── PortariaLight.Api            → Controllers, Health Checks, Program.cs
├── PortariaLight.Application    → Services, DTOs, Interfaces
├── PortariaLight.Domain         → Entities, Repository Interfaces
├── PortariaLight.Infrastructure → AppDbContext, Repositories (Oracle EF Core)
├── PortariaLight.Web            → Frontend Blazor WebAssembly
├── PortariaLight.Tests.Unit     → Testes Unitários (xUnit + Moq)
└── PortariaLight.Tests.Integration → Testes de Integração (WebApplicationFactory)
```

---

## ⚙️ Tecnologias

| Camada            | Tecnologia                                     |
|-------------------|------------------------------------------------|
| Framework         | ASP.NET Core 8                                 |
| ORM               | Entity Framework Core 8 + Oracle               |
| Logging           | **Serilog** (console + arquivo rotativo)       |
| Tracing           | **OpenTelemetry** (AspNetCore + EFCore)        |
| Métricas          | **OpenTelemetry + Prometheus** (`/metrics`)    |
| Health Checks     | `Microsoft.Extensions.Diagnostics.HealthChecks`|
| Documentação      | Swagger / OpenAPI                              |
| Testes unitários  | **xUnit** + **Moq** + **FluentAssertions**     |
| Testes integração | **WebApplicationFactory** + InMemory DB        |

---

## 🚀 Como executar

### Pré-requisitos
- .NET 8 SDK
- Oracle Database (ou usar InMemory para testes)

### 1. Clonar e restaurar
```bash
git clone https://github.com/FabricioJ0se/Sprint01-.NET.git
cd Sprint01-.NET
dotnet restore
```

### 2. Configurar connection string
Edite `PortariaLight.Api/appsettings.json`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "User Id=SEU_USER;Password=SUA_SENHA;Data Source=oracle.host:1521/orcl;"
  }
}
```

### 3. Rodar a API
```bash
cd PortariaLight.Api
dotnet run
```

A API ficará disponível em `http://localhost:5000` (Swagger na raiz `/`).

---

## 🩺 Health Checks e Monitoramento

### Endpoints de saúde

| Endpoint          | Descrição                              |
|-------------------|----------------------------------------|
| `GET /health`     | Saúde geral (todos os checks)          |
| `GET /health/ready` | Apenas conectividade com o banco Oracle |
| `GET /health/live`  | Apenas disponibilidade da API          |
| `GET /metrics`    | Métricas Prometheus                    |

### Exemplo de resposta `/health`
```json
{
  "status": "Healthy",
  "checks": [
    { "name": "oracle-db", "status": "Healthy" },
    { "name": "api-self",  "status": "Healthy" }
  ]
}
```

### Logs estruturados
Os logs ficam em `logs/portarialight-YYYYMMDD.txt` e também no console, com o formato:
```
[14:32:01 INF] a3f2... HTTP GET /api/Encomenda respondeu 200 em 12.4ms
[14:32:02 WRN] a3f2... Morador não encontrado para id=99
```

Cada requisição recebe um **X-Correlation-ID** automático rastreável em todos os logs.

### Métricas Prometheus
Após iniciar a aplicação, acesse `http://localhost:5000/metrics` para ver métricas como:
- `http_server_duration_ms` — tempo de resposta por rota
- `process_cpu_seconds_total` — uso de CPU
- `dotnet_gc_collections_total` — coletas de GC

---

## 🧪 Executando os Testes

### Todos os testes
```bash
dotnet test
```

### Apenas testes unitários
```bash
dotnet test PortariaLight.Tests.Unit/
```

### Apenas testes de integração
```bash
dotnet test PortariaLight.Tests.Integration/
```

### Com cobertura de código
```bash
dotnet test --collect:"XPlat Code Coverage"
```

### Verbose (ver cada teste)
```bash
dotnet test --logger "console;verbosity=detailed"
```

---

## 📋 Endpoints da API

### Encomendas
| Método | Rota                                   | Descrição                         |
|--------|----------------------------------------|-----------------------------------|
| GET    | `/api/Encomenda`                       | Lista todas as encomendas         |
| GET    | `/api/Encomenda/{id}`                  | Busca encomenda por ID            |
| POST   | `/api/Encomenda`                       | Cadastra nova encomenda           |
| PUT    | `/api/Encomenda/{id}`                  | Atualiza encomenda                |
| DELETE | `/api/Encomenda/{id}`                  | Remove encomenda                  |
| GET    | `/api/Encomenda/morador/{moradorId}`   | Encomendas de um morador          |
| GET    | `/api/Encomenda/nao-retiradas`         | Encomendas ainda não retiradas    |

### Moradores
| Método | Rota                | Descrição               |
|--------|---------------------|-------------------------|
| GET    | `/api/Morador`      | Lista todos os moradores|
| GET    | `/api/Morador/{id}` | Busca morador por ID    |
| POST   | `/api/Morador`      | Cadastra novo morador   |
| PUT    | `/api/Morador/{id}` | Atualiza morador        |
| DELETE | `/api/Morador/{id}` | Remove morador          |

### Apartamentos · Portarias · Retiradas
Seguem o mesmo padrão CRUD acima — consulte o Swagger em `/`.

---

## 🧪 Organização dos Testes

```
PortariaLight.Tests.Unit/
└── Application/
    └── Services/
        ├── EncomendaServiceTests.cs    ← 8 testes unitários
        └── MoradorServiceTests.cs      ← 8 testes unitários

PortariaLight.Tests.Integration/
├── Fixtures/
│   ├── PortariaLightWebApplicationFactory.cs   ← substitui Oracle por InMemory
│   └── PortariaLightCollectionFixture.cs       ← Collection fixture compartilhada
└── Controllers/
    ├── EncomendaControllerIntegrationTests.cs  ← 8 testes de integração
    └── MoradorControllerIntegrationTests.cs    ← 7 testes de integração
```

**Nomenclatura dos testes:** `MetodoTestado_Cenario_ResultadoEsperado`  
Exemplo: `CreateEncomendaAsync_QuandoMoradorNaoExiste_LancaArgumentException`

---

## 👥 Equipe

| Nome | RM |
|------|----|
| Fabrício José | RM560694 |

---

## 📄 Licença

Projeto acadêmico — FIAP 2025.