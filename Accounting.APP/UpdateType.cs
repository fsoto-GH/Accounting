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
    public partial class UpdateType : Form
    {
        public string Type { get; set; }
        public UpdateType(string type)
        {
            InitializeComponent();
            if (type == "Debit")
            {
                cbType.SelectedIndex = 0;
            } else
            {
                cbType.SelectedIndex = 1;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Type = cbType.Text.Trim();
            DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
