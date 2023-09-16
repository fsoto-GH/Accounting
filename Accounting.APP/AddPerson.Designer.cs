namespace Accounting.APP;

partial class AddPerson
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
        this.label1 = new System.Windows.Forms.Label();
        this.label2 = new System.Windows.Forms.Label();
        this.label3 = new System.Windows.Forms.Label();
        this.txtFirstName = new System.Windows.Forms.TextBox();
        this.txtLastName = new System.Windows.Forms.TextBox();
        this.txtMiddleName = new System.Windows.Forms.TextBox();
        this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
        this.btnAdd = new System.Windows.Forms.Button();
        ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
        this.SuspendLayout();
        // 
        // label1
        // 
        this.label1.AutoSize = true;
        this.label1.Location = new System.Drawing.Point(28, 31);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(60, 13);
        this.label1.TabIndex = 0;
        this.label1.Text = "First Name:";
        // 
        // label2
        // 
        this.label2.AutoSize = true;
        this.label2.Location = new System.Drawing.Point(27, 68);
        this.label2.Name = "label2";
        this.label2.Size = new System.Drawing.Size(61, 13);
        this.label2.TabIndex = 1;
        this.label2.Text = "Last Name:";
        // 
        // label3
        // 
        this.label3.AutoSize = true;
        this.label3.Location = new System.Drawing.Point(16, 109);
        this.label3.Name = "label3";
        this.label3.Size = new System.Drawing.Size(72, 13);
        this.label3.TabIndex = 2;
        this.label3.Text = "Middle Name:";
        // 
        // txtFirstName
        // 
        this.txtFirstName.Location = new System.Drawing.Point(109, 28);
        this.txtFirstName.Name = "txtFirstName";
        this.txtFirstName.Size = new System.Drawing.Size(164, 20);
        this.txtFirstName.TabIndex = 3;
        this.txtFirstName.Validating += new System.ComponentModel.CancelEventHandler(this.txtFirstName_Validating);
        // 
        // txtLastName
        // 
        this.txtLastName.Location = new System.Drawing.Point(109, 65);
        this.txtLastName.Name = "txtLastName";
        this.txtLastName.Size = new System.Drawing.Size(164, 20);
        this.txtLastName.TabIndex = 4;
        this.txtLastName.Validating += new System.ComponentModel.CancelEventHandler(this.txtLastName_Validating);
        // 
        // txtMiddleName
        // 
        this.txtMiddleName.Location = new System.Drawing.Point(109, 106);
        this.txtMiddleName.Name = "txtMiddleName";
        this.txtMiddleName.Size = new System.Drawing.Size(164, 20);
        this.txtMiddleName.TabIndex = 5;
        // 
        // errorProvider
        // 
        this.errorProvider.ContainerControl = this;
        // 
        // btnAdd
        // 
        this.btnAdd.Location = new System.Drawing.Point(181, 148);
        this.btnAdd.Name = "btnAdd";
        this.btnAdd.Size = new System.Drawing.Size(92, 23);
        this.btnAdd.TabIndex = 6;
        this.btnAdd.Text = "Add Person";
        this.btnAdd.UseVisualStyleBackColor = true;
        this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
        // 
        // AddPerson
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
        this.ClientSize = new System.Drawing.Size(307, 193);
        this.Controls.Add(this.btnAdd);
        this.Controls.Add(this.txtMiddleName);
        this.Controls.Add(this.txtLastName);
        this.Controls.Add(this.txtFirstName);
        this.Controls.Add(this.label3);
        this.Controls.Add(this.label2);
        this.Controls.Add(this.label1);
        this.Name = "AddPerson";
        this.Text = "AddPerson";
        ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
        this.ResumeLayout(false);
        this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.TextBox txtFirstName;
    private System.Windows.Forms.TextBox txtLastName;
    private System.Windows.Forms.TextBox txtMiddleName;
    private System.Windows.Forms.ErrorProvider errorProvider;
    private System.Windows.Forms.Button btnAdd;
}