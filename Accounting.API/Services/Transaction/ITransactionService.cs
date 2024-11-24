using Accounting.API.Exceptions.Person;
using Accounting.API.Exceptions.Account;
using Accounting.API.Exceptions.Transaction;
using Accounting.API.DTOs.Transaction;
using Accounting.API.Controllers.QueryParamaters;

namespace Accounting.API.Services.Transaction
{
    public interface ITransactionService
    {
        /// <summary>
        /// Retrieves the details for the specified transaction.
        /// </summary>
        /// <param name="personID"></param>
        /// <param name="accountID"></param>
        /// <param name="transactionID"></param>
        /// <returns></returns>
        /// <exception cref="NotFoundPersonException"></exception>
        /// <exception cref="NotFoundAccountException"></exception>
        /// <exception cref="NotFoundTransactionException"></exception>
        public Task<TransactionDto> GetAsync(int personID, int accountID, int transactionID);

        /// <summary>
        /// Retrieves all the transactions associated to the specified account.
        /// </summary>
        /// <param name="personID"></param>
        /// <param name="accountID"></param>
        /// <returns></returns>
        /// <exception cref="NotFoundPersonException"></exception>
        /// <exception cref="NotFoundAccountException"></exception>
        public Task<AccountTransactionsDto> GetAllAsync(int personID, int accountID, TransactionQueryParameters nameQuery);

        /// <summary>
        /// Creates a transaction entry tied to the associated person account.
        /// </summary>
        /// <param name="personID"></param>
        /// <param name="accountID"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        /// <exception cref="InvalidTransactionAdditionException"></exception>
        /// <exception cref="NotFoundPersonException"></exception>
        /// <exception cref="NotFoundAccountException"></exception>
        public Task<TransactionDto?> AddAsync(int personID, int accountID, TransactionAddDto transaction);

        /// <summary>
        /// Updates the details of the specified transaction.
        /// </summary>
        /// <param name="personID"></param>
        /// <param name="accountID"></param>
        /// <param name="transactionID"></param>
        /// <param name="transactionPatchDto"></param>
        /// <returns></returns>
        /// <exception cref="NotFoundPersonException"></exception>
        /// <exception cref="NotFoundAccountException"></exception>
        /// <exception cref="InvalidAccountUpdateException"></exception>
        /// <exception cref="NotFoundTransactionException"></exception>
        /// <exception cref="InvalidTransactionUpdateException"></exception>
        public Task<TransactionDto?> UpdateAsync(int personID, int accountID, int transactionID, TransactionPatchDto transaction);

        /// <summary>
        /// Deletes the transaction.
        /// </summary>
        /// <param name="personID"></param>
        /// <param name="accountID"></param>
        /// <param name="transactionID"></param>
        /// <returns></returns>
        /// <exception cref="NotFoundPersonException"></exception>
        /// <exception cref="NotFoundAccountException"></exception>
        /// <exception cref="NotFoundTransactionException"></exception>
        public Task<bool> DeleteAsync(int personID, int accountID, int transactionID);
    }
}
