using LibCostXCavSoft;
using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Integration_Costx_x_CavSoft
{
    public partial class SelectProjects : Form
    {
        private DB cavSoft { get; set; }
        private DbPostgres costX { get; set; }
        public bool allChecked { get; private set; }

        public SelectProjects()
        {
            InitializeComponent();
        }

        public SelectProjects(DB cavSoft, DbPostgres costX)
        {
            this.cavSoft = cavSoft;
            this.costX = costX;

            InitializeComponent();            
        }

        private void SelectProjects_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void btnNext_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void dgProjects_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        public void loadProjectDg()
        {
            var queryProj = Queries.listProjects();

            NpgsqlCommand sqlCmd = new NpgsqlCommand(queryProj, costX.Connection);

            DataGridViewCheckBoxColumn checkProj = new DataGridViewCheckBoxColumn();
            checkProj.Name = "checkProj";
            checkProj.HeaderText = "[ X ]";
            
            dgProjects.Columns.Add(checkProj);

            NpgsqlDataAdapter sqlDataAdap = new NpgsqlDataAdapter(sqlCmd);

            DataTable dtRecord = new DataTable();
            sqlDataAdap.Fill(dtRecord);
            dgProjects.DataSource = dtRecord;
            dgProjects.Columns[2].HeaderText = "Project Name";
            dgProjects.Columns[1].Visible = false;
            dgProjects.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
        }

        private void SelectProjects_Load(object sender, EventArgs e)
        {
            loadProjectDg();
        }

        private void dgProjects_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dgProjects.Columns["checkProj"].Index && e.RowIndex != -1)
            {
                if (!Convert.ToBoolean(dgProjects.Rows[e.RowIndex].Cells[0].Value))
                {
                    dgProjects.Rows[e.RowIndex].Cells[0].Value = CheckState.Checked;
                }
                else
                {
                    dgProjects.Rows[e.RowIndex].Cells[0].Value = CheckState.Unchecked;
                }             
                
            }

            if (e.ColumnIndex == dgProjects.Columns["checkProj"].Index && e.RowIndex == -1)
            {
                if (!allChecked)
                {
                    foreach (DataGridViewRow row in dgProjects.Rows)
                    {
                        row.Cells[0].Value = CheckState.Checked;
                    }
                    allChecked = true;
                }
                else
                {
                    foreach (DataGridViewRow row in dgProjects.Rows)
                    {
                        row.Cells[0].Value = CheckState.Unchecked;
                    }
                    allChecked = false;
                }

            }

            if (checkSelecteds())
            {
                btnExecute.Enabled = true;
            }
            else
            {
                btnExecute.Enabled = false;
            }
        }

        private bool checkSelecteds()
        {
            var selecteds = 0;
            foreach (DataGridViewRow row in dgProjects.Rows)
            {
                if (row.Cells[0].Value != null && Convert.ToBoolean(row.Cells[0].Value))
                {
                    selecteds++;
                }
            }
            if (selecteds > 0)
            {
                return true;
            }
            else
            {
                return false;
            }            
        }

        private void btnNext_Click_1(object sender, EventArgs e)
        {
            List<string> projKeys = new List<string>();
            foreach (DataGridViewRow row in dgProjects.Rows)
            {
                if (Convert.ToBoolean(row.Cells[0].Value))
                {
                    projKeys.Add(Convert.ToString(row.Cells[1].Value));
                }                
            }
            var execute = new Execute(cavSoft, costX, projKeys);
            execute.Show();
            this.Hide();

        }
    }
}
