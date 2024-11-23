using Accounting.API.DAOs;
using Accounting.API.DTOs.Account;
using Accounting.API.Enums;
using Accounting.API.Exceptions.Account;
using Accounting.API.Services;
using Dapper;
using System.Data;

namespace Accounting.API.DAOs.Account;

public class AccountDao : IAccountDao
{
    public async Task<AccountDto> GetAsync(int personID, int accountID)
    {
        using var db = AccountDatabaseFactory.CreateConnection();
        string sql = $@"
SELECT 
    A.AccountID [{nameof(AccountDto.AccountID)}]
    , P.PersonID [{nameof(AccountDto.PersonID)}]
    , NickName [{nameof(AccountDto.NickName)}]
    , Status [{nameof(AccountDto.Status)}]
    , AccountTypeID [{nameof(AccountDto.Type)}]
	, CAST(ISNULL(SUM(CASE T.TransactionTypeID WHEN {(int)TransactionType.DEBIT} THEN T.Amount ELSE 0 END), 0) * 100 as int) [{nameof(AccountDto.TotalPurchases)}]
	, CAST(ISNULL(SUM(CASE T.TransactionTypeID WHEN {(int)TransactionType.CREDIT} THEN T.Amount ELSE 0 END), 0) * 100 as int) [{nameof(AccountDto.TotalPayments)}]
	, CAST(ISNULL(SUM(CASE T.TransactionTypeID WHEN {(int)TransactionType.DEBIT} THEN -T.Amount ELSE T.Amount END) * 100, 0) as int) [{nameof(AccountDto.NetBalance)}]
    , COUNT(T.TransactionID) [{nameof(AccountDto.CountOfTransactions)}]
FROM
    Persons P LEFT OUTER JOIN Accounts A ON P.PersonID = A.PersonID
    LEFT OUTER JOIN Transactions T ON T.AccountID = A.AccountID
WHERE
    P.PersonID = @personID
    AND A.AccountID = @accountID
GROUP BY
    A.AccountID
    , P.PersonID
    , A.NickName
    , A.Status
    , A.AccountTypeID";

        var res = await db.QuerySingleOrDefaultAsync<AccountDto>(sql, new { personID, accountID });

        return res;
    }

    public async Task<AccountsSummaryDto> GetAllAsync(int personID)
    {
        using var db = AccountDatabaseFactory.CreateConnection();
        var _params = new DynamicParameters();
        _params.Add("personID", value: personID, DbType.Int32);
        _params.Add("@netBalance", direction: ParameterDirection.Output, dbType: DbType.Double);
        _params.Add("@accountCount", direction: ParameterDirection.Output, dbType: DbType.Int32);

        var res = await db.QueryAsync<AccountDto>("[dbo].[usp_ViewPersonAccounts]", _params, commandType: CommandType.StoredProcedure);

        return new AccountsSummaryDto(
            personID: personID,
            accounts: res.ToArray(),
            totalAccounts: _params.Get<int?>("@accountCount") ?? 0,
            netBalace: _params.Get<double?>("@netBalance") ?? 0
        );
    }

    public async Task<AccountDto> AddAsync(int personID, AccountAddDto account)
    {
        using var db = AccountDatabaseFactory.CreateConnection();
        string sql = $@"
INSERT INTO
    [dbo].[Accounts] ([PersonID], [AccountTypeID], [NickName]) 
OUTPUT 
    INSERTED.AccountID 
VALUES 
    (@personID, @type, @nickName)";

        var insertedID = await db.QuerySingleAsync<int>(sql, new
        {
            personID,
            type = account.Type,
            nickName = string.IsNullOrWhiteSpace(account.NickName) ? null : account.NickName.Trim()
        });

        return await GetAsync(personID, insertedID);
    }

    public async Task<AccountDto> UpdateAsync(int personID, int accountID, AccountPatchDto account)
    {
        if (account is null)
            throw new InvalidAccountUpdateException("Account cannot be null.");

        List<string> sqlSteps = new();
        if (account.Type is not null)
        {
            sqlSteps.Add("AccountTypeID = @type");
        }
        if (account.NickName == string.Empty)
        {
            sqlSteps.Add("NickName = @nickName");
        }
        if (account.Status is not null)
        {
            sqlSteps.Add("Status = @status");
        }

        if (sqlSteps.Count != 0)
        {
            string sql =$@"
UPDATE 
    [dbo].[Accounts] 
SET 
    {string.Join(", ", sqlSteps)} 
WHERE 
    AccountID = @accountID";
            using var db = AccountDatabaseFactory.CreateConnection();

            await db.ExecuteAsync(sql, new
            {
                personID,
                accountID,
                type = account?.Type,
                nickName = account?.NickName == string.Empty ? null : account!.NickName,
                status = account?.Status,
            });
        }

        return await GetAsync(personID, accountID);
    }

    public async Task<bool> DeleteAsync(int personID, int accountID)
    {
        using var db = AccountDatabaseFactory.CreateConnection();
        string sql = $@"
DELETE FROM 
    [dbo].[Accounts] 
WHERE 
    PersonID = @personID 
AND 
    AccountID = @accountID";

        var res = await db.ExecuteAsync(sql, new { personID, accountID });

        return res == 1;
    }
}
