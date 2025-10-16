🏢 PortariaLight

📘 Definição do Projeto

Objetivo do Projeto:
O PortariaLight foi desenvolvido para otimizar o controle de entrada e saída de encomendas em condomínios residenciais. A aplicação visa facilitar o trabalho dos porteiros e melhorar a organização e rastreabilidade das entregas e retiradas pelos moradores.

Escopo:
O sistema gerencia moradores, encomendas e retiradas, permitindo o registro, atualização, listagem e exclusão desses dados. O foco inicial da primeira sprint é o CRUD de Encomendas, evoluindo futuramente para controle completo de portaria e autenticação de usuários.

Requisitos Funcionais:

Cadastrar, listar, atualizar e remover encomendas.

Associar encomendas a moradores.

Registrar retiradas de encomendas.

Exibir informações completas das entregas registradas.


Requisitos Não Funcionais:

Utilizar arquitetura limpa e código desacoplado.

Persistência de dados com Entity Framework Core.

API REST desenvolvida em .NET 8.0.

Utilização de Swagger para documentação dos endpoints.



---

🧱 Desenho da Arquitetura

O projeto segue o padrão Clean Architecture, dividindo responsabilidades em quatro camadas principais:

Apresentação (API):
Responsável pela comunicação externa, controladores e documentação Swagger.

Aplicação:
Contém os casos de uso, serviços e DTOs usados para a interação entre camadas.

Domínio:
Define as entidades centrais e regras de negócio (ex: Encomenda, Morador).

Infraestrutura:
Responsável pelo acesso aos dados, mapeamentos com Entity Framework Core e integração com APIs externas (futuras).



---

⚙️ Tecnologias Utilizadas

.NET 8.0

C#

Entity Framework Core

SQL Server

Swagger

Visual Studio / VS Code

Git & GitHub



---

🗂️ Estrutura do Projeto

PortariaLight/
├── PortariaLight.Api/              # Camada de Apresentação (Controllers, Swagger)
├── PortariaLight.Application/      # Casos de uso, serviços e DTOs
├── PortariaLight.Domain/           # Entidades e regras de negócio
├── PortariaLight.Infrastructure/   # Repositórios, DbContext e Migrations
└── README.md                       # Documentação do projeto


---

🚀 Funcionalidades Implementadas (Sprint 01)

✅ Estrutura completa baseada em Clean Architecture

✅ CRUD de Encomendas

✅ Integração com Entity Framework Core

✅ Documentação Swagger configurada

🔜 CRUD de Moradores e Retiradas (Sprint 02)

🔜 Autenticação com JWT

🔜 Consumo de APIs externas



---

 Publicar API no Azure ou Render

 Projeto desenvolvido para fins acadêmicos e de aprendizado em .NET.

Licença

Este projeto é distribuído sob a licença MIT.
Sinta-se à vontade para usar, modificar e distribuir com os devidos créditos.

 PortariaLight — Simplificando o controle de encomendas no seu condomínio.

