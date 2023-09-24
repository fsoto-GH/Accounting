using System.Data;
using System.Data.SqlClient;

namespace Accounting.API.Services;

public static class AccountDatabaseFactory
{
    // TODO: Add a check constraint so that type is in ('Checking', 'Savings')
    // TODO: Add a TR so that deleting is not viable without first closing the account.
    // TODO: Add a TR so that deleting a person is not viable without first closing all the user's accounts.

    public static SqlConnection CreateConnection()
    {
        var connStr = new SqlConnectionStringBuilder()
        {
            DataSource = @$"{@"."}",
            InitialCatalog = "Accounting",
            IntegratedSecurity = true,
            ConnectTimeout = 30,
            ApplicationIntent = ApplicationIntent.ReadWrite,
            MultiSubnetFailover = false,
            Encrypt = false,
            TrustServerCertificate = false,
        };

        Console.WriteLine(connStr.ToString());
        return new(connStr.ConnectionString);
    }

    public static SqlCommand StoredProcedureCommand(SqlConnection connection, string storedProcedureName)
    {
        var cmd = new SqlCommand(storedProcedureName, connection)
        {
            CommandType = CommandType.StoredProcedure,
        };

        return cmd;
    }
}
