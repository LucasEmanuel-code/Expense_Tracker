# 💰 Expense Tracker CLI

Aplicação de linha de comando desenvolvida em **C# (.NET)** para gerenciamento de despesas pessoais.
Permite registrar gastos, categorizar, controlar orçamento mensal e exportar dados.



## 📌 Funcionalidades

✔ Adicionar despesas com descrição, valor e categoria
✔ Listar todas as despesas
✔ Filtrar despesas por categoria
✔ Definir orçamento mensal
✔ Alerta quando o orçamento é ultrapassado
✔ Exportar despesas para arquivo CSV



## 🛠️ Tecnologias utilizadas

* C#
* .NET
* LINQ
* System.IO

## Comandos Disponíveis

| Comando | Descrição | Exemplo |
|---------|-----------|---------|
| `add` | Adiciona uma nova despesa com descrição, valor e categoria. | `dotnet run add --description "Café" --amount 5.50 --category Alimentação` |
| `list` | Lista todas as despesas ou filtra por categoria. | `dotnet run list` ou `dotnet run list --category Alimentação` |
| `summary` | Mostra o total de despesas ou para um mês específico. | `dotnet run summary` ou `dotnet run summary --month 3` |
| `delete` | Remove uma despesa pelo ID. | `dotnet run delete --id 1` |
| `update` | Atualiza uma despesa existente (pelo ID). | `dotnet run update --id 1 --description "Café da manhã" --amount 6.00` |
| `budget` | Define um orçamento para um mês. | `dotnet run budget --month 3 --limit 1000.00` |
| `export` | Exporta as despesas para um arquivo CSV. | `dotnet run export` ou `dotnet run export --file minhas_despesas.csv` |

## Como Usar

1. Navegue até o diretório do projeto.
2. Compile o projeto: `dotnet build`.
3. Execute os comandos conforme os exemplos acima.

Certifique-se de que o .NET SDK está instalado.

## Exemplo Funcional

```
$ dotnet run add --description "Almoço" --amount 20 --category Alimentação
Expense added successfully!

$ dotnet run add --description "Jantar" --amount 10 --category Alimentação
Expense added successfully!

$ dotnet run list
ID   Date       Description        Category       Amount
1    2026-03-31 Almoço             Alimentação    R$20
2    2026-03-31 Jantar             Alimentação    R$10

$ dotnet run summary
Total Expenses: R$30

$ dotnet run delete --id 2
Expense deleted successfully.

$ dotnet run summary
Total Expenses: R$20

$ dotnet run summary --month 3
Total expenses for month 3: R$20
```
