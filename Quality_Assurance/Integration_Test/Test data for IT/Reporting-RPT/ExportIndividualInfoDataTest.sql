USE [D:\PROJECTS\FBD_GRADUATING_PROJECT\SVN_FBD\SOURCES\SOURCE_CODES\FBDSOURCE\FBD\APP_DATA\FBD.MDF]
GO

--Delete from IndividualBorrowingPurposes
--Delete from CustomersLoanTerm
--Delete from IndividualBasicRanks
--Delete from IndividualCollateralRanks
--Delete from IndividualSummaryRanks
--Delete from IndividualBasicIndex
--Delete from IndividualBasicIndexLevels
--Delete from IndividualCollateralIndex
--Delete from IndividualCollateralIndexLevels
--Delete from CustomersIndividuals
--Delete from CustomersIndividualRanking
--Delete from CustomersIndividualBasicIndex
--Delete from CustomersIndividualCollateralIndex

Insert into IndividualBorrowingPurposes values ('01', N'Mua xe máy')
Insert into CustomersLoanTerm values ('01', N'Trung Hạn')
Insert into IndividualBasicRanks values ('01', 23, 40, 'AAA', N'Nguy hiểm')
Insert into IndividualCollateralRanks values ('01', 23, 40, 'AAA', N'Nguy hiểm')
SET IDENTITY_INSERT IndividualSummaryRanks ON
Insert into IndividualSummaryRanks(ID, BasicRankID, CollateralRankID, Evaluation) values (1, '01', '01', 'Nguy hiểm')
SET IDENTITY_INSERT IndividualSummaryRanks OFF

Insert into IndividualBasicIndex values ('001', N'Chỉ tiêu 001', Null, Null, 'C', 0)
Insert into IndividualBasicIndex values ('001001', N'Chỉ tiêu 001001', Null, Null, 'C', 1)
Insert into IndividualBasicIndex values ('001002', N'Chỉ tiêu 001002', Null, Null, 'C', 1)

Insert into IndividualBasicIndexLevels values (20, 20)
Insert into IndividualBasicIndexLevels values (40, 40)
Insert into IndividualBasicIndexLevels values (60, 60)
Insert into IndividualBasicIndexLevels values (80, 80)
Insert into IndividualBasicIndexLevels values (100, 100)

Insert into IndividualCollateralIndex values ('001', N'Chỉ tiêu 001', Null, Null, 'C', 0)
Insert into IndividualCollateralIndex values ('001001', N'Chỉ tiêu 001001', Null, Null, 'C', 1)
Insert into IndividualCollateralIndex values ('001002', N'Chỉ tiêu 001002', Null, Null, 'C', 1)

Insert into IndividualCollateralIndexLevels values (20, 20)
Insert into IndividualCollateralIndexLevels values (40, 40)
Insert into IndividualCollateralIndexLevels values (60, 60)
Insert into IndividualCollateralIndexLevels values (80, 80)
Insert into IndividualCollateralIndexLevels values (100, 100)

SET IDENTITY_INSERT CustomersIndividuals ON
Insert into CustomersIndividuals(IndividualID, CIF, CustomerName, BranchID) values (1, '0123456789', N'Nguyễn Thanh Long', '010')
SET IDENTITY_INSERT CustomersIndividuals OFF
SET IDENTITY_INSERT CustomersIndividualRanking ON
Insert into CustomersIndividualRanking(ID, IndividualID, Date, CreditDepartment, PurposeID, TotalDebt, LoanTermID, BasicIndexScore, CollateralIndexScore, RankID, UserID, DateModified) values (1, 1, '04-12-2011', N'Phòng Khách Hàng DN', '01', 441131231, '01', 32, 54, 1, 'Admin', NULL)
SET IDENTITY_INSERT CustomersIndividualRanking OFF

SET IDENTITY_INSERT CustomersIndividualBasicIndex ON
Insert into CustomersIndividualBasicIndex(ID, RankingID, IndexID, Value, LevelID) values (1, 1, '001001', N'Trình độ Đại Học', 60)
Insert into CustomersIndividualBasicIndex(ID, RankingID, IndexID, Value, LevelID) values (2, 1, '001002', N'Từng đi tù vì buôn lậu quần chíp', 40)
SET IDENTITY_INSERT CustomersIndividualBasicIndex OFF

SET IDENTITY_INSERT CustomersIndividualCollateralIndex ON
Insert into CustomersIndividualCollateralIndex(ID, RankingID, IndexID, Value, LevelID) values (1, 1, '001001', N'Có ô tô', 80)
Insert into CustomersIndividualCollateralIndex(ID, RankingID, IndexID, Value, LevelID) values (2, 1, '001002', N'Nhà ba tầng', 100)
SET IDENTITY_INSERT CustomersIndividualCollateralIndex OFF