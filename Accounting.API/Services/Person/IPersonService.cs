using Accounting.API.DTOs.Person;

namespace Accounting.API.Services.Person
{
    /// <summary>
    /// The purpose of this class is to perform logical validation and massage data for DAO operations.
    /// </summary>
    public interface IPersonService
    {
        /// <summary>
        /// Retrieves the details of the specified person.
        /// </summary>
        public Task<PersonDto?> GetAsync(int personID);

        /// <summary>
        /// Retrieves all the existing persons.
        /// </summary>
        public Task<IEnumerable<PersonDto>> GetAllAsync();


        /// <summary>
        /// This updates the details of the specified person.
        /// </summary>
        public Task<PersonDto?> UpdateAsync(int personID, PersonPatchDto person);

        /// <summary>
        /// This deletes the person from the database. Referential integrity should delete associated accounts and transactions.
        /// </summary>
        /// <param name="personID"></param>
        /// <returns>Return whether the record was deleted or not.</returns>
        public Task<bool> DeleteAsync(int personID);

        /// <summary>
        /// Creates an entry for the specified person.
        /// </summary>
        public Task<PersonDto?> AddAsync(PersonAddDto person);
    }
}
