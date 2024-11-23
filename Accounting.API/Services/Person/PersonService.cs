using Accounting.API.DAOs.Account;
using Accounting.API.DAOs.Person;
using Accounting.API.DTOs.Person;
using Accounting.API.Exceptions.Person;

namespace Accounting.API.Services.Person
{
    public class PersonService : IPersonService
    {
        private readonly IPersonDao _personDao;
        private readonly IAccountDao _accountDao;

        public PersonService(IPersonDao personDao, IAccountDao accountDao)
        {
            _personDao = personDao ?? throw new ArgumentException("IPersonDao is null.");
            _accountDao = accountDao ?? throw new ArgumentException("IAccountDao is null.");
        }

        public async Task<PersonDto?> GetAsync(int personID)
        {
            var person = await _personDao.GetAsync(personID);

            if (person is null)
                throw new NotFoundPersonException(personID);

            return person;
        }

        public async Task<IEnumerable<PersonDto>> GetAllAsync()
        {
            return await _personDao.GetAllAsync();
        }

        public async Task<PersonDto?> AddAsync(PersonAddDto personAddDto)
        {
            if (personAddDto is null)
                throw new InvalidPersonAdditionException($"The supplied person details are invalid, and the entry could not be created.");

            personAddDto.TrimNames();
            return await _personDao.AddAsync(personAddDto);
        }

        public async Task<PersonDto?> UpdateAsync(int personID, PersonPatchDto personPatchDto)
        {
            var person = await _personDao.GetAsync(personID);
            if (person is null)
                throw new NotFoundPersonException(personID);

            personPatchDto.TrimNames();
            if (personPatchDto.FirstName?.Length == 0)
                throw new InvalidPersonUpdateException($"First name cannot be empty.");
            if (personPatchDto.LastName?.Length == 0)
                throw new InvalidPersonUpdateException($"Last name cannot be empty.");

            return await _personDao.UpdateAsync(personID, personPatchDto);
        }

        public async Task<bool> DeleteAsync(int personID, bool forceDelete = false)
        {
            var person = await _personDao.GetAsync(personID);

            if (person is null)
                throw new NotFoundPersonException(personID);

            var personAccounts = await _accountDao.GetAllAsync(personID);
            if (personAccounts.NetBalace != 0 && !forceDelete)
                throw new InvalidPersonDeletionException($"Person ({personID}) has a non-zero balance.");

            return await _personDao.DeleteAsync(personID);
        }
    }
}
