namespace Accounting.APP;

partial class MainWindow
{
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container();
        cbPeople = new ComboBox();
        lblAccCount = new Label();
        lblBalanceNet = new Label();
        lstbAccount = new ListBox();
        lstbTransactions = new ListBox();
        btnCloseAccount = new Button();
        label1 = new Label();
        label2 = new Label();
        lblAccountsHeader = new Label();
        lblTransactionsHeader = new Label();
        label3 = new Label();
        lblTransNet = new Label();
        btnAddAccount = new Button();
        btnAddTrans = new Button();
        btnRenameAccount = new Button();
        btnAddPerson = new Button();
        toolTip = new ToolTip(components);
        btnRefreshPeople = new Button();
        groupBox1 = new GroupBox();
        groupBox2 = new GroupBox();
        btnChangeType = new Button();
        btnEditDesc = new Button();
        btnChangeAmount = new Button();
        label4 = new Label();
        lblTotalPayments = new Label();
        label5 = new Label();
        lblTotalPurchases = new Label();
        groupBox1.SuspendLayout();
        groupBox2.SuspendLayout();
        SuspendLayout();
        // 
        // cbPeople
        // 
        cbPeople.DropDownStyle = ComboBoxStyle.DropDownList;
        cbPeople.FormattingEnabled = true;
        cbPeople.Location = new Point(14, 24);
        cbPeople.Margin = new Padding(4, 3, 4, 3);
        cbPeople.Name = "cbPeople";
        cbPeople.Size = new Size(384, 23);
        cbPeople.TabIndex = 0;
        cbPeople.SelectedIndexChanged += CbPeople_SelectedIndexChanged;
        // 
        // lblAccCount
        // 
        lblAccCount.AutoSize = true;
        lblAccCount.Location = new Point(650, 283);
        lblAccCount.Margin = new Padding(4, 0, 4, 0);
        lblAccCount.Name = "lblAccCount";
        lblAccCount.Size = new Size(89, 15);
        lblAccCount.TabIndex = 1;
        lblAccCount.Text = "[# of Accounts]";
        // 
        // lblBalanceNet
        // 
        lblBalanceNet.AutoSize = true;
        lblBalanceNet.Location = new Point(868, 283);
        lblBalanceNet.Margin = new Padding(4, 0, 4, 0);
        lblBalanceNet.Name = "lblBalanceNet";
        lblBalanceNet.Size = new Size(84, 15);
        lblBalanceNet.TabIndex = 2;
        lblBalanceNet.Text = "[Total Balance]";
        // 
        // lstbAccount
        // 
        lstbAccount.Font = new Font("Consolas", 8.25F);
        lstbAccount.FormattingEnabled = true;
        lstbAccount.ItemHeight = 13;
        lstbAccount.Location = new Point(14, 110);
        lstbAccount.Margin = new Padding(4, 3, 4, 3);
        lstbAccount.Name = "lstbAccount";
        lstbAccount.Size = new Size(945, 160);
        lstbAccount.TabIndex = 4;
        lstbAccount.SelectedIndexChanged += LstbAccount_SelectedIndexChanged;
        // 
        // lstbTransactions
        // 
        lstbTransactions.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
        lstbTransactions.Font = new Font("Consolas", 8.25F);
        lstbTransactions.FormattingEnabled = true;
        lstbTransactions.ItemHeight = 13;
        lstbTransactions.Location = new Point(14, 355);
        lstbTransactions.Margin = new Padding(4, 3, 4, 3);
        lstbTransactions.Name = "lstbTransactions";
        lstbTransactions.Size = new Size(945, 381);
        lstbTransactions.TabIndex = 5;
        // 
        // btnCloseAccount
        // 
        btnCloseAccount.Location = new Point(7, 22);
        btnCloseAccount.Margin = new Padding(4, 3, 4, 3);
        btnCloseAccount.Name = "btnCloseAccount";
        btnCloseAccount.Size = new Size(175, 27);
        btnCloseAccount.TabIndex = 6;
        btnCloseAccount.Text = "Close Account";
        btnCloseAccount.UseVisualStyleBackColor = true;
        btnCloseAccount.Click += BtnCloseAccount_Click;
        // 
        // label1
        // 
        label1.AutoSize = true;
        label1.Location = new Point(553, 283);
        label1.Margin = new Padding(4, 0, 4, 0);
        label1.Name = "label1";
        label1.Size = new Size(84, 15);
        label1.TabIndex = 7;
        label1.Text = "# of Accounts:";
        label1.TextAlign = ContentAlignment.TopRight;
        // 
        // label2
        // 
        label2.AutoSize = true;
        label2.Location = new Point(777, 283);
        label2.Margin = new Padding(4, 0, 4, 0);
        label2.Name = "label2";
        label2.Size = new Size(76, 15);
        label2.TabIndex = 8;
        label2.Text = "Net Balance: ";
        label2.TextAlign = ContentAlignment.TopRight;
        // 
        // lblAccountsHeader
        // 
        lblAccountsHeader.AutoSize = true;
        lblAccountsHeader.Font = new Font("Consolas", 8.25F, FontStyle.Bold);
        lblAccountsHeader.Location = new Point(16, 91);
        lblAccountsHeader.Margin = new Padding(4, 0, 4, 0);
        lblAccountsHeader.Name = "lblAccountsHeader";
        lblAccountsHeader.Size = new Size(91, 13);
        lblAccountsHeader.TabIndex = 9;
        lblAccountsHeader.Text = "# of Accounts:";
        lblAccountsHeader.TextAlign = ContentAlignment.TopRight;
        // 
        // lblTransactionsHeader
        // 
        lblTransactionsHeader.AutoSize = true;
        lblTransactionsHeader.Font = new Font("Consolas", 8.25F, FontStyle.Bold);
        lblTransactionsHeader.Location = new Point(16, 337);
        lblTransactionsHeader.Margin = new Padding(4, 0, 4, 0);
        lblTransactionsHeader.Name = "lblTransactionsHeader";
        lblTransactionsHeader.Size = new Size(91, 13);
        lblTransactionsHeader.TabIndex = 10;
        lblTransactionsHeader.Text = "# of Accounts:";
        lblTransactionsHeader.TextAlign = ContentAlignment.TopRight;
        // 
        // label3
        // 
        label3.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        label3.AutoSize = true;
        label3.Location = new Point(784, 758);
        label3.Margin = new Padding(4, 0, 4, 0);
        label3.Name = "label3";
        label3.Size = new Size(76, 15);
        label3.TabIndex = 12;
        label3.Text = "Net Balance: ";
        label3.TextAlign = ContentAlignment.TopRight;
        // 
        // lblTransNet
        // 
        lblTransNet.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        lblTransNet.AutoSize = true;
        lblTransNet.Location = new Point(875, 758);
        lblTransNet.Margin = new Padding(4, 0, 4, 0);
        lblTransNet.Name = "lblTransNet";
        lblTransNet.Size = new Size(84, 15);
        lblTransNet.TabIndex = 11;
        lblTransNet.Text = "[Total Balance]";
        // 
        // btnAddAccount
        // 
        btnAddAccount.Location = new Point(974, 243);
        btnAddAccount.Margin = new Padding(4, 3, 4, 3);
        btnAddAccount.Name = "btnAddAccount";
        btnAddAccount.Size = new Size(175, 27);
        btnAddAccount.TabIndex = 13;
        btnAddAccount.Text = "Add an Account";
        btnAddAccount.UseVisualStyleBackColor = true;
        btnAddAccount.Click += BtnAddAccount_Click;
        // 
        // btnAddTrans
        // 
        btnAddTrans.Location = new Point(974, 553);
        btnAddTrans.Margin = new Padding(4, 3, 4, 3);
        btnAddTrans.Name = "btnAddTrans";
        btnAddTrans.Size = new Size(175, 27);
        btnAddTrans.TabIndex = 14;
        btnAddTrans.Text = "Add  a Transaction";
        btnAddTrans.UseVisualStyleBackColor = true;
        btnAddTrans.Click += BtnAddTrans_Click;
        // 
        // btnRenameAccount
        // 
        btnRenameAccount.Location = new Point(7, 55);
        btnRenameAccount.Margin = new Padding(4, 3, 4, 3);
        btnRenameAccount.Name = "btnRenameAccount";
        btnRenameAccount.Size = new Size(175, 27);
        btnRenameAccount.TabIndex = 15;
        btnRenameAccount.Text = "Rename Account";
        btnRenameAccount.UseVisualStyleBackColor = true;
        btnRenameAccount.Click += BtnRenameAccount_Click;
        // 
        // btnAddPerson
        // 
        btnAddPerson.Location = new Point(624, 24);
        btnAddPerson.Margin = new Padding(4, 3, 4, 3);
        btnAddPerson.Name = "btnAddPerson";
        btnAddPerson.Size = new Size(132, 27);
        btnAddPerson.TabIndex = 16;
        btnAddPerson.Text = "Add Person";
        btnAddPerson.UseVisualStyleBackColor = true;
        btnAddPerson.Click += BtnAddPerson_Click;
        // 
        // btnRefreshPeople
        // 
        btnRefreshPeople.Location = new Point(461, 24);
        btnRefreshPeople.Margin = new Padding(4, 3, 4, 3);
        btnRefreshPeople.Name = "btnRefreshPeople";
        btnRefreshPeople.Size = new Size(132, 27);
        btnRefreshPeople.TabIndex = 17;
        btnRefreshPeople.Text = "Refresh People";
        btnRefreshPeople.UseVisualStyleBackColor = true;
        btnRefreshPeople.Click += BtnRefreshPeople_Click;
        // 
        // groupBox1
        // 
        groupBox1.Controls.Add(btnCloseAccount);
        groupBox1.Controls.Add(btnRenameAccount);
        groupBox1.Location = new Point(967, 110);
        groupBox1.Margin = new Padding(4, 3, 4, 3);
        groupBox1.Name = "groupBox1";
        groupBox1.Padding = new Padding(4, 3, 4, 3);
        groupBox1.Size = new Size(189, 93);
        groupBox1.TabIndex = 18;
        groupBox1.TabStop = false;
        groupBox1.Text = "Selected Account Actions";
        // 
        // groupBox2
        // 
        groupBox2.Controls.Add(btnChangeType);
        groupBox2.Controls.Add(btnEditDesc);
        groupBox2.Controls.Add(btnChangeAmount);
        groupBox2.Location = new Point(967, 355);
        groupBox2.Margin = new Padding(4, 3, 4, 3);
        groupBox2.Name = "groupBox2";
        groupBox2.Padding = new Padding(4, 3, 4, 3);
        groupBox2.Size = new Size(189, 128);
        groupBox2.TabIndex = 19;
        groupBox2.TabStop = false;
        groupBox2.Text = "Selected Transaction Actions";
        // 
        // btnChangeType
        // 
        btnChangeType.Location = new Point(7, 89);
        btnChangeType.Margin = new Padding(4, 3, 4, 3);
        btnChangeType.Name = "btnChangeType";
        btnChangeType.Size = new Size(175, 27);
        btnChangeType.TabIndex = 8;
        btnChangeType.Text = "Change Transaction Type";
        btnChangeType.UseVisualStyleBackColor = true;
        btnChangeType.Click += BtnChangeType_Click;
        // 
        // btnEditDesc
        // 
        btnEditDesc.Location = new Point(7, 55);
        btnEditDesc.Margin = new Padding(4, 3, 4, 3);
        btnEditDesc.Name = "btnEditDesc";
        btnEditDesc.Size = new Size(175, 27);
        btnEditDesc.TabIndex = 7;
        btnEditDesc.Text = "Edit Description";
        btnEditDesc.UseVisualStyleBackColor = true;
        btnEditDesc.Click += BtnEditDesc_Click;
        // 
        // btnChangeAmount
        // 
        btnChangeAmount.Location = new Point(7, 22);
        btnChangeAmount.Margin = new Padding(4, 3, 4, 3);
        btnChangeAmount.Name = "btnChangeAmount";
        btnChangeAmount.Size = new Size(175, 27);
        btnChangeAmount.TabIndex = 6;
        btnChangeAmount.Text = "Change Amount";
        btnChangeAmount.UseVisualStyleBackColor = true;
        btnChangeAmount.Click += BtnChangeAmount_Click;
        // 
        // label4
        // 
        label4.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        label4.AutoSize = true;
        label4.Location = new Point(566, 758);
        label4.Margin = new Padding(4, 0, 4, 0);
        label4.Name = "label4";
        label4.Size = new Size(65, 15);
        label4.TabIndex = 21;
        label4.Text = "Payments: ";
        label4.TextAlign = ContentAlignment.TopRight;
        // 
        // lblTotalPayments
        // 
        lblTotalPayments.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        lblTotalPayments.AutoSize = true;
        lblTotalPayments.Location = new Point(657, 758);
        lblTotalPayments.Margin = new Padding(4, 0, 4, 0);
        lblTotalPayments.Name = "lblTotalPayments";
        lblTotalPayments.Size = new Size(95, 15);
        lblTotalPayments.TabIndex = 20;
        lblTotalPayments.Text = "[Total Payments]";
        // 
        // label5
        // 
        label5.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        label5.AutoSize = true;
        label5.Location = new Point(337, 758);
        label5.Margin = new Padding(4, 0, 4, 0);
        label5.Name = "label5";
        label5.Size = new Size(66, 15);
        label5.TabIndex = 23;
        label5.Text = "Purchases: ";
        label5.TextAlign = ContentAlignment.TopRight;
        // 
        // lblTotalPurchases
        // 
        lblTotalPurchases.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        lblTotalPurchases.AutoSize = true;
        lblTotalPurchases.Location = new Point(428, 758);
        lblTotalPurchases.Margin = new Padding(4, 0, 4, 0);
        lblTotalPurchases.Name = "lblTotalPurchases";
        lblTotalPurchases.Size = new Size(96, 15);
        lblTotalPurchases.TabIndex = 22;
        lblTotalPurchases.Text = "[Total Purchases]";
        // 
        // MainWindow
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1168, 804);
        Controls.Add(label5);
        Controls.Add(lblTotalPurchases);
        Controls.Add(label4);
        Controls.Add(lblTotalPayments);
        Controls.Add(groupBox2);
        Controls.Add(groupBox1);
        Controls.Add(btnRefreshPeople);
        Controls.Add(btnAddPerson);
        Controls.Add(btnAddTrans);
        Controls.Add(btnAddAccount);
        Controls.Add(label3);
        Controls.Add(lblTransNet);
        Controls.Add(lblTransactionsHeader);
        Controls.Add(lblAccountsHeader);
        Controls.Add(label2);
        Controls.Add(label1);
        Controls.Add(lstbTransactions);
        Controls.Add(lstbAccount);
        Controls.Add(lblBalanceNet);
        Controls.Add(lblAccCount);
        Controls.Add(cbPeople);
        Margin = new Padding(4, 3, 4, 3);
        Name = "MainWindow";
        Text = "Accounting";
        Load += MainWindow_Load;
        groupBox1.ResumeLayout(false);
        groupBox2.ResumeLayout(false);
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private System.Windows.Forms.ComboBox cbPeople;
    private System.Windows.Forms.Label lblAccCount;
    private System.Windows.Forms.Label lblBalanceNet;
    private System.Windows.Forms.ListBox lstbAccount;
    private System.Windows.Forms.ListBox lstbTransactions;
    private System.Windows.Forms.Button btnCloseAccount;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label lblAccountsHeader;
    private System.Windows.Forms.Label lblTransactionsHeader;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Label lblTransNet;
    private System.Windows.Forms.Button btnAddAccount;
    private System.Windows.Forms.Button btnAddTrans;
    private System.Windows.Forms.Button btnRenameAccount;
    private System.Windows.Forms.Button btnAddPerson;
    private System.Windows.Forms.ToolTip toolTip;
    private System.Windows.Forms.Button btnRefreshPeople;
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.GroupBox groupBox2;
    private System.Windows.Forms.Button btnEditDesc;
    private System.Windows.Forms.Button btnChangeAmount;
    private System.Windows.Forms.Button btnChangeType;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.Label lblTotalPayments;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.Label lblTotalPurchases;
}

