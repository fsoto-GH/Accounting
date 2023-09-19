using System.ComponentModel;

namespace Accounting.APP;
public partial class UpdateAmount : Form
{
    public decimal Amount { get; set; }
    public UpdateAmount(string title, string label, string placeholder = "")
    {
        InitializeComponent();
        txtInput.Text = placeholder;
        lblLabel.Text = label;
        Text = title;
    }

    private void btnOK_Click(object sender, EventArgs e)
    {
        if (ValidateChildren())
        {
            DialogResult = DialogResult.OK;
        } else
        {
            DialogResult = DialogResult.None;
        }
    }

    private void txtInput_Validating(object sender, CancelEventArgs e)
    {
        if (decimal.TryParse(txtInput.Text.Trim(), out decimal amount))
        {
            Amount = amount;
        } else
        {
            e.Cancel = true;
        }
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
        DialogResult = DialogResult.Cancel;
    }
}
