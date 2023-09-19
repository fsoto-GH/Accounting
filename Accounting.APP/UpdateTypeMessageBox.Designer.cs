namespace Accounting.APP;

partial class UpdateTypeMessageBox
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
        this.label1 = new System.Windows.Forms.Label();
        this.cbType = new System.Windows.Forms.ComboBox();
        this.btnCancel = new System.Windows.Forms.Button();
        this.btnOK = new System.Windows.Forms.Button();
        this.SuspendLayout();
        // 
        // label1
        // 
        this.label1.AutoSize = true;
        this.label1.Location = new System.Drawing.Point(22, 27);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(37, 13);
        this.label1.TabIndex = 0;
        this.label1.Text = "Type: ";
        // 
        // cbType
        // 
        this.cbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        this.cbType.FormattingEnabled = true;
        this.cbType.Items.AddRange(new object[] {
        "DEBIT",
        "CREDIT"});
        this.cbType.Location = new System.Drawing.Point(65, 24);
        this.cbType.Name = "cbType";
        this.cbType.Size = new System.Drawing.Size(186, 21);
        this.cbType.TabIndex = 1;
        // 
        // btnCancel
        // 
        this.btnCancel.Location = new System.Drawing.Point(12, 61);
        this.btnCancel.Name = "btnCancel";
        this.btnCancel.Size = new System.Drawing.Size(104, 23);
        this.btnCancel.TabIndex = 9;
        this.btnCancel.Text = "Cancel";
        this.btnCancel.UseVisualStyleBackColor = true;
        this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
        // 
        // btnOK
        // 
        this.btnOK.Location = new System.Drawing.Point(147, 61);
        this.btnOK.Name = "btnOK";
        this.btnOK.Size = new System.Drawing.Size(104, 23);
        this.btnOK.TabIndex = 8;
        this.btnOK.Text = "Done";
        this.btnOK.UseVisualStyleBackColor = true;
        this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
        // 
        // UpdateTypeMessageBox
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(272, 104);
        this.Controls.Add(this.btnCancel);
        this.Controls.Add(this.btnOK);
        this.Controls.Add(this.cbType);
        this.Controls.Add(this.label1);
        this.Name = "UpdateTypeMessageBox";
        this.Text = "UpdateType";
        this.ResumeLayout(false);
        this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.ComboBox cbType;
    private System.Windows.Forms.Button btnCancel;
    private System.Windows.Forms.Button btnOK;
}