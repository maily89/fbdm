CREATE DATABASE FBD
GO
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
TypeID VARCHAR(4) NOT NULL PRIMARY KEY ,
TypeName NVARCHAR(255) UNIQUE
)
--done

CREATE TABLE [BusinessScaleCriteria]
(
CriteriaID VARCHAR(2) NOT NULL PRIMARY KEY,
CriteriaName NVARCHAR(255) NOT NULL UNIQUE,
Unit NVARCHAR(50),
Formula NVARCHAR(255),
ValueType VARCHAR(1)
)
---Not OK
CREATE TABLE [BusinessScaleScore]
(
ScoreID int NOT NULL IDENTITY PRIMARY KEY,
CriteriaID VARCHAR(2) NOT NULL,
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
Score DECIMAL(10,3) NOT NULL
)
--done
CREATE TABLE [BusinessFinancialIndexScore]
(
ScoreID INT IDENTITY PRIMARY KEY,
IndexID VARCHAR(20),
IndustryID VARCHAR(3),
ScaleID CHAR(1),
FromValue DECIMAL(18,3),
ToValue DECIMAL(18,3),
FixedValue NVARCHAR(255),
LevelID DECIMAL(10,0),
CONSTRAINT pk_FinancialIndexScore UNIQUE (IndexID,IndustryID,ScaleID,LevelID),

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
ProportionID INT IDENTITY PRIMARY KEY,
IndustryID VARCHAR(3),
IndexID VARCHAR(20),
Proportion DECIMAL(5,3),--???
CONSTRAINT pk_FinancialIndexProportion UNIQUE (IndustryID,IndexID),

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
Score DECIMAL(10,3) NOT NULL
)

--Done
-- edit base on change database
-- DROP TABLE [Business.NonFinancialIndexScore]
CREATE TABLE [BusinessNonFinancialIndexScore]
(
ScoreID INT IDENTITY PRIMARY KEY,
IndexID VARCHAR(20),
IndustryID VARCHAR(3),
FromValue DECIMAL(18,3),
ToValue DECIMAL(18,3),
FixedValue NVARCHAR(255),
LevelID DECIMAL(10,0),
CONSTRAINT pk_NonFinancialIndexScore UNIQUE (IndexID,IndustryID,LevelID),

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
ProportionID INT IDENTITY PRIMARY KEY,
IndexID VARCHAR(20) NOT NULL, 
TypeID VARCHAR(4) NOT NULL,
Proportion DECIMAL(5,3),
CONSTRAINT pk_NFIProByType UNIQUE(IndexID,TypeID),

CONSTRAINT BusinessNFIProportionByType_BusinessNonFinancialIndex_Delete FOREIGN KEY (IndexID) REFERENCES BusinessNonFinancialIndex(IndexID) ON DELETE CASCADE,
CONSTRAINT BusinessNFIProportionByType_BusinessNonFinancialIndex_Update FOREIGN KEY (IndexID) REFERENCES BusinessNonFinancialIndex(IndexID) ON UPDATE CASCADE,

CONSTRAINT BusinessNFIProportionByType_BusinessTypes_Delete FOREIGN KEY (TypeID) REFERENCES BusinessTypes(TypeID) ON DELETE CASCADE,
CONSTRAINT BusinessNFIProportionByType_BusinessTypes_Update FOREIGN KEY (TypeID) REFERENCES BusinessTypes(TypeID) ON UPDATE CASCADE,
)

--done
CREATE TABLE [BusinessNFIProportionByIndustry]
(
ProportionID INT IDENTITY PRIMARY KEY,
IndexID VARCHAR(20) NOT NULL, 
IndustryID VARCHAR(3) NOT NULL,
Proportion DECIMAL(5,3),
CONSTRAINT pk_NFIProByIndustry UNIQUE (IndexID,IndustryID),

CONSTRAINT BusinessNFIProportionByIndustry_BusinessNonFinancialIndex_Delete FOREIGN KEY (IndexID) REFERENCES BusinessNonFinancialIndex(IndexID) ON DELETE CASCADE,
CONSTRAINT BusinessNFIProportionByIndustry_BusinessNonFinancialIndex_Update FOREIGN KEY (IndexID) REFERENCES BusinessNonFinancialIndex(IndexID) ON UPDATE CASCADE,

CONSTRAINT BusinessNFIProportionByIndustry_BusinessIndustries_Delete FOREIGN KEY (IndustryID) REFERENCES BusinessIndustries(IndustryID) ON DELETE CASCADE,
CONSTRAINT BusinessNFIProportionByIndustry_BusinessIndustries_Update FOREIGN KEY (IndustryID) REFERENCES BusinessIndustries(IndustryID) ON UPDATE CASCADE,
)

--

CREATE TABLE [BusinessNFIProportionCalculated]
(
ProportionID INT IDENTITY PRIMARY KEY,
IndexID VARCHAR(20) NOT NULL, 
IndustryID VARCHAR(3) NOT NULL,
TypeID VARCHAR(4) NOT NULL,
Proportion DECIMAL(5,3),
CONSTRAINT pk_NFIProCaculated UNIQUE (IndexID,IndustryID,TypeID),

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
ID INT IDENTITY PRIMARY KEY,
IndexType VARCHAR(10) NOT NULL, 
AuditedStatus VARCHAR(10) NOT NULL ,
Percentage NUMERIC(3,0),
CONSTRAINT pk_RankingStructure UNIQUE (IndexType,AuditedStatus)
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
Score DECIMAL(10,3) NOT NULL
)

--Done
CREATE TABLE [IndividualBasicIndexScore]
(
ScoreID INT IDENTITY PRIMARY KEY,
IndexID VARCHAR(20) NOT NULL,
PurposeID VARCHAR(3) NOT NULL,
FromValue DECIMAL(18,3),
ToValue DECIMAL(18,3),
FixedValue NVARCHAR(255),
LevelID DECIMAL(10,0) NOT NULL,
CONSTRAINT pk_BasicIndexScore UNIQUE (IndexID,PurposeID,LevelID),

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
ProportionID INT IDENTITY PRIMARY KEY,
IndexID VARCHAR(20) NOT NULL,
PurposeID VARCHAR(3) NOT NULL,
Proportion DECIMAL(5,3),
CONSTRAINT pk_BasicIndexProportion UNIQUE (IndexID,PurposeID),

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
Score DECIMAL(10,3) NOT NULL
)

--Done
CREATE TABLE [IndividualCollateralIndexScore]
(
ScoreID INT IDENTITY PRIMARY KEY,
IndexID VARCHAR(20),
FromValue DECIMAL(18,3),
ToValue DECIMAL(18,3),
FixedValue NVARCHAR(255),
LevelID DECIMAL(10,0),
CONSTRAINT  pk_CollID UNIQUE (IndexID,LevelID),

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
ID INT IDENTITY PRIMARY KEY,
BasicRankID VARCHAR(3),
CollateralRankID VARCHAR(3),
Evaluation NVARCHAR(50),
CONSTRAINT pk_SummaryID UNIQUE (BasicRankID,CollateralRankID),

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
ID INT IDENTITY PRIMARY KEY,
GroupID VARCHAR(10),
RightID VARCHAR(20),
CONSTRAINT pk_UserGroupsRights UNIQUE (GroupID,RightID),

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
Active BIT NOT NULL
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
Active BIT NOT NULL
)

--Create  table 13/1/2011

CREATE TABLE [SystemCustomerTypes] 
(
TypeID VARCHAR(10)NOT NULL PRIMARY KEY,
TypeName NVARCHAR(255)
)

-- TABLES FOR RANKING --

CREATE TABLE [CustomersLoanTerm]
(
LoanTermID VARCHAR(10) PRIMARY KEY,
LoanTermName NVARCHAR(255),
)

CREATE TABLE [CustomersBusinesses]
(
BusinessID INT IDENTITY PRIMARY KEY,
CIF VARCHAR(20) UNIQUE NOT NULL,
CustomerName NVARCHAR(255),
BranchID VARCHAR(10),

CONSTRAINT CustomerBusinesses_SystemBranches_Update FOREIGN KEY (BranchID) REFERENCES SystemBranches(BranchID) ON UPDATE CASCADE,
CONSTRAINT CustomerBusinesses_SystemBranches_Delete FOREIGN KEY (BranchID) REFERENCES SystemBranches(BranchID) ON DELETE CASCADE,
)

CREATE TABLE [CustomersBusinessRanking]
(
ID INT IDENTITY PRIMARY KEY,
BusinessID INT,
PeriodID VARCHAR(10),
CreditDepartment NVARCHAR(255),
TaxCode VARCHAR(20),
CustomerGroup CHAR(1),
IndustryID VARCHAR(3),
TypeID VARCHAR(4),
AuditedStatus VARCHAR(1),
TotalDebt DECIMAL(18,3),
LoanTermID VARCHAR(10),
CustomerTypeID VARCHAR(10),
ScaleID CHAR(1),
FinancialScore DECIMAL(10,3),
NonFinancialScore DECIMAL(10,3),
RankID VARCHAR(3),
UserID VARCHAR(10),
DateModified DATETIME,

CONSTRAINT  pk_CustomersBusinessRanking UNIQUE (BusinessID,PeriodID),

CONSTRAINT CustomersBusinessRanking_CustomersBusinesses_Delete FOREIGN KEY (BusinessID) REFERENCES CustomersBusinesses(BusinessID) ON DELETE CASCADE,
CONSTRAINT CustomersBusinessRanking_CustomersBusinesses_Update FOREIGN KEY (BusinessID) REFERENCES CustomersBusinesses(BusinessID) ON UPDATE CASCADE,

CONSTRAINT CustomersBusinessRanking_SystemReportingPeriods_Update FOREIGN KEY (PeriodID) REFERENCES SystemReportingPeriods(PeriodID) ON UPDATE CASCADE,
CONSTRAINT CustomersBusinessRanking_SystemReportingPeriods_Delete FOREIGN KEY (PeriodID) REFERENCES SystemReportingPeriods(PeriodID) ON DELETE CASCADE,

CONSTRAINT CustomersBusinessRanking_BusinessIndustries_Update FOREIGN KEY (IndustryID) REFERENCES BusinessIndustries(IndustryID) ON UPDATE CASCADE,
CONSTRAINT CustomersBusinessRanking_BusinessIndustries_Delete FOREIGN KEY (IndustryID) REFERENCES BusinessIndustries(IndustryID) ON DELETE CASCADE,

CONSTRAINT CustomersBusinessRanking_BusinessTypes_Update FOREIGN KEY (TypeID) REFERENCES BusinessTypes(TypeID) ON UPDATE CASCADE,
CONSTRAINT CustomersBusinessRanking_BusinessTypes_Delete FOREIGN KEY (TypeID) REFERENCES BusinessTypes(TypeID) ON DELETE CASCADE,

CONSTRAINT CustomersBusinessRanking_CustomersLoanTerm_Update FOREIGN KEY (LoanTermID) REFERENCES CustomersLoanTerm(LoanTermID) ON UPDATE CASCADE,
CONSTRAINT CustomersBusinessRanking_CustomersLoanTerm_Delete FOREIGN KEY (LoanTermID) REFERENCES CustomersLoanTerm(LoanTermID) ON DELETE CASCADE,

CONSTRAINT CustomersBusinessRanking_SystemCustomerTypes_Update FOREIGN KEY (CustomerTypeID) REFERENCES SystemCustomerTypes(TypeID) ON UPDATE CASCADE,
CONSTRAINT CustomersBusinessRanking_SystemCustomerTypes_Delete FOREIGN KEY (CustomerTypeID) REFERENCES SystemCustomerTypes(TypeID) ON DELETE CASCADE,

CONSTRAINT CustomersBusinessRanking_BusinessScales_Delete FOREIGN KEY (ScaleID) REFERENCES BusinessScales(ScaleID) ON DELETE CASCADE,
CONSTRAINT CustomersBusinessRanking_BusinessScales_Update FOREIGN KEY (ScaleID) REFERENCES BusinessScales(ScaleID) ON UPDATE CASCADE,

CONSTRAINT CustomersBusinessRanking_BusinessRanks_Update FOREIGN KEY (RankID) REFERENCES BusinessRanks(RankID) ON UPDATE CASCADE,
CONSTRAINT CustomersBusinessRanking_BusinessRanks_Delete FOREIGN KEY (RankID) REFERENCES BusinessRanks(RankID) ON DELETE CASCADE,

--CONSTRAINT CustomersBusinessRanking_SystemUsers_Update FOREIGN KEY (UserID) REFERENCES SystemUsers(UserID) ON UPDATE CASCADE,
--CONSTRAINT CustomersBusinessRanking_SystemUsers_Delete FOREIGN KEY (UserID) REFERENCES SystemUsers(UserID) ON DELETE CASCADE,
)

CREATE TABLE [CustomersBusinessScale]
(
ID INT IDENTITY PRIMARY KEY,
RankingID INT,
CriteriaID VARCHAR(2),
[Value] DECIMAL(30,3),
Score DECIMAL(10,3),

CONSTRAINT  pk_CustomersBusinessScale UNIQUE (RankingID,CriteriaID),

CONSTRAINT CustomersBusinessScale_CustomersBusinessRanking_Delete FOREIGN KEY (RankingID) REFERENCES CustomersBusinessRanking(ID) ON DELETE CASCADE,
CONSTRAINT CustomersBusinessScale_CustomersBusinessRanking_Update FOREIGN KEY (RankingID) REFERENCES CustomersBusinessRanking(ID) ON UPDATE CASCADE,

CONSTRAINT CustomersBusinessScale_BusinessScaleCriteria_Update FOREIGN KEY (CriteriaID) REFERENCES BusinessScaleCriteria(CriteriaID) ON UPDATE CASCADE,
CONSTRAINT CustomersBusinessScale_BusinessScaleCriteria_Delete FOREIGN KEY (CriteriaID) REFERENCES BusinessScaleCriteria(CriteriaID) ON DELETE CASCADE,
)

CREATE TABLE [CustomersBusinessFinancialIndex]
(
ID INT IDENTITY PRIMARY KEY,
RankingID INT,
IndexID VARCHAR(20),
[Value] NVARCHAR(255),
LevelID DECIMAL(10,0),

CONSTRAINT  pk_CustomersBusinessFinancialIndex UNIQUE (RankingID,IndexID),

CONSTRAINT CustomersBusinessFinancialIndex_CustomersBusinessRanking_Delete FOREIGN KEY (RankingID) REFERENCES CustomersBusinessRanking(ID) ON DELETE CASCADE,
CONSTRAINT CustomersBusinessFinancialIndex_CustomersBusinessRanking_Update FOREIGN KEY (RankingID) REFERENCES CustomersBusinessRanking(ID) ON UPDATE CASCADE,

CONSTRAINT CustomersBusinessFinancialIndex_BusinessFinancialIndex_Update FOREIGN KEY (IndexID) REFERENCES BusinessFinancialIndex(IndexID) ON UPDATE CASCADE,
CONSTRAINT CustomersBusinessFinancialIndex_BusinessFinancialIndex_Delete FOREIGN KEY (IndexID) REFERENCES BusinessFinancialIndex(IndexID) ON DELETE CASCADE,

CONSTRAINT CustomersBusinessFinancialIndex_BusinessFinancialIndexLevels_Update FOREIGN KEY (LevelID) REFERENCES BusinessFinancialIndexLevels(LevelID) ON UPDATE CASCADE,
CONSTRAINT CustomersBusinessFinancialIndex_BusinessFinancialIndexLevels_Delete FOREIGN KEY (LevelID) REFERENCES BusinessFinancialIndexLevels(LevelID) ON DELETE CASCADE,
)

CREATE TABLE [CustomersBusinessNonFinancialIndex]
(
ID INT IDENTITY PRIMARY KEY,
RankingID INT,
IndexID VARCHAR(20),
[Value] NVARCHAR(255),
LevelID DECIMAL(10,0),

CONSTRAINT  pk_CustomersBusinessNonFinancialIndex UNIQUE (RankingID,IndexID),

CONSTRAINT CustomersBusinessNonFinancialIndex_CustomersBusinessRanking_Delete FOREIGN KEY (RankingID) REFERENCES CustomersBusinessRanking(ID) ON DELETE CASCADE,
CONSTRAINT CustomersBusinessNonFinancialIndex_CustomersBusinessRanking_Update FOREIGN KEY (RankingID) REFERENCES CustomersBusinessRanking(ID) ON UPDATE CASCADE,

CONSTRAINT CustomersBusinessNonFinancialIndex_BusinessNonFinancialIndex_Update FOREIGN KEY (IndexID) REFERENCES BusinessNonFinancialIndex(IndexID) ON UPDATE CASCADE,
CONSTRAINT CustomersBusinessNonFinancialIndex_BusinessNonFinancialIndex_Delete FOREIGN KEY (IndexID) REFERENCES BusinessNonFinancialIndex(IndexID) ON DELETE CASCADE,

CONSTRAINT CustomersBusinessNonFinancialIndex_BusinessNonFinancialIndexLevels_Update FOREIGN KEY (LevelID) REFERENCES BusinessNonFinancialIndexLevels(LevelID) ON UPDATE CASCADE,
CONSTRAINT CustomersBusinessNonFinancialIndex_BusinessNonFinancialIndexLevels_Delete FOREIGN KEY (LevelID) REFERENCES BusinessNonFinancialIndexLevels(LevelID) ON DELETE CASCADE,
)

CREATE TABLE [CustomersIndividuals]
(
IndividualID INT IDENTITY PRIMARY KEY,
CIF VARCHAR(20) UNIQUE NOT NULL,
CustomerName NVARCHAR(255),
BranchID VARCHAR(10),

CONSTRAINT CustomersIndividuals_SystemBranches_Update FOREIGN KEY (BranchID) REFERENCES SystemBranches(BranchID) ON UPDATE CASCADE,
CONSTRAINT CustomersIndividuals_SystemBranches_Delete FOREIGN KEY (BranchID) REFERENCES SystemBranches(BranchID) ON DELETE CASCADE,
)

CREATE TABLE [CustomersIndividualRanking]
(
ID INT IDENTITY PRIMARY KEY,
IndividualID INT,
Date DATETIME,
CreditDepartment NVARCHAR(255),
PurposeID VARCHAR(3),
TotalDebt DECIMAL(18,3),
LoanTermID VARCHAR(10),
BasicIndexScore DECIMAL(10,3),
CollateralIndexScore DECIMAL(10,3),
RankID INT,
UserID VARCHAR(10),
DateModified DATETIME,

CONSTRAINT  pk_CustomersIndividualRanking UNIQUE (IndividualID,Date),

CONSTRAINT CustomersIndividualRanking_CustomersIndividuals_Delete FOREIGN KEY (IndividualID) REFERENCES CustomersIndividuals(IndividualID) ON DELETE CASCADE,
CONSTRAINT CustomersIndividualRanking_CustomersIndividuals_Update FOREIGN KEY (IndividualID) REFERENCES CustomersIndividuals(IndividualID) ON UPDATE CASCADE,

CONSTRAINT CustomersIndividualRanking_IndividualBorrowingPurposes_Update FOREIGN KEY (PurposeID) REFERENCES IndividualBorrowingPurposes(PurposeID) ON UPDATE CASCADE,
CONSTRAINT CustomersIndividualRanking_IndividualBorrowingPurposes_Delete FOREIGN KEY (PurposeID) REFERENCES IndividualBorrowingPurposes(PurposeID) ON DELETE CASCADE,

CONSTRAINT CustomersIndividualRanking_CustomersLoanTerm_Update FOREIGN KEY (LoanTermID) REFERENCES CustomersLoanTerm(LoanTermID) ON UPDATE CASCADE,
CONSTRAINT CustomersIndividualRanking_CustomersLoanTerm_Delete FOREIGN KEY (LoanTermID) REFERENCES CustomersLoanTerm(LoanTermID) ON DELETE CASCADE,

CONSTRAINT CustomersIndividualRanking_IndividualSummaryRanks_Update FOREIGN KEY (RankID) REFERENCES IndividualSummaryRanks(ID) ON UPDATE CASCADE,
CONSTRAINT CustomersIndividualRanking_IndividualSummaryRanks_Delete FOREIGN KEY (RankID) REFERENCES IndividualSummaryRanks(ID) ON DELETE CASCADE,

--CONSTRAINT CustomersIndividualRanking_SystemUsers_Update FOREIGN KEY (UserID) REFERENCES SystemUsers(UserID) ON UPDATE CASCADE,
--CONSTRAINT CustomersIndividualRanking_SystemUsers_Delete FOREIGN KEY (UserID) REFERENCES SystemUsers(UserID) ON DELETE CASCADE,
)

CREATE TABLE [CustomersIndividualBasicIndex]
(
ID INT IDENTITY PRIMARY KEY,
RankingID INT,
IndexID VARCHAR(20),
[Value] NVARCHAR(255),
LevelID DECIMAL(10,0),

CONSTRAINT  pk_CustomersIndividualBasicIndex UNIQUE (RankingID,IndexID),

CONSTRAINT CustomersIndividualBasicIndex_CustomersIndividualRanking_Delete FOREIGN KEY (RankingID) REFERENCES CustomersIndividualRanking(ID) ON DELETE CASCADE,
CONSTRAINT CustomersIndividualBasicIndex_CustomersIndividualRanking_Update FOREIGN KEY (RankingID) REFERENCES CustomersIndividualRanking(ID) ON UPDATE CASCADE,

CONSTRAINT CustomersIndividualBasicIndex_IndividualBasicIndex_Update FOREIGN KEY (IndexID) REFERENCES IndividualBasicIndex(IndexID) ON UPDATE CASCADE,
CONSTRAINT CustomersIndividualBasicIndex_IndividualBasicIndex_Delete FOREIGN KEY (IndexID) REFERENCES IndividualBasicIndex(IndexID) ON DELETE CASCADE,

CONSTRAINT CustomersIndividualBasicIndex_IndividualBasicIndexLevels_Update FOREIGN KEY (LevelID) REFERENCES IndividualBasicIndexLevels(LevelID) ON UPDATE CASCADE,
CONSTRAINT CustomersIndividualBasicIndex_IndividualBasicIndexLevels_Delete FOREIGN KEY (LevelID) REFERENCES IndividualBasicIndexLevels(LevelID) ON DELETE CASCADE,
)

CREATE TABLE [CustomersIndividualCollateralIndex]
(
ID INT IDENTITY PRIMARY KEY,
RankingID INT,
IndexID VARCHAR(20),
[Value] NVARCHAR(255),
LevelID DECIMAL(10,0),

CONSTRAINT  pk_CustomersIndividualCollateralIndex UNIQUE (RankingID,IndexID),

CONSTRAINT CustomersIndividualCollateralIndex_CustomersIndividualRanking_Delete FOREIGN KEY (RankingID) REFERENCES CustomersIndividualRanking(ID) ON DELETE CASCADE,
CONSTRAINT CustomersIndividualCollateralIndex_CustomersIndividualRanking_Update FOREIGN KEY (RankingID) REFERENCES CustomersIndividualRanking(ID) ON UPDATE CASCADE,

CONSTRAINT CustomersIndividualCollateralIndex_IndividualCollateralIndex_Update FOREIGN KEY (IndexID) REFERENCES IndividualCollateralIndex(IndexID) ON UPDATE CASCADE,
CONSTRAINT CustomersIndividualCollateralIndex_IndividualCollateralIndex_Delete FOREIGN KEY (IndexID) REFERENCES IndividualCollateralIndex(IndexID) ON DELETE CASCADE,

CONSTRAINT CustomersIndividualCollateralIndex_IndividualCollateralIndexLevels_Update FOREIGN KEY (LevelID) REFERENCES IndividualCollateralIndexLevels(LevelID) ON UPDATE CASCADE,
CONSTRAINT CustomersIndividualCollateralIndex_IndividualCollateralIndexLevels_Delete FOREIGN KEY (LevelID) REFERENCES IndividualCollateralIndexLevels(LevelID) ON DELETE CASCADE,
)

-- INSERT DEFAULT DATA OF THE SYSTEM --
INSERT INTO SystemRights(RightID, RightName) VALUES ('001', N'XEM THAM SỐ')
INSERT INTO SystemRights(RightID, RightName) VALUES ('002', N'QUẢN LÝ THAM SỐ')
INSERT INTO SystemRights(RightID, RightName) VALUES ('003', N'CHẤM ĐIỂM TÍN DỤNG')
INSERT INTO SystemRights(RightID, RightName) VALUES ('004', N'QUẢN LÝ CẤU TRÚC HỆ THỐNG')

INSERT INTO SystemUserGroups(GroupID, GroupName) VALUES ('ADMIN', N'Quản trị hệ thống')

INSERT INTO SystemBranches(BranchID, BranchName, Active) VALUES ('010', N'Hội Sở Chính', 1)

INSERT INTO SystemUsers(UserID, GroupID, BranchID, FullName, Password, Status, CreditDepartment) VALUES ('Admin', 'ADMIN', '010', 'Barak Obama', '5F-4D-CC-3B-5A-A7-65-D6-1D-83-27-DE-B8-82-CF-99', null, N'Phòng Quản Trị')

INSERT INTO SystemUserGroupsRights(GroupID, RightID) VALUES ('ADMIN', '001')
INSERT INTO SystemUserGroupsRights(GroupID, RightID) VALUES ('ADMIN', '002')
INSERT INTO SystemUserGroupsRights(GroupID, RightID) VALUES ('ADMIN', '004')