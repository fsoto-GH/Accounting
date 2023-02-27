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
    public partial class AddAccount : Form
    {
        public string NickName { get; set; }
        public string Type { get; set; }
        public AddAccount()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            NickName = string.IsNullOrEmpty(txtNickName.Text.Trim()) ? null: txtNickName.Text.Trim();
            Type = cbType.Text.Trim();
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
}
