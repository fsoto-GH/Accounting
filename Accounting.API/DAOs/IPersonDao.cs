using AccountingAPI.DTOs.Person;

namespace AccountingAPI.DAOs;

public interface IPersonDao
{
    /// <summary>
    /// Adds the specified person and returns the PK of the inserted record.
    /// Returns 0 if adding person fails.
    /// </summary>
    /// <param name="person"></param>
    /// <returns>PK of the added person OR 0 if addition failed.</returns>
    public Task<int> Add(PersonAddDto person);

    /// <summary>
    /// Updates only the changed fields by querying database and comparing new values.
    /// </summary>
    /// <param name="person"></param>
    /// <returns>
    ///     -1 if the PersonID is invalid,
    ///      0 - if there is no change between the records,
    ///      1 - if the changes were successful
    /// </returns>
    public Task<int> Update(int personID, PersonPatchDto person);

    /// <summary>
    /// Queries DB to get person record.
    /// </summary>
    /// <param name="personID"></param>
    /// <returns>Returns Person record or empty person if not found.</returns>
    public Task<PersonDto> Get(int personID);

    /// <summary>
    /// Gets all the Person records.
    /// </summary>
    /// <returns>List of Persons.</returns>
    public Task<IEnumerable<PersonDto>> GetAll();

    /// <summary>
    /// Deletes Person record matching the passed PersonID.
    /// </summary>
    /// <param name="personID"></param>
    /// <returns>A bool indicating whether the deletion was successful.</returns>
    public Task<bool> Delete(int personID);
}
