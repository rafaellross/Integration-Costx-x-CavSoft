using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace LibCostXCavSoft
{
    public class Measurement
    {
        public string ProjectKey { get; set; }
        public string ProjectName { get; set; }
        public string DescriptionItem { get; set; }
        public string CodeItem { get; set; }
        public string Folder { get; set; }
        public string MeasurementItem { get; set; }
        public string DimensionType { get; set; }
        public string Length { get; set; }
        public string Count { get; set; }
        public string Area { get; set; }
        public Measurement(NpgsqlDataReader measurement)
        {                        
            ProjectKey = measurement.GetString(0);
            ProjectName = measurement.GetString(1);
            DescriptionItem = measurement.GetString(2);
            CodeItem = measurement.GetString(3);
            Folder = measurement.GetString(4);
            DimensionType = measurement.GetString(6);
            this.getMeasurement(measurement.GetString(5));
            MeasurementItem = getLengthCount();
        }

        public void getMeasurement(string measurementXml)
        {
            XmlDocument properties = new XmlDocument();
            properties.LoadXml(measurementXml);        
            XmlNodeList nodeList = properties.SelectNodes("//prop");
            foreach (XmlNode no in nodeList)
            {

                if (no.Attributes["name"].Value == "Length")
                {
                    Length = no.InnerText;                    
                }

                if (no.Attributes["name"].Value == "Count")
                {
                    Count = no.InnerText;
                }

                if (no.Attributes["name"].Value == "Area")
                {
                    Area = no.InnerText;
                }
            }         
        }

        public string getLengthCount()
        {
            
            if (DimensionType == "L")
            {
                return Length;
            }
            else 
            {
                return Count;
            }

        }
    }
}
