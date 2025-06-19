# 🏦 API NoBolso

Uma API RESTful para gerenciamento financeiro pessoal e compartilhado, construída com **.NET**, **Clean Architecture** e as melhores práticas de desenvolvimento de software.

---

## 🚩 O Problema

Controlar finanças pessoais pode ser um desafio. Planilhas se tornam complexas, anotações se perdem e aplicativos existentes muitas vezes são complicados ou não atendem a necessidades específicas.

Sem uma ferramenta simples e centralizada para registrar entradas e saídas, é difícil ter uma visão clara da saúde financeira, planejar o futuro e tomar decisões informadas sobre o dinheiro. Isso leva a:

* Gastos não planejados.
* Dificuldade em poupar.
* Constante incerteza sobre para onde o dinheiro está indo.

A **API NoBolso** nasce para resolver esse problema, fornecendo um backend robusto e seguro para que aplicações de finanças pessoais possam ser construídas, oferecendo uma forma simples e eficiente de gerenciar carteiras e transações em um ambiente individual ou colaborativo.

---

## ✨ Funcionalidades Principais

* ✅ **Gerenciamento de Grupos:** Crie grupos para compartilhar finanças com outras pessoas (família, casal) e gerencie os membros.
* ✅ **Gerenciamento de Carteiras:** CRUD completo para múltiplas carteiras dentro de um grupo (ex.: *Conta Corrente*, *Corretora*, *Dinheiro em espécie*).
* ✅ **Gerenciamento de Transações:** CRUD completo para registrar entradas e saídas, associadas a quem realizou a transação.
* ✅ **Gastos Recorrentes:** Configure despesas e receitas recorrentes (aluguel, salários, assinaturas) para automatizar lançamentos.
* ✅ **Metas de Investimento:** Crie e acompanhe metas financeiras, registrando aportes e resgates.
* ✅ **Cálculo de Saldo Dinâmico:** Saldo calculado em tempo real nas consultas, sempre atualizado.
* ✅ **Soft Delete:** Nenhum dado é permanentemente excluído. Registros são desativados, preservando o histórico.
* ✅ **Busca Avançada:** Filtros e paginação para consultas por período, tipo ou membro do grupo.
* ✅ **Arquitetura Escalável:** Construída sobre **Clean Architecture** e **CQRS**, garantindo código desacoplado, testável e fácil de manter.

---

## 🛠️ Arquitetura e Tecnologias

**Arquitetura:**

* Clean Architecture
* CQRS (Command Query Responsibility Segregation)
* Padrão Mediator
* Repositório Genérico

**Tecnologias:**

* [.NET 9](https://dotnet.microsoft.com/)
* C# 13
* PostgreSQL
* Swagger (OpenAPI)
* Entity Framework Core (EF Core)
* MediatR
* FluentValidation

---

## 📂 Estrutura dos Projetos

```plaintext
NoBolso.sln
├── NoBolso.Domain         → Entidades, Enums, Interfaces, Eventos de Domínio
├── NoBolso.Application    → Commands, Queries, Handlers, Validadores
├── NoBolso.Infrastructure → DbContext, Repositórios, Serviços externos
└── NoBolso.API            → Endpoints, Middlewares, Configurações
```

---

## 🚀 Como Executar o Projeto (Desenvolvimento Local)

### ⚙️ Pré-requisitos

* [.NET 9 SDK](https://dotnet.microsoft.com/download)
* PostgreSQL (local ou via Docker)
* Ferramenta opcional para gerenciar o banco (ex.: pgAdmin, DBeaver)

---

### 🏗️ Passos para Configuração

1. **Clone o repositório:**

```bash
git clone https://github.com/[seu-usuario]/no-bolso-api.git
cd no-bolso-api
```

2. **Configure a conexão com o banco:**

* Crie um banco de dados vazio (ex.: `nobolso_db`).
* Edite o arquivo `src/NoBolso.API/appsettings.Development.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Host=localhost;Port=5432;Database=nobolso_db;Username=postgres;Password=admin"
}
```

3. **Instale a ferramenta do EF Core (caso não tenha):**

```bash
dotnet tool install --global dotnet-ef
```

4. **Aplique as migrations:**

```bash
dotnet ef database update --startup-project src/NoBolso.API
```

5. **Execute a API:**

```bash
cd src/NoBolso.API
dotnet run
```

6. **Acesse a documentação (Swagger):**

* 🔗 [https://localhost:7009/swagger](https://localhost:7009/swagger)
  ou
* 🔗 [http://localhost:5170/swagger](http://localhost:5170/swagger)

---

## 📋 Endpoints da API (Planejados)

### 🔹 Grupos (`/api/grupos`)

| Verbo  | Rota                       | Descrição                            |
| ------ | -------------------------- | ------------------------------------ |
| GET    | `/`                        | Lista os grupos do usuário logado    |
| POST   | `/`                        | Cria um novo grupo                   |
| POST   | `/{id}/convidar`           | Envia um convite para um novo membro |
| DELETE | `/{id}/membros/{membroId}` | Remove um membro de um grupo         |

---

### 🔹 Carteiras (`/api/carteiras`)

| Verbo  | Rota    | Descrição                                     |
| ------ | ------- | --------------------------------------------- |
| GET    | `/`     | Lista todas as carteiras do grupo selecionado |
| POST   | `/`     | Cria uma nova carteira                        |
| PUT    | `/{id}` | Atualiza uma carteira existente               |
| DELETE | `/{id}` | Desativa (soft delete) uma carteira           |

---

### 🔹 Transações (`/api/transacoes`)

| Verbo  | Rota    | Descrição                              |
| ------ | ------- | -------------------------------------- |
| GET    | `/`     | Lista transações do grupo, com filtros |
| POST   | `/`     | Cria uma nova transação                |
| PUT    | `/{id}` | Atualiza uma transação existente       |
| DELETE | `/{id}` | Desativa (soft delete) uma transação   |

---

🚧 *(Endpoints para Gastos Recorrentes e Investimentos serão adicionados futuramente.)*

---

## 📜 Licença

Este projeto está licenciado sob a licença MIT. Veja o arquivo [LICENSE](./LICENSE) para mais detalhes.

---

## 👥 Autor

* Guilherme Souza — [gui240799@outlook.com](mailto:gui240799@outlook.com)

---