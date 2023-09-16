using Accounting.API.DAOs;
using Accounting.API.DTOs.Account;

namespace Accounting.API.Services.Account
{
    public class AccountService: IAccountService
    {
        private readonly IAccountDao _accountDao;
        public AccountService(IAccountDao accountDao)
        {
            _accountDao = accountDao ?? throw new ArgumentException("IAccountDao is null.");
        }

        public async Task<int> AddAsync(int personID, AccountAddDto account)
        {
            return await _accountDao.AddAsync(personID, account);
        }

        public async Task<bool> DeleteAsync(int personID, int accountID)
        {
            return await _accountDao.DeleteAsync(personID, accountID);
        }

        public async Task<AccountDto> GetAsync(int personID, int accountID)
        {
            return await _accountDao.GetAsync(personID, accountID);
        }

        public async Task<AccountsSummaryDto> GetAllAsync(int personID)
        {
            return await _accountDao.GetAllAsync(personID);
        }

        public async Task<int> UpdateAsync(int personID, int accountID, AccountPatchDto account)
        {
            return await _accountDao.UpdateAsync(personID, accountID, account);
        }
    }
}
