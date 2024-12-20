using Accounting.API.Enums;
using System;
using System.Windows.Forms;

namespace Accounting.APP;

public partial class UpdateTypeMessageBox : Form
{
    public TransactionType Type { get; set; }
    public UpdateTypeMessageBox(TransactionType type)
    {
        InitializeComponent();
        List<ComboBoxItem<TransactionType>> ls = [];
        foreach(TransactionType tt in Enum.GetValues(typeof(TransactionType)))
        {
            ls.Add(new ComboBoxItem<TransactionType>
            {
                Name = tt.ToString(),
                Value = tt
            });
        }

        cbType.DataSource = ls;
        cbType.DisplayMember = "Name";

        Type = type;
        cbType.SelectedItem = ls.Where(x => x.Value == type).First();
    }

    private void BtnOK_Click(object sender, EventArgs e)
    {
        var type = (ComboBoxItem<TransactionType>?)cbType.SelectedItem;

        if (type is not null)
        {
            Type = type.Value;
            DialogResult = DialogResult.OK;
        }
    }

    private void BtnCancel_Click(object sender, EventArgs e)
    {
        DialogResult = DialogResult.Cancel;
    }
}
