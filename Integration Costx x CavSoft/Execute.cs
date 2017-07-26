using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LibCostXCavSoft;

namespace Integration_Costx_x_CavSoft
{
    public partial class Execute : Form
    {
        public DB cavSoft;
        public DbPostgres costX;
        public List<string> projKeys;

        public Execute()
        {
            InitializeComponent();
        }

        public Execute(DB cavSoft, DbPostgres costX, List<string> projKeys)
        {

            InitializeComponent();
            this.cavSoft = cavSoft;
            this.costX = costX;
            this.projKeys = projKeys;
            txtResults.Text = "Click Start button!";
                                   
        }

        public void Start()
        {

        }

        public ManipulateCostx manipulate { get; private set; }

        public void extractFromCostX(string projectKey)
        {            
            manipulate.InsertIntoCavSoftTemp(manipulate.LoadMeasurements(projectKey));
        }

        public void insertIntoCavSoft(string projectKey)
        {
            //Create New Project
            Project project = new Project();

            //Get Ids from CavSoft
            project.EstimateNo = manipulate.getEstimateNo();
            project.EstimateID = manipulate.getEstimateID();
            project.Description = manipulate.getProjectName(projectKey);
            txtResults.BeginInvoke(
                 new Action(() =>
                 {
                     txtResults.Text += "*********************************" + Environment.NewLine;
                     txtResults.SelectionColor = Color.Red;
                     txtResults.SelectionFont = new Font(txtResults.Font, FontStyle.Bold);
                     txtResults.Text += "Extracting the project " + project.Description.ToUpper() + " from Costx" + Environment.NewLine;
                     txtResults.SelectionFont = new Font(txtResults.Font, FontStyle.Regular);
                 }
            ));

            manipulate.InsertProjectCover(project.EstimateID, project.EstimateNo, project.Description);
            project.ParentID = manipulate.getParentID(project.EstimateID);
            

            var drawings = manipulate.getDrawings(projectKey);
            //Start to insert drawings
            var listOrderDrawing = 1;
            foreach (var drawing in drawings)
            {
                txtResults.BeginInvoke(
                     new Action(() =>
                     {
                         txtResults.SelectionFont = new Font(txtResults.Font, FontStyle.Bold);
                         
                         txtResults.SelectionColor = Color.Blue;
                         txtResults.AppendText("Inserting Drawing " + drawing.ToUpper() + " into CavSoft" + Environment.NewLine);
                         txtResults.SelectionFont = new Font(txtResults.Font, FontStyle.Regular);

                     }
                ));

                var DrawingID = manipulate.getDetailID();
                cavSoft.Execute(Queries.insertDrawing(project.EstimateID, project.ParentID, drawing, DrawingID, listOrderDrawing.ToString()));

                var folders = manipulate.getFolders(projectKey, drawing);
                //*Folder insert Eg. Sewer*/
                var orderListFolder = 0; 

                foreach (var folder in folders)
                {
                    txtResults.BeginInvoke(
                         new Action(() =>
                         {
                             txtResults.SelectionFont = new Font(txtResults.Font, FontStyle.Bold);
                             txtResults.SelectionColor = Color.DarkGreen;
                             txtResults.AppendText("   " + folder + Environment.NewLine);
                             txtResults.SelectionFont = new Font(txtResults.Font, FontStyle.Regular);
                             txtResults.ScrollToCaret();
                         }
                    ));

                    var FolderID = manipulate.getDetailID();
                    cavSoft.Execute(Queries.insertFolder(project.EstimateID, project.ParentID, drawing, folder, DrawingID, FolderID, orderListFolder.ToString()));
                    //		/*Insert Item Eg. PVC Pipe*/
                    var items = manipulate.getItems(projectKey, drawing, folder);
                    for (int i = 0; i < items.Count; i++)
                    {
                        var ItemID = manipulate.getDetailID();
                        var ItemCode = items[i]["CodeItem"];

                        var RateCavSoft = cavSoft.queryListToDic(Queries.getRate(ItemCode))[0];
                        if (ItemCode == "SPECIAL" || ItemCode == "")
                        {
                            RateCavSoft["Description"] = items[i]["DescriptionItem"];
                        }

                        txtResults.BeginInvoke(
                             new Action(() =>
                             {
                                 txtResults.AppendText("       " + items[i]["DescriptionItem"] + Environment.NewLine);
                                 txtResults.ScrollToCaret();
                             }
                        ));


                        cavSoft.Execute(Queries.insertItem(project.EstimateID, project.ParentID, drawing, folder, DrawingID, FolderID, ItemID, i, ItemCode, items[i]["Quantity"], RateCavSoft));
                        //Insert StandardRateCostTypeTotals
                        
                        cavSoft.Execute(Queries.insertStandardRateCostTypeTotals(project.EstimateID, ItemID, ItemCode));
                        //Insert Sub-items

                        
                        cavSoft.Execute(Queries.insertSubItems(project.EstimateID, ItemID, ItemCode));

                        //Get sub-item from CavSoft to insert their sub-subItems
                        

                        var subSubItems = manipulate.getSubItems(project.EstimateID, ItemID);
                        foreach (var subItem in subSubItems)
                        {
                            //Insert StandardRateCostTypeTotals
                            cavSoft.Execute(Queries.insertStandardRateCostTypeTotals(project.EstimateID, subItem["ParentID"], subItem["RateCode"]));

                            cavSoft.Execute(Queries.insertSubItems(project.EstimateID, subItem["ParentID"], subItem["RateCode"]));
                        }
                    }
                    orderListFolder++;
                }
                listOrderDrawing++;
                
            }
            

        }

        private void Execute_Load(object sender, EventArgs e)
        {

        }

        private void btnFinish_Click(object sender, EventArgs e)
        {            
            Application.Exit();
        }

        private void Execute_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            progressBar1.Style = ProgressBarStyle.Marquee;
            btnStart.Enabled = false;

            if (bgWorker.IsBusy != true)
            {
                bgWorker.RunWorkerAsync();
            }                        
        }

        private void bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            
            txtResults.BeginInvoke(
                 new Action(() =>
                 {
                     txtResults.Text = "Creating temporary table... " + Environment.NewLine;
                 }
            ));
            
            this.cavSoft.Execute(Queries.dropTableCostx());
            
            this.cavSoft.Execute(Queries.createTableCostx());
            //txtResults.Text += "Done... " + Environment.NewLine;
            txtResults.BeginInvoke(
                 new Action(() =>
                 {
                     txtResults.Text += "Done... " + Environment.NewLine;
                 }
            ));

            txtResults.BeginInvoke(
                 new Action(() =>
                 {
                     txtResults.Text += "Loading projects... " + Environment.NewLine;
                     txtResults.Text += projKeys.Count.ToString() + " project(s) loaded! " + Environment.NewLine;
                 }
            ));


            foreach (var projKey in projKeys)
            {
                manipulate = new ManipulateCostx();
                manipulate.cavSoft = this.cavSoft;
                manipulate.costX = this.costX;

                

                
                if (manipulate.costX.Connection.State != ConnectionState.Open)
                {
                    manipulate.costX.Connection.Open();
                }
                if (manipulate.cavSoft.Connection.State != ConnectionState.Open)
                {
                    manipulate.cavSoft.Connection.Open();
                }

                extractFromCostX(projKey);
                
                
                insertIntoCavSoft(projKey);
                
                manipulate.cavSoft.Dispose();
                manipulate.costX.Dispose();
                
            }

            this.cavSoft.Dispose();
            
            
        }

        private void bgWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            
        }

        private void bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            progressBar1.Style = ProgressBarStyle.Blocks;
            progressBar1.Value = 100;
            txtResults.AppendText(Environment.NewLine + "COMPLETED!" + Environment.NewLine);
            btnFinish.Enabled = true;

        }
    }
}
