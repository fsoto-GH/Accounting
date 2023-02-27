using AccountingAPI.DAOs;
using AccountingAPI.DTOs.Account;
using Dapper;
using System.Data;

namespace AccountingAPI.Services;

public class AccountService : IAccountDao
{
    public async Task<int> Add(int personID, AccountAddDto account)
    {
        using var db = AccountingDB.CreateConnection();
        string sql = "INSERT INTO Accounts ([PersonID], [Type], [NickName]) OUTPUT INSERTED.PersonID VALUES (@personID, @type, @nickName)";
        var res = await db.QuerySingleAsync<int?>(sql, new { 
            personID,
            type = account.Type,
            nickName = account.NickName
        });

        return res ?? 0;
    }

    public async Task<bool> Delete(int personID, int accountID)
    {
        using var db = AccountingDB.CreateConnection();
        string sql = "DELETE FROM Accounts WHERE PersonID = @personID AND AccountID = @accountID";

        var res = await db.ExecuteAsync(sql, new { personID, accountID });

        return res == 1;
    }

    public async Task<AccountDto> Get(int personID, int accountID)
    {
        string sql = "SELECT * FROM Accounts WHERE PersonID = @personID AND AccountID = @accountID";
        using var db = AccountingDB.CreateConnection();
        var res = await db.QuerySingleOrDefaultAsync<AccountDto>(sql, new { personID, accountID });

        return res ?? new AccountDto();
    }

    public async Task<AccountsSummaryDto> GetAll(int personID)
    {
        using var db = AccountingDB.CreateConnection();
        var _params = new DynamicParameters();
        _params.Add("personID", value: personID, DbType.Int32);
        _params.Add("@netBalance", direction: ParameterDirection.Output, dbType: DbType.Double);
        _params.Add("@accountCount", direction: ParameterDirection.Output, dbType: DbType.Int32);

        var res = (await db.QueryAsync<AccountDto>("[dbo].[usp_ViewPersonAccounts]", _params, commandType: CommandType.StoredProcedure));

        return new AccountsSummaryDto(
            personID: personID,
            accounts: res.ToArray(),
            totalAccounts: _params.Get<int?>("@accountCount") ?? -1,
            netBalace: _params.Get<double?>("@netBalance") ?? 0
        );
    }

    public async Task<int> Update(int personID, int accountID, AccountPatchDto account)
    {
        var dbAccount = await Get(personID, accountID);
        if (account is null) return -1;

        List<string> sqlSteps = new();
        if (account?.Type != string.Empty && !string.Equals(account?.Type, dbAccount.Type))
        {
            sqlSteps.Add("Type = @type");
        }
        if (account?.Type is not null && !string.Equals(account.NickName, dbAccount.NickName))
        {
            sqlSteps.Add("NickName = @nickName");
        }

        if (sqlSteps.Count == 0) return 0;

        string sql = $"UPDATE Accounts SET {string.Join(", ", sqlSteps)} WHERE AccountID = @accountID AND PersonID = @personID";
        using var db = AccountingDB.CreateConnection();

        return await db.ExecuteAsync(sql, new
        {
            personID,
            accountID,
            type = $"{account?.Type?[..1].ToUpper()}{account?.Type[1..].ToLower()}",
            nickName = account?.NickName == string.Empty ? null: account?.NickName
        });
    }
}
