# PortariaLight API

Sistema de gerenciamento de portaria de condomínio desenvolvido com **ASP.NET Core 8**, **Oracle Database**, **MongoDB** e arquitetura em camadas (Domain, Application, Infrastructure, API).

---

## Arquitetura

```
PortariaLight.sln
├── PortariaLight.Api             → Controllers, Middleware, Health Checks, Program.cs
├── PortariaLight.Application     → Services, DTOs, Interfaces
├── PortariaLight.Domain          → Entities, Repository Interfaces
├── PortariaLight.Infrastructure  → AppDbContext, Repositories (Oracle EF Core + MongoDB)
├── PortariaLight.Web             → Frontend Blazor WebAssembly
├── PortariaLight.Tests.Unit      → Testes Unitários (xUnit + Moq)
└── PortariaLight.Tests.Integration → Testes de Integração (WebApplicationFactory)
```

### Diagrama de camadas

```
[ Client / Swagger ]
        ↓
[ PortariaLight.Api ]
   Controllers · Middleware · Auth (JWT)
        ↓
[ PortariaLight.Application ]
   Services · DTOs · Interfaces
        ↓
[ PortariaLight.Domain ]
   Entities · Repository Interfaces
        ↓
[ PortariaLight.Infrastructure ]
   Oracle (EF Core) · MongoDB · Repositories
```

---

## Tecnologias

| Camada             | Tecnologia                                              |
|--------------------|---------------------------------------------------------|
| Framework          | ASP.NET Core 8                                          |
| ORM                | Entity Framework Core 8 + Oracle                        |
| NoSQL              | **MongoDB** (log de acessos com TTL de 30 dias)         |
| Autenticação       | **JWT Bearer** (roles: Admin, Porteiro)                 |
| Logging            | **Serilog** (console + arquivo rotativo)                |
| Tracing            | **OpenTelemetry** (AspNetCore + EFCore)                 |
| Métricas           | **OpenTelemetry + Prometheus** (`/metrics`)             |
| Health Checks      | `Microsoft.Extensions.Diagnostics.HealthChecks`         |
| Documentação       | Swagger / OpenAPI (com suporte a Bearer)                |
| Testes unitários   | **xUnit** + **Moq** + **FluentAssertions**              |
| Testes integração  | **WebApplicationFactory** + InMemory DB                 |

---

## Como executar

### Pré-requisitos
- .NET 8 SDK
- Oracle Database (FIAP)
- MongoDB (opcional — logs são ignorados silenciosamente se indisponível)
  - Para subir localmente: `docker run -d -p 27017:27017 mongo`

### 1. Clonar e restaurar
```bash
git clone https://github.com/FabricioJ0se/Sprint01-.NET.git
cd Sprint01-.NET
dotnet restore
```

### 2. Configurar `appsettings.json`
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "User Id=RM560694;Password=Fiap#2025;Data Source=oracle.fiap.com.br:1521/orcl;"
  },
  "JwtSettings": {
    "Secret":   "PortariaLight_SuperSecreta_ChaveJWT_2025!",
    "Issuer":   "PortariaLight.Api",
    "Audience": "PortariaLight.Clients"
  },
  "MongoDbSettings": {
    "ConnectionString": "mongodb://localhost:27017",
    "DatabaseName":     "portarialight"
  }
}
```

### 3. Rodar a API
```bash
dotnet run --project PortariaLight.Api
```

A API ficará disponível em `https://localhost:7169` / `http://localhost:5204` (Swagger na raiz `/`).

---

## Autenticação JWT

Todos os endpoints (exceto `/api/auth/login`) exigem token JWT.

### Obter token
```http
POST /api/auth/login
Content-Type: application/json

{ "username": "admin", "password": "admin123" }
```

Resposta:
```json
{ "token": "eyJ...", "expiresIn": 3600 }
```

### Usuários disponíveis

| Username   | Password      | Role     | Permissões                        |
|------------|---------------|----------|-----------------------------------|
| `admin`    | `admin123`    | Admin    | Acesso total (GET, POST, PUT, DELETE) |
| `porteiro` | `porteiro123` | Porteiro | Apenas leitura e criação          |

### Usar no Swagger
Clique em **Authorize** (cadeado) → informe `Bearer {token}`.

### Usar via HTTP
```http
GET /api/morador
Authorization: Bearer eyJ...
```

---

## Endpoints da API

### Autenticação
| Método | Rota               | Descrição         | Auth |
|--------|--------------------|-------------------|------|
| POST   | `/api/auth/login`  | Gera token JWT    | ❌    |

### Moradores
| Método | Rota                              | Descrição                  | Role     |
|--------|-----------------------------------|----------------------------|----------|
| GET    | `/api/morador`                    | Lista com paginação/filtro | Qualquer |
| GET    | `/api/morador/{id}`               | Busca por ID               | Qualquer |
| POST   | `/api/morador`                    | Cadastra morador           | Admin    |
| PUT    | `/api/morador/{id}`               | Atualiza morador           | Admin    |
| DELETE | `/api/morador/{id}`               | Remove morador             | Admin    |
| GET    | `/api/morador/apartamento/{id}`   | Moradores do apartamento   | Qualquer |
| GET    | `/api/morador/contato/{contato}`  | Busca por contato          | Qualquer |

**Query params disponíveis:** `?page=1&pageSize=10&nome=João&sortBy=Nome&desc=false`

### Encomendas
| Método | Rota                                 | Descrição                    | Role     |
|--------|--------------------------------------|------------------------------|----------|
| GET    | `/api/encomenda`                     | Lista com paginação/filtro   | Qualquer |
| GET    | `/api/encomenda/{id}`                | Busca por ID                 | Qualquer |
| POST   | `/api/encomenda`                     | Cadastra encomenda           | Qualquer |
| PUT    | `/api/encomenda/{id}`                | Atualiza encomenda           | Qualquer |
| DELETE | `/api/encomenda/{id}`                | Remove encomenda             | Admin    |
| GET    | `/api/encomenda/morador/{moradorId}` | Encomendas de um morador     | Qualquer |
| GET    | `/api/encomenda/nao-retiradas`       | Encomendas não retiradas     | Qualquer |

**Query params disponíveis:** `?page=1&pageSize=10&moradorId=3&sortBy=DataRecebimento&desc=true`

### Apartamentos · Portarias · Retiradas
Seguem o mesmo padrão CRUD — consulte o Swagger em `/`.

### Logs de Acesso (MongoDB)
| Método | Rota                              | Descrição                   | Role  |
|--------|-----------------------------------|-----------------------------|-------|
| GET    | `/api/logacesso`                  | Últimos N logs (padrão 100) | Admin |
| GET    | `/api/logacesso/endpoint?endpoint=/api/morador` | Logs por endpoint | Admin |

---

## HATEOAS

Todos os endpoints de consulta retornam `_links` navegáveis:

```json
{
  "idMorador": 1,
  "nome": "João Silva",
  "_links": [
    { "rel": "self",        "href": "http://localhost:5204/api/morador/1", "method": "GET"    },
    { "rel": "update",      "href": "http://localhost:5204/api/morador/1", "method": "PUT"    },
    { "rel": "delete",      "href": "http://localhost:5204/api/morador/1", "method": "DELETE" },
    { "rel": "encomendas",  "href": "http://localhost:5204/api/encomenda/morador/1", "method": "GET" },
    { "rel": "apartamento", "href": "http://localhost:5204/api/apartamento/2", "method": "GET" }
  ]
}
```

### Paginação

```json
{
  "data": [...],
  "pagination": { "page": 1, "pageSize": 10, "total": 47, "totalPages": 5 },
  "_links": {
    "self": "http://localhost:5204/api/morador?page=1&pageSize=10",
    "next": "http://localhost:5204/api/morador?page=2&pageSize=10",
    "prev": null
  }
}
```

---

## MongoDB — Log de Acessos

Cada requisição à API é registrada automaticamente no MongoDB via `LogAcessoMiddleware`.

```json
{
  "_id":           "ObjectId",
  "timestamp":     "2025-04-12T14:32:01Z",
  "endpoint":      "/api/morador",
  "metodo":        "GET",
  "statusCode":    200,
  "usuarioNome":   "admin",
  "correlationId": "a3f2e1...",
  "duracaoMs":     12,
  "ipOrigem":      "192.168.1.10"
}
```

- Logs têm **TTL de 30 dias** (índice automático no MongoDB)
- Se o MongoDB estiver indisponível, os logs são ignorados silenciosamente sem afetar a API

---

## Health Checks e Monitoramento

| Endpoint            | Descrição                               |
|---------------------|-----------------------------------------|
| `GET /health`       | Saúde geral (todos os checks)           |
| `GET /health/ready` | Apenas conectividade com o Oracle       |
| `GET /health/live`  | Apenas disponibilidade da API           |
| `GET /metrics`      | Métricas Prometheus                     |

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
Os logs ficam em `logs/portarialight-YYYYMMDD.txt` e também no console:
```
[14:32:01 INF] a3f2... HTTP GET /api/morador respondeu 200 em 12.4ms
[14:32:02 WRN] a3f2... Morador não encontrado para id=99
```

Cada requisição recebe um **X-Correlation-ID** automático rastreável em todos os logs.

### Métricas Prometheus
Acesse `http://localhost:5204/metrics`:
- `http_server_duration_ms` — tempo de resposta por rota
- `process_cpu_seconds_total` — uso de CPU
- `dotnet_gc_collections_total` — coletas de GC

---

## Executando os Testes

```bash
# Todos os testes
dotnet test

# Apenas testes unitários
dotnet test PortariaLight.Tests.Unit/

# Apenas testes de integração
dotnet test PortariaLight.Tests.Integration/

# Com cobertura de código
dotnet test --collect:"XPlat Code Coverage"

# Verbose (ver cada teste)
dotnet test --logger "console;verbosity=detailed"
```

---

## Organização dos Testes

```
PortariaLight.Tests.Unit/
└── Application/
    └── Services/
        ├── EncomendaServiceTests.cs    → 8 testes unitários
        └── MoradorServiceTests.cs      → 8 testes unitários

PortariaLight.Tests.Integration/
├── Fixtures/
│   ├── PortariaLightWebApplicationFactory.cs   → substitui Oracle por InMemory
│   └── PortariaLightCollectionFixture.cs       → Collection fixture compartilhada
└── Controllers/
    ├── EncomendaControllerIntegrationTests.cs  → 8 testes de integração
    └── MoradorControllerIntegrationTests.cs    → 7 testes de integração
```

**Nomenclatura:** `MetodoTestado_Cenario_ResultadoEsperado`
Exemplo: `CreateEncomendaAsync_QuandoMoradorNaoExiste_LancaArgumentException`

---

## Equipe

| Nome           | RM       |
|----------------|----------|
| Fabrício José  | RM560694 |

---

*Projeto acadêmico — FIAP 2025.*