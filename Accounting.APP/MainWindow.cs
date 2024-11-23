using Accounting.API.Enums;
using System.Data;
using System.Data.SqlClient;

namespace Accounting.APP;
public partial class MainWindow : Form
{
    // for local dev
    //private const string cxnStr = @"Data Source=(LocalDB)\MSSQLLocalDB; Initial Catalog=Accounting; Integrated Security=True; Persist Security Info=False";
    private readonly string cxnStr;
    private List<Person> people;

    public MainWindow()
    {
        InitializeComponent();

        SqlConnectionStringBuilder cnnStrBuilder;
        bool useLocal = Properties.Settings.Default.UseLocalDB;
        if (useLocal)
        {
            cnnStrBuilder = new()
            {
                DataSource = @"(localdb)\MSSQLLocalDB",
                InitialCatalog = "Accounting",
                PersistSecurityInfo = false,
                MultipleActiveResultSets = false,
                TrustServerCertificate = false,
                ConnectTimeout = 30,
                IntegratedSecurity = true
            };
        } else
        {
            string? serverName = Environment.GetEnvironmentVariable("Accounting.ServerName");
            string? userId = Environment.GetEnvironmentVariable("Accounting.UserID");
            string? password = Environment.GetEnvironmentVariable("Accounting.Password");

            if (serverName is null || userId is null || password is null)
                throw new ArgumentException("Server name, user id, or password is undefined.");

            cnnStrBuilder = new()
            {
                DataSource = serverName,
                UserID = userId,
                Password = password,
                InitialCatalog = "Accounting",
                PersistSecurityInfo = false,
                MultipleActiveResultSets = false,
                TrustServerCertificate = false,
                ConnectTimeout = 30
            };
        }

        cxnStr = cnnStrBuilder.ConnectionString;
        people = new List<Person>();
    }

    private void MainWindow_Load(object sender, EventArgs e)
    {
        lblAccountsHeader.Text = $"{"Type",-20} | {"Nickname",-100} | {"Status",-6}";
        lblTransactionsHeader.Text = $"{"Date",-10} | {"Description",-100} | {"Amount"}";
        RefreshPeople(cbPeople.SelectedIndex);
    }

    private void RefreshPeople(int? indexToSelect = null)
    {
        people = new List<Person>();
        using (SqlConnection cxn = new(cxnStr))
        {
            try
            {
                SqlCommand cmd = new()
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
                        people.Add(new Person
                        {
                            ID = (int)rdr["PersonID"],
                            FirstName = (string)rdr["FirstName"],
                            LastName = (string)rdr["LastName"],
                            MiddleName = hasMiddle ? (string)rdr["MiddleName"] : ""
                        });
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

    private void CbPeople_SelectedIndexChanged(object sender, EventArgs e)
    {
        RefreshAccounts(lstbAccount.SelectedIndex);
        RefreshTransactions();
    }

    private void RefreshAccounts(int? indexToSelect = null)
    {
        List<Account> accs = new();
        if (cbPeople.SelectedItem != null)
        {
            Person selected = (Person)cbPeople.SelectedItem;
            using SqlConnection cxn = new(cxnStr);
            try
            {
                SqlCommand cmd = new()
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
        lstbAccount.DataSource = accs;
        if (indexToSelect != null && indexToSelect > 0)
        {
            if (indexToSelect < lstbAccount.Items.Count)
            {
                lstbAccount.SelectedIndex = (int)indexToSelect;
            }
        }
    }

    private void LstbAccount_SelectedIndexChanged(object sender, EventArgs e)
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
        RefreshTransactions();
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

    private void RefreshTransactions()
    {
        List<Transaction> trans = new();
        if (lstbAccount.SelectedItem != null)
        {
            {
                Account selectedAccount = (Account)lstbAccount.SelectedItem;
                Person selectedPerson = (Person)cbPeople.SelectedItem;
                using SqlConnection cxn = new(cxnStr);
                try
                {
                    SqlCommand cmd = new()
                    {
                        CommandText = "usp_ViewAccountTransactions",
                        CommandType = CommandType.StoredProcedure,
                        Connection = cxn
                    };
                    cmd.Parameters.AddWithValue("@accountID", selectedAccount.Id);
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
                            trans.Add(new Transaction(
                                id: (int)rdr["TransactionID"],
                                desc: hasDescription ? (string)rdr["Description"] : "",
                                type: (TransactionType)Enum.Parse(typeof(TransactionType), (string)rdr["Type"]),
                                date: (DateTime)rdr["Date"],
                                amount: (decimal)rdr["Amount"]));
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

    private void BtnCloseAccount_Click(object sender, EventArgs e)
    {
        DialogResult res = MessageBox.Show($"Close \"{((Account)lstbAccount.SelectedItem).NickName}\"?", "Close selected account?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

        if (res == DialogResult.Yes)
        {
            using SqlConnection cxn = new(cxnStr);
            try
            {
                SqlCommand cmd = new()
                {
                    CommandText = "UPDATE Accounts SET Status = 0 WHERE AccountID = @accountID",
                    Connection = cxn
                };

                cmd.Parameters.AddWithValue("@accountID", ((Account)lstbAccount.SelectedItem).Id);

                cxn.Open();
                int inserted = cmd.ExecuteNonQuery();
                cxn.Close();

                if (inserted == 1)
                {
                    MessageBox.Show("Successfully closed account!");
                    RefreshAccounts(lstbAccount.SelectedIndex);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }

    private void BtnAddPerson_Click(object sender, EventArgs e)
    {
        AddPerson add = new();
        int inserted = -1;
        if (add.ShowDialog() == DialogResult.OK)
        {
            string fName = add.Person.FirstName;
            string lName = add.Person.LastName;
            string mName = string.IsNullOrEmpty(add.Person.MiddleName) ? string.Empty : add.Person.MiddleName;

            using SqlConnection cxn = new(cxnStr);
            try
            {
                SqlCommand cmd = new()
                {
                    CommandText = "INSERT INTO Persons (FirstName, LastName, MiddleName) OUTPUT inserted.PersonID VALUES (@f, @l, @m)",
                    Connection = cxn
                };
                cmd.Parameters.AddWithValue("@f", fName);
                cmd.Parameters.AddWithValue("@l", lName);

                if (string.IsNullOrWhiteSpace(mName))
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
        if (inserted == -1)
        {
            RefreshPeople(cbPeople.SelectedIndex);
        }
        else
        {
            RefreshPeople();
            cbPeople.SelectedIndex = people.IndexOf(people.Where(x => x.ID == inserted).First());
        }
    }

    private void BtnRefreshPeople_Click(object sender, EventArgs e)
    {
        RefreshPeople(cbPeople.SelectedIndex);
    }

    private void BtnRenameAccount_Click(object sender, EventArgs e)
    {
        Account selected = (Account)lstbAccount.SelectedItem;
        UpdateTextBox update = new("Rename account", "Account Name: ", selected.NickName);
        if (update.ShowDialog() == DialogResult.OK)
        {
            using SqlConnection cxn = new(cxnStr);
            try
            {
                SqlCommand cmd = new()
                {
                    CommandText = "UPDATE Accounts SET NickName = @nickName WHERE AccountID = @accountID",
                    Connection = cxn
                };

                cmd.Parameters.AddWithValue("@accountID", selected.Id);
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
                    RefreshAccounts(lstbAccount.SelectedIndex);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        RefreshPeople(cbPeople.SelectedIndex);
    }

    private void BtnAddAccount_Click(object sender, EventArgs e)
    {
        Person selected = (Person)cbPeople.SelectedItem;
        AddAccount add = new();
        if (add.ShowDialog() == DialogResult.OK)
        {
            using SqlConnection cxn = new(cxnStr);
            try
            {
                SqlCommand cmd = new()
                {
                    CommandText = "INSERT INTO Accounts (PersonID, AccountTypeID, NickName) VALUES (@personID, @type, @nickName)",
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
        RefreshAccounts(lstbAccount.SelectedIndex);
    }

    private void BtnAddTrans_Click(object sender, EventArgs e)
    {
        Account selected = (Account)lstbAccount.SelectedItem;
        AddTransaction add = new();

        if (add.ShowDialog() == DialogResult.OK)
        {
            using SqlConnection cxn = new(cxnStr);
            try
            {
                SqlCommand cmd = new()
                {
                    CommandText = "INSERT INTO Transactions (AccountID, TransactionTypeID, Amount, Description) VALUES (@accountID, @transactionTypeID, @amount, @description)",
                    Connection = cxn
                };
                TransactionType transactionTypeID = (TransactionType)Enum.Parse(typeof(TransactionType), add.Type.ToUpper());
                cmd.Parameters.AddWithValue("@accountID", selected.Id);
                cmd.Parameters.AddWithValue("@transactionTypeID", transactionTypeID);
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
        RefreshAccounts(lstbAccount.SelectedIndex);
    }

    private void BtnChangeAmount_Click(object sender, EventArgs e)
    {
        Transaction selected = (Transaction)lstbTransactions.SelectedItem;
        UpdateAmount update = new("Change transaction amount", "Transaction amount: ", selected.Amount.ToString("N2"));
        if (update.ShowDialog() == DialogResult.OK)
        {
            using SqlConnection cxn = new(cxnStr);
            try
            {
                SqlCommand cmd = new()
                {
                    CommandText = "UPDATE Transactions SET Amount = @amount WHERE TransactionID = @transID",
                    Connection = cxn
                };

                cmd.Parameters.AddWithValue("@transID", selected.TransactionID);
                cmd.Parameters.AddWithValue("@amount", SqlDbType.Decimal);
                cmd.Parameters["@amount"].Value = update.Amount.ToString("N2");

                cxn.Open();
                int updated = cmd.ExecuteNonQuery();
                cxn.Close();

                if (updated == 1)
                {
                    MessageBox.Show("Successfully updated transaction amount!");
                    RefreshAccounts(lstbAccount.SelectedIndex);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        RefreshTransactions();
    }

    private void BtnEditDesc_Click(object sender, EventArgs e)
    {
        Transaction selected = (Transaction)lstbTransactions.SelectedItem;
        UpdateTextBox update = new("Rename transaction", "Transaction Name: ", selected.Description);
        if (update.ShowDialog() == DialogResult.OK)
        {
            using SqlConnection cxn = new(cxnStr);
            try
            {
                SqlCommand cmd = new()
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
                    RefreshAccounts(lstbAccount.SelectedIndex);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        RefreshTransactions();
    }

    private void BtnChangeType_Click(object sender, EventArgs e)
    {
        Transaction selected = (Transaction)lstbTransactions.SelectedItem;
        UpdateTypeMessageBox update = new(selected.Type);

        if (update.ShowDialog() == DialogResult.OK)
        {
            using SqlConnection cxn = new(cxnStr);
            try
            {
                SqlCommand cmd = new()
                {
                    CommandText = "UPDATE Transactions SET TransactionTypeID = @type WHERE TransactionID = @transID",
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
        RefreshAccounts(lstbAccount.SelectedIndex);
    }
}
