namespace MyProjectCodGenerator
{
    partial class Form1
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
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.cmbDatabase = new System.Windows.Forms.ComboBox();
            this.cmbTable = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnGeneratBussnissCod = new System.Windows.Forms.Button();
            this.btnGeneratAccessCod = new System.Windows.Forms.Button();
            this.txtCodDataAccess = new System.Windows.Forms.TextBox();
            this.txtCodDataBussinss = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.btnConnectionToDatabase = new System.Windows.Forms.Button();
            this.btnGeneratFileDataAccess = new System.Windows.Forms.Button();
            this.btnGeneratFileBussnissAccess = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(485, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(268, 30);
            this.label1.TabIndex = 0;
            this.label1.Text = "Generat Cod Manual";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 190);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(113, 25);
            this.label2.TabIndex = 1;
            this.label2.Text = "Password:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(12, 111);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(126, 25);
            this.label3.TabIndex = 2;
            this.label3.Text = "User Name:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(36, 368);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(111, 25);
            this.label4.TabIndex = 3;
            this.label4.Text = "Database:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(369, 368);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(74, 25);
            this.label5.TabIndex = 4;
            this.label5.Text = "Table:";
            // 
            // txtUserName
            // 
            this.txtUserName.Location = new System.Drawing.Point(144, 112);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(193, 26);
            this.txtUserName.TabIndex = 5;
            this.txtUserName.Text = "sa";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(131, 191);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(193, 26);
            this.txtPassword.TabIndex = 6;
            this.txtPassword.Text = "sa123456";
            // 
            // cmbDatabase
            // 
            this.cmbDatabase.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDatabase.FormattingEnabled = true;
            this.cmbDatabase.Location = new System.Drawing.Point(153, 369);
            this.cmbDatabase.Name = "cmbDatabase";
            this.cmbDatabase.Size = new System.Drawing.Size(184, 28);
            this.cmbDatabase.TabIndex = 7;
            this.cmbDatabase.SelectedIndexChanged += new System.EventHandler(this.cmbDatabase_SelectedIndexChanged);
            // 
            // cmbTable
            // 
            this.cmbTable.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTable.FormattingEnabled = true;
            this.cmbTable.Location = new System.Drawing.Point(449, 368);
            this.cmbTable.Name = "cmbTable";
            this.cmbTable.Size = new System.Drawing.Size(194, 28);
            this.cmbTable.TabIndex = 8;
            this.cmbTable.SelectedIndexChanged += new System.EventHandler(this.cmbTable_SelectedIndexChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnGeneratFileBussnissAccess);
            this.panel1.Controls.Add(this.btnGeneratFileDataAccess);
            this.panel1.Controls.Add(this.btnGeneratBussnissCod);
            this.panel1.Controls.Add(this.btnGeneratAccessCod);
            this.panel1.Location = new System.Drawing.Point(13, 444);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(630, 140);
            this.panel1.TabIndex = 9;
            // 
            // btnGeneratBussnissCod
            // 
            this.btnGeneratBussnissCod.Enabled = false;
            this.btnGeneratBussnissCod.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGeneratBussnissCod.Location = new System.Drawing.Point(339, 18);
            this.btnGeneratBussnissCod.Name = "btnGeneratBussnissCod";
            this.btnGeneratBussnissCod.Size = new System.Drawing.Size(279, 41);
            this.btnGeneratBussnissCod.TabIndex = 2;
            this.btnGeneratBussnissCod.Text = "Generat Bussniss Cod";
            this.btnGeneratBussnissCod.UseVisualStyleBackColor = true;
            this.btnGeneratBussnissCod.Click += new System.EventHandler(this.btnGeneratBussnissCod_Click);
            // 
            // btnGeneratAccessCod
            // 
            this.btnGeneratAccessCod.Enabled = false;
            this.btnGeneratAccessCod.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGeneratAccessCod.Location = new System.Drawing.Point(21, 18);
            this.btnGeneratAccessCod.Name = "btnGeneratAccessCod";
            this.btnGeneratAccessCod.Size = new System.Drawing.Size(279, 41);
            this.btnGeneratAccessCod.TabIndex = 0;
            this.btnGeneratAccessCod.Text = "Generat Access Cod";
            this.btnGeneratAccessCod.UseVisualStyleBackColor = true;
            this.btnGeneratAccessCod.Click += new System.EventHandler(this.btnGeneratAccessCod_Click);
            // 
            // txtCodDataAccess
            // 
            this.txtCodDataAccess.Location = new System.Drawing.Point(649, 142);
            this.txtCodDataAccess.Multiline = true;
            this.txtCodDataAccess.Name = "txtCodDataAccess";
            this.txtCodDataAccess.Size = new System.Drawing.Size(434, 517);
            this.txtCodDataAccess.TabIndex = 10;
            this.txtCodDataAccess.TextChanged += new System.EventHandler(this.txtCodDataAccess_TextChanged);
            // 
            // txtCodDataBussinss
            // 
            this.txtCodDataBussinss.Location = new System.Drawing.Point(1089, 142);
            this.txtCodDataBussinss.Multiline = true;
            this.txtCodDataBussinss.Name = "txtCodDataBussinss";
            this.txtCodDataBussinss.Size = new System.Drawing.Size(434, 517);
            this.txtCodDataBussinss.TabIndex = 11;
            this.txtCodDataBussinss.TextChanged += new System.EventHandler(this.txtCodDataBussinss_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(1235, 93);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(150, 25);
            this.label6.TabIndex = 12;
            this.label6.Text = "Data Bussniss";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(784, 93);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(140, 25);
            this.label7.TabIndex = 13;
            this.label7.Text = "Data Access ";
            // 
            // btnConnectionToDatabase
            // 
            this.btnConnectionToDatabase.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConnectionToDatabase.Location = new System.Drawing.Point(144, 263);
            this.btnConnectionToDatabase.Name = "btnConnectionToDatabase";
            this.btnConnectionToDatabase.Size = new System.Drawing.Size(320, 41);
            this.btnConnectionToDatabase.TabIndex = 14;
            this.btnConnectionToDatabase.Text = "Connection To Database";
            this.btnConnectionToDatabase.UseVisualStyleBackColor = true;
            this.btnConnectionToDatabase.Click += new System.EventHandler(this.btnConnectionToDatabase_Click);
            // 
            // btnGeneratFileDataAccess
            // 
            this.btnGeneratFileDataAccess.Enabled = false;
            this.btnGeneratFileDataAccess.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGeneratFileDataAccess.Location = new System.Drawing.Point(21, 85);
            this.btnGeneratFileDataAccess.Name = "btnGeneratFileDataAccess";
            this.btnGeneratFileDataAccess.Size = new System.Drawing.Size(279, 41);
            this.btnGeneratFileDataAccess.TabIndex = 4;
            this.btnGeneratFileDataAccess.Text = "Generat File";
            this.btnGeneratFileDataAccess.UseVisualStyleBackColor = true;
            this.btnGeneratFileDataAccess.Click += new System.EventHandler(this.btnGeneratFileDataAccess_Click);
            // 
            // btnGeneratFileBussnissAccess
            // 
            this.btnGeneratFileBussnissAccess.Enabled = false;
            this.btnGeneratFileBussnissAccess.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGeneratFileBussnissAccess.Location = new System.Drawing.Point(339, 85);
            this.btnGeneratFileBussnissAccess.Name = "btnGeneratFileBussnissAccess";
            this.btnGeneratFileBussnissAccess.Size = new System.Drawing.Size(279, 41);
            this.btnGeneratFileBussnissAccess.TabIndex = 5;
            this.btnGeneratFileBussnissAccess.Text = "Generat File";
            this.btnGeneratFileBussnissAccess.UseVisualStyleBackColor = true;
            this.btnGeneratFileBussnissAccess.Click += new System.EventHandler(this.btnGeneratFileBussnissAccess_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightGreen;
            this.ClientSize = new System.Drawing.Size(1534, 694);
            this.Controls.Add(this.btnConnectionToDatabase);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtCodDataBussinss);
            this.Controls.Add(this.txtCodDataAccess);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.cmbTable);
            this.Controls.Add(this.cmbDatabase);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.txtUserName);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.ComboBox cmbDatabase;
        private System.Windows.Forms.ComboBox cmbTable;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnGeneratBussnissCod;
        private System.Windows.Forms.Button btnGeneratAccessCod;
        private System.Windows.Forms.TextBox txtCodDataAccess;
        private System.Windows.Forms.TextBox txtCodDataBussinss;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnConnectionToDatabase;
        private System.Windows.Forms.Button btnGeneratFileBussnissAccess;
        private System.Windows.Forms.Button btnGeneratFileDataAccess;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
    }
}

