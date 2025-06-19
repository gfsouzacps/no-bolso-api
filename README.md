# API NoBolso

Uma API RESTful para gerenciamento financeiro pessoal, construída com .NET, Clean Architecture e as melhores práticas de desenvolvimento de software.

---

## O Problema

No dia a dia, controlar finanças pessoais pode ser um desafio. Planilhas se tornam complexas, anotações se perdem e aplicativos existentes são muitas vezes complicados ou não atendem a necessidades específicas. Sem uma ferramenta simples e centralizada para registrar entradas e saídas, é difícil ter uma visão clara da saúde financeira, planejar o futuro e tomar decisões informadas sobre o dinheiro. A falta de controle leva a gastos não planejados, dificuldade em poupar e uma constante incerteza sobre para onde o dinheiro está indo.

A **API NoBolso** nasce para resolver este problema, fornecendo um backend robusto e seguro para que aplicações de finanças pessoais possam ser construídas, oferecendo uma forma simples e eficiente de gerenciar carteiras e transações.

## ✨ Features Principais

* ✅ **Gerenciamento de Carteiras:** CRUD completo para múltiplas carteiras (Ex: "Carteira do Banco", "Corretora", "Dinheiro em espécie").
* ✅ **Gerenciamento de Transações:** CRUD completo para registrar entradas e saídas em cada carteira.
* ✅ **Cálculo de Saldo em Tempo Real:** O saldo de cada carteira é atualizado automaticamente a cada nova transação, com o cálculo sendo feito de forma performática no banco de dados.
* ✅ **Soft Delete:** Nenhum dado é permanentemente excluído. Carteiras e transações são desativadas, preservando o histórico completo.
* ✅ **Busca Avançada:** Suporte a filtros e paginação na listagem de transações, permitindo consultas por período, tipo ou carteira.
* ✅ **Arquitetura Escalável:** Construído sobre os princípios da Clean Architecture e CQRS, garantindo um código desacoplado, testável e fácil de manter.

## 🛠️ Arquitetura e Tecnologias

O projeto foi desenvolvido utilizando tecnologias modernas e uma arquitetura de software robusta para garantir qualidade, performance e escalabilidade.

* **Arquitetura:** Clean Architecture, CQRS (Command Query Responsibility Segregation), Domain-Driven Design (Rich Model), Repository Pattern.
* **Tecnologias Principais:**
    * <img src="https://raw.githubusercontent.com/github/explore/80688e429a7d407ba3c3542e0989ad781945862B/topics/dotnet/dotnet.png" alt=".NET" width="20"/> **.NET 9**
    * <img src="https://raw.githubusercontent.com/github/explore/80688e429a7d407ba3c3542e0989ad781945862B/topics/csharp/csharp.png" alt="C#" width="20"/> **C# 13**
    * <img src="https://raw.githubusercontent.com/github/explore/80688e429a7d407ba3c3542e0989ad781945862B/topics/postgresql/postgresql.png" alt="PostgreSQL" width="20"/> **PostgreSQL**
    * <img src="https://static1.smartbear.co/swagger/media/assets/swagger_fav.png" alt="Swagger" width="20"/> **Swagger (OpenAPI)**
    * **Entity Framework Core:** ORM para acesso a dados.
    * **MediatR:** Implementação do padrão Mediator para o CQRS.
    * **AutoMapper:** Mapeamento de objetos entre camadas.
    * **FluentValidation:** Para validações declarativas e robustas.

## 📂 Estrutura dos Projetos

A solução está organizada em projetos que representam as camadas da Clean Architecture:

* `NoBolso.Domain`: A camada mais interna. Contém as entidades de negócio (Carteira, Transacao), enums, eventos de domínio e as interfaces dos repositórios. Não depende de nenhum outro projeto.
* `NoBolso.Application`: Contém a lógica da aplicação. Orquestra o fluxo de dados usando o padrão CQRS (Commands, Queries, Handlers), DTOs, validadores e as interfaces de serviços da aplicação.
* `NoBolso.Infrastructure`: Contém as implementações técnicas das interfaces definidas no Domínio. É responsável pelo acesso ao banco de dados (EF Core, DbContext, Repositórios) e outros serviços externos.
* `NoBolso.API`: O ponto de entrada da aplicação. Expõe os endpoints RESTful para o mundo exterior e lida com as requisições HTTP.

## 🚀 Como Executar o Projeto (Desenvolvimento Local)

### Pré-requisitos

* **.NET 9 SDK:** [Link para download](https://dotnet.microsoft.com/download/dotnet/9.0)
* **PostgreSQL:** Uma instância do PostgreSQL rodando localmente ou em um contêiner Docker.

### Passos para Configuração

1.  **Clone o repositório:**
    ```bash
    git clone [https://github.com/](https://github.com/)[seu-usuario]/NoBolso.git
    cd NoBolso
    ```

2.  **Configure a Conexão com o Banco:**
    * Abra o arquivo `src/NoBolso.API/appsettings.Development.json`.
    * Altere a `DefaultConnection` com os dados da sua instância do PostgreSQL:
        ```json
        "ConnectionStrings": {
          "DefaultConnection": "Host=localhost;Port=5432;Database=NoBolsoDb;Username=postgres;Password=your_password"
        }
        ```

3.  **Restaure as Dependências:**
    ```bash
    dotnet restore
    ```
4.  **Execute a Aplicação:**
    * Navegue até a pasta do projeto da API: `cd src/NoBolso.API`
    * Execute o comando:
        ```bash
        dotnet run
        ```

5.  **Acesse a Documentação:**
    * A API estará rodando e a documentação interativa do Swagger estará disponível em: **`http://localhost:<porta>/swagger`**.

## 📖 Guia do Desenvolvedor: Adicionando um Novo Campo

Adicionar um novo campo nesta arquitetura requer a atualização de várias camadas para manter a consistência. Vamos usar o exemplo de adicionar um campo `Descricao` à entidade `Carteira`.

1.  **Camada de Domínio (`Domain`):**
    * Em `Entities/Carteira.cs`, adicione a propriedade e a lógica de atualização:
        ```csharp
        public string Descricao { get; private set; } = string.Empty;
        // No construtor...
        Descricao = descricao;
        // Novo método...
        public void AtualizarDescricao(string novaDescricao) { /* sua validação aqui */ Descricao = novaDescricao; }
        ```

2.  **Camada de Infraestrutura (`Infrastructure`):**
    * Em `Data/Configurations/CarteiraConfiguration.cs`, mapeie a nova propriedade para o banco:
        ```csharp
        builder.Property(c => c.Descricao).HasMaxLength(250);
        ```
    * Crie e aplique uma nova migração do EF Core:
        ```bash
        dotnet ef migrations add AddDescricaoToCarteira -p src/NoBolso.Infrastructure -s src/NoBolso.API
        dotnet ef database update -p src/NoBolso.API
        ```
3.  **Camada de Aplicação (`Application`):**
    * **DTO:** Adicione a propriedade `Descricao` em `DTOs/CarteiraDto.cs`.
    * **Commands:** Adicione a propriedade `Descricao` em `Commands/CriarCarteiraCommand.cs` e `Commands/AtualizarCarteiraCommand.cs`.
    * **Handlers:** Nos handlers `CriarCarteiraCommandHandler` e `AtualizarCarteiraCommandHandler`, use o novo campo do command para preencher ou atualizar a entidade.
    * **Validação:** Em `Validators/CriarCarteiraCommandValidator.cs`, adicione regras de validação para a `Descricao`.

Este processo garante que a nova informação flua de forma segura e consistente por todas as camadas da aplicação.

## 📋 Endpoints da API

### Carteiras (`/api/carteiras`)

| Verbo HTTP | Rota           | Descrição                                 |
| :--------- | :------------- | :---------------------------------------- |
| `GET`      | `/`            | Lista todas as carteiras ativas.          |
| `GET`      | `/{id}`        | Obtém uma carteira específica pelo seu ID.  |
| `POST`     | `/`            | Cria uma nova carteira.                   |
| `PUT`      | `/{id}`        | Atualiza o nome de uma carteira existente.|
| `DELETE`   | `/{id}`        | Desativa (soft delete) uma carteira.      |

### Transações (`/api/transacoes`)

| Verbo HTTP | Rota           | Descrição                                 |
| :--------- | :------------- | :---------------------------------------- |
| `GET`      | `/`            | Lista transações com filtros e paginação. |
| `GET`      | `/{id}`        | Obtém uma transação específica pelo seu ID. |
| `POST`     | `/`            | Cria uma nova transação para uma carteira.|
| `PUT`      | `/{id}`        | Atualiza uma transação existente.         |
| `DELETE`   | `/{id}`        | Desativa (soft delete) uma transação.     |

## 📄 Licença

Este projeto está sob a licença MIT.

## 👥 Equipe

* [Seu Nome Completo] - [seu.email@exemplo.com]