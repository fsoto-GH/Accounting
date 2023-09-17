using Accounting.API.DTOs.Person;
using Accounting.API.Exceptions.Person;


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
        /// <param name="personID"></param>
        /// <returns></returns>
        /// <exception cref="NotFoundPersonException"></exception>
        public Task<PersonDto?> GetAsync(int personID);

        /// <summary>
        /// Retrieves all the existing persons.
        /// </summary>
        public Task<IEnumerable<PersonDto>> GetAllAsync();

        /// <summary>
        /// This updates the details of the specified person.
        /// </summary>
        /// /// <param name="personID"></param>
        /// <param name="personPatchDto"></param>
        /// <returns></returns>
        /// <exception cref="NotFoundPersonException"></exception>
        /// <exception cref="InvalidPersonUpdateException"></exception>
        public Task<PersonDto?> UpdateAsync(int personID, PersonPatchDto person);

        /// <summary>
        /// This deletes the person from the database.
        /// </summary>
        /// <param name="personID"></param>
        /// <param name="forceDelete">whether for forcefully delete person even with a net balance</param>
        /// <returns></returns>
        /// <exception cref="NotFoundPersonException"></exception>
        /// <exception cref="InvalidPersonDeletionException"></exception>
        public Task<bool> DeleteAsync(int personID, bool forceDelete = false);

        /// <summary>
        /// Creates an entry for the specified person.
        /// </summary>
        /// <param name="personAddDto"></param>
        /// <returns>A <seealso cref="PersonDto"/> corresponding to the created person entry.</returns>
        /// <exception cref="InvalidPersonAdditionException"></exception>
        public Task<PersonDto?> AddAsync(PersonAddDto person);
    }
}
