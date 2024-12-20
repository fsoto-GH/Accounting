using Accounting.API.DAOs.Account;
using Accounting.API.DAOs.Person;
using Accounting.API.DTOs.Account;
using Accounting.API.Exceptions.Account;
using Accounting.API.Exceptions.Person;
using Accounting.API.Exceptions.Transaction;

namespace Accounting.API.Services.Account
{
    public class AccountService(IAccountDao accountDao, IPersonDao personDao) : IAccountService
    {
        private readonly IAccountDao _accountDao = accountDao ?? throw new ArgumentException("IAccountDao is null.");
        private readonly IPersonDao _personDao = personDao ?? throw new ArgumentException("IPersonDao is null.");

        public async Task<AccountDto> GetAsync(int personID, int accountID)
        {
            _ = await _personDao.GetAsync(personID) ?? throw new NotFoundPersonException(personID);
            var account = await _accountDao.GetAsync(personID, accountID);
            return account is null ? throw new NotFoundAccountException(personID, accountID) : account;
        }

        public async Task<AccountsSummaryDto> GetAllAsync(int personID)
        {
            _ = await _personDao.GetAsync(personID) ?? throw new NotFoundPersonException(personID);
            return await _accountDao.GetAllAsync(personID);
        }

        public async Task<AccountDto?> AddAsync(int personID, AccountAddDto account)
        {
            if (account is null)
                throw new InvalidAccountAdditionException("Account cannot be null.");
            _ = await _personDao.GetAsync(personID) ?? throw new NotFoundPersonException(personID);
            return await _accountDao.AddAsync(personID, account);
        }

        public async Task<AccountDto?> UpdateAsync(int personID, int accountID, AccountPatchDto accountPatchDto)
        {
            _ = await _personDao.GetAsync(personID) ?? throw new NotFoundPersonException(personID);
            var account = await _accountDao.GetAsync(personID, accountID) ?? throw new NotFoundAccountException(personID, accountID);
            if (account.NetBalance != 0 && accountPatchDto.Status == false)
                throw new InvalidAccountUpdateException(personID, accountID);

            return await _accountDao.UpdateAsync(personID, accountID, accountPatchDto);
        }
    }
}
