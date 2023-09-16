using Accounting.API.DAOs;
using Accounting.API.DTOs.Account;

namespace Accounting.API.Services.Account
{
    public interface IAccountService
    {
        /// <summary>
        /// Creates the specified account for the specified person.
        /// </summary>
        public Task<int> AddAsync(int personID, AccountAddDto account);

        /// <summary>
        /// Deletes the specied account.
        /// </summary>
        public Task<bool> DeleteAsync(int personID, int accountID);

        /// <summary>
        /// Retrieves the specied account details.
        /// </summary>
        public Task<AccountDto> GetAsync(int personID, int accountID);

        /// <summary>
        /// This returns all the accounts associated to a person.
        /// </summary>
        public Task<AccountsSummaryDto> GetAllAsync(int personID);

        /// <summary>
        /// Updates the details of the corresponding person account.
        /// </summary>
        public Task<int> UpdateAsync(int personID, int accountID, AccountPatchDto account);
    }
}
