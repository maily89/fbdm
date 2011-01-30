CREATE DATABASE FBD
USE FBD
GO
--done

CREATE TABLE [BusinessIndustries]
(
IndustryID VARCHAR(3) NOT NULL PRIMARY KEY ,
IndustryName NVARCHAR(255) UNIQUE
)
--done

CREATE TABLE [BusinessLines]
(
LineID INT IDENTITY NOT NULL PRIMARY KEY ,
IndustryID VARCHAR(3) NOT NULL,
LineName NVARCHAR(255) UNIQUE,
CONSTRAINT BusinessLines_BusinessIndustries_Delete FOREIGN KEY (IndustryID) REFERENCES BusinessIndustries(IndustryID) ON DELETE CASCADE,
CONSTRAINT BusinessLines_BusinessIndustries_Update FOREIGN KEY (IndustryID) REFERENCES BusinessIndustries(IndustryID) ON UPDATE CASCADE,
)
--done
CREATE TABLE [BusinessTypes]
(
TypeID CHAR(4) NOT NULL PRIMARY KEY ,
TypeName NVARCHAR(255) UNIQUE
)
--done

CREATE TABLE [BusinessScaleCriteria]
(
CriteriaID CHAR(2) NOT NULL PRIMARY KEY,
CriteriaName NVARCHAR(255) NOT NULL UNIQUE,
Unit NVARCHAR(50),
Formula NVARCHAR(255),
ValueType VARCHAR(1)
)
---Not OK
CREATE TABLE [BusinessScaleScore]
(
ScoreID int NOT NULL IDENTITY,
CriteriaID CHAR(2) NOT NULL,
IndustryID VARCHAR(3) NOT NULL,
FromValue DECIMAL(30,3),
ToValue DECIMAL(30,3),
Score DECIMAL(10,3),
CONSTRAINT BusinessScaleScore_BusinessScaleCriteria_Delete FOREIGN KEY (CriteriaID) REFERENCES BusinessScaleCriteria(CriteriaID) ON DELETE CASCADE,
CONSTRAINT BusinessScaleScore_BusinessScaleCriteria_Update FOREIGN KEY (CriteriaID) REFERENCES BusinessScaleCriteria(CriteriaID) ON UPDATE CASCADE,
CONSTRAINT BusinessScaleScore_BusinessIndustries_Delete FOREIGN KEY (IndustryID) REFERENCES BusinessIndustries(IndustryID) ON DELETE CASCADE,
CONSTRAINT BusinessScaleScore_BusinessIndustries_Update FOREIGN KEY (IndustryID) REFERENCES BusinessIndustries(IndustryID) ON UPDATE CASCADE,
)
--Edit 
--DROP TABLE [Business.ScaleScore]

--done

CREATE TABLE [BusinessScales]
(
ScaleID CHAR(1) NOT NULL PRIMARY KEY, 
FromValue DECIMAL(10,3),
ToValue DECIMAL(10,3),
Scale NVARCHAR(50) UNIQUE
)
--done

CREATE TABLE [BusinessFinancialIndex]
(
IndexID VARCHAR(20) NOT NULL PRIMARY KEY ,
IndexName NVARCHAR(255) NOT NULL UNIQUE ,
Unit NVARCHAR(50),
Formula NVARCHAR(255),
ValueType VARCHAR(1) NOT NULL,
LeafIndex BIT NOT NULL 
)
--done

CREATE TABLE [BusinessFinancialIndexLevels]
(
LevelID DECIMAL(10,0) NOT NULL  PRIMARY KEY,
Score DECIMAL(10,3)
)
--done
CREATE TABLE [BusinessFinancialIndexScore]
(
IndexID VARCHAR(20),
IndustryID VARCHAR(3),
ScaleID CHAR(1),
FromValue DECIMAL(18,3),
ToValue DECIMAL(18,3),
FixedValue NVARCHAR(255),
LevelID DECIMAL(10,0),
CONSTRAINT pk_FinancialIndexScore PRIMARY KEY (IndexID,IndustryID,ScaleID,LevelID),

CONSTRAINT BusinessFinancialIndexScore_BusinessIndustries_Delete FOREIGN KEY (IndustryID) REFERENCES BusinessIndustries(IndustryID) ON DELETE CASCADE,
CONSTRAINT BusinessFinancialIndexScore_BusinessIndustries_Update FOREIGN KEY (IndustryID) REFERENCES BusinessIndustries(IndustryID) ON UPDATE CASCADE,

CONSTRAINT BusinessFinancialIndexScore_BusinessFinancialIndex_Delete FOREIGN KEY (IndexID) REFERENCES BusinessFinancialIndex(IndexID) ON DELETE CASCADE,
CONSTRAINT BusinessFinancialIndexScore_BusinessFinancialIndex_Update FOREIGN KEY (IndexID) REFERENCES BusinessFinancialIndex(IndexID) ON UPDATE CASCADE,

CONSTRAINT BusinessFinancialIndexScore_BusinessScales_Delete FOREIGN KEY (ScaleID) REFERENCES BusinessScales(ScaleID) ON DELETE CASCADE,
CONSTRAINT BusinessFinancialIndexScore_BusinessScales_Update FOREIGN KEY (ScaleID) REFERENCES BusinessScales(ScaleID) ON UPDATE CASCADE,

CONSTRAINT BusinessFinancialIndexScore_BusinessFinancialIndexLevels_Delete FOREIGN KEY (LevelID) REFERENCES BusinessFinancialIndexLevels(LevelID) ON DELETE CASCADE,
CONSTRAINT BusinessFinancialIndexScore_BusinessFinancialIndexLevels_Update FOREIGN KEY (LevelID) REFERENCES BusinessFinancialIndexLevels(LevelID) ON UPDATE CASCADE,
)
--done
CREATE TABLE [BusinessFinancialIndexProportion]
(
IndustryID VARCHAR(3),
IndexID VARCHAR(20),
Proportion DECIMAL(5,3),--???
CONSTRAINT pk_FinancialIndexProportion PRIMARY KEY  (IndustryID,IndexID),

CONSTRAINT BusinessFinancialIndexProportion_BusinessIndustries_Delete FOREIGN KEY (IndustryID) REFERENCES BusinessIndustries(IndustryID) ON DELETE CASCADE,
CONSTRAINT BusinessFinancialIndexProportion_BusinessIndustries_Update FOREIGN KEY (IndustryID) REFERENCES BusinessIndustries(IndustryID) ON UPDATE CASCADE,

CONSTRAINT BusinessFinancialIndexProportion_BusinessFinancialIndex_Delete FOREIGN KEY (IndexID) REFERENCES BusinessFinancialIndex(IndexID) ON DELETE CASCADE,
CONSTRAINT BusinessFinancialIndexProportion_BusinessFinancialIndex_Update FOREIGN KEY (IndexID) REFERENCES BusinessFinancialIndex(IndexID) ON UPDATE CASCADE,
)

--DONE
CREATE TABLE [BusinessNonFinancialIndex]
(
IndexID VARCHAR(20) NOT NULL PRIMARY KEY,
IndexName NVARCHAR(255) NOT NULL UNIQUE,
Unit NVARCHAR(15),
Formula NVARCHAR(100),
ValueType CHAR(1) NOT NULL,
LeafIndex BIT NOT NULL,
)

--Done
CREATE TABLE [BusinessNonFinancialIndexLevels]
(
LevelID DECIMAL(10,0) NOT NULL PRIMARY KEY,
Score DECIMAL(10,3)
)

--Done
-- edit base on change database
-- DROP TABLE [Business.NonFinancialIndexScore]
CREATE TABLE [BusinessNonFinancialIndexScore]
(
IndexID VARCHAR(20),
IndustryID VARCHAR(3),
FromValue DECIMAL(18,3),
ToValue DECIMAL(18,3),
FixedValue NVARCHAR(255),
LevelID DECIMAL(10,0),
CONSTRAINT pk_NonFinancialIndexScore PRIMARY KEY (IndexID,IndustryID,LevelID),

CONSTRAINT BusinessNonFinancialIndexScore_BusinessIndustries_Delete FOREIGN KEY (IndustryID) REFERENCES BusinessIndustries(IndustryID) ON DELETE CASCADE,
CONSTRAINT BusinessNonFinancialIndexScore_BusinessIndustries_Update FOREIGN KEY (IndustryID) REFERENCES BusinessIndustries(IndustryID) ON UPDATE CASCADE,

CONSTRAINT BusinessNonFinancialIndexScore_BusinessNonFinancialIndex_Delete FOREIGN KEY (IndexID) REFERENCES BusinessNonFinancialIndex(IndexID) ON DELETE CASCADE,
CONSTRAINT BusinessNonFinancialIndexScore_BusinessNonFinancialIndex_Update FOREIGN KEY (IndexID) REFERENCES BusinessNonFinancialIndex(IndexID) ON UPDATE CASCADE,

CONSTRAINT BusinessNonFinancialIndexScore_BusinessNonFinancialIndexLevels_Delete FOREIGN KEY (LevelID) REFERENCES BusinessNonFinancialIndexLevels(LevelID) ON DELETE CASCADE,
CONSTRAINT BusinessNonFinancialIndexScore_BusinessNonFinancialIndexLevels_Update FOREIGN KEY (LevelID) REFERENCES BusinessNonFinancialIndexLevels(LevelID) ON UPDATE CASCADE,
)

-- Create 3tables for changes
--DONE
CREATE TABLE [BusinessNFIProportionByType]
(
IndexID VARCHAR(20) NOT NULL, 
TypeID CHAR(4) NOT NULL,
Proportion DECIMAL(5,3),
CONSTRAINT pk_NFIProByType PRIMARY KEY(IndexID,TypeID),

CONSTRAINT BusinessNFIProportionByType_BusinessNonFinancialIndex_Delete FOREIGN KEY (IndexID) REFERENCES BusinessNonFinancialIndex(IndexID) ON DELETE CASCADE,
CONSTRAINT BusinessNFIProportionByType_BusinessNonFinancialIndex_Update FOREIGN KEY (IndexID) REFERENCES BusinessNonFinancialIndex(IndexID) ON UPDATE CASCADE,

CONSTRAINT BusinessNFIProportionByType_BusinessTypes_Delete FOREIGN KEY (TypeID) REFERENCES BusinessTypes(TypeID) ON DELETE CASCADE,
CONSTRAINT BusinessNFIProportionByType_BusinessTypes_Update FOREIGN KEY (TypeID) REFERENCES BusinessTypes(TypeID) ON UPDATE CASCADE,
)

--done
CREATE TABLE [BusinessNFIProportionByIndustry]
(
IndexID VARCHAR(20) NOT NULL, 
IndustryID VARCHAR(3) NOT NULL,
Proportion DECIMAL(5,3),
CONSTRAINT pk_NFIProByIndustry PRIMARY KEY(IndexID,IndustryID),

CONSTRAINT BusinessNFIProportionByIndustry_BusinessNonFinancialIndex_Delete FOREIGN KEY (IndexID) REFERENCES BusinessNonFinancialIndex(IndexID) ON DELETE CASCADE,
CONSTRAINT BusinessNFIProportionByIndustry_BusinessNonFinancialIndex_Update FOREIGN KEY (IndexID) REFERENCES BusinessNonFinancialIndex(IndexID) ON UPDATE CASCADE,

CONSTRAINT BusinessNFIProportionByIndustry_BusinessIndustries_Delete FOREIGN KEY (IndustryID) REFERENCES BusinessIndustries(IndustryID) ON DELETE CASCADE,
CONSTRAINT BusinessNFIProportionByIndustry_BusinessIndustries_Update FOREIGN KEY (IndustryID) REFERENCES BusinessIndustries(IndustryID) ON UPDATE CASCADE,
)

--

CREATE TABLE [BusinessNFIProportionCalculated]
(
IndexID VARCHAR(20) NOT NULL, 
IndustryID VARCHAR(3) NOT NULL,
TypeID CHAR(4) NOT NULL,
Proportion DECIMAL(5,3),
CONSTRAINT pk_NFIProCaculated PRIMARY KEY (IndexID,IndustryID,TypeID),

CONSTRAINT BusinessNFIProportionCalculated_BusinessNonFinancialIndex_Delete FOREIGN KEY (IndexID) REFERENCES BusinessNonFinancialIndex(IndexID) ON DELETE CASCADE,
CONSTRAINT BusinessNFIProportionCalculated_BusinessNonFinancialIndex_Update FOREIGN KEY (IndexID) REFERENCES BusinessNonFinancialIndex(IndexID) ON UPDATE CASCADE,

CONSTRAINT BusinessNFIProportionCalculated_BusinessIndustries_Delete FOREIGN KEY (IndustryID) REFERENCES BusinessIndustries(IndustryID) ON DELETE CASCADE,
CONSTRAINT BusinessNFIProportionCalculated_BusinessIndustries_Update FOREIGN KEY (IndustryID) REFERENCES BusinessIndustries(IndustryID) ON UPDATE CASCADE,

CONSTRAINT BusinessNFIProportionCalculated_BusinessTypes_Delete FOREIGN KEY (TypeID) REFERENCES BusinessTypes(TypeID) ON DELETE CASCADE,
CONSTRAINT BusinessNFIProportionCalculated_BusinessTypes_Update FOREIGN KEY (TypeID) REFERENCES BusinessTypes(TypeID) ON UPDATE CASCADE,
)
-- End of edit
--NO

--DONE
CREATE TABLE [BusinessRankingStructure]
(
IndexType VARCHAR(10) NOT NULL, 
AuditedStatus VARCHAR(10) NOT NULL ,
Percentage NUMERIC(3,0),
CONSTRAINT pk_RankingStructure PRIMARY KEY (IndexType,AuditedStatus)
)

--Done
CREATE TABLE [BusinessRanks]
(
RankID VARCHAR(3) NOT NULL PRIMARY KEY ,
FromValue DECIMAL(10,3),
ToValue DECIMAL(10,3),
Rank NVARCHAR(10),
Evaluation NVARCHAR(50),
RiskGroup NVARCHAR(50),
DebtGroup INT
)

--Done
CREATE TABLE [IndividualBorrowingPurposes]
(
PurposeID VARCHAR(3) NOT NULL PRIMARY KEY ,
Purpose NVARCHAR(255) NOT NULL,
) 

--Done
CREATE TABLE [IndividualBasicIndex]
(
IndexID VARCHAR(20) NOT NULL PRIMARY KEY,
IndexName NVARCHAR(255) NOT NULL UNIQUE,
Unit NVARCHAR(50),
Formula NVARCHAR(255),
ValueType VARCHAR(1) NOT NULL,
LeafIndex BIT NOT NULL
)

--Done
CREATE TABLE [IndividualBasicIndexLevels]
(
LevelID DECIMAL(10,0) NOT NULL PRIMARY KEY,
Score DECIMAL(10,3)
)

--Done
CREATE TABLE [IndividualBasicIndexScore]
(
IndexID VARCHAR(20) NOT NULL,
PurposeID VARCHAR(3) NOT NULL,
FromValue DECIMAL(18,3),
ToValue DECIMAL(18,3),
FixedValue NVARCHAR(255),
LevelID DECIMAL(10,0) NOT NULL,
CONSTRAINT pk_BasicIndexScore PRIMARY KEY (IndexID,PurposeID,LevelID),

CONSTRAINT IndividualBasicIndexScore_IndividualBasicIndex_Delete FOREIGN KEY (IndexID) REFERENCES IndividualBasicIndex(IndexID) ON DELETE CASCADE,
CONSTRAINT IndividualBasicIndexScore_IndividualBasicIndex_Update FOREIGN KEY (IndexID) REFERENCES IndividualBasicIndex(IndexID) ON UPDATE CASCADE,

CONSTRAINT IndividualBasicIndexScore_IndividualBorrowingPurposes_Delete FOREIGN KEY (PurposeID) REFERENCES IndividualBorrowingPurposes(PurposeID) ON DELETE CASCADE,
CONSTRAINT IndividualBasicIndexScore_IndividualBorrowingPurposes_Update FOREIGN KEY (PurposeID) REFERENCES IndividualBorrowingPurposes(PurposeID) ON UPDATE CASCADE,

CONSTRAINT IndividualBasicIndexScore_IndividualBasicIndexLevels_Delete FOREIGN KEY (LevelID) REFERENCES IndividualBasicIndexLevels(LevelID) ON DELETE CASCADE,
CONSTRAINT IndividualBasicIndexScore_IndividualBasicIndexLevels_Update FOREIGN KEY (LevelID) REFERENCES IndividualBasicIndexLevels(LevelID) ON UPDATE CASCADE,
)

--Done
CREATE TABLE [IndividualBasicIndexProportion]
(
IndexID VARCHAR(20) NOT NULL,
PurposeID VARCHAR(3) NOT NULL,
Proportion DECIMAL(5,3),
CONSTRAINT pk_BasicIndexProportion PRIMARY KEY (IndexID,PurposeID),

CONSTRAINT IndividualBasicIndexProportion_IndividualBasicIndex_Delete FOREIGN KEY (IndexID) REFERENCES IndividualBasicIndex(IndexID) ON DELETE CASCADE,
CONSTRAINT IndividualBasicIndexProportion_IndividualBasicIndex_Update FOREIGN KEY (IndexID) REFERENCES IndividualBasicIndex(IndexID) ON UPDATE CASCADE,

CONSTRAINT IndividualBasicIndexProportion_IndividualBorrowingPurposes_Delete FOREIGN KEY (PurposeID) REFERENCES IndividualBorrowingPurposes(PurposeID) ON DELETE CASCADE,
CONSTRAINT IndividualBasicIndexProportion_IndividualBorrowingPurposes_Update FOREIGN KEY (PurposeID) REFERENCES IndividualBorrowingPurposes(PurposeID) ON UPDATE CASCADE,
)

--Done
CREATE TABLE [IndividualCollateralIndex]
(
IndexID VARCHAR(20) NOT NULL PRIMARY KEY ,
IndexName NVARCHAR(255) NOT NULL UNIQUE,
Unit NVARCHAR(50),
Formula NVARCHAR(255),
ValueType VARCHAR(1) NOT NULL,
LeafIndex BIT NOT NULL
)
--Done
CREATE TABLE [IndividualCollateralIndexLevels]
(
LevelID DECIMAL(10,0) NOT NULL PRIMARY KEY,
Score DECIMAL(10,3)
)

--Done
CREATE TABLE [IndividualCollateralIndexScore]
(
IndexID VARCHAR(20),
FromValue DECIMAL(18,3),
ToValue DECIMAL(18,3),
FixedValue NVARCHAR(255),
LevelID DECIMAL(10,0),
CONSTRAINT  pk_CollID PRIMARY KEY (IndexID,LevelID),

CONSTRAINT IndividualCollateralIndexScore_IndividualCollateralIndex_Delete FOREIGN KEY (IndexID) REFERENCES IndividualCollateralIndex(IndexID) ON DELETE CASCADE,
CONSTRAINT IndividualCollateralIndexScore_IndividualCollateralIndex_Update FOREIGN KEY (IndexID) REFERENCES IndividualCollateralIndex(IndexID) ON UPDATE CASCADE,

CONSTRAINT IndividualCollateralIndexScore_IndividualCollateralIndexLevels_Delete FOREIGN KEY (LevelID) REFERENCES IndividualCollateralIndexLevels(LevelID) ON DELETE CASCADE,
CONSTRAINT IndividualCollateralIndexScore_IndividualCollateralIndexLevels_Update FOREIGN KEY (LevelID) REFERENCES IndividualCollateralIndexLevels(LevelID) ON UPDATE CASCADE,
)

--Done
CREATE TABLE [IndividualBasicRanks]
(
RankID VARCHAR(3) NOT NULL PRIMARY KEY ,
FromValue DECIMAL(10,3),
ToValue DECIMAL(10,3),
Rank NVARCHAR(10),
RiskGroup NVARCHAR(50)
)

--Done
CREATE TABLE [IndividualCollateralRanks]
(
RankID VARCHAR(3) NOT NULL PRIMARY KEY ,
FromValue DECIMAL(10,3),
ToValue DECIMAL(10,3),
Rank NVARCHAR(10),
Evaluation NVARCHAR(50)
)

--done
CREATE TABLE [IndividualSummaryRanks]
(
BasicRankID VARCHAR(3),
CollateralRankID VARCHAR(3),
Evaluation NVARCHAR(50),
CONSTRAINT pk_SummaryID PRIMARY KEY (BasicRankID,CollateralRankID),

CONSTRAINT IndividualSummaryRanks_IndividualBasicRanks_Delete FOREIGN KEY (BasicRankID) REFERENCES IndividualBasicRanks(RankID) ON DELETE CASCADE,
CONSTRAINT IndividualSummaryRanks_IndividualBasicRanks_Update FOREIGN KEY (BasicRankID) REFERENCES IndividualBasicRanks(RankID) ON UPDATE CASCADE,

CONSTRAINT IndividualSummaryRanks_IndividualCollateralRanks_Delete FOREIGN KEY (CollateralRankID) REFERENCES IndividualCollateralRanks(RankID) ON DELETE CASCADE,
CONSTRAINT IndividualSummaryRanks_IndividualCollateralRanks_Update FOREIGN KEY (CollateralRankID) REFERENCES IndividualCollateralRanks(RankID) ON UPDATE CASCADE,
)

--Done
CREATE TABLE [SystemRights]
(
RightID VARCHAR(20) NOT NULL PRIMARY KEY ,
RightName NVARCHAR(255)
)

--Done
CREATE TABLE [SystemUserGroups]
(
GroupID VARCHAR(10) NOT NULL PRIMARY KEY ,
GroupName NVARCHAR(255)
)

--Done
CREATE TABLE [SystemUserGroupsRights]
(
GroupID VARCHAR(10),
RightID VARCHAR(20),
CONSTRAINT pk_UserGroupsRights PRIMARY KEY (GroupID,RightID),

CONSTRAINT SystemUserGroupsRights_SystemUserGroups_Delete FOREIGN KEY (GroupID) REFERENCES SystemUserGroups(GroupID) ON DELETE CASCADE,
CONSTRAINT SystemUserGroupsRights_SystemUserGroups_Update FOREIGN KEY (GroupID) REFERENCES SystemUserGroups(GroupID) ON UPDATE CASCADE,

CONSTRAINT SystemUserGroupsRights_SystemRights_Delete FOREIGN KEY (RightID) REFERENCES SystemRights(RightID) ON DELETE CASCADE,
CONSTRAINT SystemUserGroupsRights_SystemRights_Update FOREIGN KEY (RightID) REFERENCES SystemRights(RightID) ON UPDATE CASCADE,
)
--Done
CREATE TABLE [SystemBranches]
(
BranchID VARCHAR(10) NOT NULL PRIMARY KEY ,
BranchName NVARCHAR(255),
Active VARCHAR(1)
)

--Done
CREATE TABLE [SystemUsers]
(
UserID VARCHAR(10) NOT NULL PRIMARY KEY ,
GroupID VARCHAR(10) NOT NULL,
BranchID VARCHAR(10) NOT NULL,
FullName NVARCHAR(50),
Password NVARCHAR(50) NOT NULL,
Status CHAR(1),
CreditDepartment NVARCHAR(50),

CONSTRAINT SystemUsers_SystemUserGroups_Delete FOREIGN KEY (GroupID) REFERENCES SystemUserGroups(GroupID) ON DELETE CASCADE,
CONSTRAINT SystemUsers_SystemUserGroups_Update FOREIGN KEY (GroupID) REFERENCES SystemUserGroups(GroupID) ON UPDATE CASCADE,

CONSTRAINT SystemUsers_SystemBranches_Delete FOREIGN KEY (BranchID) REFERENCES SystemBranches(BranchID) ON DELETE CASCADE,
CONSTRAINT SystemUsers_SystemBranches_Update FOREIGN KEY (BranchID) REFERENCES SystemBranches(BranchID) ON UPDATE CASCADE,
)
-- Done
CREATE TABLE [SystemReportingPeriods]
(
PeriodID VARCHAR(10) NOT NULL PRIMARY KEY ,
PeriodName NVARCHAR(50),
FromDate  DATETIME,
ToDate DATETIME,
Active VARCHAR(1)
)

--Create  table 13/1/2011

CREATE TABLE [SystemCustomerTypes] 
(
TypeID VARCHAR(10)NOT NULL PRIMARY KEY,
TypeName NVARCHAR(255)
)

--CREATE TABLE [Customers.Businesses]
--(
--CIF NUMERIC(18,0) NOT NULL PRIMARY KEY,
--CustomerName NVARCHAR(255),
--FCCName NVARCHAR(255)
--)
--
--GO 
--
--CREATE TABLE [Customers.BusinessRanking]
--(
--CIF NUMERIC(18,0) FOREIGN KEY REFERENCES [Customers.Businesses](CIF),
--PeriodID VARCHAR(10) FOREIGN KEY REFERENCES [System.ReportingPeriods](PeriodID),
--BranchID VARCHAR(10) FOREIGN KEY REFERENCES [System.Branches](BranchID),
--TaxCode VARCHAR(20),
--CustomerGroup CHAR(1),
--IndustryID INT FOREIGN KEY REFERENCES [Business.Industries](IndustryID), 
--LineID NVARCHAR(10) FOREIGN KEY REFERENCES [Business.Lines](LineID),
--TypeID NVARCHAR(10) FOREIGN KEY REFERENCES [Business.Types](TypeID),
--AuditedStatus VARCHAR(1),
--TotalDebt DECIMAL(18,3),
--LoanTerm NVARCHAR(10),
--CustomerTypeID VARCHAR(10) FOREIGN KEY REFERENCES [System.CustomerTypes](TypeID),
--ScaleScore DECIMAL(10,3),
--FinancialScore DECIMAL(10,3),
--NonFinancialScore DECIMAL(10,3),
--RankID INT FOREIGN KEY REFERENCES [Business.Ranks](RankID),
--UserID VARCHAR(10) FOREIGN KEY REFERENCES [System.Users](UserID),
--CONSTRAINT pk_BusinessRanking PRIMARY KEY (CIF,PeriodID,BranchID)
--)


---------------------------UPDATE TABLE INFORMATION 

-- ko dùng lệnh được thì vào trực tiếp DB thay đổi kiểu dữ liệu
