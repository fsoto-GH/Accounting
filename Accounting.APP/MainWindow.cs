using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace Accounting
{
    public partial class MainWindow : Form
    {
        private const string cxnStr = @"Data Source=(LocalDB)\MSSQLLocalDB; Initial Catalog=Accounting; Integrated Security=True; Persist Security Info=False";
        private List<Person> people;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            lblAccountsHeader.Text = $"{"Type",-20} | {"Nickname",-100} | {"Status",-6}";
            lblTransactionsHeader.Text = $"{"Date",-10} | {"Description",-100} | {"Amount"}";
            refreshPeople(cbPeople.SelectedIndex);
        }

        private void refreshPeople(int? indexToSelect = null)
        {
            people = new List<Person>();
            using (SqlConnection cxn = new SqlConnection(cxnStr))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand()
                    {
                        CommandText = "SELECT * FROM Persons",
                        CommandType = CommandType.Text,
                        Connection = cxn,
                    };

                    cxn.Open();
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            bool hasMiddle = rdr["MiddleName"] != DBNull.Value;
                            people.Add(new Person((int)rdr["PersonID"], (string)rdr["FirstName"], (string)rdr["LastName"], hasMiddle ? (string)rdr["MiddleName"] : ""));
                        }
                    }
                    cxn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            cbPeople.DataSource = people;
            if (indexToSelect != null && indexToSelect > 0)
            {
                if (indexToSelect < cbPeople.Items.Count)
                {
                    cbPeople.SelectedIndex = (int)indexToSelect;
                }
            }
        }

        private void cbPeople_SelectedIndexChanged(object sender, EventArgs e)
        {
            refreshAccounts(lstbAccount.SelectedIndex);
            refreshTransactions();
        }

        private void refreshAccounts(int? indexToSelect = null)
        {
            List<Account> accs = new List<Account>();
            if (cbPeople.SelectedItem != null)
            {
                Person selected = (Person)cbPeople.SelectedItem;
                using (SqlConnection cxn = new SqlConnection(cxnStr))
                {
                    try
                    {
                        SqlCommand cmd = new SqlCommand()
                        {
                            CommandText = "usp_ViewPersonAccounts",
                            CommandType = CommandType.StoredProcedure,
                            Connection = cxn
                        };
                        cmd.Parameters.AddWithValue("@personID", selected.ID);

                        cmd.Parameters.Add("@netBalance", SqlDbType.Money);
                        cmd.Parameters["@netBalance"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add("@accountCount", SqlDbType.Int);
                        cmd.Parameters["@accountCount"].Direction = ParameterDirection.Output;


                        cxn.Open();
                        using (SqlDataReader rdr = cmd.ExecuteReader())
                        {
                            while (rdr.Read())
                            {
                                bool hasName = rdr["NickName"] != DBNull.Value;
                                accs.Add(new Account((int)rdr["AccountID"], hasName ? (string)rdr["NickName"] : "", (string)rdr["Type"], (bool)rdr["Status"]));
                            }
                        }
                        cxn.Close();

                        lblAccCount.Text = $"{cmd.Parameters["@accountCount"].Value}";
                        lblBalanceNet.Text = $"{cmd.Parameters["@netBalance"].Value:C2}";

                        if (accs.Count > 0)
                        {
                            btnRenameAccount.Enabled = true;
                        }
                        else
                        {
                            btnCloseAccount.Enabled = false;
                            btnRenameAccount.Enabled = false;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            lstbAccount.DataSource = accs;
            if (indexToSelect != null && indexToSelect > 0)
            {
                if (indexToSelect < lstbAccount.Items.Count)
                {
                    lstbAccount.SelectedIndex = (int)indexToSelect;
                }
            }
        }

        private void lstbAccount_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstbAccount.SelectedItem != null)
            {
                Account selected = (Account)lstbAccount.SelectedItem;

                if (selected.Status == "Open")
                {
                    btnCloseAccount.Enabled = true;
                    btnAddTrans.Enabled = true;
                    btnChangeAmount.Enabled = true;
                }
                else
                {
                    btnCloseAccount.Enabled = false;
                    btnAddTrans.Enabled = false;
                    btnChangeAmount.Enabled = false;
                }
                btnRenameAccount.Enabled = true;
            }
            else
            {
                btnRenameAccount.Enabled = false;
                DisableAll();
            }
            refreshTransactions();
        }

        private void DisableAll()
        {
            btnCloseAccount.Enabled = false;
            btnRenameAccount.Enabled = false;
            btnAddAccount.Enabled = false;
            btnChangeAmount.Enabled = false;
            btnEditDesc.Enabled = false;
            btnAddTrans.Enabled = false;
        }

        private void refreshTransactions()
        {
            List<Transaction> trans = new List<Transaction>();
            if (lstbAccount.SelectedItem != null)
            {
                {
                    Account selectedAccount = (Account)lstbAccount.SelectedItem;
                    Person selectedPerson = (Person)cbPeople.SelectedItem;
                    using (SqlConnection cxn = new SqlConnection(cxnStr))
                    {
                        try
                        {
                            SqlCommand cmd = new SqlCommand()
                            {
                                CommandText = "usp_ViewAccountTransactions",
                                CommandType = CommandType.StoredProcedure,
                                Connection = cxn
                            };
                            cmd.Parameters.AddWithValue("@accountID", selectedAccount.ID);
                            cmd.Parameters.AddWithValue("@personID", selectedPerson.ID);

                            cmd.Parameters.Add("@netBalance", SqlDbType.Money);
                            cmd.Parameters["@netBalance"].Direction = ParameterDirection.Output;

                            cmd.Parameters.Add("@totalPayments", SqlDbType.Money);
                            cmd.Parameters["@totalPayments"].Direction = ParameterDirection.Output;

                            cmd.Parameters.Add("@totalPurchases", SqlDbType.Money);
                            cmd.Parameters["@totalPurchases"].Direction = ParameterDirection.Output;

                            cxn.Open();
                            using (SqlDataReader rdr = cmd.ExecuteReader())
                            {
                                while (rdr.Read())
                                {
                                    bool hasDescription = rdr["Description"] != DBNull.Value;
                                    trans.Add(new Transaction((int)rdr["TransactionID"], hasDescription ? (string)rdr["Description"] : "", (string)rdr["Type"], (DateTime)rdr["Date"], (decimal)rdr["Amount"]));
                                }
                            }
                            cxn.Close();

                            lblTransNet.Text = $"{cmd.Parameters["@netBalance"].Value:C2}";
                            lblTotalPayments.Text = $"{cmd.Parameters["@totalPayments"].Value:C2}";
                            lblTotalPurchases.Text = $"{cmd.Parameters["@totalPurchases"].Value:C2}";
                            if (selectedAccount.Status == "Open")
                            {
                                btnChangeType.Enabled = true;
                            }
                            else
                            {
                                btnChangeType.Enabled = false;
                            }

                            if ((decimal)cmd.Parameters["@netBalance"].Value == 0 && selectedAccount.Status == "Open")
                            {
                                btnCloseAccount.Enabled = true;
                            }
                            else
                            {
                                btnCloseAccount.Enabled = false;
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                }
            }
            else
            {
                lblTransNet.Text = $"{0:C2}";
                lblTotalPayments.Text = $"{0:C2}";
                lblTotalPurchases.Text = $"{0:C2}";
            }
            lstbTransactions.DataSource = trans;
            if (lstbTransactions.Items.Count > 0)
            {
                btnEditDesc.Enabled = true;
            }
            else
            {
                btnEditDesc.Enabled = false;
                btnChangeAmount.Enabled = false;
                btnChangeType.Enabled = false;
            }
        }

        private void btnCloseAccount_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show($"Close \"{((Account)lstbAccount.SelectedItem).NickName}\"?", "Close selected account?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (res == DialogResult.Yes)
            {
                using (SqlConnection cxn = new SqlConnection(cxnStr))
                {
                    try
                    {
                        SqlCommand cmd = new SqlCommand()
                        {
                            CommandText = "UPDATE Accounts SET Status = 0 WHERE AccountID = @accountID",
                            Connection = cxn
                        };

                        cmd.Parameters.AddWithValue("@accountID", ((Account)lstbAccount.SelectedItem).ID);

                        cxn.Open();
                        int inserted = cmd.ExecuteNonQuery();
                        cxn.Close();

                        if (inserted == 1)
                        {
                            MessageBox.Show("Successfully closed account!");
                            refreshAccounts(lstbAccount.SelectedIndex);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        private void btnAddPerson_Click(object sender, EventArgs e)
        {
            AddPerson add = new AddPerson();
            int inserted = -1;
            if (add.ShowDialog() == DialogResult.OK)
            {
                string fName = add.Person.FirstName;
                string lName = add.Person.LastName;
                string mName = string.IsNullOrEmpty(add.Person.MiddleName) ? null : add.Person.MiddleName;

                using (SqlConnection cxn = new SqlConnection(cxnStr))
                {
                    try
                    {
                        SqlCommand cmd = new SqlCommand()
                        {
                            CommandText = "INSERT INTO Persons (FirstName, LastName, MiddleName) OUTPUT inserted.PersonID VALUES (@f, @l, @m)",
                            Connection = cxn
                        };
                        cmd.Parameters.AddWithValue("@f", fName);
                        cmd.Parameters.AddWithValue("@l", lName);

                        if (mName == null)
                        {
                            cmd.Parameters.AddWithValue("@m", DBNull.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@m", mName);
                        }

                        cxn.Open();
                        inserted = (int)cmd.ExecuteScalar();
                        cxn.Close();

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            if (inserted == -1)
            {
                refreshPeople(cbPeople.SelectedIndex);
            }
            else
            {
                refreshPeople();
                cbPeople.SelectedIndex = people.IndexOf(people.Where(x => x.ID == inserted).First());
            }
        }

        private void btnRefreshPeople_Click(object sender, EventArgs e)
        {
            refreshPeople(cbPeople.SelectedIndex);
        }

        private void btnRenameAccount_Click(object sender, EventArgs e)
        {
            Account selected = (Account)lstbAccount.SelectedItem;
            UpdateTextBox update = new UpdateTextBox("Rename account", "Account Name: ", selected.NickName);
            if (update.ShowDialog() == DialogResult.OK)
            {
                using (SqlConnection cxn = new SqlConnection(cxnStr))
                {
                    try
                    {
                        SqlCommand cmd = new SqlCommand()
                        {
                            CommandText = "UPDATE Accounts SET NickName = @nickName WHERE AccountID = @accountID",
                            Connection = cxn
                        };

                        cmd.Parameters.AddWithValue("@accountID", selected.ID);
                        if (update.NewInput == null)
                        {
                            cmd.Parameters.AddWithValue("@nickName", DBNull.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@nickName", update.NewInput);
                        }

                        cxn.Open();
                        int updated = cmd.ExecuteNonQuery();
                        cxn.Close();

                        if (updated == 1)
                        {
                            MessageBox.Show("Successfully renamed account!");
                            refreshAccounts(lstbAccount.SelectedIndex);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            refreshPeople(cbPeople.SelectedIndex);
        }

        private void btnAddAccount_Click(object sender, EventArgs e)
        {
            Person selected = (Person)cbPeople.SelectedItem;
            AddAccount add = new AddAccount();
            if (add.ShowDialog() == DialogResult.OK)
            {
                using (SqlConnection cxn = new SqlConnection(cxnStr))
                {
                    try
                    {
                        SqlCommand cmd = new SqlCommand()
                        {
                            CommandText = "INSERT INTO Accounts (PersonID, Type, NickName) VALUES (@personID, @type, @nickName)",
                            Connection = cxn
                        };

                        cmd.Parameters.AddWithValue("@type", add.Type);
                        cmd.Parameters.AddWithValue("@personID", selected.ID);

                        if (add.NickName == null)
                        {
                            cmd.Parameters.AddWithValue("@nickName", DBNull.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@nickName", add.NickName);
                        }

                        cxn.Open();
                        int inserted = cmd.ExecuteNonQuery();
                        if (inserted == 1)
                        {
                            MessageBox.Show("Successfully added account!");
                        }
                        cxn.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            refreshAccounts(lstbAccount.SelectedIndex);
        }

        private void btnAddTrans_Click(object sender, EventArgs e)
        {
            Account selected = (Account)lstbAccount.SelectedItem;
            AddTransaction add = new AddTransaction();

            if (add.ShowDialog() == DialogResult.OK)
            {
                using (SqlConnection cxn = new SqlConnection(cxnStr))
                {
                    try
                    {
                        SqlCommand cmd = new SqlCommand()
                        {
                            CommandText = "INSERT INTO Transactions (AccountID, Type, Amount, Description) VALUES (@accountID, @type, @amount, @description)",
                            Connection = cxn
                        };

                        cmd.Parameters.AddWithValue("@accountID", selected.ID);
                        cmd.Parameters.AddWithValue("@type", add.Type);
                        cmd.Parameters.AddWithValue("@amount", add.Amount);

                        if (string.IsNullOrEmpty(add.Description.Trim()))
                        {
                            cmd.Parameters.AddWithValue("@description", DBNull.Value);

                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@description", add.Description);
                        }

                        cxn.Open();
                        int inserted = cmd.ExecuteNonQuery();

                        if (inserted == 1)
                        {
                            MessageBox.Show("Successfully added transaction.");
                        }
                        cxn.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            refreshAccounts(lstbAccount.SelectedIndex);
        }

        private void btnChangeAmount_Click(object sender, EventArgs e)
        {
            Transaction selected = (Transaction)lstbTransactions.SelectedItem;
            UpdateAmount update = new UpdateAmount("Change transaction amount", "Transaction amount: ", selected.Amount.ToString());
            if (update.ShowDialog() == DialogResult.OK)
            {
                using (SqlConnection cxn = new SqlConnection(cxnStr))
                {
                    try
                    {
                        SqlCommand cmd = new SqlCommand()
                        {
                            CommandText = "UPDATE Transactions SET Amount = @amount WHERE TransactionID = @transID",
                            Connection = cxn
                        };

                        cmd.Parameters.AddWithValue("@transID", selected.TransactionID);
                        cmd.Parameters.AddWithValue("@amount", SqlDbType.Decimal);
                        cmd.Parameters["@amount"].Value = update.Amount;

                        cxn.Open();
                        int updated = cmd.ExecuteNonQuery();
                        cxn.Close();

                        if (updated == 1)
                        {
                            MessageBox.Show("Successfully updated transaction amount!");
                            refreshAccounts(lstbAccount.SelectedIndex);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            refreshTransactions();
        }

        private void btnEditDesc_Click(object sender, EventArgs e)
        {
            Transaction selected = (Transaction)lstbTransactions.SelectedItem;
            UpdateTextBox update = new UpdateTextBox("Rename transaction", "Transaction Name: ", selected.Description);
            if (update.ShowDialog() == DialogResult.OK)
            {
                using (SqlConnection cxn = new SqlConnection(cxnStr))
                {
                    try
                    {
                        SqlCommand cmd = new SqlCommand()
                        {
                            CommandText = "UPDATE Transactions SET Description = @description WHERE TransactionID = @transID",
                            Connection = cxn
                        };

                        cmd.Parameters.AddWithValue("@transID", selected.TransactionID);
                        if (update.NewInput == null)
                        {
                            cmd.Parameters.AddWithValue("@description", DBNull.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@description", update.NewInput);
                        }

                        cxn.Open();
                        int updated = cmd.ExecuteNonQuery();
                        cxn.Close();

                        if (updated == 1)
                        {
                            MessageBox.Show("Successfully renamed transaction!");
                            refreshAccounts(lstbAccount.SelectedIndex);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            refreshTransactions();
        }

        private void btnChangeType_Click(object sender, EventArgs e)
        {
            Transaction selected = (Transaction)lstbTransactions.SelectedItem;
            UpdateType update = new UpdateType(selected.Type);

            if (update.ShowDialog() == DialogResult.OK)
            {
                using (SqlConnection cxn = new SqlConnection(cxnStr))
                {
                    try
                    {
                        SqlCommand cmd = new SqlCommand()
                        {
                            CommandText = "UPDATE Transactions SET Type = @type WHERE TransactionID = @transID",
                            Connection = cxn
                        };

                        cmd.Parameters.AddWithValue("@transID", selected.TransactionID);
                        cmd.Parameters.AddWithValue("@type", update.Type);

                        cxn.Open();
                        int updated = cmd.ExecuteNonQuery();
                        cxn.Close();

                        if (updated == 1)
                        {
                            MessageBox.Show("Successfully updated transaction type.");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            refreshAccounts(lstbAccount.SelectedIndex);
        }
    }
}
