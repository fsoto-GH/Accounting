using System.Data;
using System.Data.SqlClient;

namespace Accounting.API.Services;

public static class AccountDatabaseFactory
{
    // TODO: Add a check constraint so that type is in ('Checking', 'Savings')
    // TODO: Add a TR so that deleting is not viable without first closing the account.
    // TODO: Add a TR so that deleting a person is not viable without first closing all the user's accounts.
    public static SqlConnection CreateConnection() => new("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Accounting;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
    
    public static SqlCommand StoredProcedureCommand(SqlConnection connection, string storedProcedureName)
    {
        var cmd = new SqlCommand(storedProcedureName, connection)
        {
            CommandType = CommandType.StoredProcedure,
        };

        return cmd;
    }
}
