using System.Data;
using System.Data.SqlClient;

namespace Accounting.API.Services;

public static class AccountDatabaseFactory
{
    private static readonly string? serverName = Environment.GetEnvironmentVariable("Accounting.ServerName");
    private static readonly string? userID = Environment.GetEnvironmentVariable("Accounting.UserID");
    private static readonly string? password = Environment.GetEnvironmentVariable("Accounting.Password");
    private static bool IsLocal
    {
        get
        {

            if (bool.TryParse(Environment.GetEnvironmentVariable("Accounting.UseLocal"), out bool isLocal))
                return isLocal;
            return true;
        }
    }

    public static SqlConnection CreateConnection()
    {

        return new(ConnectionString);
    }

    private static string ConnectionString
    {
        get
        {
            SqlConnectionStringBuilder? connStr;
            if (!IsLocal)
            {
                if (string.IsNullOrWhiteSpace(serverName) || string.IsNullOrWhiteSpace(userID) || string.IsNullOrWhiteSpace(password))
                {
                    throw new ArgumentException("Server name, user id, or password is undefined.");
                }

                connStr = new()
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
            }
            else
            {
                connStr = new()
                {
                    DataSource = @"(localdb)\MSSQLLocalDB",
                    InitialCatalog = "Accounting",
                    PersistSecurityInfo = false,
                    MultipleActiveResultSets = false,
                    ConnectTimeout = 30,
                    IntegratedSecurity = true,
                    TrustServerCertificate = false,
                };
            }

            return connStr.ConnectionString;
        }
    }

    public static SqlCommand StoredProcedureCommand(SqlConnection connection, string storedProcedureName)
    {
        return new SqlCommand(storedProcedureName, connection)
        {
            CommandType = CommandType.StoredProcedure,
        };
    }
}
