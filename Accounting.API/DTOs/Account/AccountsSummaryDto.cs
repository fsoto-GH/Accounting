using System.Globalization;

namespace AccountingAPI.DTOs.Account;

public class AccountsSummaryDto
{
    public AccountsSummaryDto(double netBalace, int personID, int totalAccounts, AccountDto[] accounts)
    {
        NetBalace = netBalace;
        PersonID = personID;
        TotalAccounts = totalAccounts;
        Accounts = accounts.Select(x => new AccountDetailDto(x)).ToArray();
    }

    public double NetBalace { get; }
    public int PersonID { get; set; }
    public int TotalAccounts { get; set; }
    public string FormattedNetBalance => NetBalace.ToString("C", CultureInfo.GetCultureInfo("en-us"));
    public AccountDetailDto[] Accounts { get; set; }
}
