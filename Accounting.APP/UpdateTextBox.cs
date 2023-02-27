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
    public partial class UpdateTextBox : Form
    {
        public string NewInput { get; set; }
        public UpdateTextBox(string title, string label, string placeholder = "")
        {
            InitializeComponent();
            txtInput.Text = placeholder;
            lblLabel.Text = label;
            this.Text = title;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtInput.Text.Trim()))
            {
                NewInput = null;
            } else
            {
                NewInput = txtInput.Text.Trim();
            }
            DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
