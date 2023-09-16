using Accounting.API.DAOs;
using Accounting.API.DAOs.Person;
using Accounting.API.DTOs.Person;

namespace Accounting.API.Services.Person
{
    public class PersonService: IPersonService
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
            return await _personDao.GetAsync(personID);
        }

        public async Task<IEnumerable<PersonDto>> GetAllAsync()
        {
            return await _personDao.GetAllAsync();
        }

        public async Task<PersonDto?> UpdateAsync(int personID, PersonPatchDto person)
        {
            if (_personDao.GetAsync(personID) is null)
                return null;

            person.TrimNames();
            return await _personDao.UpdateAsync(personID, person);
        }

        public async Task<bool> DeleteAsync(int personID)
        {
            if (_personDao.GetAsync(personID) is null)
                return false;

            return await _personDao.DeleteAsync(personID);
        }

        public async Task<PersonDto?> AddAsync(PersonAddDto person)
        {
            if (person is null)
                return null;

            person.TrimNames();
            return await _personDao.AddAsync(person);
        }

        public async Task<bool> ValidateCredentialsAsync(PersonCredentialsDto personCredentials)
        {
            return await _personDao.ValidateCredentials(personCredentials);
        }
    }
}
