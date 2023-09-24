using Accounting.API.DTOs.Account;

namespace Accounting.API.DAOs.Account;

public interface IAccountDao
{
    /// <summary>
    /// Adds an account for the specified person.
    /// </summary>
    /// <param name="personID"></param>
    /// <param name="account"></param>
    public Task<AccountDto> AddAsync(int personID, AccountAddDto account);

    /// <summary>
    /// Updates only the changed fields for the specified account.
    /// </summary>
    /// <param name="personID"></param>
    /// <param name="accountID"></param>
    /// <param name="account"></param>
    public Task<AccountDto> UpdateAsync(int personID, int accountID, AccountPatchDto account);

    /// <summary>
    /// Gets the details for a specific account.
    /// </summary>
    /// <param name="personID"></param>
    /// <param name="accountID"></param>
    public Task<AccountDto> GetAsync(int personID, int accountID);

    /// <summary>
    /// Gets all the existing accounts for a specific user.
    /// </summary>
    /// <param name="personID"></param>
    public Task<AccountsSummaryDto> GetAllAsync(int personID);

    /// <summary>
    /// Deletes the specified account.
    /// </summary>
    /// <param name="personID"></param>
    /// <param name="accountID"></param>
    /// <returns>A bool indicating whether the deletion was successful.</returns>
    public Task<bool> DeleteAsync(int personID, int accountID);
}
