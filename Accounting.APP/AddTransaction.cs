using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Accounting
{
    public partial class AddTransaction : Form
    {
        public decimal Amount { get; set; } = decimal.Zero;
        public string Type { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public AddTransaction()
        {
            InitializeComponent();
        }

        private void txtAmount_Validating(object sender, CancelEventArgs e)
        {
            if (decimal.TryParse(txtAmount.Text.Trim(), out decimal amount))
            {
                Amount = amount;
                Type = cbType.Text.Trim();
                Description = txtDesc.Text.Trim();
                errorProvider.SetError(txtAmount, "");
            } else
            {
                errorProvider.SetError(txtAmount, "Not a valid amount. Must be numeric.");
                e.Cancel = true;
            }
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

        private void AddTransaction_Load(object sender, EventArgs e)
        {
            cbType.SelectedIndex = 0;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
