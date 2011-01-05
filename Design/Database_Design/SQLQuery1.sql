CREATE DATABASE FBD
USE FBD
GO

CREATE TABLE [Business.Industries]
(
IndustryID int NOT NULL PRIMARY KEY ,
IndustryName NVARCHAR(255) UNIQUE
)

CREATE TABLE [Business.Lines]
(
LineID INT NOT NULL PRIMARY KEY ,
IndustryID int FOREIGN KEY REFERENCES [Business.Industries](IndustryID),
LineName NVARCHAR(255) UNIQUE
)
CREATE TABLE [Business.Types]
(
TypeID int NOT NULL PRIMARY KEY ,
TypeName NVARCHAR(255) UNIQUE
)

CREATE TABLE [Business.ScaleCriteria]
(
CriteriaID int NOT NULL PRIMARY KEY,
CriteriaName NVARCHAR(255) UNIQUE,
Unit NVARCHAR(50),
Formula NVARCHAR(255),
ValueType VARCHAR(1)
)

CREATE TABLE [Business.ScaleScore]
(
ScoreID int NOT NULL PRIMARY KEY,
CriteriaID int FOREIGN KEY REFERENCES [Business.ScaleCriteria](CriteriaID),
IndustryID int FOREIGN KEY REFERENCES [Business.Industries](IndustryID),
FromValue DECIMAL(30,3),
ToValue DECIMAL(30,3),
Score DECIMAL(10,3)
)

CREATE TABLE [Business.Scales]
(
ScaleID int NOT NULL PRIMARY KEY, 
FromValue DECIMAL(10,3),
ToValue DECIMAL(10,3),
Scale NVARCHAR(50) UNIQUE
)
CREATE TABLE [Business.FinancialIndex]
(
IndexID int NOT NULL PRIMARY KEY ,
IndexName NVARCHAR(255) UNIQUE ,
Unit NVARCHAR(50),
Formula NVARCHAR(255),
ValueType VARCHAR(1),
LeafIndex BIT  
)

CREATE TABLE [Business.FinancialIndexLevels]
(
LevelID INT NOT NULL  PRIMARY KEY,
Score DECIMAL(10,3)
)

CREATE TABLE [Business.FinancialIndexScore]
(
IndexID int FOREIGN KEY REFERENCES [Business.FinancialIndex](IndexID),
IndustryID INT FOREIGN KEY REFERENCES [Business.Industries](IndustryID),
ScaleID INT FOREIGN KEY REFERENCES [Business.Scales](ScaleID),
FromValue DECIMAL(18,3),
ToValue DECIMAL(18,3),
FixedValue NVARCHAR(255),
LevelID INT FOREIGN KEY REFERENCES [Business.FinancialIndexLevels](LevelID),
CONSTRAINT pk_FinancialIndexScore PRIMARY KEY (IndexID,IndustryID,ScaleID,LevelID)
)

CREATE TABLE [Business.FinancialIndexProportion]
(
IndustryID INT FOREIGN KEY REFERENCES [Business.Industries](IndustryID),
IndexID INT FOREIGN KEY REFERENCES [Business.FinancialIndex](IndexID),
Proportion DECIMAL(5,3),
CONSTRAINT pk_FinancialIndexProportion PRIMARY KEY  (IndustryID,IndexID)
)


CREATE TABLE [Business.NonFinancialIndex]
(
IndexID INT NOT NULL PRIMARY KEY,
IndexName NVARCHAR(255) UNIQUE,
Unit NVARCHAR(50),
Formula NVARCHAR(100),
ValueType CHAR(1),
LeafIndex BIT
)
CREATE TABLE [Business.NonFinancialIndexLevels]
(
LevelID INT NOT NULL PRIMARY KEY,
Score DECIMAL(10,3)
)

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

CREATE TABLE [Business.NonFinancialIndexProportion]
(
IndexID INT FOREIGN KEY REFERENCES [Business.NonFinancialIndex](IndexID),
IndustryID INT FOREIGN KEY REFERENCES [Business.Industries](IndustryID ),
TypeID INT FOREIGN KEY REFERENCES [Business.Types](TypeID),
Proportion DECIMAL(5,3),
CONSTRAINT pk_NonFinancialIndexProportion PRIMARY KEY (IndexID,IndustryID,TypeID)
)

CREATE TABLE [Business.RankingStructure]
(
IndexType INT NOT NULL, 
AuditedStatus VARCHAR(10) NOT NULL ,
Percentage NUMERIC(3,0),
CONSTRAINT pk_RankingStructure PRIMARY KEY (IndexType,AuditedStatus)
)

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

CREATE TABLE [Individual.BorrowingPurposes]
(
PurposeID INT NOT NULL PRIMARY KEY ,
Purpose NVARCHAR(255)
) 

CREATE TABLE [Individual.BasicIndex]
(
IndexID INT NOT NULL PRIMARY KEY,
IndexName NVARCHAR(255) UNIQUE,
Unit NVARCHAR(50),
Formula NVARCHAR(255),
ValueType VARCHAR(1),
LeafIndex BIT
)

CREATE TABLE [Individual.BasicIndexLevels]
(
LevelID INT NOT NULL PRIMARY KEY,
Score DECIMAL(10,3)
)

CREATE TABLE [Individual.BasicIndexScore]
(
IndexID INT FOREIGN KEY REFERENCES [Individual.BasicIndex](IndexID),
PurposeID INT FOREIGN KEY REFERENCES [Individual.BorrowingPurposes](PurposeID),
FromValue DECIMAL(18,3),
ToValue DECIMAL(18,3),
FixedValue NVARCHAR(255),
LevelID INT FOREIGN KEY REFERENCES [Individual.BasicIndexLevels](LevelID),
CONSTRAINT pk_BasicIndexScore PRIMARY KEY (IndexID,PurposeID,LevelID)
)

CREATE TABLE [Individual.BasicIndexProportion]
(
IndexID INT FOREIGN KEY REFERENCES [Individual.BasicIndex](IndexID),
PurposeID INT FOREIGN KEY REFERENCES [Individual.BorrowingPurposes](PurposeID),
Proportion DECIMAL(5,3),
CONSTRAINT pk_BasicIndexProportion PRIMARY KEY (IndexID,PurposeID)
)


CREATE TABLE [Individual.CollateralIndex]
(
IndexID INT NOT NULL PRIMARY KEY ,
IndexName NVARCHAR(255) UNIQUE,
Unit NVARCHAR(50),
Formula NVARCHAR(255),
ValueType VARCHAR(1),
LeafIndex BIT
)

CREATE TABLE [Individual.CollateralIndexLevels]
(
LevelID INT NOT NULL PRIMARY KEY,
Score DECIMAL(10,3)
)

CREATE TABLE [Individual.CollateralIndexScore]
(
IndexID INT FOREIGN KEY REFERENCES [Individual.CollateralIndex](IndexID),
FromValue DECIMAL(18,3),
ToValue DECIMAL(18,3),
FixedValue NVARCHAR(255),
LevelID INT FOREIGN KEY REFERENCES [Individual.CollateralIndexLevels](LevelID),
CONSTRAINT  pk_CollID PRIMARY KEY (IndexID,LevelID)
)

CREATE TABLE [Individual.BasicRanks]
(
RankID INT NOT NULL PRIMARY KEY ,
FromValue DECIMAL(10,3),
ToValue DECIMAL(10,3),
Rank NVARCHAR(10),
RiskGroup NVARCHAR(50)
)

CREATE TABLE [Individual.CollateralRanks]
(
RankID INT NOT NULL PRIMARY KEY ,
FromValue DECIMAL(10,3),
ToValue DECIMAL(10,3),
Rank NVARCHAR(10),
Evaluation NVARCHAR(50)
)

CREATE TABLE [Individual.SummaryRanks]
(
BasicRankID INT FOREIGN KEY REFERENCES [Individual.BasicRanks](RankID),
CollateralRankID INT FOREIGN KEY REFERENCES [Individual.CollateralRanks](RankID),
Evaluation NVARCHAR(50),
CONSTRAINT pk_SummaryID PRIMARY KEY (BasicRankID,CollateralRankID)
)

CREATE TABLE [System.Rights]
(
RightID INT NOT NULL PRIMARY KEY ,
RightName NVARCHAR(255)
)

CREATE TABLE [System.UserGroups]
(
GroupID INT NOT NULL PRIMARY KEY ,
GroupName NVARCHAR(255)
)

CREATE TABLE [System.UserGroupsRights]
(
GroupID INT FOREIGN KEY REFERENCES [System.UserGroups](GroupID),
RightID INT FOREIGN KEY REFERENCES [System.Rights](RightID),
CONSTRAINT pk_UserGroupsRights PRIMARY KEY (GroupID,RightID)
)

CREATE TABLE [System.Users]
(
UserID INT NOT NULL PRIMARY KEY ,
GroupID INT FOREIGN KEY REFERENCES [System.UserGroups](GroupID),
BranchID INT FOREIGN KEY REFERENCES [System.Branches](BranchID),
FullName NVARCHAR(50),
Password NVARCHAR(50),
Status CHAR(1),
CreditDepartment NVARCHAR(50),

)

CREATE TABLE [System.ReportingPeriods]
(
PeriodID INT NOT NULL PRIMARY KEY ,
PeriodName NVARCHAR(50),
FromDate  DATETIME,
ToDate DATETIME,
Active VARCHAR(1)
)

CREATE TABLE [System.Branches]
(
BranchID INT NOT NULL PRIMARY KEY ,
BranchName NVARCHAR(255),
Active VARCHAR(1)
)