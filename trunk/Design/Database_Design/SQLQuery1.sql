CREATE DATABASE FBD
USE FBD
GO
--done

CREATE TABLE [Business.Industries]
(
IndustryID int NOT NULL PRIMARY KEY ,
IndustryName NVARCHAR(255) UNIQUE
)
--done

CREATE TABLE [Business.Lines]
(
LineID NVARCHAR(10) NOT NULL PRIMARY KEY ,
IndustryID int FOREIGN KEY REFERENCES [Business.Industries](IndustryID),
LineName NVARCHAR(255) UNIQUE
)
--done
CREATE TABLE [Business.Types]
(
TypeID NVARCHAR(10) NOT NULL PRIMARY KEY ,
TypeName NVARCHAR(255) UNIQUE
)
--done

CREATE TABLE [Business.ScaleCriteria]
(
CriteriaID int NOT NULL PRIMARY KEY,
CriteriaName NVARCHAR(255) UNIQUE,
Unit NVARCHAR(50),
Formula NVARCHAR(255),
ValueType VARCHAR(1)
)
---Not OK
CREATE TABLE [Business.ScaleScore]
(
ScoreID int NOT NULL IDENTITY,
CriteriaID int FOREIGN KEY REFERENCES [Business.ScaleCriteria](CriteriaID),
IndustryID int FOREIGN KEY REFERENCES [Business.Industries](IndustryID),
FromValue DECIMAL(30,3),
ToValue DECIMAL(30,3),
Score DECIMAL(10,3)
)
--Edit 
--DROP TABLE [Business.ScaleScore]

--done

CREATE TABLE [Business.Scales]
(
ScaleID CHAR(1) NOT NULL PRIMARY KEY, 
FromValue DECIMAL(10,3),
ToValue DECIMAL(10,3),
Scale NVARCHAR(50) UNIQUE
)
--done

CREATE TABLE [Business.FinancialIndex]
(
IndexID int NOT NULL PRIMARY KEY ,
IndexName NVARCHAR(255) UNIQUE ,
Unit NVARCHAR(50),
Formula NVARCHAR(255),
ValueType VARCHAR(1),
LeafIndex BIT  
)
--done

CREATE TABLE [Business.FinancialIndexLevels]
(
LevelID INT NOT NULL  PRIMARY KEY,
Score DECIMAL(10,3)
)
--done
CREATE TABLE [Business.FinancialIndexScore]
(
IndexID VARCHAR(20) FOREIGN KEY REFERENCES [Business.FinancialIndex](IndexID),
IndustryID INT FOREIGN KEY REFERENCES [Business.Industries](IndustryID),
ScaleID CHAR(1) FOREIGN KEY REFERENCES [Business.Scales](ScaleID),
FromValue DECIMAL(18,3),
ToValue DECIMAL(18,3),
FixedValue NVARCHAR(255),
LevelID INT FOREIGN KEY REFERENCES [Business.FinancialIndexLevels](LevelID),
CONSTRAINT pk_FinancialIndexScore PRIMARY KEY (IndexID,IndustryID,ScaleID,LevelID)
)
--done
CREATE TABLE [Business.FinancialIndexProportion]
(
IndustryID INT FOREIGN KEY REFERENCES [Business.Industries](IndustryID),
IndexID INT FOREIGN KEY REFERENCES [Business.FinancialIndex](IndexID),
Proportion DECIMAL(5,3),--???
CONSTRAINT pk_FinancialIndexProportion PRIMARY KEY  (IndustryID,IndexID)
)

--DONE
CREATE TABLE [Business.NonFinancialIndex]
(
IndexID INT NOT NULL PRIMARY KEY,
IndexName NVARCHAR(255) UNIQUE,
Unit NVARCHAR(50),
Formula NVARCHAR(100),
ValueType CHAR(1),
LeafIndex BIT,
Comment NVARCHAR(255)
)

--Done
CREATE TABLE [Business.NonFinancialIndexLevels]
(
LevelID INT NOT NULL PRIMARY KEY,
Score DECIMAL(10,3)
)

--Done
/* edit base on change database
DROP TABLE [Business.NonFinancialIndexScore]
CREATE TABLE [Business.NonFinancialIndexScore]
(
IndexID INT FOREIGN KEY REFERENCES [Business.NonFinancialIndex](IndexID),
IndustryID INT FOREIGN KEY REFERENCES [Business.Industries](IndustryID),
FromValue DECIMAL(18,3),
ToValue DECIMAL(18,3),
FixedValue NVARCHAR(255),
LevelID INT FOREIGN KEY REFERENCES [Business.NonFinancialIndexLevels](LevelID),
CONSTRAINT pk_NonFinancialIndexScore PRIMARY KEY (IndexID,IndustryID,LevelID)
)
*/
-- Create 3tables for changes
--DONE
CREATE TABLE [Business.NFIProportionByType]
(
IndexID INT NOT NULL FOREIGN KEY REFERENCES [Business.NonFinancialIndex](IndexID), 
TypeID NVARCHAR(10) FOREIGN KEY REFERENCES [Business.Types](TypeID),
Proportion DECIMAL(5,3),
CONSTRAINT pk_NFIProByType PRIMARY KEY(IndexID,TypeID)
)

--done
CREATE TABLE [Business.NFIProportionByIndustry]
(
IndexID INT NOT NULL FOREIGN KEY REFERENCES [Business.NonFinancialIndex](IndexID), 
IndustryID INT FOREIGN KEY REFERENCES [Business.Industries](IndustryID),
Proportion DECIMAL(5,2),
CONSTRAINT pk_NFIProByIndustry PRIMARY KEY(IndexID,IndustryID)
)

--

CREATE TABLE [Business.NFIProportionCalculated]
(
IndexID INT NOT NULL FOREIGN KEY REFERENCES [Business.NonFinancialIndex](IndexID),
IndustryID INT FOREIGN KEY REFERENCES [Business.Industries](IndustryID),
TypeID NVARCHAR(10) FOREIGN KEY REFERENCES [Business.Types](TypeID),
Proportion DECIMAL(5,3),
CONSTRAINT pk_NFIProCacul PRIMARY KEY (IndexID,IndustryID,TypeID)
)
-- End of edit
--NO
CREATE TABLE [Business.NonFinancialIndexProportion]
(
IndexID INT FOREIGN KEY REFERENCES [Business.NonFinancialIndex](IndexID),
IndustryID INT FOREIGN KEY REFERENCES [Business.Industries](IndustryID ),
TypeID NVARCHAR(10) FOREIGN KEY REFERENCES [Business.Types](TypeID),
Proportion DECIMAL(5,3),
CONSTRAINT pk_NonFinancialIndexProportion PRIMARY KEY (IndexID,IndustryID,TypeID)
)

--DONE
CREATE TABLE [Business.RankingStructure]
(
IndexType INT NOT NULL, 
AuditedStatus VARCHAR(10) NOT NULL ,
Percentage NUMERIC(3,0),
CONSTRAINT pk_RankingStructure PRIMARY KEY (IndexType,AuditedStatus)
)

--Done
CREATE TABLE [Business.Ranks]
(
RankID INT NOT NULL PRIMARY KEY ,
FromValue DECIMAL(10,3),
ToValue DECIMAL(10,3),
Rank NVARCHAR(10),
Evaluation NVARCHAR(50),
RiskGroup NVARCHAR(50),
DebtGroup INT
)

--Done
CREATE TABLE [Individual.BorrowingPurposes]
(
PurposeID INT NOT NULL PRIMARY KEY ,
Purpose NVARCHAR(255)
) 

--Done
CREATE TABLE [Individual.BasicIndex]
(
IndexID VARCHAR(20) NOT NULL PRIMARY KEY,
IndexName NVARCHAR(255) UNIQUE,
Unit NVARCHAR(50),
Formula NVARCHAR(255),
ValueType VARCHAR(1),
LeafIndex BIT
)

--Done
CREATE TABLE [Individual.BasicIndexLevels]
(
LevelID INT NOT NULL PRIMARY KEY,
Score DECIMAL(10,3)
)

--Done
CREATE TABLE [Individual.BasicIndexScore]
(
IndexID VARCHAR(20) FOREIGN KEY REFERENCES [Individual.BasicIndex](IndexID),
PurposeID INT FOREIGN KEY REFERENCES [Individual.BorrowingPurposes](PurposeID),
FromValue DECIMAL(18,3),
ToValue DECIMAL(18,3),
FixedValue NVARCHAR(255),
LevelID INT FOREIGN KEY REFERENCES [Individual.BasicIndexLevels](LevelID),
CONSTRAINT pk_BasicIndexScore PRIMARY KEY (IndexID,PurposeID,LevelID)
)

--Done
CREATE TABLE [Individual.BasicIndexProportion]
(
IndexID INT FOREIGN KEY REFERENCES [Individual.BasicIndex](IndexID),
PurposeID INT FOREIGN KEY REFERENCES [Individual.BorrowingPurposes](PurposeID),
Proportion DECIMAL(5,3),
CONSTRAINT pk_BasicIndexProportion PRIMARY KEY (IndexID,PurposeID)
)

--Done
CREATE TABLE [Individual.CollateralIndex]
(
IndexID INT NOT NULL PRIMARY KEY ,
IndexName NVARCHAR(255) UNIQUE,
Unit NVARCHAR(50),
Formula NVARCHAR(255),
ValueType VARCHAR(1),
LeafIndex BIT
)
--Done
CREATE TABLE [Individual.CollateralIndexLevels]
(
LevelID INT NOT NULL PRIMARY KEY,
Score DECIMAL(10,3)
)

--Done
CREATE TABLE [Individual.CollateralIndexScore]
(
IndexID INT FOREIGN KEY REFERENCES [Individual.CollateralIndex](IndexID),
FromValue DECIMAL(18,3),
ToValue DECIMAL(18,3),
FixedValue NVARCHAR(255),
LevelID INT FOREIGN KEY REFERENCES [Individual.CollateralIndexLevels](LevelID),
CONSTRAINT  pk_CollID PRIMARY KEY (IndexID,LevelID)
)

--Done
CREATE TABLE [Individual.BasicRanks]
(
RankID INT NOT NULL PRIMARY KEY ,
FromValue DECIMAL(10,3),
ToValue DECIMAL(10,3),
Rank NVARCHAR(10),
RiskGroup NVARCHAR(50)
)

--Done
CREATE TABLE [Individual.CollateralRanks]
(
RankID INT NOT NULL PRIMARY KEY ,
FromValue DECIMAL(18,3),
ToValue DECIMAL(18,3),
Rank NVARCHAR(10),
Evaluation NVARCHAR(50)
)

--done
CREATE TABLE [Individual.SummaryRanks]
(
BasicRankID INT FOREIGN KEY REFERENCES [Individual.BasicRanks](RankID),
CollateralRankID INT FOREIGN KEY REFERENCES [Individual.CollateralRanks](RankID),
Evaluation NVARCHAR(50),
CONSTRAINT pk_SummaryID PRIMARY KEY (BasicRankID,CollateralRankID)
)

--Done
CREATE TABLE [System.Rights]
(
RightID INT NOT NULL PRIMARY KEY ,
RightName NVARCHAR(255)
)

--Done
CREATE TABLE [System.UserGroups]
(
GroupID NVARCHAR(10) NOT NULL PRIMARY KEY ,
GroupName NVARCHAR(255)
)

--Done
CREATE TABLE [System.UserGroupsRights]
(
GroupID NVARCHAR(10) FOREIGN KEY REFERENCES [System.UserGroups](GroupID),
RightID INT FOREIGN KEY REFERENCES [System.Rights](RightID),
CONSTRAINT pk_UserGroupsRights PRIMARY KEY (GroupID,RightID)
)
--Done
CREATE TABLE [System.Branches]
(
BranchID VARCHAR(10) NOT NULL PRIMARY KEY ,
BranchName NVARCHAR(255),
Active VARCHAR(1)
)

--Done
CREATE TABLE [System.Users]
(
UserID VARCHAR(10) NOT NULL PRIMARY KEY ,
GroupID NVARCHAR(10) FOREIGN KEY REFERENCES [System.UserGroups](GroupID),
BranchID VARCHAR(10) FOREIGN KEY REFERENCES [System.Branches](BranchID),
FullName NVARCHAR(50),
Password NVARCHAR(50),
Status CHAR(1),
CreditDepartment NVARCHAR(50),

)
-- Done
CREATE TABLE [System.ReportingPeriods]
(
PeriodID VARCHAR(10) NOT NULL PRIMARY KEY ,
PeriodName NVARCHAR(255),
FromDate  DATETIME,
ToDate DATETIME,
Active VARCHAR(1)
)

--Create  table 13/1/2011

CREATE TABLE [System.CustomerTypes] 
(
TypeID VARCHAR(10)NOT NULL PRIMARY KEY,
TypeName NVARCHAR(255)
)

CREATE TABLE [Customers.Businesses]
(
CIF NUMERIC(18,0) NOT NULL PRIMARY KEY,
CustomerName NVARCHAR(255),
FCCName NVARCHAR(255)
)

GO 

CREATE TABLE [Customers.BusinessRanking]
(
CIF NUMERIC(18,0) FOREIGN KEY REFERENCES [Customers.Businesses](CIF),
PeriodID VARCHAR(10) FOREIGN KEY REFERENCES [System.ReportingPeriods](PeriodID),
BranchID VARCHAR(10) FOREIGN KEY REFERENCES [System.Branches](BranchID),
TaxCode VARCHAR(20),
CustomerGroup CHAR(1),
IndustryID INT FOREIGN KEY REFERENCES [Business.Industries](IndustryID), 
LineID NVARCHAR(10) FOREIGN KEY REFERENCES [Business.Lines](LineID),
TypeID NVARCHAR(10) FOREIGN KEY REFERENCES [Business.Types](TypeID),
AuditedStatus VARCHAR(1),
TotalDebt DECIMAL(18,3),
LoanTerm NVARCHAR(10),
CustomerTypeID VARCHAR(10) FOREIGN KEY REFERENCES [System.CustomerTypes](TypeID),
ScaleScore DECIMAL(10,3),
FinancialScore DECIMAL(10,3),
NonFinancialScore DECIMAL(10,3),
RankID INT FOREIGN KEY REFERENCES [Business.Ranks](RankID),
UserID VARCHAR(10) FOREIGN KEY REFERENCES [System.Users](UserID),
CONSTRAINT pk_BusinessRanking PRIMARY KEY (CIF,PeriodID,BranchID)
)


---------------------------UPDATE TABLE INFORMATION 

-- ko dùng lệnh được thì vào trực tiếp DB thay đổi kiểu dữ liệu
