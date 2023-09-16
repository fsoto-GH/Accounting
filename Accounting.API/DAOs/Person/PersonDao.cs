using Accounting.API.DTOs.Person;
using Accounting.API.DTOs.Person.PasswordHasher;
using Accounting.API.Services;
using Dapper;
using System.Data;
using System.Data.SqlClient;

namespace Accounting.API.DAOs.Person;

public class PersonDao : IPersonDao
{
    public async Task<PersonDto> GetAsync(int personID)
    {
        using var db = AccountDatabaseFactory.CreateConnection();

        string sql =
            "SELECT " +
            "	[PersonID] " +
            "	,[FirstName] " +
            "	,[LastName] " +
            "	,[MiddleName] " +
            "	,[PasswordSalt] " +
            "	,[UserName] " +
            "	,[PasswordHash] " +
            "FROM  " +
            "	[dbo].[Persons] " +
            "WHERE  " +
            "	PersonID = @personID";

        var res = await db.QuerySingleOrDefaultAsync<PersonDto>(sql, new { personID });

        return res;
    }

    public async Task<IEnumerable<PersonDto>> GetAllAsync()
    {
        using var db = AccountDatabaseFactory.CreateConnection();

        string sql =
            "SELECT " +
            "	[PersonID] " +
            "	,[FirstName] " +
            "	,[LastName] " +
            "	,[MiddleName] " +
            "	,[PasswordSalt] " +
            "	,[UserName] " +
            "	,[PasswordHash] " +
            "FROM  " +
            "	[dbo].[Persons]";

        var res = await db.QueryAsync<PersonDto>(sql);

        return res ?? Array.Empty<PersonDto>();
    }

    public async Task<PersonDto> AddAsync(PersonAddDto person)
    {
        using var db = AccountDatabaseFactory.CreateConnection();

        string sql = 
            "INSERT INTO [dbo].[Persons] ([FirstName], [LastName], [MiddleName]) " +
            "OUTPUT INSERTED.PersonID " +
            "VALUES (@firstName, @lastName, @middleName)";

        var insertedId = await db.QuerySingleAsync<int>(sql, person);

        return await GetAsync(insertedId);
    }

    public async Task<PersonDto> UpdateAsync(int personID, PersonPatchDto person)
    {
        List<string> sqlSteps = new();
        if (!string.IsNullOrWhiteSpace(person.FirstName))
        {
            sqlSteps.Add("FirstName = @firstName");
        }
        if (!string.IsNullOrWhiteSpace(person.LastName))
        {
            sqlSteps.Add("LastName = @lastName");
        }
        if (!string.IsNullOrWhiteSpace(person?.MiddleName))
        {
            sqlSteps.Add("MiddleName = @middleName");
        }

        if (sqlSteps.Count != 0)
        {
            string sql = $"UPDATE Persons SET {string.Join(", ", sqlSteps)} WHERE PersonID = @personID";
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

    public async Task<bool> DeleteAsync(int personID, bool? forceDelete=null)
    {
        string sql = 
            "DELETE FROM Persons " +
            "WHERE PersonID = @personID";

        using var db = AccountDatabaseFactory.CreateConnection();
        using var cmd = AccountDatabaseFactory.StoredProcedureCommand(db, "[dbo].[usp_DeletePerson]");
        
        cmd.Parameters.AddWithValue("@personID", personID);
        if (forceDelete is not null)
        {
            cmd.Parameters.AddWithValue("@forceClose", forceDelete.Value);
        }
        var res = await db.ExecuteAsync(sql, new { personID });

        return res == 1;
    }

    public async Task<bool> ValidateCredentials(PersonCredentialsDto personCredentials)
    {
        var passwordHash = "";
        string sql = "SELECT TOP(1) 1 FROM Persons WHERE UserName = @userName AND PasswordHash = @passwordHash";
        using var db = AccountDatabaseFactory.CreateConnection();
        return (await db.ExecuteScalarAsync<int?>(sql, new { UserName = personCredentials.UserName, passwordHash }) ?? 0) == 1;
    }

    public async Task<bool> StoreCredentials(int personID, PasswordHashResult passwordHashResult)
    {
        string sql = "UPDATE Persons SET PasswordHash = @passwordHash, PasswordSalt = @passwordSalt WHERE PersonID = @personID";
        using var db = AccountDatabaseFactory.CreateConnection();
        return await db.ExecuteAsync(sql, new { passwordHashResult.PasswordHash, passwordHashResult.PasswordSalt, personID }) == 1;
    }
}
