namespace Accounting.API.DTOs.Account;

public class AccountDto : AccountBaseDto
{
    public int AccountID { get; set; }

    public int PersonID { get; set; }

    public bool Status { get; set; }
}
