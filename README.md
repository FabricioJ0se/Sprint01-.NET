#  PortariaLight

**PortariaLight** é uma aplicação desenvolvida em **.NET 10 (C#)** que tem como objetivo **gerenciar o controle de encomendas e retiradas em condomínios**, permitindo que a portaria registre e acompanhe o fluxo de entregas e retiradas de forma eficiente e centralizada.

---

##  Tecnologias Utilizadas

- **.NET 10.0**
- **Entity Framework Core**
- **ASP.NET Core Web API**
- **Swagger (Swashbuckle)**
- **SQL Server (ou outro banco configurado no `AppDbContext`)**

---

##  Estrutura do Projeto

Sprint01-.NET/
│
├── PortariaLight.Api/ # Camada de API (Controllers, Program.cs)
├── PortariaLight.Application/ # Camada de aplicação (Services, Interfaces)
├── PortariaLight.Domain/ # Entidades e Interfaces de Domínio
├── PortariaLight.Infrastructure/ # Repositórios e acesso ao banco de dados
│
└── README.md
Padrão de Arquitetura

A aplicação segue uma arquitetura em camadas:

Domain → Entidades e contratos.

Infrastructure → Implementação dos repositórios e persistência de dados.

Application → Regras de negócio e serviços.

API → Controladores e endpoints públicos.

Status do Projeto

 Em desenvolvimento (Sprint 01 concluída)

 CRUD de Encomendas

 Integração com Swagger

 Camadas configuradas (Domain, Application, Infrastructure, API)

Próximas etapas:

 Implementar CRUD de Morador e Retirada

 Configurar autenticação (JWT)

 Publicar API no Azure ou Render

 Projeto desenvolvido para fins acadêmicos e de aprendizado em .NET.

Licença

Este projeto é distribuído sob a licença MIT.
Sinta-se à vontade para usar, modificar e distribuir com os devidos créditos.

 PortariaLight — Simplificando o controle de encomendas no seu condomínio.

