using LibCostXCavSoft;
using Npgsql;
using System;
using System.Collections.Generic;
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

namespace Integration_Costx_x_CavSoft___WPF
{
    /// <summary>
    /// Interaction logic for SelectProject.xaml
    /// </summary>
    public partial class SelectProject : UserControl
    {
        private DB cavSoft { get; set; }
        private DbPostgres costX { get; set; }

        public SelectProject()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            loadProjectDg();
        }

        public void loadProjectDg()
        {
            var queryProj = Queries.listProjects();

            NpgsqlCommand sqlCmd = new NpgsqlCommand(queryProj, costX.Connection);

            DataGridCheckBoxColumn checkProj = new DataGridCheckBoxColumn();
            checkProj.SortMemberPath = "checkProj";            
            checkProj.Header = "[ X ]";

            dgProjects.Columns.Add(checkProj);

            NpgsqlDataAdapter sqlDataAdap = new NpgsqlDataAdapter(sqlCmd);

            DataTable dtRecord = new DataTable();
            sqlDataAdap.Fill(dtRecord);
            dgProjects.DataContext = dtRecord;            
            dgProjects.Columns[3].Header= "Date Added";
            dgProjects.Columns[2].Header= "Project Name";
            dgProjects.Columns[1].Visibility = Visibility.Hidden;
            
        }
    }
}
