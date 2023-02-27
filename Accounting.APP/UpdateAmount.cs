using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Accounting
{
    public partial class UpdateAmount : Form
    {
        public decimal Amount { get; set; }
        public UpdateAmount(string title, string label, string placeholder = "")
        {
            InitializeComponent();
            txtInput.Text = placeholder;
            lblLabel.Text = label;
            this.Text = title;
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

        }
    }
}
