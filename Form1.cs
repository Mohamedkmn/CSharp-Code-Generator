using BusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Contracts;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
 
namespace MyProjectCodGenerator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void cmbDatabase_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbTable.DataSource = clsGetInfoAboutDatabases.GetNameTablesForDatabase(txtUserName.Text.Trim(), txtPassword.Text.Trim(),cmbDatabase.Text);
            cmbTable.DisplayMember = "TABLE_NAME";
            cmbTable.SelectedIndex = -1;



        }

        private void btnConnectionToDatabase_Click(object sender, EventArgs e)
        {
            cmbDatabase.SelectedIndexChanged -= cmbDatabase_SelectedIndexChanged;

            cmbDatabase.DataSource = clsGetInfoAboutDatabases.GetNamesDatabasesFromDatabase(txtUserName.Text.Trim(), txtPassword.Text.Trim());

            if (cmbDatabase.DataSource != null)
            {
                cmbDatabase.DisplayMember = "Name";
                cmbDatabase.SelectedIndex = -1;
                txtUserName.Enabled = false;
                txtPassword.Enabled = false;
                btnConnectionToDatabase.Enabled = false;
                MessageBox.Show("Successfully connected", "Connection Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Connection faild", "Connection Info", MessageBoxButtons.OK, MessageBoxIcon.Warning);


            cmbDatabase.SelectedIndexChanged += cmbDatabase_SelectedIndexChanged;
        }

        private void btnGeneratAccessCod_Click(object sender, EventArgs e)
        {
 
            clsCreateDataLayer clsCreateDataLayer1 = new clsCreateDataLayer(txtUserName.Text.Trim(), txtPassword.Text.Trim(), cmbDatabase.Text, cmbTable.Text);

            txtCodDataAccess.Text =  clsCreateDataLayer1.CreateDataLayer();
        }

        private void btnGeneratBussnissCod_Click(object sender, EventArgs e)
        {

            clsCreateBusinessLayer clsCreateBusinessLayer1 = new clsCreateBusinessLayer(txtUserName.Text.Trim(), txtPassword.Text.Trim(), cmbDatabase.Text, cmbTable.Text);

            txtCodDataBussinss.Text = clsCreateBusinessLayer1.CreateBusinessLayer();
        }

        private void cmbTable_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbTable.SelectedIndex != -1)
            {
                btnGeneratAccessCod.Enabled = true;
                btnGeneratBussnissCod.Enabled = true;
            }
            else
            {
                btnGeneratAccessCod.Enabled = false;
                btnGeneratBussnissCod.Enabled = false;
            }
        }

        private void btnGeneratFileDataAccess_Click(object sender, EventArgs e)
        {
            string Path = "";

            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                Path = folderBrowserDialog1.SelectedPath + $"\\cls{cmbTable.Text}Data.cs";


                if (!File.Exists(Path))
                {
                    using (StreamWriter streamWriter = File.CreateText(Path))
                    {
                        streamWriter.WriteLine(txtCodDataAccess.Text);
                        MessageBox.Show("File created successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }

                }
                else
                    MessageBox.Show("File already exists.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);


            }
        }

        private void btnGeneratFileBussnissAccess_Click(object sender, EventArgs e)
        {
            string Path = "";

            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                Path = folderBrowserDialog1.SelectedPath + $"\\cls{cmbTable.Text}.cs";


                if (!File.Exists(Path))
                {
                    using (StreamWriter streamWriter = File.CreateText(Path))
                    {
                        streamWriter.WriteLine(txtCodDataBussinss.Text);
                        MessageBox.Show("File created successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }

                }
                else
                    MessageBox.Show("File already exists.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);


            }
        }

        private void txtCodDataAccess_TextChanged(object sender, EventArgs e)
        {
            if (txtCodDataAccess.Text != "")
            {
                btnGeneratFileDataAccess.Enabled = true;
            }
            else
                btnGeneratFileDataAccess.Enabled = false;

        }

        private void txtCodDataBussinss_TextChanged(object sender, EventArgs e)
        {
            if(txtCodDataBussinss.Text!="")
                btnGeneratFileBussnissAccess.Enabled = true;
            else
                btnGeneratFileBussnissAccess.Enabled = false;

        }
    }
}
