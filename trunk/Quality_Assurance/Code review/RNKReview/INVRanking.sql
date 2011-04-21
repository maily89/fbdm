USE [D:\PROJECTS\FBD_GRADUATING_PROJECT\SVN_FBD\SOURCES\SOURCE_CODES\FBDSOURCE\FBD\APP_DATA\FBD.MDF]
GO

--Delete from SystemBranches where BranchID = '020'
--Delete from SystemCustomerTypes
--Delete from CustomersLoanTerm
--Delete from SystemUserGroups where GroupID = 'CRD'
--Delete from SystemUsers where UserID = 'LONGNT'
--Delete from IndividualBorrowingPurposes
--Delete from IndividualBasicIndex
--Delete from IndividualBasicIndexLevels
--Delete from IndividualBasicIndexScore
--Delete from IndividualBasicIndexProportion
--Delete from IndividualCollateralIndex
--Delete from IndividualCollateralIndexLevels
--Delete from IndividualCollateralIndexScore
--Delete from IndividualBasicRanks
--Delete from IndividualCollateralRanks
--Delete from IndividualSummaryRanks
--Delete from CustomersIndividuals
--Delete from CustomersIndividualRanking
--Delete from CustomersIndividualBasicIndex
--Delete from CustomersIndividualCollateralIndex

-- Chi nhánh
Insert into SystemBranches values ('020', N'Chi nhánh 2', 1)

-- Nhóm khách hàng
Insert into SystemCustomerTypes values ('01', N'Doanh nghiệp')
Insert into SystemCustomerTypes values ('02', N'Cá nhân')

-- Thời hạn vay
Insert into CustomersLoanTerm values ('01', N'Ngắn hạn')
Insert into CustomersLoanTerm values ('02', N'Trung hạn')
Insert into CustomersLoanTerm values ('03', N'Dài hạn')

-- Phân quyền cán bộ tín dụng
Insert into SystemUserGroups values ('CRD', N'CÁN BỘ TÍN DỤNG')
Insert into SystemUsers values ('LONGNT', 'CRD', '020', N'Nguyễn Thanh Long', '5F-4D-CC-3B-5A-A7-65-D6-1D-83-27-DE-B8-82-CF-99', null, N'Phòng tín dụng doanh nghiệp')
SET Identity_insert SystemUserGroupsRights on
Insert into SystemUserGroupsRights(ID, GroupID, RightID) values (5, 'CRD', '001')
Insert into SystemUserGroupsRights(ID, GroupID, RightID) values (6, 'CRD', '003')
Insert into SystemUserGroupsRights(ID, GroupID, RightID) values (7, 'CRD', '004')
SET Identity_insert SystemUserGroupsRights off

-- Mục đích vay cá nhân
Insert into IndividualBorrowingPurposes values ('01', N'Mục đích 01')
Insert into IndividualBorrowingPurposes values ('02', N'Mục đích 02')

-- Chỉ tiêu cá nhân cơ bản
Insert into IndividualBasicIndex values ('001', N'Chỉ tiêu 001', null, null, 'N', 0)
Insert into IndividualBasicIndex values ('001001', N'Chỉ tiêu 001001', null, null, 'N', 1)
Insert into IndividualBasicIndex values ('001002', N'Chỉ tiêu 001002', null, null, 'N', 1)
Insert into IndividualBasicIndex values ('002', N'Chỉ tiêu 002', null, null, 'N', 0)
Insert into IndividualBasicIndex values ('002001', N'Chỉ tiêu 002001', null, null, 'C', 1)
Insert into IndividualBasicIndex values ('002002', N'Chỉ tiêu 002002', null, null, 'C', 1)

Insert into IndividualBasicIndexLevels values (20, 20)
Insert into IndividualBasicIndexLevels values (40, 40)
Insert into IndividualBasicIndexLevels values (60, 60)
Insert into IndividualBasicIndexLevels values (80, 80)
Insert into IndividualBasicIndexLevels values (100, 100)

SET Identity_insert IndividualBasicIndexScore on
Insert into IndividualBasicIndexScore(ScoreID, IndexID, PurposeID, FromValue, ToValue, FixedValue, LevelID) 
values (1, '001001', '01', 0, 50, Null, 40)
Insert into IndividualBasicIndexScore(ScoreID, IndexID, PurposeID, FromValue, ToValue, FixedValue, LevelID) 
values (2, '001001', '01', 50, 100, Null, 80)
Insert into IndividualBasicIndexScore(ScoreID, IndexID, PurposeID, FromValue, ToValue, FixedValue, LevelID) 
values (3, '001001', '02', 0, 50, Null, 60)
Insert into IndividualBasicIndexScore(ScoreID, IndexID, PurposeID, FromValue, ToValue, FixedValue, LevelID) 
values (4, '001001', '02', 50, 100, Null, 100)
Insert into IndividualBasicIndexScore(ScoreID, IndexID, PurposeID, FromValue, ToValue, FixedValue, LevelID) 
values (5, '001002', '01', 0, 50, Null, 40)
Insert into IndividualBasicIndexScore(ScoreID, IndexID, PurposeID, FromValue, ToValue, FixedValue, LevelID) 
values (6, '001002', '01', 50, 100, Null, 80)
Insert into IndividualBasicIndexScore(ScoreID, IndexID, PurposeID, FromValue, ToValue, FixedValue, LevelID) 
values (7, '001002', '02', 0, 50, Null, 60)
Insert into IndividualBasicIndexScore(ScoreID, IndexID, PurposeID, FromValue, ToValue, FixedValue, LevelID) 
values (8, '001002', '02', 50, 100, Null, 100)
Insert into IndividualBasicIndexScore(ScoreID, IndexID, PurposeID, FromValue, ToValue, FixedValue, LevelID) 
values (9, '002001', '01', Null, Null, N'Từ 0 đến 50', 40)
Insert into IndividualBasicIndexScore(ScoreID, IndexID, PurposeID, FromValue, ToValue, FixedValue, LevelID) 
values (10, '002001', '01', Null, Null, N'Từ 50 đến 100', 80)
Insert into IndividualBasicIndexScore(ScoreID, IndexID, PurposeID, FromValue, ToValue, FixedValue, LevelID) 
values (11, '002001', '02', Null, Null, N'Từ 0 đến 50', 60)
Insert into IndividualBasicIndexScore(ScoreID, IndexID, PurposeID, FromValue, ToValue, FixedValue, LevelID) 
values (12, '002001', '02', Null, Null, N'Từ 50 đến 100', 100)
Insert into IndividualBasicIndexScore(ScoreID, IndexID, PurposeID, FromValue, ToValue, FixedValue, LevelID) 
values (13, '002002', '01', Null, Null, N'Từ 0 đến 50', 40)
Insert into IndividualBasicIndexScore(ScoreID, IndexID, PurposeID, FromValue, ToValue, FixedValue, LevelID) 
values (14, '002002', '01', Null, Null, N'Từ 50 đến 100', 80)
Insert into IndividualBasicIndexScore(ScoreID, IndexID, PurposeID, FromValue, ToValue, FixedValue, LevelID) 
values (15, '002002', '02', Null, Null, N'Từ 0 đến 50', 60)
Insert into IndividualBasicIndexScore(ScoreID, IndexID, PurposeID, FromValue, ToValue, FixedValue, LevelID) 
values (16, '002002', '02', Null, Null, N'Từ 50 đến 100', 100)
SET Identity_insert IndividualBasicIndexScore off

SET Identity_insert IndividualBasicIndexProportion on
Insert into IndividualBasicIndexProportion(ProportionID, PurposeID, IndexID, Proportion) values (1, '01', '001001', 10)
Insert into IndividualBasicIndexProportion(ProportionID, PurposeID, IndexID, Proportion) values (2, '01', '001002', 20)
Insert into IndividualBasicIndexProportion(ProportionID, PurposeID, IndexID, Proportion) values (3, '01', '002001', 30)
Insert into IndividualBasicIndexProportion(ProportionID, PurposeID, IndexID, Proportion) values (4, '01', '002002', 40)
Insert into IndividualBasicIndexProportion(ProportionID, PurposeID, IndexID, Proportion) values (5, '02', '001001', 10)
Insert into IndividualBasicIndexProportion(ProportionID, PurposeID, IndexID, Proportion) values (6, '02', '001002', 20)
Insert into IndividualBasicIndexProportion(ProportionID, PurposeID, IndexID, Proportion) values (7, '02', '002001', 30)
Insert into IndividualBasicIndexProportion(ProportionID, PurposeID, IndexID, Proportion) values (8, '02', '002002', 40)
SET Identity_insert IndividualBasicIndexProportion off

-- Chỉ tiêu tài sản đảm bảo
Insert into IndividualCollateralIndex values ('001', N'Chỉ tiêu 001', null, null, 'N', 0)
Insert into IndividualCollateralIndex values ('001001', N'Chỉ tiêu 001001', null, null, 'N', 1)
Insert into IndividualCollateralIndex values ('001002', N'Chỉ tiêu 001002', null, null, 'N', 1)
Insert into IndividualCollateralIndex values ('002', N'Chỉ tiêu 002', null, null, 'N', 0)
Insert into IndividualCollateralIndex values ('002001', N'Chỉ tiêu 002001', null, null, 'C', 1)
Insert into IndividualCollateralIndex values ('002002', N'Chỉ tiêu 002002', null, null, 'C', 1)

Insert into IndividualCollateralIndexLevels values (20, 20)
Insert into IndividualCollateralIndexLevels values (40, 40)
Insert into IndividualCollateralIndexLevels values (60, 60)
Insert into IndividualCollateralIndexLevels values (80, 80)
Insert into IndividualCollateralIndexLevels values (100, 100)

SET Identity_insert IndividualCollateralIndexScore on
Insert into IndividualCollateralIndexScore(ScoreID, IndexID, FromValue, ToValue, FixedValue, LevelID) 
values (1, '001001', 0, 50, Null, 40)
Insert into IndividualCollateralIndexScore(ScoreID, IndexID, FromValue, ToValue, FixedValue, LevelID) 
values (2, '001001', 50, 100, Null, 80)
Insert into IndividualCollateralIndexScore(ScoreID, IndexID, FromValue, ToValue, FixedValue, LevelID) 
values (3, '001002', 0, 50, Null, 40)
Insert into IndividualCollateralIndexScore(ScoreID, IndexID, FromValue, ToValue, FixedValue, LevelID) 
values (4, '001002', 50, 100, Null, 80)
Insert into IndividualCollateralIndexScore(ScoreID, IndexID, FromValue, ToValue, FixedValue, LevelID) 
values (5, '002001', Null, Null, N'Từ 0 đến 50', 40)
Insert into IndividualCollateralIndexScore(ScoreID, IndexID, FromValue, ToValue, FixedValue, LevelID) 
values (6, '002001', Null, Null, N'Từ 50 đến 100', 80)
Insert into IndividualCollateralIndexScore(ScoreID, IndexID, FromValue, ToValue, FixedValue, LevelID) 
values (7, '002002', Null, Null, N'Từ 0 đến 50', 40)
Insert into IndividualCollateralIndexScore(ScoreID, IndexID, FromValue, ToValue, FixedValue, LevelID) 
values (8, '002002', Null, Null, N'Từ 50 đến 100', 80)
SET Identity_insert IndividualCollateralIndexScore off

-- Bảng đánh giá xếp loại chỉ tiêu cá nhân cơ bản
Insert into IndividualBasicRanks values ('01', 90, 100, 'AAA', N'Rất tốt')
Insert into IndividualBasicRanks values ('02', 80, 90, 'AA', N'Khá tốt')
Insert into IndividualBasicRanks values ('03', 70, 80, 'A', N'Tốt')
Insert into IndividualBasicRanks values ('04', 60, 70, 'BBB', N'Rất khá')
Insert into IndividualBasicRanks values ('05', 50, 60, 'BB', N'Được')
Insert into IndividualBasicRanks values ('06', 40, 50, 'B', N'Khá')
Insert into IndividualBasicRanks values ('07', 30, 40, 'CCC', N'Bình thường')
Insert into IndividualBasicRanks values ('08', 20, 30, 'CC', N'Hơi kém')
Insert into IndividualBasicRanks values ('09', 10, 20, 'C', N'Kém')
Insert into IndividualBasicRanks values ('10', 0, 10, 'D', N'Rất kém')

-- Bảng đánh giá xếp loại chỉ tiêu tài sản đảm bảo
Insert into IndividualCollateralRanks values ('01', 200, 9999999, 'A', N'Tốt')
Insert into IndividualCollateralRanks values ('02', 100, 200, 'B', N'Khá')
Insert into IndividualCollateralRanks values ('03', 0, 100, 'C', N'Kém')

-- Bảng đánh giá tổng hợp xếp loại khách hàng cá nhân
SET Identity_insert IndividualSummaryRanks on
Insert into IndividualSummaryRanks(ID, BasicRankID, CollateralRankID, Evaluation) values (1, '01', '01', N'Xuất sắc')
Insert into IndividualSummaryRanks(ID, BasicRankID, CollateralRankID, Evaluation) values (2, '01', '02', N'Tốt')
Insert into IndividualSummaryRanks(ID, BasicRankID, CollateralRankID, Evaluation) values (3, '01', '03', N'Khá')
Insert into IndividualSummaryRanks(ID, BasicRankID, CollateralRankID, Evaluation) values (4, '02', '01', N'Xuất sắc')
Insert into IndividualSummaryRanks(ID, BasicRankID, CollateralRankID, Evaluation) values (5, '02', '02', N'Tốt')
Insert into IndividualSummaryRanks(ID, BasicRankID, CollateralRankID, Evaluation) values (6, '02', '03', N'Khá')
Insert into IndividualSummaryRanks(ID, BasicRankID, CollateralRankID, Evaluation) values (7, '03', '01', N'Xuất sắc')
Insert into IndividualSummaryRanks(ID, BasicRankID, CollateralRankID, Evaluation) values (8, '03', '02', N'Tốt')
Insert into IndividualSummaryRanks(ID, BasicRankID, CollateralRankID, Evaluation) values (9, '03', '03', N'Khá')
Insert into IndividualSummaryRanks(ID, BasicRankID, CollateralRankID, Evaluation) values (10, '04', '01', N'Tốt')
Insert into IndividualSummaryRanks(ID, BasicRankID, CollateralRankID, Evaluation) values (11, '04', '02', N'Khá')
Insert into IndividualSummaryRanks(ID, BasicRankID, CollateralRankID, Evaluation) values (12, '04', '03', N'Trung bình')
Insert into IndividualSummaryRanks(ID, BasicRankID, CollateralRankID, Evaluation) values (13, '05', '01', N'Tốt')
Insert into IndividualSummaryRanks(ID, BasicRankID, CollateralRankID, Evaluation) values (14, '05', '02', N'Khá')
Insert into IndividualSummaryRanks(ID, BasicRankID, CollateralRankID, Evaluation) values (15, '05', '03', N'Trung bình')
Insert into IndividualSummaryRanks(ID, BasicRankID, CollateralRankID, Evaluation) values (16, '06', '01', N'Tốt')
Insert into IndividualSummaryRanks(ID, BasicRankID, CollateralRankID, Evaluation) values (17, '06', '02', N'Khá')
Insert into IndividualSummaryRanks(ID, BasicRankID, CollateralRankID, Evaluation) values (18, '06', '03', N'Trung bình')
Insert into IndividualSummaryRanks(ID, BasicRankID, CollateralRankID, Evaluation) values (19, '07', '01', N'Khá')
Insert into IndividualSummaryRanks(ID, BasicRankID, CollateralRankID, Evaluation) values (20, '07', '02', N'Trung bình')
Insert into IndividualSummaryRanks(ID, BasicRankID, CollateralRankID, Evaluation) values (21, '07', '03', N'Trung bình thấp')
Insert into IndividualSummaryRanks(ID, BasicRankID, CollateralRankID, Evaluation) values (22, '08', '01', N'Khá')
Insert into IndividualSummaryRanks(ID, BasicRankID, CollateralRankID, Evaluation) values (23, '08', '02', N'Trung bình')
Insert into IndividualSummaryRanks(ID, BasicRankID, CollateralRankID, Evaluation) values (24, '08', '03', N'Trung bình thấp')
Insert into IndividualSummaryRanks(ID, BasicRankID, CollateralRankID, Evaluation) values (25, '09', '01', N'Khá')
Insert into IndividualSummaryRanks(ID, BasicRankID, CollateralRankID, Evaluation) values (26, '09', '02', N'Trung bình')
Insert into IndividualSummaryRanks(ID, BasicRankID, CollateralRankID, Evaluation) values (27, '09', '03', N'Trung bình thấp')
Insert into IndividualSummaryRanks(ID, BasicRankID, CollateralRankID, Evaluation) values (28, '10', '01', N'Trung bình')
Insert into IndividualSummaryRanks(ID, BasicRankID, CollateralRankID, Evaluation) values (29, '10', '02', N'Từ chối')
Insert into IndividualSummaryRanks(ID, BasicRankID, CollateralRankID, Evaluation) values (30, '10', '03', N'Từ chối')
SET Identity_insert IndividualSummaryRanks off

-- Danh sách khách hàng
SET Identity_insert CustomersIndividuals on
Insert into CustomersIndividuals(IndividualID, CIF, CustomerName, BranchID) values (1, '11111', 'Quang Chinh', '010')
Insert into CustomersIndividuals(IndividualID, CIF, CustomerName, BranchID) values (2, '22222', 'Mai Ly', '010')
Insert into CustomersIndividuals(IndividualID, CIF, CustomerName, BranchID) values (3, '33333', 'Huong Giang', '020')
Insert into CustomersIndividuals(IndividualID, CIF, CustomerName, BranchID) values (4, '44444', 'Phuong Thao', '020')
SET Identity_insert CustomersIndividuals off

-- Khách hàng được chấm điểm tín dụng
SET Identity_insert CustomersIndividualRanking on
-- Khách hàng số 1
Insert into CustomersIndividualRanking(ID,IndividualID,Date,CreditDepartment,PurposeID,TotalDebt,LoanTermID,BasicIndexScore,CollateralIndexScore,RankID,ClusterRankID,UserID,DateModified)
values (1, 1, '04-17-2011', N'Phòng Cá Nhân', '01', 111111111, '01', 64, 240, 10, Null, 'LONGNT', '04-15-2011')
-- Khách hàng số 2
Insert into CustomersIndividualRanking(ID,IndividualID,Date,CreditDepartment,PurposeID,TotalDebt,LoanTermID,BasicIndexScore,CollateralIndexScore,RankID,ClusterRankID,UserID,DateModified)
values (2, 2, '04-17-2011', N'Phòng Cá Nhân', '02', 222222222, '02', 84, 240, 4, Null, 'LONGNT', '04-15-2011')
-- Khách hàng số 3
Insert into CustomersIndividualRanking(ID,IndividualID,Date,CreditDepartment,PurposeID,TotalDebt,LoanTermID,BasicIndexScore,CollateralIndexScore,RankID,ClusterRankID,UserID,DateModified)
values (3, 3, '04-17-2011', N'Phòng Cá Nhân', Null, 222222222, Null, 84, 240, Null, Null, 'LONGNT', '04-15-2011')
SET Identity_insert CustomersIndividualRanking off

-- Điểm cá nhân cơ bản
SET Identity_insert CustomersIndividualBasicIndex on
-- Khách hàng số 1
Insert into CustomersIndividualBasicIndex(ID, RankingID, IndexID, Value, LevelID) values (1, 1, '001001', '40', 40)
Insert into CustomersIndividualBasicIndex(ID, RankingID, IndexID, Value, LevelID) values (2, 1, '001002', '60', 80)
Insert into CustomersIndividualBasicIndex(ID, RankingID, IndexID, Value, LevelID) values (3, 1, '002001', N'Từ 0 đến 50', 40)
Insert into CustomersIndividualBasicIndex(ID, RankingID, IndexID, Value, LevelID) values (4, 1, '002002', N'Từ 50 đến 100', 80)
-- Khách hàng số 2
Insert into CustomersIndividualBasicIndex(ID, RankingID, IndexID, Value, LevelID) values (5, 2, '001001', '40', 60)
Insert into CustomersIndividualBasicIndex(ID, RankingID, IndexID, Value, LevelID) values (6, 2, '001002', '60', 100)
Insert into CustomersIndividualBasicIndex(ID, RankingID, IndexID, Value, LevelID) values (7, 2, '002001', N'Từ 0 đến 50', 60)
Insert into CustomersIndividualBasicIndex(ID, RankingID, IndexID, Value, LevelID) values (8, 2, '002002', N'Từ 50 đến 100', 100)
-- Khách hàng số 3
Insert into CustomersIndividualBasicIndex(ID, RankingID, IndexID, Value, LevelID) values (9, 3, '002001', N'Từ 0 đến 50', 60)
Insert into CustomersIndividualBasicIndex(ID, RankingID, IndexID, Value, LevelID) values (10, 3, '002002', N'Từ 50 đến 100', 100)
SET Identity_insert CustomersIndividualBasicIndex off

-- Điểm tài sản đảm bảo
SET Identity_insert CustomersIndividualCollateralIndex on
-- Khách hàng số 1
Insert into CustomersIndividualCollateralIndex(ID, RankingID, IndexID, Value, LevelID) values (1, 1, '001001', '40', 40)
Insert into CustomersIndividualCollateralIndex(ID, RankingID, IndexID, Value, LevelID) values (2, 1, '001002', '60', 80)
Insert into CustomersIndividualCollateralIndex(ID, RankingID, IndexID, Value, LevelID) values (3, 1, '002001', N'Từ 0 đến 50', 40)
Insert into CustomersIndividualCollateralIndex(ID, RankingID, IndexID, Value, LevelID) values (4, 1, '002002', N'Từ 50 đến 100', 80)
-- Khách hàng số 2
Insert into CustomersIndividualCollateralIndex(ID, RankingID, IndexID, Value, LevelID) values (5, 2, '001001', '40', 40)
Insert into CustomersIndividualCollateralIndex(ID, RankingID, IndexID, Value, LevelID) values (6, 2, '001002', '60', 80)
Insert into CustomersIndividualCollateralIndex(ID, RankingID, IndexID, Value, LevelID) values (7, 2, '002001', N'Từ 0 đến 50', 40)
Insert into CustomersIndividualCollateralIndex(ID, RankingID, IndexID, Value, LevelID) values (8, 2, '002002', N'Từ 50 đến 100', 80)
-- Khách hàng số 3
Insert into CustomersIndividualCollateralIndex(ID, RankingID, IndexID, Value, LevelID) values (9, 3, '001001', '40', 40)
Insert into CustomersIndividualCollateralIndex(ID, RankingID, IndexID, Value, LevelID) values (10, 3, '002002', N'Từ 50 đến 100', 80)
SET Identity_insert CustomersIndividualCollateralIndex off