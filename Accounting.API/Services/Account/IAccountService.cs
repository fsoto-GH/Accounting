using Accounting.API.Exceptions.Account;
using Accounting.API.Exceptions.Person;
using Accounting.API.DTOs.Account;

namespace Accounting.API.Services.Account
{
    public interface IAccountService
    {
        /// <summary>
        /// Retrieves the specied account details.
        /// </summary>
        /// <param name="personID"></param>
        /// <param name="accountID"></param>
        /// <returns></returns>
        /// <exception cref="NotFoundPersonException"></exception>
        /// <exception cref="NotFoundAccountException"></exception>
        public Task<AccountDto> GetAsync(int personID, int accountID);

        /// <summary>
        /// This returns all the accounts associated to a person.
        /// </summary>
        /// <param name="personID"></param>
        /// <returns></returns>
        /// <exception cref="NotFoundPersonException"></exception>
        public Task<AccountsSummaryDto> GetAllAsync(int personID);
        /// <summary>
        /// Creates the specified account for the specified person.
        /// </summary>
        /// <param name="personID"></param>
        /// <param name="account"></param>
        /// <returns></returns>
        /// <exception cref="NotFoundPersonException"></exception>
        public Task<AccountDto> AddAsync(int personID, AccountAddDto account);

        /// <summary>
        /// Updates the details of the corresponding person account.
        /// </summary>
        /// <param name="personID"></param>
        /// <param name="accountID"></param>
        /// <param name="accountPatchDto"></param>
        /// <returns></returns>
        /// <exception cref="NotFoundPersonException"></exception>
        /// <exception cref="NotFoundAccountException"></exception>
        /// <exception cref="InvalidAccountUpdateException"></exception>
        public Task<AccountDto> UpdateAsync(int personID, int accountID, AccountPatchDto accountPatchDto);
    }
}
