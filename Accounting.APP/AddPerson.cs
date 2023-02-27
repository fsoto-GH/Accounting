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
    public partial class AddPerson : Form
    {
        public Person Person { get; set; }
        public AddPerson()
        {
            InitializeComponent();
        }

        private void txtLastName_Validating(object sender, CancelEventArgs e)
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

        private void txtFirstName_Validating(object sender, CancelEventArgs e)
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

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (ValidateChildren())
            {
                Person = new Person(-1, txtFirstName.Text.Trim(), txtLastName.Text.Trim(), txtMiddleName.Text.Trim());
                DialogResult = DialogResult.OK;
            } else
            {
                DialogResult = DialogResult.None;
            }
        }
    }
}
