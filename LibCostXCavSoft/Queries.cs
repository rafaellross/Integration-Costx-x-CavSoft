﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibCostXCavSoft
{
    public static class Queries
    {
        public static string ListMeasurements(string projKey)
        {
            var query = @"select 
                        proj.projkey,
                        proj.name as proj_name,
                        group_dim.name, 
                        group_dim.custom1name as code_cav,
                        group_dim.fldr as folder,
                        dim.properties as measurement,
                        dimtype

                        from dimgrprvsn group_dim
                        inner join dim
                        on group_dim.dimgrpkey = dim.dimgrpkey
                        inner join dimgrp
                        on dimgrp.dimgrpkey = dim.dimgrpkey
                        inner join bldg
                        on bldg.bldgkey = dimgrp.bldgkey
                        join proj
                        on proj.projkey = bldg.projkey
                        where group_dim.custom1name is not null 
                        and proj.projkey = '" + projKey + "'";
            return query;
        }

        public static string InsertMeasurementCavSoft(Measurement measurement)
        {
            var query = @"insert into costx(ProjectKey, ProjectName, DescriptionItem, CodeItem, Folder, MeasurementItem, DimensionType, Length, Area, Count, Drawing)";
            query += "values(";
            query += "'" + measurement.ProjectKey + "',";
            query += "'" + measurement.ProjectName + "', ";
            query += "'" + measurement.DescriptionItem + "', ";
            query += "'" + measurement.CodeItem + "', ";

            query += "'" + (measurement.Folder.Split('\\').Length > 1? measurement.Folder.Split('\\')[1] : "") + "', ";
            query +=    measurement.MeasurementItem + ", ";
            query += "'" + measurement.DimensionType + "',";
            query +=    measurement.Length + ", ";
            query +=    measurement.Area + ", ";
            query +=    measurement.Count + ", ";
            query += "'" + measurement.Folder.Split('\\')[0] + "'); ";
            return query;                       
        }

        public static string getDrawings(string projectKey)
        {
            return "select distinct Drawing  from costx Where ProjectKey = '" + projectKey + "'";
        }

        public static string insertFolder(string estimateID, string parentID, string drawing, string folder, string DrawingID, string FolderID)
        {
            return @"Declare @Folder_DetailID int = " + FolderID + @";
                        Declare @EstimateID int = " + estimateID + @";
                        Declare @Draw_DetailID int = " + DrawingID + @";

		                INSERT INTO EstimateDetails (DetailID, EstimateID, ParentID, TreeLevel, ListOrder) VALUES (@Folder_DetailID, @EstimateID, @Draw_DetailID, 2, 1);
		                UPDATE EstimateDetails SET ListOrder = 1 WHERE DetailID = @Folder_DetailID;
		                UPDATE EstimateDetails SET EstimateID = @EstimateID, ParentID = @Draw_DetailID, TreeLevel = 2, ListOrder = 1, CodeType = 0, RateCodeID = 0, CostType = 0, Quantity = 1.000, Cost = 0.00, Charge = 0.00, TotalMaterial = 0.00, TotalLabour = 0.00, TotalHours = 0.000, TotalOther = 0.00, TotalSubcontract = 0.00, TotalSubcontractHrs = 0.00, TotalCharge = 0.00, Description = '" + folder + @"', RateCode = '', Units = '', MarkupLevel = 'A', ItemChanged = 1, LastUpdate = '', CostCodeID = 0, UserID = 0, ColourID = 0, Formula = '' WHERE DetailID = @Folder_DetailID;";
        }

        public static string insertSubItems(string EstimateID, string ItemID, string itemCode)
        {
            return @"insert into EstimateDetails
		                select (select Max(DetailID) from EstimateDetails)+1 + ROW_NUMBER() OVER (ORDER BY RateID) as DetailID,
		                '" + EstimateID + @"' as EstimateID,
		                '" + ItemID + @"' as ItemID,
		                '4' as TreeLevel,
		                rates.ListOrder,
		                rates.CodeType,
		                rates.RateCode,
		                rates.Description,
		                rates.Units,
		                rates.Quantity,
		                rates.Cost,
		                rates.RateCodeID,
		                rates.CostType,
		                rates.TotalMaterial,
		                rates.TotalLabour,
		                rates.TotalHours,
		                rates.TotalOther,
		                'A' MarkupLevel,
		                0 as TotalSubcontract,
		                0 as TotalSubcontractHrs,
		                0 as ItemChanged,
		                '' as LastUpdate,
		                0 as CostCodeID,
		                0 as Deleted,
		                '' as Formula,
		                '0' as UserID,
		                Quantity*Cost as TotalCharge,
		                Cost as Charge,
		                0 ColourID
		
		                from StandardRates rates
		                where ParentID = (select Top 1 RateID from StandardRates where RateCode = '" + itemCode + "');";
        }

        public static string getRate(string itemCode)
        {
            return "select Description, Units, CostType, Cost from StandardRates where RateCode = '" + itemCode + "' and LibraryID = 1;";
        }

        public static string insertItem(string estimateID, string parentID, string drawing, string folder, string drawingID, string folderID, string itemID, int i, string itemCode, string Quantity, Dictionary<string, string> RateCavSoft)
        {
            return @"INSERT INTO EstimateDetails (DetailID, EstimateID, ParentID, TreeLevel, ListOrder, Description, RateCode, Units, MarkupLevel, CodeType, CostType, CostCodeID, RateCodeID, Cost, Quantity, ItemChanged, Formula) 
		            VALUES (" + itemID + ", " + estimateID + ", " + folderID + ", 3, " + i + ", '" + RateCavSoft["Description"].Replace("'", "") + "', '" + itemCode + "', '" + RateCavSoft["Units"] + "', 'A', 3, 0, 0, 0, replace('" + RateCavSoft["Cost"] + "', ',', '.'), " + Quantity.Replace(",", ".") + ", 1, '');";
        }

        public static string getItems(string projectKey, string drawing, string folder)
        {
            return @"select CodeItem,
                    Case when DimensionType = 'C' then Sum(Count)
                    else Sum(Length) end Quantity

                    from costx where ProjectKey = '" + projectKey + "' and Drawing = '" + drawing + "' and Folder = '" + folder + @"'
                    group by CodeItem, DimensionType";
        }

        public static string InsertProjectCover(string estimateID, string estimateNo, string description)
        {
            var query = @"
                            Declare @EstimateID Int = " + estimateID + @";
                            Declare @EstimateNo Varchar(10) = '" + estimateNo + @"';
                            Declare @Project_Description Nvarchar(Max) = '" + description + @"';

                            INSERT INTO Estimates (EstimateID) VALUES (@EstimateID);

                            UPDATE Estimates SET EstimateNo = '', ContactID = 0, RefNumber = 0, Status = 1, PriceToCostAt = 0, TenderPrice = 0.00, Prelims = 0.00, StaticPrelims = 0.00, Overheads = 0.00, Profits = 0.00, TotalCost = 0.00, TotalCharge = 0.00, Retention = 0.00, Rounding = 0.00, Description = '', DueDate = '', JobAddress1 = '', JobAddress2 = '', JobAddress3 = '', JobAddress4 = '', JobAddress5 = '', ContactPhone = '', ContactAreaCode = '', ContactName = '', Builder = '', Consultant = '', Notes = '', LastModifiedDate = '', LastModifiedTime = '', Estimator = '', Supervisor = '', RPD = '', BuildingApplication = '', DrawingNumbers = '', ContactEmail = '', ContactMobile = '', ContactFax = '', ContactFaxAreaCode = '', DateLastUpdated = '', RoundOffTenderPrice = 0, LockTenderPrice = 0, TakeOffUseLocal = 0, OpenedBy = 0, CheckedOutBy = 0, MarkupsSet = 0, PropertyCategory = 0, CreatingUser = 0, DetailsArchived = 0, CallDate = '', CallTime = '', BaseFolder = '' WHERE EstimateID = @EstimateID;
                            
                            EXECUTE [dbo].[InsertEstimateDetail] @EstimateID,0,0,'Estimate Details','','','A',0,0,0,0,0,1,'',0;

                            EXECUTE [dbo].[InsertEstimateDetail] @EstimateID,0,0,'Preliminaries','','','A',5,0,0,0,0,1,'',0;

                            EXECUTE [dbo].[InsertEstimateDetail] @EstimateID,0,0,'Variations','','','A',6,0,0,0,0,1,'',0; 

                            UPDATE Estimates SET 
		                            EstimateNo = @EstimateNo, 
		                            ContactID = 0, 
		                            RefNumber = 0, 
		                            Status = 2, 
		                            PriceToCostAt = 1, 
		                            TenderPrice = 0.00, 
		                            Prelims = 0.00, 
		                            StaticPrelims = 0.00, 
		                            Overheads = 0.00, 
		                            Profits = 0.00, 
		                            TotalCost = 0.00, 
		                            TotalCharge = 0.00, 
		                            Retention = 0.00, 
		                            Rounding = 0.00, 
		                            Description = @Project_Description, 
		                            DueDate = convert(varchar, GETDATE(), 111), 
		                            JobAddress1 = '', 
		                            JobAddress2 = '', 
		                            JobAddress3 = '', 
		                            JobAddress4 = '', 
		                            JobAddress5 = '', 
		                            ContactPhone = '', 
		                            ContactAreaCode = '', 
		                            ContactName = '', 
		                            Builder = '', 
		                            Consultant = '', 
		                            Notes = '', 		
		                            LastModifiedDate = REPLACE(convert(varchar, GETDATE(), 111), '/', ''), 				
		                            LastModifiedTime = REPLACE(SUBSTRING(CONVERT(VARCHAR, GETDATE(), 108), 1, 5), ':', ''), 
		                            Estimator = '', 
		                            Supervisor = '', 
		                            RPD = '', 
		                            BuildingApplication = '', 
		                            DrawingNumbers = '', 
		                            ContactEmail = '', 
		                            ContactMobile = '', 
		                            ContactFax = '', 
		                            ContactFaxAreaCode = '', 
		                            DateLastUpdated = '', 
		                            RoundOffTenderPrice = 1, 
		                            LockTenderPrice = 0, 
		                            TakeOffUseLocal = 0, 
		                            OpenedBy = 0, 
		                            CheckedOutBy = 0, 
		                            MarkupsSet = 0, 
		                            PropertyCategory = 0, 
		                            CreatingUser = 0, 
		                            DetailsArchived = 0, 
		                            CallDate = convert(varchar, GETDATE(), 111), 
		                            CallTime = SUBSTRING(CONVERT(VARCHAR, GETDATE()+60, 0), 14, 8), 
		                            BaseFolder = 'C:\CavSoft Projects\' + @EstimateNo 
		                            WHERE EstimateID = @EstimateID;";
            return query;
        }

        public static string getFolders(string projectKey, string drawing)
        {
            return "select distinct Folder  from costx Where ProjectKey = '" + projectKey + "' and Drawing = '" + drawing + "';";
        }

        public static string getParentID(string estimateID)
        {
            return "SELECT EstimateDetails.DetailID FROM EstimateDetails LEFT JOIN EstimateCostCodes ON EstimateDetails.CostCodeID = EstimateCostCodes.CostCodeID WHERE EstimateID = " + estimateID + "AND TreeLevel = 0 AND CodeType = 0 AND Deleted = 0";
        }

        public static string getEstimateNo()
        {
            return "SELECT StringValue FROM ProgramOptions WHERE UPPER(OptionName) = 'NEXTESTIMATENUMBER' AND UserID = 0";
        }

        public static string updateEstimateNo()
        {
            return "UPDATE ProgramOptions SET StringValue = 'A' + cast(replace(StringValue, 'A', '') + 1 as varchar(10)) WHERE OptionName = 'NextEstimateNumber' AND UserID = 0 AND OptionType = 3; ";
        }

        public static string getEstimateID()
        {
            return "SELECT MAX(EstimateID) + 1 AS EstimateID FROM Estimates";
        }

        public static string getProjectName(string ProjectKey)
        {

            return "select distinct ProjectName from costx where ProjectKey = '" + ProjectKey + "'";
        }

        public static string getDetailID()
        {
            return "SELECT MAX(DetailID)+1 AS DetailID FROM EstimateDetails;";
        }


        public static string listProjects()
        {
            return @"select distinct 
                        proj.projkey,
                        proj.name as proj_name

                        from dimgrprvsn group_dim
                        inner join dim
                        on group_dim.dimgrpkey = dim.dimgrpkey
                        inner join dimgrp
                        on dimgrp.dimgrpkey = dim.dimgrpkey
                        inner join bldg
                        on bldg.bldgkey = dimgrp.bldgkey
                        join proj
                        on proj.projkey = bldg.projkey
                        where group_dim.custom1name is not null ";
        }       

        public static string dropTableCostx()
        {
            return @"IF OBJECT_ID('costx') IS NOT NULL
	                    DROP TABLE [dbo].[costx];";
        }


        public static string createTableCostx()
        {
            return @"CREATE TABLE [dbo].[costx](
	                    [id] [int] IDENTITY(1,1) NOT NULL,
	                    [ProjectKey] [nvarchar](max) NULL,
	                    [ProjectName] [nvarchar](max) NULL,
	                    [DescriptionItem] [nvarchar](max) NULL,
	                    [CodeItem] [nvarchar](max) NULL,
	                    [Folder] [nvarchar](max) NULL,
	                    [MeasurementItem] [float] NULL,
	                    [DimensionType] [nvarchar](max) NULL,
	                    [Length] [float] NULL,
	                    [Area] [float] NULL,
	                    [Count] [float] NULL,
	                    [Drawing] [nvarchar](max) NULL,
                    PRIMARY KEY CLUSTERED 
                    (
	                    [id] ASC
                    )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
                    ) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]";
        }

        public static string insertDrawing(string EstimateID, string ParentID, string Description, string DrawingID)
        {
            return @"Declare @Draw_DetailID int =" + DrawingID + @";
                     Declare @EstimateID int = "+ EstimateID + @";
                     Declare @ParentID int = " + ParentID + @";
		            INSERT INTO EstimateDetails (DetailID, EstimateID, ParentID, TreeLevel, ListOrder) 
		            VALUES (@Draw_DetailID, @EstimateID, @ParentID, 1, isnull((SELECT MAX(ListOrder)+1 AS MaxOrder FROM EstimateDetails WHERE ParentID = @ParentID), 1));

		            UPDATE EstimateDetails SET ListOrder = 1 WHERE DetailID = @Draw_DetailID;
		            
		            UPDATE EstimateDetails SET EstimateID = @EstimateID, ParentID = @ParentID, TreeLevel = 1, ListOrder = 1, CodeType = 0, RateCodeID = 0, CostType = 0, Quantity = 1.000, Cost = 0.00, Charge = 0.00, TotalMaterial = 0.00, TotalLabour = 0.00, TotalHours = 0.000, TotalOther = 0.00, TotalSubcontract = 0.00, TotalSubcontractHrs = 0.00, TotalCharge = 0.00, Description = '" + Description + @"', RateCode = '', Units = '', MarkupLevel = 'A', ItemChanged = 1, LastUpdate = '', CostCodeID = 0, UserID = 0, ColourID = 0, Formula = '' WHERE DetailID = @Draw_DetailID;";
        }
    }
}