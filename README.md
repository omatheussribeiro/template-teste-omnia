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

<img width="1330" height="872" alt="image" src="https://github.com/user-attachments/assets/51f5bd86-0149-4593-b806-e973c4aad7c0" />


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

<img width="574" height="476" alt="image" src="https://github.com/user-attachments/assets/40a222c6-1d69-4b21-a200-0f1455669c11" />


---

## 🗂️ Postman Collection (importar)

Como usar:

> Abra o Postman → Import → cole o JSON abaixo.

> Crie um Environment com as variáveis:
```text
baseUrl (ex: https://localhost:7181)
```
```text
token (Seu JWT. Apenas o Token, sem Bearer)
```
Rode os requests na ordem (eles salvam saleId e itemId em variáveis automáticas).

```text
{
  "info": {
    "name": "Sales API - template-teste-omnia",
    "_postman_id": "d7d0f6d0-0000-4444-8888-aaaa0000bbbb",
    "description": "Coleção para testar os endpoints de Sales com regras de domínio (quantidade ≤ 20, descontos por quantidade, cancelamentos, etc).",
    "schema": "https://schema.getpostman.com/json/collection/v2.1.0/collection.json"
  },
  "item": [
    {
      "name": "00 - Get Token",
      "request": {
        "method": "POST",
        "header": [
          { "key": "Content-Type", "value": "application/json", "type": "text" }
        ],
        "url": { "raw": "{{baseUrl}}/api/Auth", "host": ["{{baseUrl}}"], "path": ["api","Auth"] },
        "body": {
          "mode": "raw",
          "raw": "{\n  \"email\": \"admin@ambev.com\",\n  \"password\": \"$2a$12$u9FSTREch8M2oM7brh7gIuwiWTr6bAGQmCGK4O7ztHZqweDJ5Pv/q\"}"
        }
      },
      "event": [
        {
          "listen": "test",
          "script": {
            "exec": [
              "pm.test('Status 201 CREATED', function(){ pm.response.to.have.status(201); });",
              "var json = pm.response.json();",
              "pm.environment.set('saleId', json.saleId);",
              "pm.test('Tem saleId', function(){ pm.expect(json.saleId).to.exist; });"
            ],
            "type": "text/javascript"
          }
        }
      ]
    },
    {
      "name": "01 - Create (OK)",
      "request": {
        "method": "POST",
        "header": [
          { "key": "Authorization", "value": "Bearer {{token}}", "type": "text" },
          { "key": "Content-Type", "value": "application/json", "type": "text" }
        ],
        "url": { "raw": "{{baseUrl}}/api/sales", "host": ["{{baseUrl}}"], "path": ["api","sales"] },
        "body": {
          "mode": "raw",
          "raw": "{\n  \"saleNumber\": \"V-OK-0001\",\n  \"saleDate\": \"2025-03-01T12:00:00Z\",\n  \"clientId\": \"aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa\",\n  \"clientName\": \"Cliente Bom\",\n  \"branchId\": \"bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb\",\n  \"branchName\": \"Filial Centro\",\n  \"items\": [\n    { \"productId\": \"11111111-1111-1111-1111-111111111111\", \"productName\": \"Produto A\", \"unitPrice\": 100, \"quantity\": 4 },\n    { \"productId\": \"22222222-2222-2222-2222-222222222222\", \"productName\": \"Produto B\", \"unitPrice\": 50,  \"quantity\": 10 }\n  ]\n}"
        }
      },
      "event": [
        {
          "listen": "test",
          "script": {
            "exec": [
              "pm.test('Status 201 CREATED', function(){ pm.response.to.have.status(201); });",
              "var json = pm.response.json();",
              "pm.environment.set('saleId', json.saleId);",
              "pm.test('Tem saleId', function(){ pm.expect(json.saleId).to.exist; });"
            ],
            "type": "text/javascript"
          }
        }
      ]
    },
    {
      "name": "02 - Get by Id",
      "request": {
        "method": "GET",
        "header": [
          { "key": "Authorization", "value": "Bearer {{token}}", "type": "text" }
        ],
        "url": {
          "raw": "{{baseUrl}}/api/sales/{{saleId}}",
          "host": ["{{baseUrl}}"],
          "path": ["api","sales","{{saleId}}"]
        }
      },
      "event": [
        {
          "listen": "test",
          "script": {
            "exec": [
              "pm.test('Status 200 OK', function(){ pm.response.to.have.status(200); });",
              "var json = pm.response.json();",
              "// pega primeiro item NÃO cancelado como exemplo",
              "const firstItem = (json.items || []).find(i => !i.isCancelled);",
              "if(firstItem){ pm.environment.set('itemId', firstItem.id); }",
              "pm.test('Tem itemId capturado', function(){ pm.expect(pm.environment.get('itemId')).to.exist; });"
            ],
            "type": "text/javascript"
          }
        }
      ]
    },
    {
      "name": "03 - List",
      "request": {
        "method": "GET",
        "header": [
          { "key": "Authorization", "value": "Bearer {{token}}", "type": "text" }
        ],
        "url": {
          "raw": "{{baseUrl}}/api/sales?page=1&pageSize=10",
          "host": ["{{baseUrl}}"],
          "path": ["api","sales"],
          "query": [
            { "key": "page", "value": "1" },
            { "key": "pageSize", "value": "10" }
          ]
        }
      }
    },
    {
      "name": "04 - Update quantity (to 10)",
      "request": {
        "method": "PUT",
        "header": [
          { "key": "Authorization", "value": "Bearer {{token}}", "type": "text" },
          { "key": "Content-Type", "value": "application/json", "type": "text" }
        ],
        "url": {
          "raw": "{{baseUrl}}/api/sales/{{saleId}}",
          "host": ["{{baseUrl}}"],
          "path": ["api","sales","{{saleId}}"]
        },
        "body": {
          "mode": "raw",
          "raw": "{\n  \"items\": [ { \"itemId\": \"{{itemId}}\", \"newQuantity\": 10 } ]\n}"
        }
      }
    },
    {
      "name": "05 - Cancel item",
      "request": {
        "method": "POST",
        "header": [
          { "key": "Authorization", "value": "Bearer {{token}}", "type": "text" }
        ],
        "url": {
          "raw": "{{baseUrl}}/api/sales/{{saleId}}/items/{{itemId}}/cancel",
          "host": ["{{baseUrl}}"],
          "path": ["api","sales","{{saleId}}","items","{{itemId}}","cancel"]
        }
      }
    },
    {
      "name": "06 - Update cancelled item (expect 400/409)",
      "request": {
        "method": "PUT",
        "header": [
          { "key": "Authorization", "value": "Bearer {{token}}", "type": "text" },
          { "key": "Content-Type", "value": "application/json", "type": "text" }
        ],
        "url": {
          "raw": "{{baseUrl}}/api/sales/{{saleId}}",
          "host": ["{{baseUrl}}"],
          "path": ["api","sales","{{saleId}}"]
        },
        "body": {
          "mode": "raw",
          "raw": "{\n  \"items\": [ { \"itemId\": \"{{itemId}}\", \"newQuantity\": 5 } ]\n}"
        }
      }
    },
    {
      "name": "07 - Cancel sale",
      "request": {
        "method": "POST",
        "header": [
          { "key": "Authorization", "value": "Bearer {{token}}", "type": "text" }
        ],
        "url": {
          "raw": "{{baseUrl}}/api/sales/{{saleId}}/cancel",
          "host": ["{{baseUrl}}"],
          "path": ["api","sales","{{saleId}}","cancel"]
        }
      }
    },
    {
      "name": "08 - Post-cancel update (expect 400/409)",
      "request": {
        "method": "PUT",
        "header": [
          { "key": "Authorization", "value": "Bearer {{token}}", "type": "text" },
          { "key": "Content-Type", "value": "application/json", "type": "text" }
        ],
        "url": {
          "raw": "{{baseUrl}}/api/sales/{{saleId}}",
          "host": ["{{baseUrl}}"],
          "path": ["api","sales","{{saleId}}"]
        },
        "body": {
          "mode": "raw",
          "raw": "{\n  \"items\": [ { \"itemId\": \"{{itemId}}\", \"newQuantity\": 3 } ]\n}"
        }
      }
    }
  ],
  "auth": {
    "type": "bearer",
    "bearer": [{ "key": "token", "value": "{{token}}", "type": "string" }]
  },
  "event": [],
  "variable": []
}

```

<img width="1919" height="841" alt="image" src="https://github.com/user-attachments/assets/a5a70770-21bd-4ddb-a13b-72b2ad960af5" />

---

## 🧾 Observações Finais

Todos os Domain Events (ex.: SaleCreatedEvent, SaleModifiedEvent, SaleCancelledEvent, ItemCancelledEvent) são despachados automaticamente via MediatR após SaveChangesAsync.

As validações de domínio e entrada utilizam FluentValidation + Specifications.

O projeto está preparado para expansão modular (novos contextos, bounded contexts e features).
