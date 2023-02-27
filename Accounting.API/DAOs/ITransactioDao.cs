using AccountingAPI.DTOs.Transaction;

namespace AccountingAPI.DAOs;

public interface ITransactionDao
{
    /// <summary>
    /// Add a transaction to the specified person account.
    /// </summary>
    /// <param name="personID"></param>
    /// <param name="accountID"></param>
    /// <param name="transaction"></param>
    /// <returns>TBD</returns>
    public Task<int> Add(int personID, int accountID, TransactionAddDto transaction);

    /// <summary>
    /// Update the specified transaction details.
    /// </summary>
    /// <param name="personID"></param>
    /// <param name="accountID"></param>
    /// <param name="transaction"></param>
    /// <returns>TBD</returns>
    public Task<int> Update(int personID, int accountID, TransactionPatchDto transaction);
    
    /// <summary>
    /// Get the details of a specific person account transaction.
    /// </summary>
    /// <param name="personID"></param>
    /// <param name="accountID"></param>
    /// <param name="transactionID"></param>
    /// <returns></returns>
    public Task<TransactionDto> Get(int personID, int accountID, int transactionID);

    /// <summary>
    /// Get all the transactions for a specific person account.
    /// </summary>
    /// <param name="personID"></param>
    /// <param name="accountID"></param>
    /// <returns></returns>
    public Task<TransactionDetailDto> GetAll(int personID, int accountID);

    /// <summary>
    /// Delete a specific user account transaction.
    /// </summary>
    /// <param name="personID"></param>
    /// <param name="accountID"></param>
    /// <param name="transactionID"></param>
    /// <returns>A bool indicating whether the deletion was successful.</returns>
    public Task<bool> Delete(int personID, int accountID, int transactionID);
}
