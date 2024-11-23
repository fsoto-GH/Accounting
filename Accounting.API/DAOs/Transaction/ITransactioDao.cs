using Accounting.API.Controllers.QueryParamaters;
using Accounting.API.DTOs.Transaction;

namespace Accounting.API.DAOs.Transaction;

public interface ITransactionDao
{
    /// <summary>
    /// Add a transaction to the specified person account.
    /// </summary>
    /// <param name="personID"></param>
    /// <param name="accountID"></param>
    /// <param name="transaction"></param>
    public Task<TransactionDto> AddAsync(int personID, int accountID, TransactionAddDto transaction);

    /// <summary>
    /// Update the specified transaction details.
    /// </summary>
    /// <param name="personID"></param>
    /// <param name="accountID"></param>
    /// <param name="transactionID"></param>
    /// <param name="transaction"></param>
    public Task<TransactionDto> UpdateAsync(int personID, int accountID, int transactionID, TransactionPatchDto transaction);

    /// <summary>
    /// Get the details of a specific person account transaction.
    /// </summary>
    /// <param name="personID"></param>
    /// <param name="accountID"></param>
    /// <param name="transactionID"></param>
    public Task<TransactionDto> GetAsync(int personID, int accountID, int transactionID);

    /// <summary>
    /// Get all the transactions for a specific person account.
    /// </summary>
    /// <param name="personID"></param>
    /// <param name="accountID"></param>
    public Task<AccountTransactionsDto> GetAllAsync(int personID, int accountID, TransactionQueryParameters queryParameters);

    /// <summary>
    /// Delete a specific user account transaction.
    /// </summary>
    /// <param name="personID"></param>
    /// <param name="accountID"></param>
    /// <param name="transactionID"></param>
    /// <returns>A bool indicating whether the deletion was successful.</returns>
    public Task<bool> DeleteAsync(int transactionID);
}
