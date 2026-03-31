# 💸 Expense Tracker CLI

Projeto GitHub User Activity feito em C#: Exemplo de solução para o desafio [expense-tracker](https://roadmap.sh/projects/expense-tracker) do [roadmap.sh](https://roadmap.sh).

Um rastreador de despesas simples desenvolvido em **C# (.NET)** com interface de linha de comando (CLI).
Permite gerenciar despesas de forma rápida diretamente pelo terminal.

---

## 🚀 Funcionalidades

* ✅ Adicionar despesas com descrição e valor
* ✅ Listar todas as despesas
* ✅ Atualizar uma despesa existente
* ✅ Remover despesas
* ✅ Visualizar total geral de despesas
* ✅ Filtrar despesas por mês
* ✅ Persistência de dados com JSON (não perde ao fechar)

---

## 🛠️ Tecnologias utilizadas

* C#
* .NET
* System.Text.Json
* LINQ

---

## 📦 Como executar o projeto

### 1. Clone o repositório

```bash
git clone https://github.com/seu-usuario/expense-tracker-cli.git
cd expense-tracker-cli
```

### 2. Execute os comandos

```bash
dotnet run add --description "Dinner" --amount 100
dotnet run list
dotnet run summary
dotnet run summary --month 3
dotnet run delete --id 1
dotnet run update --id 1 --description "Lunch" --amount 50
```

---

## 💻 Exemplos de uso

```bash
dotnet run add --description "Dinner" --amount 100
# Adicionar despesa

dotnet run list
# Listar despesas

Saída:
# ID   Date       Description        Amount
# 1    2026-03-30 Dinner             R$100

dotnet run summary
# Resumo total

dotnet run summary --month 3
# Resumo por mês

dotnet run delete --id 1
# Remover despesa
```

