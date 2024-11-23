using Accounting.API.DAOs;
using Accounting.API.DAOs.Person;
using Accounting.API.DTOs.Transaction;
using Accounting.API.Exceptions.Person;
using Accounting.API.Exceptions.Account;
using Accounting.API.Exceptions.Transaction;
using Accounting.API.DAOs.Transaction;
using Accounting.API.DAOs.Account;
using Accounting.API.Controllers.QueryParamaters;

namespace Accounting.API.Services.Transaction
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionDao _transactionDao;
        private readonly IAccountDao _accountDao;
        private readonly IPersonDao _personDao;

        public TransactionService(ITransactionDao transactionDao, IAccountDao accountDao, IPersonDao personDao)
        {
            _transactionDao = transactionDao ?? throw new ArgumentException("ITransactionDao is null.");
            _accountDao = accountDao ?? throw new ArgumentException("IAccountDao is null.");
            _personDao = personDao ?? throw new ArgumentException("IPersonDao is null.");
        }

        public async Task<TransactionDto> GetAsync(int personID, int accountID, int transactionID)
        {
            var person = await _personDao.GetAsync(personID);
            if (person is null)
                throw new NotFoundPersonException(personID);

            var account = await _accountDao.GetAsync(personID, accountID);
            if (account is null)
                throw new NotFoundAccountException(personID, accountID);

            var transaction = await _transactionDao.GetAsync(personID, accountID, transactionID);
            if (transaction is null)
                throw new NotFoundTransactionException(personID, accountID, transactionID);

            return transaction;
        }

        public async Task<AccountTransactionsDto> GetAllAsync(int personID, int accountID, TransactionQueryParameters nameQuery)
        {
            var person = await _personDao.GetAsync(personID);

            if (person is null)
                throw new NotFoundPersonException(personID);

            var account = await _accountDao.GetAsync(personID, accountID);

            if (account is null)
                throw new NotFoundAccountException(personID, accountID);

            return await _transactionDao.GetAllAsync(personID, accountID, nameQuery);
        }

        public async Task<TransactionDto> AddAsync(int personID, int accountID, TransactionAddDto transaction)
        {
            if (transaction is null)
                throw new InvalidTransactionAdditionException("Transaction cannot be null.");

            var person = await _personDao.GetAsync(personID);

            if (person is null)
                throw new NotFoundPersonException(personID);

            var account = await _accountDao.GetAsync(personID, accountID);
            if (account is null)
                throw new NotFoundAccountException(personID, accountID);
            if (!account.Status)
                throw new InvalidTransactionAdditionException($"Account ({accountID}) is closed, and transactions cannot be added to the account.");

            if (transaction.Amount < 0)
                throw new InvalidTransactionAdditionException("Transaction amount must be non-negative.");
            if (transaction.Type is null)
                throw new InvalidTransactionAdditionException("Transaction type is required.");

            return await _transactionDao.AddAsync(personID, accountID, transaction);
        }

        public async Task<TransactionDto> UpdateAsync(int personID, int accountID, int transactionID, TransactionPatchDto transactionPatchDto)
        {
            var person = await _personDao.GetAsync(personID);
            if (person is null)
                throw new NotFoundPersonException(personID);

            var account = await _accountDao.GetAsync(personID, accountID);
            if (account is null)
                throw new NotFoundAccountException(personID, accountID);
            if (!account.Status)
                throw new InvalidAccountUpdateException($"Account ({accountID}) is closed, and transactions cannot be updated for the account.");

            var transaction = await _transactionDao.GetAsync(personID, accountID, transactionID);
            if (transaction is null)
                throw new NotFoundTransactionException(personID, accountID, transactionID);

            if (transactionPatchDto.Amount is not null && transactionPatchDto.Amount < 0)
                throw new InvalidTransactionUpdateException("Transaction amount must be non-negative.");

            return await _transactionDao.UpdateAsync(personID, accountID, transactionID, transactionPatchDto);
        }

        public async Task<bool> DeleteAsync(int personID, int accountID, int transactionID)
        {
            var person = await _personDao.GetAsync(personID);
            if (person is null)
                throw new NotFoundPersonException(personID);

            var account = await _accountDao.GetAsync(personID, accountID);
            if (account is null)
                throw new NotFoundAccountException(personID, accountID);
            if (!account.Status)
                throw new InvalidTransactionUpdateException($"Account ({accountID}) is closed, and transactions cannot be deleted.");

            var transaction = await _transactionDao.GetAsync(personID, accountID, transactionID);
            if (transaction is null)
                throw new NotFoundTransactionException(personID, accountID, transactionID);

            return await _transactionDao.DeleteAsync(transactionID);
        }
    }
}
