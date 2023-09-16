using Accounting.API.Shared;
using Accounting.API.DAOs;
using Accounting.API.DTOs.Transaction;
using Accounting.API.Enums;

namespace Accounting.API.Services.Transaction
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionDao _transactionDao;
        private readonly IAccountDao _accountDao;
        private readonly ILogger<TransactionService> _logger;

        public TransactionService(ITransactionDao transactionDao, IAccountDao accountDao, ILogger<TransactionService> logger)
        {
            _transactionDao = transactionDao ?? throw new ArgumentException("ITransactionDao is null.");
            _accountDao = accountDao ?? throw new ArgumentException("IAccountDao is null.");
            _logger = logger ?? throw new ArgumentException("ILogger is null.");
        }

        public async Task<TransactionDto> AddAsync(int personID, int accountID, TransactionAddDto transaction)
        {
            var account = await _accountDao.GetAsync(personID, accountID);

            if (account is null) 

            if (transaction.Amount < 0)
            {
                _logger.Log(LogLevel.Error, "Attempted to add transaction with amount of {amount}", transaction.Amount);
                throw new BusinessException("Transaction amount is required and must be non-negative.");
            }
            if (string.IsNullOrWhiteSpace(transaction.Type))
            {
                _logger.Log(LogLevel.Error, "Attempted to add transaction with type of '{type}'", transaction.Type);
                throw new BusinessException("Transaction type is required.");
            } else if (EnumExtensions.ParseEnum<TransactionType>(transaction.Type) == default)
            {
                _logger.Log(LogLevel.Error, "Attempted to add transaction with type of {type}", transaction.Type);
                throw new BusinessException("Transaction type is invalid.");
            }
            return await _transactionDao.AddAsync(personID, accountID, transaction);
        }

        public async Task<bool> DeleteAsync(int personID, int accountID, int transactionID)
        {
            return await _transactionDao.DeleteAsync(transactionID);
        }

        public async Task<AccountTransactionsDto> GetAllAsync(int personID, int accountID)
        {
            return await _transactionDao.GetAllAsync(personID, accountID);
        }

        public async Task<TransactionDto> GetAsync(int personID, int accountID, int transactionID)
        {
            return await _transactionDao.GetAsync(transactionID);
        }

        public async Task<TransactionDto> UpdateAsync(int personID, int accountID, int transactionID, TransactionPatchDto transaction)
        {
            return await _transactionDao.UpdateAsync(personID, accountID, transactionID, transaction);
        }
    }
}
