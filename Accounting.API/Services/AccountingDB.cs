using System.Data.SqlClient;

namespace AccountingAPI.Services;

public static class AccountingDB
{
    // TODO: Add a check constraint so that type is in ('Checking', 'Savings')
    // TODO: Add a TR so that deleting is not viable without first closing the account.
    // TODO: Add a TR so that deleting a person is not viable without first closing all the user's accounts.
    public static SqlConnection CreateConnection() => new("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Accounting;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
}
