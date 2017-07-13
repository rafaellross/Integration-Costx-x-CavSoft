using LibCostXCavSoft;
using Npgsql;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace TestCostx
{
    class Program
    {
        static void Main(string[] args)
        {
            DbCredentials credentialsCostx = new DbCredentials();            
            credentialsCostx.Server = "192.168.1.113";
            credentialsCostx.Port = "17005";
            credentialsCostx.DataBaseName = "costx";
            credentialsCostx.UserName = "integration";
            credentialsCostx.Password = "1234";

            DbCredentials credentialsCavSoft = new DbCredentials();
            credentialsCavSoft.Server = "localhost";            
            credentialsCavSoft.DataBaseName = "SharpSvrDB";
            credentialsCavSoft.UserName = "smart";
            credentialsCavSoft.Password = "smart";

            ManipulateCostx manipulate = new ManipulateCostx(credentialsCostx, credentialsCavSoft);
            manipulate.InsertIntoCavSoftTemp(manipulate.LoadMeasurements("f8299964-32e3-4dad-ad39-dc3b3271b973"));

            //Create New Project
            Project project = new Project();

            project.EstimateNo = manipulate.getEstimateNo();
            project.EstimateID = manipulate.getEstimateID();
            project.Description = manipulate.getProjectName("f8299964-32e3-4dad-ad39-dc3b3271b973");

            manipulate.InsertProjectCover(project.EstimateID, project.EstimateNo, project.Description);

            project.ParentID = manipulate.getParentID(project.EstimateID);
            
            Console.WriteLine("End");

        }
}
    }
