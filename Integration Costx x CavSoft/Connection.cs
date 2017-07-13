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

namespace Integration_Costx_x_CavSoft
{
    public partial class Connection : Form
    {

        private DbCredentials credentialsCostx { get; set; }
        private DbCredentials credentialsCavSoft { get; set; }
        private DB cavSoft { get; set; }
        private DbPostgres costX { get; set; }

        public Connection()
        {
            InitializeComponent();
            credentialsCostx = new DbCredentials();
            credentialsCostx.Server = "192.168.1.113";
            credentialsCostx.Port = "17005";
            credentialsCostx.DataBaseName = "costx";
            credentialsCostx.UserName = "integration";
            credentialsCostx.Password = "1234";

            txtServerCostx.Text = credentialsCostx.Server;
            txtDatabaseCostx.Text = credentialsCostx.DataBaseName;
            txtUserCostx.Text = credentialsCostx.UserName;
            txtPasswordCostx.Text = credentialsCostx.Password;

            credentialsCavSoft = new DbCredentials();
            credentialsCavSoft.Server = System.Environment.GetEnvironmentVariable("COMPUTERNAME");
            credentialsCavSoft.DataBaseName = "CavSoft_Dev";
            credentialsCavSoft.UserName = "smart";
            credentialsCavSoft.Password = "smart";

            txtServerCavSoft.Text = credentialsCavSoft.Server;
            txtDatabaseCavSoft.Text = credentialsCavSoft.DataBaseName;
            txtUserCavSoft.Text = credentialsCavSoft.UserName;
            txtPasswordCavSoft.Text = credentialsCavSoft.Password;



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

            DbPostgres costX = new DbPostgres(txtServerCostx.Text, "17005", txtDatabaseCostx.Text, txtUserCostx.Text, txtPasswordCostx.Text);
            if (costX.Connection.State == ConnectionState.Open)
            {
                MessageBox.Show("Connected to Costx!");

            }
            else
            {
                try
                {
                    costX.Connection.Open();
                }
                catch (Exception ex )
                {
                    MessageBox.Show(ex.Message);                    
                }
                
            }

        }

        private void btnTestCavSoft_Click(object sender, EventArgs e)
        {
            DB cavSoft = new DB(false, txtServerCavSoft.Text, txtDatabaseCavSoft.Text, txtUserCavSoft.Text, txtPasswordCavSoft.Text);
            if (cavSoft.Connection.State == ConnectionState.Open)
            {
                MessageBox.Show("Connected to CavSoft!");

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

            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            costX = new DbPostgres(txtServerCostx.Text, "17005", txtDatabaseCostx.Text, txtUserCostx.Text, txtPasswordCostx.Text);
            cavSoft = new DB(false, txtServerCavSoft.Text, txtDatabaseCavSoft.Text, txtUserCavSoft.Text, txtPasswordCavSoft.Text);
            var projs = new SelectProjects(cavSoft, costX);
            projs.Show();
            this.Hide();
        }
    }
}
