using Accounting.API.DAOs.Account;
using Accounting.API.DAOs.Person;
using Accounting.API.DAOs.Transaction;
using Accounting.API.DTOs.Account;
using Accounting.API.Exceptions.Account;
using Accounting.API.Exceptions.Person;
using Accounting.API.Exceptions.Transaction;

namespace Accounting.API.Services.Account
{
    public class AccountService : IAccountService
    {
        private readonly IAccountDao _accountDao;
        private readonly IPersonDao _personDao;
        private readonly ITransactionDao _transactionDao;

        public AccountService(IAccountDao accountDao, IPersonDao personDao, ITransactionDao transactionDao)
        {
            _accountDao = accountDao ?? throw new ArgumentException("IAccountDao is null.");
            _personDao = personDao ?? throw new ArgumentException("IPersonDao is null.");
            _transactionDao = transactionDao ?? throw new ArgumentException("ITransactionDao is null.");
        }

        public async Task<AccountDto> GetAsync(int personID, int accountID)
        {
            var person = await _personDao.GetAsync(personID);
            if (person is null)
                throw new NotFoundPersonException(personID);

            var account = await _accountDao.GetAsync(personID, accountID);
            if (account is null)
                throw new NotFoundAccountException(personID, accountID);

            return account;
        }

        public async Task<AccountsSummaryDto> GetAllAsync(int personID)
        {
            var person = await _personDao.GetAsync(personID);

            if (person is null)
                throw new NotFoundPersonException(personID);

            return await _accountDao.GetAllAsync(personID);
        }

        public async Task<AccountDto> AddAsync(int personID, AccountAddDto account)
        {
            if (account is null)
                throw new InvalidAccountAdditionException("Account cannot be null.");
            var person = await _personDao.GetAsync(personID);

            if (person is null)
                throw new NotFoundPersonException(personID);

            return await _accountDao.AddAsync(personID, account);
        }

        public async Task<AccountDto> UpdateAsync(int personID, int accountID, AccountPatchDto accountPatchDto)
        {
            var person = await _personDao.GetAsync(personID);

            if (person is null)
                throw new NotFoundPersonException(personID);

            var account = await _accountDao.GetAsync(personID, accountID);

            if (account is null)
                throw new NotFoundAccountException(personID, accountID);

            var transactions = await _transactionDao.GetAllAsync(personID, accountID);
            
            if (transactions.NetBalance != 0 && accountPatchDto.Status == false)
            {
                throw new InvalidAccountUpdateException(personID, accountID);
            }


            return await _accountDao.UpdateAsync(personID, accountID, accountPatchDto);
        }
    }
}
