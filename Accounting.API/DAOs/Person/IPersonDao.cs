using Accounting.API.DTOs.Person;
using Accounting.API.DTOs.Person.PasswordHasher;
namespace Accounting.API.DAOs.Person;

public interface IPersonDao
{
    /// <summary>
    /// Adds the specified person and returns the PK of the inserted record.
    /// </summary>
    /// <param name="person"></param>
    /// <returns>PK of the added person OR 0 if addition failed.</returns>
    public Task<PersonDto> AddAsync(PersonAddDto person);

    /// <summary>
    /// Updates the corresponding fields of the for the specified record.
    /// </summary>
    /// <param name="person"></param>
    /// <returns>
    ///      0 - if there is no change between the records,
    ///      1 - if the changes were successful
    /// </returns>
    public Task<PersonDto> UpdateAsync(int personID, PersonPatchDto person);

    /// <summary>
    /// Queries DB to get person record.
    /// </summary>
    /// <param name="personID"></param>
    /// <returns>Returns Person record.</returns>
    public Task<PersonDto> GetAsync(int personID);

    /// <summary>
    /// Gets all the Person records.
    /// </summary>
    /// <returns>List of Persons or an empty list.</returns>
    public Task<IEnumerable<PersonDto>> GetAllAsync();

    /// <summary>
    /// Deletes Person record matching the passed PersonID.
    /// </summary>
    /// <param name="personID"></param>
    /// <returns>A bool indicating whether the deletion was successful.</returns>
    public Task<bool> DeleteAsync(int personID, bool forceDelete = false);
}