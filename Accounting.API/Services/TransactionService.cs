using AccountingAPI.DAOs;
using AccountingAPI.DTOs;
using AccountingAPI.DTOs.Account;
using AccountingAPI.DTOs.Transaction;

namespace AccountingAPI.Services;

public class TransactionService : ITransactionDao
{
    /*            using var db = AccountingDB.CreateConnection();
var _params = new DynamicParameters();
_params.Add("personID", value: personID, DbType.Int32);
_params.Add("accountID", value: accountID, DbType.Int32);
_params.Add("@netBalance", direction: ParameterDirection.Output, dbType: DbType.Double);
_params.Add("@totalPayments", direction: ParameterDirection.Output, dbType: DbType.Double);
_params.Add("@totalPurchases", direction: ParameterDirection.Output, dbType: DbType.Double);

var res = (await db.QueryAsync<TransactionDto>("[dbo].[usp_ViewAccountTransactions]", _params, commandType: CommandType.StoredProcedure));

return new AccountTransactionsDto(
        personID: personID,
        accountID: accountID,
        transactions: res.ToArray(),
        netBalance: _params.Get<double?>("@netBalance"),
        totalPurchases: _params.Get<double?>("@totalPayments"),
        totalPayments: _params.Get<double?>("@totalPurchases")
        );
*/
    public Task<int> Add(int personID, int accountID, TransactionAddDto transaction)
    {
        throw new NotImplementedException();
    }

    public Task<bool> Delete(int personID, int accountID, int transactionID)
    {
        throw new NotImplementedException();
    }

    public Task<TransactionDto> Get(int personID, int accountID, int transactionID)
    {
        throw new NotImplementedException();
    }

    public Task<TransactionDetailDto> GetAll(int personID, int accountID)
    {
        throw new NotImplementedException();
    }

    public Task<int> Update(int personID, int accountID, TransactionPatchDto transaction)
    {
        throw new NotImplementedException();
    }
}
