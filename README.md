# 🧩 template-teste-omnia

Template base desenvolvido para avaliação técnica utilizando **.NET 8**, **DDD (Domain-Driven Design)**, **Clean Architecture**, **PostgreSQL**, **Entity Framework Core**, **MediatR**, **FluentValidation**, **AutoMapper** e **JWT Authentication**.

---

O objetivo principal é oferecer uma base completa para aplicações corporativas com autenticação, versionamento via PRs e módulos de negócio bem definidos.

---

## 🧱 Funcionalidades Desenvolvidas

### 🔹 PR 1
**Infraestrutura e configuração inicial**

- [01] Correção do `ApplicationDbContext`
- [02] Correção das configurações de Usuários
- [03] Ajustes na camada de **IoC**
- [04] Instalação e configuração do **Docker** para subir o **PostgreSQL** via container
- [05] Ajuste do `appsettings.json`
- [06] Criação e aplicação de novas **migrations** dentro da camada **ORM**
- [07] Aplicados novos **mapeamentos de classes** para os Controllers
- [08] Ajuste na **regra de negócio** de autorização de usuários
- [09] Correção e funcionamento completo do **JWT**

---

### 🔹 PR 2
**Autenticação e endpoints de Usuários**

- [01] Finalizada a configuração do **Swagger** para aceitar o token JWT
- [02] Inclusos todos os **mappers necessários** para o endpoint de usuários
- [03] Criado o **endpoint de alteração de usuários** com sua própria regra de negócio

---

### 🔹 PR 3
**Módulo de Vendas**

- [01] Criadas as **entidades de Venda (`Sale`) e Itens de Venda (`SaleItem`)**
- [02] Incluído novo **migration** para a criação das tabelas correspondentes
- [03] Disponibilizados os **endpoints RESTful** de Vendas:

| Endpoint | Método | Descrição |
|-----------|---------|-----------|
| `/api/sales` | **POST** | Criação de uma nova venda |
| `/api/sales/{id}` | **GET** | Consulta de uma venda específica |
| `/api/sales` | **GET** | Listagem paginada e filtrada de vendas |
| `/api/sales/{id}` | **PUT** | Atualização de itens e quantidades da venda |
| `/api/sales/{id}/cancel` | **POST** | Cancelamento completo da venda |
| `/api/sales/{id}/items/{itemId}/cancel` | **POST** | Cancelamento individual de item |

Esses endpoints implementam todas as **regras de negócio do domínio**, como:
- Aplicação automática de políticas de desconto por quantidade (0%, 10%, 20%)
- Respeito à quantidade máxima (≤ 20)
- Bloqueio de alterações em vendas canceladas
- Recalculo automático dos totais

---

## 🧰 Tecnologias e Pacotes

- **.NET 8**
- **Entity Framework Core 8**
- **PostgreSQL**
- **Docker**
- **AutoMapper**
- **FluentValidation**
- **MediatR**
- **JWT Authentication**
- **Swagger / Swashbuckle**
- **xUnit** (para testes)

---

## 🧑‍💻 Autenticação e Usuário Padrão

O sistema utiliza **JWT** para autenticação.

> Para realizar login ou testar o Swagger, utilize o usuário padrão abaixo:

```text
Email: admin@ambev.com
Senha: $2a$12$u9FSTREch8M2oM7brh7gIuwiWTr6bAGQmCGK4O7ztHZqweDJ5Pv/q
```

---

## 🐘 Banco de Dados

Banco: PostgreSQL

Nome do banco: DeveloperEvaluation

> 🔧 Conexão e Migrações

```text
appsettings.json
```
```text
"ConnectionStrings": {
  "DefaultConnection": "Host=localhost;Port=5432;Database=DeveloperEvaluation;Username=postgres;Password=postgres"
}
```
> Suba o container do PostgreSQL (se usar Docker):
```text
docker compose up -d
```
> Ou
```text
docker run --name developer-evaluation-db -e POSTGRES_PASSWORD=postgres -p 5432:5432 -d postgres
```
> Atualize o banco com as migrations:
```text
update-database
```

O EF Core criará automaticamente todas as tabelas (Users, Sales, SaleItems, etc.) com os mapeamentos definidos na camada ORM.
---

## 🧾 Observações Finais

Todos os Domain Events (ex.: SaleCreatedEvent, SaleModifiedEvent, SaleCancelledEvent, ItemCancelledEvent) são despachados automaticamente via MediatR após SaveChangesAsync.

As validações de domínio e entrada utilizam FluentValidation + Specifications.

O projeto está preparado para expansão modular (novos contextos, bounded contexts e features).
