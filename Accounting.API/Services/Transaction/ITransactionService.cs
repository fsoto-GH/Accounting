using Accounting.API.DAOs;
using Accounting.API.DTOs.Transaction;

namespace Accounting.API.Services.Transaction
{
    public interface ITransactionService
    {
        /// <summary>
        /// Creates a transaction entry tied to the associated person account.
        /// </summary>
        public Task<TransactionDto> AddAsync(int personID, int accountID, TransactionAddDto transaction);

        /// <summary>
        /// Deletes the transaction.
        /// </summary>
        public Task<bool> DeleteAsync(int personID, int accountID, int transactionID);

        /// <summary>
        /// Retrieves the details for the specified transaction.
        /// </summary>
        public Task<TransactionDto> GetAsync(int personID, int accountID, int transactionID);

        /// <summary>
        /// Retrieves all the transactions associated to the specified account.
        /// </summary>
        public Task<AccountTransactionsDto> GetAllAsync(int personID, int accountID);

        /// <summary>
        /// Updates the details of the specified transaction.
        /// </summary>
        public Task<TransactionDto> UpdateAsync(int personID, int accountID, int transactionID, TransactionPatchDto transaction);
    }
}
