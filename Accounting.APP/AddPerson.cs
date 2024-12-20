using System.ComponentModel;

namespace Accounting.APP;

public partial class AddPerson : Form
{
    public Person Person { get; set; }

    public AddPerson()
    {
        InitializeComponent();
        Person = new();
    }

    private void TxtLastName_Validating(object sender, CancelEventArgs e)
    {
        if (string.IsNullOrEmpty(txtLastName.Text.Trim()))
        {
            e.Cancel = true;
            errorProvider.SetError(txtLastName, "Name cannot be empty.");
        } else
        {
            errorProvider.SetError(txtLastName, "");
        }
    }

    private void TxtFirstName_Validating(object sender, CancelEventArgs e)
    {
        if (string.IsNullOrEmpty(txtFirstName.Text.Trim()))
        {
            e.Cancel = true;
            errorProvider.SetError(txtFirstName, "Name cannot be empty.");
        }
        else
        {
            errorProvider.SetError(txtFirstName, "");
        }
    }

    private void BtnAdd_Click(object sender, EventArgs e)
    {
        if (ValidateChildren())
        {
            Person = new Person
            {
                ID = -1,
                FirstName = txtFirstName.Text.Trim(),
                LastName = txtLastName.Text.Trim(),
                MiddleName = txtMiddleName.Text.Trim()
            };
            DialogResult = DialogResult.OK;
        } else
        {
            DialogResult = DialogResult.None;
        }
    }
}
