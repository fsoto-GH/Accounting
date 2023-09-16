using Accounting.API.Enums;
using Accounting.APP;

namespace Accounting;

public partial class AddAccount : Form
{
    public string NickName { get; set; } = string.Empty;
    public AccountType Type { get; set; } = AccountType.CHECKING;


    public AddAccount()
    {
        InitializeComponent();
        List<ComboBoxItem<AccountType>> ls = new();
        foreach (AccountType tt in Enum.GetValues(typeof(AccountType)))
        {
            ls.Add(new ComboBoxItem<AccountType>
            {
                Name = tt.ToString(),
                Value = tt
            });
        }
        cbType.DataSource = ls;
        cbType.DisplayMember = "Name";
    }

    private void btnOK_Click(object sender, EventArgs e)
    {
        NickName = string.IsNullOrEmpty(txtNickName.Text) ? string.Empty: txtNickName.Text.Trim();
        var type = (ComboBoxItem<AccountType>)cbType.SelectedItem;
        Type = type.Value;
        DialogResult = DialogResult.OK;
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
        DialogResult = DialogResult.Cancel;
    }

    private void AddAccount_Load(object sender, EventArgs e)
    {
        cbType.SelectedIndex = 0;
    }
}
