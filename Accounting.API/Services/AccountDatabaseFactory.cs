using System.Data;
using System.Data.SqlClient;

namespace Accounting.API.Services;

public static class AccountDatabaseFactory
{
    private static readonly string? serverName = Environment.GetEnvironmentVariable("Accounting.ServerName");
    private static readonly string? userID = Environment.GetEnvironmentVariable("Accounting.UserID");
    private static readonly string? password = Environment.GetEnvironmentVariable("Accounting.Password");

    public static SqlConnection CreateConnection()
    {
        if (serverName is null || userID is null || password is null)
            throw new ArgumentException("Server name, user id, or password is undefined.");

        var connStr = new SqlConnectionStringBuilder()
        {
            DataSource = serverName,
            UserID = userID,
            Password = password,
            InitialCatalog = "Accounting",
            IntegratedSecurity = false,
            ConnectTimeout = 30,
            ApplicationIntent = ApplicationIntent.ReadWrite,
            MultiSubnetFailover = false,
            Encrypt = false,
            TrustServerCertificate = false,
        };

        return new(connStr.ConnectionString);
    }

    public static SqlCommand StoredProcedureCommand(SqlConnection connection, string storedProcedureName)
    {
        return new SqlCommand(storedProcedureName, connection)
        {
            CommandType = CommandType.StoredProcedure,
        };
    }
}
