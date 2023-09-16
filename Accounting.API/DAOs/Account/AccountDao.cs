using Accounting.API.DAOs;
using Accounting.API.DTOs.Account;
using Accounting.API.Enums;
using Accounting.API.Services;
using Dapper;
using System.Data;

namespace Accounting.API.DAOs.Account;

public class AccountDao : IAccountDao
{
    public async Task<int> AddAsync(int personID, AccountAddDto account)
    {
        using var db = AccountDatabaseFactory.CreateConnection();
        string sql = "INSERT INTO Accounts ([PersonID], [AccountTypeID], [NickName]) OUTPUT INSERTED.AccountID VALUES (@personID, @type, @nickName)";
        var res = await db.QuerySingleAsync<int?>(sql, new
        {
            personID,
            type = account.Type,
            nickName = account?.NickName?.Trim()
        });

        return res ?? 0;
    }

    public async Task<bool> DeleteAsync(int personID, int accountID)
    {
        using var db = AccountDatabaseFactory.CreateConnection();
        string sql = "DELETE FROM Accounts WHERE PersonID = @personID AND AccountID = @accountID";

        var res = await db.ExecuteAsync(sql, new { personID, accountID });

        return res == 1;
    }

    public async Task<AccountDto> GetAsync(int personID, int accountID)
    {
        string sql = "SELECT AccountID, PersonID, AT.Name [Type], NickName, Status FROM Accounts A INNER JOIN AccountTypes AT ON A.AccountTypeID = AT.AccountTypeID WHERE PersonID = @personID AND AccountID = @accountID";
        using var db = AccountDatabaseFactory.CreateConnection();
        var res = await db.QuerySingleOrDefaultAsync<AccountDto>(sql, new { personID, accountID });

        return res ?? new AccountDto();
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
            totalAccounts: _params.Get<int?>("@accountCount") ?? -1,
            netBalace: _params.Get<double?>("@netBalance") ?? 0
        );
    }

    public async Task<int> UpdateAsync(int personID, int accountID, AccountPatchDto account)
    {
        var dbAccount = await GetAsync(personID, accountID);
        if (account is null) return -1;

        var dbType = Enum.Parse(typeof(AccountType), dbAccount.Type.ToUpper()) as AccountType?;
        List<string> sqlSteps = new();
        if (account.Type is not null && dbType != account.Type)
        {
            sqlSteps.Add("AccountTypeID = @type");
        }
        if (account.NickName is not null && !string.Equals(account.NickName, dbAccount?.NickName))
        {
            sqlSteps.Add("NickName = @nickName");
        }
        if (account.Status is not null && account.Status != dbAccount?.Status)
        {
            sqlSteps.Add("Status = @status");
        }

        if (sqlSteps.Count == 0) return 0;

        string sql = $"UPDATE Accounts SET {string.Join(", ", sqlSteps)} WHERE AccountID = @accountID AND PersonID = @personID";
        using var db = AccountDatabaseFactory.CreateConnection();

        return await db.ExecuteAsync(sql, new
        {
            personID,
            accountID,
            type = account?.Type,
            nickName = account?.NickName?.Trim(),
            status = account?.Status
        });
    }
}
