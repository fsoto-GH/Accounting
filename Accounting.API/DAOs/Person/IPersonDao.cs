using Accounting.API.DTOs.Person;
namespace Accounting.API.DAOs.Person;

public interface IPersonDao
{
    /// <summary>
    /// Adds the specified person and returns the PK of the inserted record.
    /// </summary>
    /// <param name="person"></param>
    public Task<PersonDto> AddAsync(PersonAddDto person);

    /// <summary>
    /// Updates the corresponding fields of the for the specified record.
    /// </summary>
    /// <param name="person"></param>
    public Task<PersonDto> UpdateAsync(int personID, PersonPatchDto person);

    /// <summary>
    /// Queries DB to get person record.
    /// </summary>
    /// <param name="personID"></param>
    public Task<PersonDto> GetAsync(int personID);

    /// <summary>
    /// Gets all the Person records.
    /// </summary>
    public Task<IEnumerable<PersonDto>> GetAllAsync();

    /// <summary>
    /// Deletes Person record matching the passed PersonID.
    /// </summary>
    /// <param name="personID"></param>
    public Task<bool> DeleteAsync(int personID, bool forceDelete = false);
}