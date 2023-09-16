using Accounting.API.Shared;
using Accounting.API.DTOs.Transaction;
using Accounting.API.Enums;
using Accounting.API.Services;
using Dapper;

namespace Accounting.API.DAOs.Transaction;

public class TransactionDao : ITransactionDao
{
    public async Task<TransactionDto> AddAsync(int personID, int accountID, TransactionAddDto transaction)
    {
        using var db = AccountDatabaseFactory.CreateConnection();
        string sql =
            "INSERT INTO Transactions(AccountID, Amount, Description, TransactionTypeID) " +
            "OUTPUT INSERTED.TransactionID " +
            "VALUES(@accountID, @amount, @description, @transactionTypeID)";

        TransactionType ttID = EnumExtensions.ParseEnum<TransactionType>(transaction.Type?.ToUpperInvariant());

        var insertedID = await db.QuerySingleAsync<int>(sql, new
        {
            accountID,
            amount = transaction!.Amount / 100m,
            description = transaction.Description,
            transactionTypeID = ttID
        });
        
        return await GetAsync(insertedID);
    }

    public async Task<bool> DeleteAsync(int transactionID)
    {
        using var db = AccountDatabaseFactory.CreateConnection();
        string sql =
            "DELETE FROM Transactions " +
            "WHERE " +
            "    TransactionID = @transactionID ";

        var res = await db.ExecuteAsync(sql, new { transactionID });

        return res == 1;
    }

    public async Task<TransactionDto> GetAsync(int transactionID)
    {
        using var db = AccountDatabaseFactory.CreateConnection();
        string sql =
            "SELECT " +
            "	P.PersonID [PersonID] " +
            "	, A.AccountID [AccountID] " +
            "	, T.TransactionID [TransactionID] " +
            "	, T.Description [Description] " +
            "	, T.Date [Date] " +
            "	, TT.Name [Type] " +
            "	, CAST(T.Amount * 100 as int) [Amount] " +
            "FROM " +
            "	[dbo].[Persons] P INNER JOIN [dbo].[Accounts] A ON P.PersonID = A.PersonID " +
            "	INNER JOIN [dbo].[Transactions] T ON A.AccountID = T.AccountID " +
            "	INNER JOIN [dbo].[TransactionTypes] TT ON T.TransactionTypeID = TT.TransactionTypeID " +
            "WHERE " +
            "   TransactionID = @transactionID";

        var res = await db.QuerySingleAsync<TransactionDto>(sql, new { transactionID });

        return res;
    }

    public async Task<AccountTransactionsDto> GetAllAsync(int personID, int accountID)
    {
        using var db = AccountDatabaseFactory.CreateConnection();
        string sql =
            "SELECT " +
            "   T.TransactionID [TransactionID] " +
            "   , T.Date [Date] " +
            "   , T.Description [Description] " +
            "   , TT.Name [Type] " +
            "   , CAST(T.Amount * 100 as int) [Amount] " +
            "FROM " +
            "	[dbo].[Persons] P INNER JOIN [dbo].[Accounts] A ON P.PersonID = A.PersonID " +
            "	INNER JOIN [dbo].[Transactions] T ON A.AccountID = T.AccountID " +
            "	INNER JOIN [dbo].[TransactionTypes] TT ON T.TransactionTypeID = TT.TransactionTypeID " +
            "WHERE " +
            "   A.AccountID = @accountID " +
            "ORDER BY T.Date DESC";

        var res = await db.QueryAsync<TransactionDetailDto>(sql, new { accountID });

        return res is null ? new AccountTransactionsDto() : new AccountTransactionsDto(personID, accountID, res.ToList());
    }

    public async Task<TransactionDto> UpdateAsync(int personID, int accountID, int transactionID, TransactionPatchDto transaction)
    {
        using var db = AccountDatabaseFactory.CreateConnection();

        TransactionType tt = EnumExtensions.ParseEnum<TransactionType>(transaction.Type?.ToUpperInvariant());
        List<string> sqlSteps = new();
        if (!string.IsNullOrWhiteSpace(transaction.Type) && tt != default)
        {
            sqlSteps.Add("TransactionTypeID = @transactionTypeID");
        }
        if (transaction.Description is not null)
        {
            sqlSteps.Add("Description = @description");
        }
        if (transaction.Amount is not null && transaction.Amount >= 0)
        {
            sqlSteps.Add("Amount = @amount");
        }

        if (sqlSteps.Count != 0)
        {
            string sql = $"UPDATE Transactions SET {string.Join(", ", sqlSteps)} WHERE AccountID = @accountID";
            await db.ExecuteAsync(sql, new
            {
                accountID,
                personID,
                transactionID,
                transactionTypeID = tt,
                description = transaction.Description,
                amount = transaction.Amount,
            });
        }

        return await GetAsync(transactionID);
    }
}
