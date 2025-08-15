# 📌 DIO - Trilha .NET - API e Entity Framework

Este repositório contém a solução do **Desafio de Projeto** da trilha .NET da [DIO](https://www.dio.me), onde foi desenvolvida uma API para gerenciamento de tarefas, utilizando **ASP.NET Core** e **Entity Framework Core**.

---

## 📖 Descrição do Desafio

O objetivo é criar uma **API REST** para um sistema **organizador de tarefas**, permitindo realizar as operações CRUD, bem como consultas filtradas por título, data e status.

O projeto também implementa **Swagger** para documentação automática e **migrations** para criação do banco de dados.

---

## 🛠 Tecnologias Utilizadas

- **.NET 8.0**
- **ASP.NET Core Web API**
- **Entity Framework Core** (Code First)
- **SQL Server**
- **Swagger / Swashbuckle**
- **C# 12**
- **Migrations para versionamento do banco**

---

## 📂 Estrutura do Projeto

```
├── Context/
│   └── OrganizadorContext.cs   # DbContext com configuração da tabela Tarefas
├── Controllers/
│   └── TarefaController.cs     # Endpoints CRUD e filtros
├── Enums/
│   └── StatusTarefa.cs         # Enum com status possíveis
├── Models/
│   └── Tarefa.cs               # Modelo da entidade
├── Migrations/                 # Migrations do EF Core
├── Program.cs                  # Configuração inicial da API
├── appsettings.json            # Configurações gerais
├── appsettings.Development.json# Configuração do ambiente de desenvolvimento
└── README.md                   # Este arquivo
```

---

## 📊 Modelo de Dados

![Diagrama da classe Tarefa](diagrama.png)

**Schema da entidade Tarefa**:

```json
{
  "id": 0,
  "titulo": "string",
  "descricao": "string",
  "data": "2025-08-15T10:00:00",
  "status": "Pendente"
}
```

**Enum `StatusTarefa`**:
- `Pendente`
- `Finalizado`

---

## 🚀 Como Executar

1. **Configurar a Connection String**  
   No arquivo `appsettings.Development.json` insira sua string de conexão:
   ```json
   "ConnectionStrings": {
       "ConexaoPadrao": "Server=SEU_SERVIDOR;Database=OrganizadorTarefas;User Id=usuario;Password=senha;"
   }
   ```

2. **Criar o banco de dados**  
   O projeto já contém uma migration inicial (`InitialCreate`):
   ```bash
   dotnet tool update --global dotnet-ef
   dotnet ef database update
   ```

3. **Rodar a aplicação**  
   ```bash
   dotnet run
   ```

4. **Acessar a documentação Swagger**  
   [https://localhost:5001/swagger](https://localhost:5001/swagger) *(ou porta informada no console)*

---

## 📌 Endpoints Disponíveis

| Método | Endpoint                 | Parâmetro | Corpo (Body)   |
|--------|--------------------------|-----------|----------------|
| GET    | `/Tarefa/{id}`           | id        | N/A            |
| PUT    | `/Tarefa/{id}`           | id        | Schema Tarefa  |
| DELETE | `/Tarefa/{id}`           | id        | N/A            |
| GET    | `/Tarefa/ObterTodos`     | N/A       | N/A            |
| GET    | `/Tarefa/ObterPorTitulo` | titulo    | N/A            |
| GET    | `/Tarefa/ObterPorData`   | data      | N/A            |
| GET    | `/Tarefa/ObterPorStatus` | status    | N/A            |
| POST   | `/Tarefa`                | N/A       | Schema Tarefa  |

---

## 📷 Swagger em execução

![Swagger API](swagger.png)

---

## 📅 Status do Projeto
✅ **Concluído** – API funcional com todos os requisitos do desafio implementados.


