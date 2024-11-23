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
            this.components = new System.ComponentModel.Container();
            this.cbPeople = new System.Windows.Forms.ComboBox();
            this.lblAccCount = new System.Windows.Forms.Label();
            this.lblBalanceNet = new System.Windows.Forms.Label();
            this.lstbAccount = new System.Windows.Forms.ListBox();
            this.lstbTransactions = new System.Windows.Forms.ListBox();
            this.btnCloseAccount = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblAccountsHeader = new System.Windows.Forms.Label();
            this.lblTransactionsHeader = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblTransNet = new System.Windows.Forms.Label();
            this.btnAddAccount = new System.Windows.Forms.Button();
            this.btnAddTrans = new System.Windows.Forms.Button();
            this.btnRenameAccount = new System.Windows.Forms.Button();
            this.btnAddPerson = new System.Windows.Forms.Button();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.btnRefreshPeople = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnChangeType = new System.Windows.Forms.Button();
            this.btnEditDesc = new System.Windows.Forms.Button();
            this.btnChangeAmount = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.lblTotalPayments = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblTotalPurchases = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbPeople
            // 
            this.cbPeople.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPeople.FormattingEnabled = true;
            this.cbPeople.Location = new System.Drawing.Point(14, 24);
            this.cbPeople.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cbPeople.Name = "cbPeople";
            this.cbPeople.Size = new System.Drawing.Size(384, 23);
            this.cbPeople.TabIndex = 0;
            this.cbPeople.SelectedIndexChanged += new System.EventHandler(this.CbPeople_SelectedIndexChanged);
            // 
            // lblAccCount
            // 
            this.lblAccCount.AutoSize = true;
            this.lblAccCount.Location = new System.Drawing.Point(650, 283);
            this.lblAccCount.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblAccCount.Name = "lblAccCount";
            this.lblAccCount.Size = new System.Drawing.Size(89, 15);
            this.lblAccCount.TabIndex = 1;
            this.lblAccCount.Text = "[# of Accounts]";
            // 
            // lblBalanceNet
            // 
            this.lblBalanceNet.AutoSize = true;
            this.lblBalanceNet.Location = new System.Drawing.Point(868, 283);
            this.lblBalanceNet.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblBalanceNet.Name = "lblBalanceNet";
            this.lblBalanceNet.Size = new System.Drawing.Size(84, 15);
            this.lblBalanceNet.TabIndex = 2;
            this.lblBalanceNet.Text = "[Total Balance]";
            // 
            // lstbAccount
            // 
            this.lstbAccount.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lstbAccount.FormattingEnabled = true;
            this.lstbAccount.Location = new System.Drawing.Point(14, 110);
            this.lstbAccount.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.lstbAccount.Name = "lstbAccount";
            this.lstbAccount.Size = new System.Drawing.Size(945, 160);
            this.lstbAccount.TabIndex = 4;
            this.lstbAccount.SelectedIndexChanged += new System.EventHandler(this.LstbAccount_SelectedIndexChanged);
            // 
            // lstbTransactions
            // 
            this.lstbTransactions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lstbTransactions.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lstbTransactions.FormattingEnabled = true;
            this.lstbTransactions.Location = new System.Drawing.Point(14, 355);
            this.lstbTransactions.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.lstbTransactions.Name = "lstbTransactions";
            this.lstbTransactions.Size = new System.Drawing.Size(945, 381);
            this.lstbTransactions.TabIndex = 5;
            // 
            // btnCloseAccount
            // 
            this.btnCloseAccount.Location = new System.Drawing.Point(7, 22);
            this.btnCloseAccount.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnCloseAccount.Name = "btnCloseAccount";
            this.btnCloseAccount.Size = new System.Drawing.Size(175, 27);
            this.btnCloseAccount.TabIndex = 6;
            this.btnCloseAccount.Text = "Close Account";
            this.btnCloseAccount.UseVisualStyleBackColor = true;
            this.btnCloseAccount.Click += new System.EventHandler(this.BtnCloseAccount_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(553, 283);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 15);
            this.label1.TabIndex = 7;
            this.label1.Text = "# of Accounts:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(777, 283);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 15);
            this.label2.TabIndex = 8;
            this.label2.Text = "Net Balance: ";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblAccountsHeader
            // 
            this.lblAccountsHeader.AutoSize = true;
            this.lblAccountsHeader.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblAccountsHeader.Location = new System.Drawing.Point(16, 91);
            this.lblAccountsHeader.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblAccountsHeader.Name = "lblAccountsHeader";
            this.lblAccountsHeader.Size = new System.Drawing.Size(91, 13);
            this.lblAccountsHeader.TabIndex = 9;
            this.lblAccountsHeader.Text = "# of Accounts:";
            this.lblAccountsHeader.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblTransactionsHeader
            // 
            this.lblTransactionsHeader.AutoSize = true;
            this.lblTransactionsHeader.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblTransactionsHeader.Location = new System.Drawing.Point(16, 337);
            this.lblTransactionsHeader.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTransactionsHeader.Name = "lblTransactionsHeader";
            this.lblTransactionsHeader.Size = new System.Drawing.Size(91, 13);
            this.lblTransactionsHeader.TabIndex = 10;
            this.lblTransactionsHeader.Text = "# of Accounts:";
            this.lblTransactionsHeader.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(784, 758);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 15);
            this.label3.TabIndex = 12;
            this.label3.Text = "Net Balance: ";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblTransNet
            // 
            this.lblTransNet.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTransNet.AutoSize = true;
            this.lblTransNet.Location = new System.Drawing.Point(875, 758);
            this.lblTransNet.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTransNet.Name = "lblTransNet";
            this.lblTransNet.Size = new System.Drawing.Size(84, 15);
            this.lblTransNet.TabIndex = 11;
            this.lblTransNet.Text = "[Total Balance]";
            // 
            // btnAddAccount
            // 
            this.btnAddAccount.Location = new System.Drawing.Point(974, 243);
            this.btnAddAccount.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnAddAccount.Name = "btnAddAccount";
            this.btnAddAccount.Size = new System.Drawing.Size(175, 27);
            this.btnAddAccount.TabIndex = 13;
            this.btnAddAccount.Text = "Add an Account";
            this.btnAddAccount.UseVisualStyleBackColor = true;
            this.btnAddAccount.Click += new System.EventHandler(this.BtnAddAccount_Click);
            // 
            // btnAddTrans
            // 
            this.btnAddTrans.Location = new System.Drawing.Point(974, 553);
            this.btnAddTrans.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnAddTrans.Name = "btnAddTrans";
            this.btnAddTrans.Size = new System.Drawing.Size(175, 27);
            this.btnAddTrans.TabIndex = 14;
            this.btnAddTrans.Text = "Add  a Transaction";
            this.btnAddTrans.UseVisualStyleBackColor = true;
            this.btnAddTrans.Click += new System.EventHandler(this.BtnAddTrans_Click);
            // 
            // btnRenameAccount
            // 
            this.btnRenameAccount.Location = new System.Drawing.Point(7, 55);
            this.btnRenameAccount.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnRenameAccount.Name = "btnRenameAccount";
            this.btnRenameAccount.Size = new System.Drawing.Size(175, 27);
            this.btnRenameAccount.TabIndex = 15;
            this.btnRenameAccount.Text = "Rename Account";
            this.btnRenameAccount.UseVisualStyleBackColor = true;
            this.btnRenameAccount.Click += new System.EventHandler(this.BtnRenameAccount_Click);
            // 
            // btnAddPerson
            // 
            this.btnAddPerson.Location = new System.Drawing.Point(624, 24);
            this.btnAddPerson.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnAddPerson.Name = "btnAddPerson";
            this.btnAddPerson.Size = new System.Drawing.Size(132, 27);
            this.btnAddPerson.TabIndex = 16;
            this.btnAddPerson.Text = "Add Person";
            this.btnAddPerson.UseVisualStyleBackColor = true;
            this.btnAddPerson.Click += new System.EventHandler(this.BtnAddPerson_Click);
            // 
            // btnRefreshPeople
            // 
            this.btnRefreshPeople.Location = new System.Drawing.Point(461, 24);
            this.btnRefreshPeople.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnRefreshPeople.Name = "btnRefreshPeople";
            this.btnRefreshPeople.Size = new System.Drawing.Size(132, 27);
            this.btnRefreshPeople.TabIndex = 17;
            this.btnRefreshPeople.Text = "Refresh People";
            this.btnRefreshPeople.UseVisualStyleBackColor = true;
            this.btnRefreshPeople.Click += new System.EventHandler(this.BtnRefreshPeople_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnCloseAccount);
            this.groupBox1.Controls.Add(this.btnRenameAccount);
            this.groupBox1.Location = new System.Drawing.Point(967, 110);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox1.Size = new System.Drawing.Size(189, 93);
            this.groupBox1.TabIndex = 18;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Selected Account Actions";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnChangeType);
            this.groupBox2.Controls.Add(this.btnEditDesc);
            this.groupBox2.Controls.Add(this.btnChangeAmount);
            this.groupBox2.Location = new System.Drawing.Point(967, 355);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox2.Size = new System.Drawing.Size(189, 128);
            this.groupBox2.TabIndex = 19;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Selected Account Actions";
            // 
            // btnChangeType
            // 
            this.btnChangeType.Location = new System.Drawing.Point(7, 89);
            this.btnChangeType.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnChangeType.Name = "btnChangeType";
            this.btnChangeType.Size = new System.Drawing.Size(175, 27);
            this.btnChangeType.TabIndex = 8;
            this.btnChangeType.Text = "Change Transaction Type";
            this.btnChangeType.UseVisualStyleBackColor = true;
            this.btnChangeType.Click += new System.EventHandler(this.BtnChangeType_Click);
            // 
            // btnEditDesc
            // 
            this.btnEditDesc.Location = new System.Drawing.Point(7, 55);
            this.btnEditDesc.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnEditDesc.Name = "btnEditDesc";
            this.btnEditDesc.Size = new System.Drawing.Size(175, 27);
            this.btnEditDesc.TabIndex = 7;
            this.btnEditDesc.Text = "Edit Description";
            this.btnEditDesc.UseVisualStyleBackColor = true;
            this.btnEditDesc.Click += new System.EventHandler(this.BtnEditDesc_Click);
            // 
            // btnChangeAmount
            // 
            this.btnChangeAmount.Location = new System.Drawing.Point(7, 22);
            this.btnChangeAmount.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnChangeAmount.Name = "btnChangeAmount";
            this.btnChangeAmount.Size = new System.Drawing.Size(175, 27);
            this.btnChangeAmount.TabIndex = 6;
            this.btnChangeAmount.Text = "Change Amount";
            this.btnChangeAmount.UseVisualStyleBackColor = true;
            this.btnChangeAmount.Click += new System.EventHandler(this.BtnChangeAmount_Click);
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(566, 758);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 15);
            this.label4.TabIndex = 21;
            this.label4.Text = "Payments: ";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblTotalPayments
            // 
            this.lblTotalPayments.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTotalPayments.AutoSize = true;
            this.lblTotalPayments.Location = new System.Drawing.Point(657, 758);
            this.lblTotalPayments.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTotalPayments.Name = "lblTotalPayments";
            this.lblTotalPayments.Size = new System.Drawing.Size(95, 15);
            this.lblTotalPayments.TabIndex = 20;
            this.lblTotalPayments.Text = "[Total Payments]";
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(337, 758);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(66, 15);
            this.label5.TabIndex = 23;
            this.label5.Text = "Purchases: ";
            this.label5.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblTotalPurchases
            // 
            this.lblTotalPurchases.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTotalPurchases.AutoSize = true;
            this.lblTotalPurchases.Location = new System.Drawing.Point(428, 758);
            this.lblTotalPurchases.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTotalPurchases.Name = "lblTotalPurchases";
            this.lblTotalPurchases.Size = new System.Drawing.Size(96, 15);
            this.lblTotalPurchases.TabIndex = 22;
            this.lblTotalPurchases.Text = "[Total Purchases]";
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1168, 804);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lblTotalPurchases);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lblTotalPayments);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnRefreshPeople);
            this.Controls.Add(this.btnAddPerson);
            this.Controls.Add(this.btnAddTrans);
            this.Controls.Add(this.btnAddAccount);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblTransNet);
            this.Controls.Add(this.lblTransactionsHeader);
            this.Controls.Add(this.lblAccountsHeader);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lstbTransactions);
            this.Controls.Add(this.lstbAccount);
            this.Controls.Add(this.lblBalanceNet);
            this.Controls.Add(this.lblAccCount);
            this.Controls.Add(this.cbPeople);
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "MainWindow";
            this.Text = "Accounting";
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

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

