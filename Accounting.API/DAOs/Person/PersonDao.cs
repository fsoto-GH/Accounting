using Accounting.API.DTOs.Person;
using Accounting.API.DTOs.Person.PasswordHasher;
using Accounting.API.Exceptions.Person;
using Accounting.API.Services;
using Dapper;

namespace Accounting.API.DAOs.Person;

public class PersonDao : IPersonDao
{
    public async Task<PersonDto?> GetAsync(int personID)
    {
        using var db = AccountDatabaseFactory.CreateConnection();

        string sql = $@"
SELECT 
    PersonID [{nameof(PersonDto.PersonID)}]
    , FirstName [{nameof(PersonDto.FirstName)}]
    , LastName [{nameof(PersonDto.LastName)}]
    , MiddleName [{nameof(PersonDto.MiddleName)}]
FROM 
    [dbo].[Persons] 
WHERE 
    PersonID = @personID";

        return await db.QuerySingleOrDefaultAsync<PersonDto>(sql, new { personID });
    }

    public async Task<IEnumerable<PersonDto>> GetAllAsync()
    {
        using var db = AccountDatabaseFactory.CreateConnection();

        string sql = $@"
SELECT 
    PersonID [{nameof(PersonDto.PersonID)}]
    , FirstName [{nameof(PersonDto.FirstName)}]
    , LastName [{nameof(PersonDto.LastName)}]
    , MiddleName [{nameof(PersonDto.MiddleName)}]
FROM  
    [dbo].[Persons]
ORDER BY
    FirstName
    , LastName
    , MiddleName";

        return await db.QueryAsync<PersonDto>(sql); ;
    }

    public async Task<PersonDto?> AddAsync(PersonAddDto person)
    {
        using var db = AccountDatabaseFactory.CreateConnection();

        string sql = $@"
INSERT INTO 
    [dbo].[Persons] ([FirstName], [LastName], [MiddleName]) 
OUTPUT 
    INSERTED.PersonID 
VALUES (@firstName, @lastName, @middleName)";

        var insertedID = await db.QuerySingleAsync<int>(sql, person);

        return await GetAsync(insertedID);
    }

    public async Task<PersonDto?> UpdateAsync(int personID, PersonPatchDto person)
    {
        if (person is null)
            throw new InvalidPersonUpdateException("Person cannot be null.");

        List<string> sqlSteps = [];
        if (!string.IsNullOrWhiteSpace(person.FirstName))
        {
            sqlSteps.Add("FirstName = @firstName");
        }
        if (!string.IsNullOrWhiteSpace(person.LastName))
        {
            sqlSteps.Add("LastName = @lastName");
        }
        if (person?.MiddleName == string.Empty)
        {
            sqlSteps.Add("MiddleName = @middleName");
        }

        if (sqlSteps.Count != 0)
        {
            string sql = $@"
UPDATE
    [dbo].[Persons] 
SET 
    {string.Join(", ", sqlSteps)} 
WHERE 
    PersonID = @personID";
            using var db = AccountDatabaseFactory.CreateConnection();

            await db.ExecuteAsync(sql, new
            {
                personID,
                firstName = person?.FirstName,
                lastName = person?.LastName,
                middleName = person?.MiddleName == string.Empty ? null : person?.MiddleName
            });
        }

        return await GetAsync(personID);
    }

    public async Task<bool> DeleteAsync(int personID, bool forceDelete = false)
    {
        string sql = $@"
DELETE FROM 
    [dbo].[Persons] 
WHERE 
    PersonID = @personID";

        using var db = AccountDatabaseFactory.CreateConnection();
        using var cmd = AccountDatabaseFactory.StoredProcedureCommand(db, "[dbo].[usp_DeletePerson]");

        cmd.Parameters.AddWithValue("@personID", personID);
        cmd.Parameters.AddWithValue("@forceClose", forceDelete);

        var res = await db.ExecuteAsync(sql, new { personID });

        return res == 1;
    }
}
