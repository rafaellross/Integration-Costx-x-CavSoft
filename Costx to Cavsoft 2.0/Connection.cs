using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;
using LibCostXCavSoft;
using System.Configuration;

namespace Integration_Costx_x_CavSoft
{
    public partial class Connection : Form
    {

        private DbCredentials credentialsCostx { get; set; }
        private DbCredentials credentialsCavSoft { get; set; }
        private DB cavSoft { get; set; }
        private DbPostgres costX { get; set; }
        public Configuration config { get; set; }

        public Connection()
        {
            InitializeComponent();

            config = ConfigurationManager.OpenExeConfiguration(Application.ExecutablePath);
                                    

            txtServerCostx.Text = config.AppSettings.Settings["CostxServer"].Value;
            
            txtDatabaseCostx.Text = config.AppSettings.Settings["CostxDataBaseName"].Value;
            txtUserCostx.Text = config.AppSettings.Settings["CostxUserName"].Value;
            txtPasswordCostx.Text = config.AppSettings.Settings["CostxPassword"].Value;

            txtServerCavSoft.Text = config.AppSettings.Settings["CavSoftServer"].Value;
            txtDatabaseCavSoft.Text = config.AppSettings.Settings["CavSoftDataBaseName"].Value;
            txtUserCavSoft.Text = config.AppSettings.Settings["CavSoftUserName"].Value;
            txtPasswordCavSoft.Text = config.AppSettings.Settings["CavSoftPassword"].Value;

        }



        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private bool checkFormCostx()
        {
            if (!string.IsNullOrWhiteSpace(txtServerCostx.Text) && !string.IsNullOrWhiteSpace(txtDatabaseCostx.Text) && !string.IsNullOrWhiteSpace(txtUserCostx.Text) && !string.IsNullOrWhiteSpace(txtPasswordCostx.Text))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool checkFormCavSoft()
        {
            if (!string.IsNullOrWhiteSpace(txtServerCavSoft.Text) && !string.IsNullOrWhiteSpace(txtDatabaseCavSoft.Text) && !string.IsNullOrWhiteSpace(txtUserCavSoft.Text) && !string.IsNullOrWhiteSpace(txtPasswordCavSoft.Text))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void enableTestCostX()
        {
            if (checkFormCostx())
            {
                btnTestCostx.Enabled = true;
            }
            else
            {
                btnTestCostx.Enabled = false;
            }
        }

        private void enableTestCavSoft()
        {
            if (checkFormCavSoft())
            {
                btnTestCavSoft.Enabled = true;
            }
            else
            {
                btnTestCavSoft.Enabled = false;
            }
        }

        private void enableNext()
        {
            if (checkFormCostx() && checkFormCavSoft())
            {
                btnNext.Enabled = true;
            }
            else
            {
                btnNext.Enabled = false;
            }

        }
        private void txtServerCostx_TextChanged(object sender, EventArgs e)
        {
            enableTestCostX();
            enableNext();
        }

        private void txtDatabaseCostx_TextChanged(object sender, EventArgs e)
        {
            enableTestCostX();
            enableNext();
        }

        private void txtUserCostx_TextChanged(object sender, EventArgs e)
        {
            enableTestCostX();
            enableNext();
        }

        private void txtPasswordCostx_TextChanged(object sender, EventArgs e)
        {
            enableTestCostX();
            enableNext();
        }

        private void txtServerCavSoft_TextChanged(object sender, EventArgs e)
        {
            enableTestCavSoft();
            enableNext();
        }

        private void txtDatabaseCavSoft_TextChanged(object sender, EventArgs e)
        {
            enableTestCavSoft();
            enableNext();
        }

        private void txtUserCavSoft_TextChanged(object sender, EventArgs e)
        {
            enableTestCavSoft();
            enableNext();
        }

        private void txtPasswordCavSoft_TextChanged(object sender, EventArgs e)
        {
            enableTestCavSoft();
            enableNext();
        }

        private void btnTestCostx_Click(object sender, EventArgs e)
        {
            if (testConnectionCostx())
            {
                MessageBox.Show("Connected to Costx!");
            }
          
        }

        private bool testConnectionCostx()
        {
            DbPostgres costX = new DbPostgres(txtServerCostx.Text, "17005", txtDatabaseCostx.Text, txtUserCostx.Text, txtPasswordCostx.Text);
            if (costX.Connection.State == ConnectionState.Open)
            {
                return true;

            }
            else
            {
                try
                {
                    costX.Connection.Open();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                return false;

            }

        }

        private bool testConnectionCavSoft()
        {
            DB cavSoft = new DB(false, txtServerCavSoft.Text, txtDatabaseCavSoft.Text, txtUserCavSoft.Text, txtPasswordCavSoft.Text);
            if (cavSoft.Connection.State == ConnectionState.Open)
            {
                return true;

            }
            else
            {
                try
                {
                    cavSoft.Connection.Open();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                return false;
            }

        }

        private void btnTestCavSoft_Click(object sender, EventArgs e)
        {
            if (testConnectionCavSoft())
            {
                MessageBox.Show("Connected to CavSoft!");
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (testConnectionCavSoft() && testConnectionCostx())
            {
                config.Save(ConfigurationSaveMode.Modified);
                costX = new DbPostgres(txtServerCostx.Text, "17005", txtDatabaseCostx.Text, txtUserCostx.Text, txtPasswordCostx.Text);
                cavSoft = new DB(false, txtServerCavSoft.Text, txtDatabaseCavSoft.Text, txtUserCavSoft.Text, txtPasswordCavSoft.Text);
                var projs = new SelectProjects(cavSoft, costX);
                projs.Show();
                this.Hide();
            }
        }

        private void Connection_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
