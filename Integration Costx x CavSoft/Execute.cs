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
            this.cavSoft = cavSoft;
            this.costX = costX;
            this.projKeys = projKeys;
                                   
        }

        public void Start()
        {
            this.cavSoft.Execute(Queries.dropTableCostx());
            this.cavSoft.Execute(Queries.createTableCostx());

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

            manipulate.InsertProjectCover(project.EstimateID, project.EstimateNo, project.Description);
            project.ParentID = manipulate.getParentID(project.EstimateID);
            

            var drawings = manipulate.getDrawings(projectKey);
            //Start to insert drawings
            var listOrderDrawing = 1;
            foreach (var drawing in drawings)
            {
                
                var DrawingID = manipulate.getDetailID();
                cavSoft.Execute(Queries.insertDrawing(project.EstimateID, project.ParentID, drawing, DrawingID, listOrderDrawing.ToString()));

                var folders = manipulate.getFolders(projectKey, drawing);
                //*Folder insert Eg. Sewer*/
                var folderOrder = 1;
                foreach (var folder in folders)
                {
                    var FolderID = manipulate.getDetailID();
                    cavSoft.Execute(Queries.insertFolder(project.EstimateID, project.ParentID, drawing, folder, DrawingID, FolderID, folderOrder.ToString()));
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
                        
                        //var test = Queries.insertItem(project.EstimateID, project.ParentID, drawing, folder, DrawingID, FolderID, ItemID, i, ItemCode, items[i]["Quantity"], RateCavSoft);
                        cavSoft.Execute(Queries.insertItem(project.EstimateID, project.ParentID, drawing, folder, DrawingID, FolderID, ItemID, i, ItemCode, items[i]["Quantity"], RateCavSoft));
                        //Insert StandardRateCostTypeTotals
                        var testStandardRateCostTypeTotals = Queries.insertStandardRateCostTypeTotals(project.EstimateID, ItemID, ItemCode);
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
                }
                listOrderDrawing++;
            }
            txtResults.AppendText("Project " + project.EstimateNo + " - " + project.Description + Environment.NewLine);

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
    }
}
