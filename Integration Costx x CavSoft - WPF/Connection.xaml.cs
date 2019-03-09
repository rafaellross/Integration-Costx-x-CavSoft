using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using FirstFloor.ModernUI.Windows.Controls;
using LibCostXCavSoft;


namespace Integration_Costx_x_CavSoft___WPF
{
    /// <summary>
    /// Interaction logic for BasicPage1.xaml
    /// </summary>
    public partial class BasicPage1 : UserControl
    {
        
        

        public BasicPage1()
        {
            InitializeComponent();
            
            
            
            txtServerCostx.Text = ConfigurationManager.AppSettings["CostxServer"];

            txtDatabaseCostx.Text = ConfigurationManager.AppSettings["CostxDataBaseName"];
            txtUserCostx.Text = ConfigurationManager.AppSettings["CostxUserName"];
            txtPasswordCostx.Text = ConfigurationManager.AppSettings["CostxPassword"];

            txtServerCavSoft.Text = ConfigurationManager.AppSettings["CavSoftServer"];
            txtDatabaseCavSoft.Text = ConfigurationManager.AppSettings["CavSoftDataBaseName"];
            txtUserCavSoft.Text = ConfigurationManager.AppSettings["CavSoftUserName"];
            txtPasswordCavSoft.Text = ConfigurationManager.AppSettings["CavSoftPassword"];
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnTestCostX_Click(object sender, RoutedEventArgs e)
        {
            if (testConnectionCostx())
            {
                ModernDialog.ShowMessage("Connected to CostX", "CostX Connection", MessageBoxButton.OK);
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
                    Console.WriteLine(ex.Message);
                    ModernDialog.ShowMessage(ex.Message, "CostX Connection - Error", MessageBoxButton.OK);                    
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

        private void btnTestCavSoft_Click(object sender, RoutedEventArgs e)
        {
            if (testConnectionCavSoft())
            {
                ModernDialog.ShowMessage("Connected to CavSoft", "CavSoft Connection", MessageBoxButton.OK);
            }
        }
    }
}
