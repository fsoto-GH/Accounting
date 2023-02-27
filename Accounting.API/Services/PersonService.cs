using AccountingAPI.DAOs;
using AccountingAPI.DTOs.Person;
using Dapper;

namespace AccountingAPI.Services;

public class PersonService : IPersonDao
{
    public async Task<PersonDto> Get(int personID)
    {
        using var db = AccountingDB.CreateConnection();

        string sql = "SELECT * FROM Persons WHERE PersonID = @personID";
        var res = await db.QueryFirstOrDefaultAsync<PersonDto>(sql, new { personID });

        return res ?? new PersonDto();
    }

    public async Task<IEnumerable<PersonDto>> GetAll()
    {
        using var db = AccountingDB.CreateConnection();

        string sql = "SELECT * FROM Persons";
        var res = await db.QueryAsync<PersonDto>(sql);

        return res ?? new List<PersonDto>();
    }

    public async Task<int> Add(PersonAddDto person)
    {
        using var db = AccountingDB.CreateConnection();

        string sql = "INSERT INTO Persons ([FirstName], [LastName], [MiddleName]) OUTPUT INSERTED.PersonID VALUES (@firstName, @lastName, @middleName)";
        var res = await db.QuerySingleAsync<int?>(sql, person);

        return res ?? 0;
    }

    public async Task<int> Update(int personID, PersonPatchDto person)
    {
        var dbPerson = await Get(personID);
        if (dbPerson is null) return -1;

        List<string> sqlSteps = new();
        if (!string.IsNullOrWhiteSpace(person.FirstName) && !string.Equals(person.FirstName, dbPerson.FirstName))
        {
            sqlSteps.Add("FirstName = @firstName");
        }
        if (!string.IsNullOrWhiteSpace(person.LastName) && !string.Equals(person.LastName, dbPerson.LastName))
        {
            sqlSteps.Add("LastName = @lastName");
        }
        if (person?.MiddleName is not null && !string.Equals(person.MiddleName, dbPerson?.MiddleName))
        {
            sqlSteps.Add("middleName = @middleName");
        }

        if (sqlSteps.Count == 0) return 0;

        string sql = $"UPDATE Persons SET {string.Join(", ", sqlSteps)} WHERE PersonID = @personID";
        using var db = AccountingDB.CreateConnection();

        return await db.ExecuteAsync(sql, new
        {
            personID,
            firstName = person?.FirstName,
            lastName = person?.LastName,
            middleName = person?.MiddleName == string.Empty ? null: person?.MiddleName
        });
    }

    public async Task<bool> Delete(int personID)
    {
        string sql = "DELETE FROM Persons WHERE PersonID = @personID";
        using var db = AccountingDB.CreateConnection();
        var res = await db.ExecuteAsync(sql, new { personID });

        return res == 1;
    }
}
