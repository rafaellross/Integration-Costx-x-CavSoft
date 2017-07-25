using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibCostXCavSoft
{
    public class ManipulateCostx
    {
        public DbPostgres costX { get; set; }
        public DB cavSoft { get; set; }

        public ManipulateCostx(DbCredentials credentialsCostx, DbCredentials credentialsCavSoft)
        {
            costX = new DbPostgres(credentialsCostx.Server, credentialsCostx.Port, credentialsCostx.DataBaseName, credentialsCostx.UserName, credentialsCostx.Password);
            cavSoft = new DB(false, credentialsCavSoft.Server, credentialsCavSoft.DataBaseName, credentialsCavSoft.UserName, credentialsCavSoft.Password);            
        }

        public ManipulateCostx()
        {

        }

        public List<Measurement> LoadMeasurements(string projectKey)
        {
            List<Measurement> measurements = new List<Measurement>();

            var listMeasurements = costX.Query(Queries.ListMeasurements(projectKey));            

            while (listMeasurements.Read())
            {
                Measurement measurement = new Measurement(listMeasurements);
                measurements.Add(measurement);
            }
            
            return measurements;
        }

        public void InsertIntoCavSoftTemp(List<Measurement> measurementsList)
        {
            foreach (var measurement in measurementsList)
            {
                this.cavSoft.Execute(Queries.InsertMeasurementCavSoft(measurement));
            }
        }

        public void InsertProjectCover(string estimateID, string estimateNo, string description)
        {
            cavSoft.Execute(Queries.InsertProjectCover(estimateID, estimateNo, description));
        }

        public string getParentID(string estimateID)
        {
            var query = cavSoft.queryListToDic(Queries.getParentID(estimateID));
            return query[0]["DetailID"];

        }

        public List<string> getDrawings(string projectKey)
        {
            List<string> drawings = new List<string>();

            var query = cavSoft.Query(Queries.getDrawings(projectKey));
            while (query.Read())
            {
                drawings.Add(query.GetString(0));
            }
            return drawings;
        }

        public List<string> getFolders(string projectKey, string drawing)
        {
            List<string> folders = new List<string>();

            var query = cavSoft.Query(Queries.getFolders(projectKey, drawing));
            while (query.Read())
            {
                folders.Add(query.GetString(0));
            }
            return folders;
        }

        public string getEstimateNo()
        {            
            var query = cavSoft.queryListToDic(Queries.getEstimateNo());
            cavSoft.Execute(Queries.updateEstimateNo());
            return query[0]["StringValue"];            
        }

        public List<Dictionary<string, string>> getItems(string projectKey, string drawing, string folder)
        {
            var query = cavSoft.queryListToDic(Queries.getItems(projectKey, drawing, folder));
            return query;
        }

        public string getEstimateID()
        {
            var query = cavSoft.queryListToDic(Queries.getEstimateID());            
            return query[0]["EstimateID"];
        }

        public string getDetailID()
        {
            var query = cavSoft.queryListToDic(Queries.getDetailID());
            return query[0]["DetailID"];
        }

        public string getProjectName(string ProjectKey)
        {
            var query = cavSoft.queryListToDic(Queries.getProjectName(ProjectKey));            
            return query[0]["ProjectName"];

        }
        
        public List<Dictionary<string, string>> getSubItems(string EstimateID, string SubItemID)
        {
            var query = cavSoft.queryListToDic(Queries.getSubItems(EstimateID, SubItemID));
            return query;
        }

    }
}
