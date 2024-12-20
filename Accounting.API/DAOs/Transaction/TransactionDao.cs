﻿using Accounting.API.DTOs.Transaction;
using Accounting.API.Services;
using Dapper;
using Accounting.API.Exceptions.Transaction;
using Accounting.API.Enums;
using Accounting.API.Controllers.QueryParamaters;

namespace Accounting.API.DAOs.Transaction;

public class TransactionDao : ITransactionDao
{
    
    public async Task<TransactionDto?> GetAsync(int personID, int accountID, int transactionID)
    {
        using var db = AccountDatabaseFactory.CreateConnection();
        string sql = $@"
SELECT 
    P.PersonID [{nameof(TransactionDto.PersonID)}] 
    , A.AccountID [{nameof(TransactionDto.AccountID)}] 
    , T.TransactionID [{nameof(TransactionDto.TransactionID)}] 
    , T.Description [{nameof(TransactionDto.Description)}] 
    , T.Date [{nameof(TransactionDto.Date)}] 
    , T.TransactionTypeID [{nameof(TransactionDto.Type)}] 
    , CAST(T.Amount * 100 as int) [{nameof(TransactionDto.Amount)}] 
FROM 
    [dbo].[Persons] P INNER JOIN [dbo].[Accounts] A ON P.PersonID = A.PersonID 
    INNER JOIN [dbo].[Transactions] T ON A.AccountID = T.AccountID 
WHERE 
    P.PersonID = @personID
    AND A.AccountID = @accountID
    AND T.TransactionID = @transactionID";

        return await db.QuerySingleOrDefaultAsync<TransactionDto>(sql, new { personID, accountID, transactionID });
    }

    public async Task<AccountTransactionsDto> GetAllAsync(int personID, int accountID, TransactionQueryParameters queryParameters)
    {
        using var db = AccountDatabaseFactory.CreateConnection();

        string sql = TransactionQueryBuilder.GetAllAsync(queryParameters);
        Console.WriteLine(sql);


        var reader = await db.QueryMultipleAsync(sql, new { 
            personID, 
            accountID, 
            queryParameters.StartDate,
            queryParameters.EndDate,
            queryParameters.MinAmount, 
            queryParameters.MaxAmount,
            queryParameters.NameQuery, 
        });

        var totalApplicableTransactions = await reader.ReadSingleAsync<int>();
        var res = await reader.ReadAsync<TransactionDetailDto>();


        return new AccountTransactionsDto(personID, accountID, totalApplicableTransactions, res);
    }

    public async Task<TransactionDto?> AddAsync(int personID, int accountID, TransactionAddDto transaction)
    {
        using var db = AccountDatabaseFactory.CreateConnection();
        string sql = $@"
INSERT INTO 
    Transactions(AccountID, Amount, Description, TransactionTypeID) 
OUTPUT 
    INSERTED.TransactionID 
VALUES(@accountID, @amount, @description, @transactionTypeID)";

        var insertedID = await db.QuerySingleAsync<int>(sql, new
        {
            accountID,
            amount = transaction.Amount / 100m,
            description = string.IsNullOrWhiteSpace(transaction.Description) ? null : transaction.Description.Trim(),
            transactionTypeID = transaction.Type
        });

        return await GetAsync(personID, accountID, insertedID);
    }

    public async Task<TransactionDto?> UpdateAsync(int personID, int accountID, int transactionID, TransactionPatchDto transaction)
    {
        if (transaction is null)
            throw new InvalidTransactionUpdateException("Transaction cannot be null.");

        using var db = AccountDatabaseFactory.CreateConnection();

        List<string> sqlSteps = [];
        if (transaction.Type is not null)
        {
            sqlSteps.Add("TransactionTypeID = @transactionTypeID");
        }
        if (transaction.Description is not null)
        {
            sqlSteps.Add("Description = @description");
        }
        if (transaction.Amount is not null)
        {
            sqlSteps.Add("Amount = @amount");
        }

        if (sqlSteps.Count != 0)
        {
            string sql = $@"
UPDATE 
    [dbo].[Transactions] 
SET 
    {string.Join(", ", sqlSteps)} 
WHERE 
    AccountID = @accountID
    AND TransactionID = @transactionID";

            await db.ExecuteAsync(sql, new
            {
                accountID,
                personID,
                transactionID,
                transactionTypeID = transaction?.Type,
                description = transaction?.Description == string.Empty ? null: transaction!.Description,
                amount = transaction?.Amount / 100m,
            });
        }

        return await GetAsync(personID, accountID, transactionID);
    }

    public async Task<bool> DeleteAsync(int transactionID)
    {
        using var db = AccountDatabaseFactory.CreateConnection();
        string sql = $@"
DELETE FROM 
    Transactions 
WHERE 
    TransactionID = @transactionID ";

        var res = await db.ExecuteAsync(sql, new { transactionID });

        return res == 1;
    }
}
