USE [D:\PROJECTS\FBD_GRADUATING_PROJECT\SVN_FBD\SOURCES\SOURCE_CODES\FBDSOURCE\FBD\APP_DATA\FBD.MDF]
GO

--Delete from SystemBranches where BranchID = '020'
--Delete from SystemReportingPeriods 
--Delete from SystemCustomerTypes
--Delete from CustomersLoanTerm
--Delete from SystemUserGroups where GroupID = 'CRD'
--Delete from SystemUsers where UserID = 'LONGNT'
--Delete from BusinessIndustries
--Delete from BusinessTypes
--Delete from BusinessRanks
--Delete from BusinessScales
--Delete from BusinessScaleCriteria
--Delete from BusinessFinancialIndex
--Delete from BusinessFinancialIndexLevels
--Delete from BusinessFinancialIndexScore
--Delete from BusinessFinancialIndexProportion
--Delete from BusinessNonFinancialIndex
--Delete from BusinessNonFinancialIndexLevels
--Delete from BusinessNonFinancialIndexScore
--Delete from BusinessNFIProportionByType
--Delete from BusinessNFIProportionByIndustry
--Delete from BusinessNFIProportionCalculated
--Delete from CustomersBusinesses
--Delete from CustomersBusinessRanking
--Delete from CustomersBusinessScale
--Delete from CustomersBusinessFinancialIndex
--Delete from CustomersBusinessNonFinancialIndex


-- Chi nhánh
Insert into SystemBranches values ('020', N'Chi nhánh 2', 1)

-- Kỳ báo cáo
Insert into SystemReportingPeriods values ('01', N'Kỳ báo cáo số 1', '04-15-2011', '05-15-2011', 1)
Insert into SystemReportingPeriods values ('02', N'Kỳ báo cáo số 2', '05-15-2011', '06-15-2011', 1)

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

-- Ngành nghề doanh nghiệp
Insert into BusinessIndustries values ('01', N'Ngành 01')
Insert into BusinessIndustries values ('02', N'Ngành 02')

-- Loại hình doanh nghiệp
Insert into BusinessTypes values ('LH01', N'Loại hình 01')
Insert into BusinessTypes values ('LH02', N'Loại hình 02')

-- Bảng chỉ tiêu đánh giá doanh nghiệp
Insert into BusinessRanks values ('01', 90, 100, 'AAA', N'Rất tốt', Null, Null)
Insert into BusinessRanks values ('02', 80, 90, 'AA', N'Khá tốt', Null, Null)
Insert into BusinessRanks values ('03', 70, 80, 'A', N'Tốt', Null, Null)
Insert into BusinessRanks values ('04', 60, 70, 'BBB', N'Rất khá', Null, Null)
Insert into BusinessRanks values ('05', 50, 60, 'BB', N'Được', Null, Null)
Insert into BusinessRanks values ('06', 40, 50, 'B', N'Khá', Null, Null)
Insert into BusinessRanks values ('07', 30, 40, 'CCC', N'Bình thường', Null, Null)
Insert into BusinessRanks values ('08', 20, 30, 'CC', N'Hơi kém', Null, Null)
Insert into BusinessRanks values ('09', 10, 20, 'C', N'Kém', Null, Null)
Insert into BusinessRanks values ('10', 0, 10, 'D', N'Rất kém', Null, Null)

-- Chỉ tiêu quy mô
Insert into BusinessScales values ('L', 70, 100, N'Lớn')
Insert into BusinessScales values ('M', 40, 70, N'Vừa')
Insert into BusinessScales values ('S', 0, 40, N'nhỏ')

Insert into BusinessScaleCriteria values ('01', N'Chỉ tiêu 01', null, null, 'N')
Insert into BusinessScaleCriteria values ('02', N'Chỉ tiêu 02', null, null, 'N')

SET Identity_insert BusinessScaleScore on
Insert into BusinessScaleScore(ScoreID, CriteriaID, IndustryID, FromValue, ToValue, Score) values (1, '01', '01', 0, 50, 40)
Insert into BusinessScaleScore(ScoreID, CriteriaID, IndustryID, FromValue, ToValue, Score) values (2, '01', '01', 50, 100, 80)
Insert into BusinessScaleScore(ScoreID, CriteriaID, IndustryID, FromValue, ToValue, Score) values (3, '02', '01', 0, 50, 40)
Insert into BusinessScaleScore(ScoreID, CriteriaID, IndustryID, FromValue, ToValue, Score) values (4, '02', '01', 50, 100, 80)
Insert into BusinessScaleScore(ScoreID, CriteriaID, IndustryID, FromValue, ToValue, Score) values (5, '01', '02', 0, 50, 40)
Insert into BusinessScaleScore(ScoreID, CriteriaID, IndustryID, FromValue, ToValue, Score) values (6, '01', '02', 50, 100, 80)
Insert into BusinessScaleScore(ScoreID, CriteriaID, IndustryID, FromValue, ToValue, Score) values (7, '02', '02', 0, 50, 40)
Insert into BusinessScaleScore(ScoreID, CriteriaID, IndustryID, FromValue, ToValue, Score) values (8, '02', '02', 50, 100, 80)
SET Identity_insert BusinessScaleScore off

-- Chỉ tiêu tài chính
Insert into BusinessFinancialIndex values ('001', N'Chỉ tiêu 001', null, null, 'N', 0)
Insert into BusinessFinancialIndex values ('001001', N'Chỉ tiêu 001001', null, null, 'N', 1)
Insert into BusinessFinancialIndex values ('001002', N'Chỉ tiêu 001002', null, null, 'N', 1)
Insert into BusinessFinancialIndex values ('002', N'Chỉ tiêu 002', null, null, 'N', 0)
Insert into BusinessFinancialIndex values ('002001', N'Chỉ tiêu 002001', null, null, 'N', 1)
Insert into BusinessFinancialIndex values ('002002', N'Chỉ tiêu 002002', null, null, 'N', 1)

Insert into BusinessFinancialIndexLevels values (20, 20)
Insert into BusinessFinancialIndexLevels values (40, 40)
Insert into BusinessFinancialIndexLevels values (60, 60)
Insert into BusinessFinancialIndexLevels values (80, 80)
Insert into BusinessFinancialIndexLevels values (100, 100)

SET Identity_insert BusinessFinancialIndexScore on
Insert into BusinessFinancialIndexScore(ScoreID, IndexID, IndustryID, ScaleID, FromValue, ToValue, FixedValue, LevelID) 
values (1, '001001', '01', 'L', 0, 50, Null, 40)
Insert into BusinessFinancialIndexScore(ScoreID, IndexID, IndustryID, ScaleID, FromValue, ToValue, FixedValue, LevelID) 
values (2, '001001', '01', 'L', 50, 100, Null, 80)
Insert into BusinessFinancialIndexScore(ScoreID, IndexID, IndustryID, ScaleID, FromValue, ToValue, FixedValue, LevelID) 
values (3, '001001', '02', 'L', 0, 50, Null, 60)
Insert into BusinessFinancialIndexScore(ScoreID, IndexID, IndustryID, ScaleID, FromValue, ToValue, FixedValue, LevelID)
values (4, '001001', '02', 'L', 50, 100, Null, 100)
Insert into BusinessFinancialIndexScore(ScoreID, IndexID, IndustryID, ScaleID, FromValue, ToValue, FixedValue, LevelID) 
values (5, '001002', '01', 'L', 0, 50, Null, 40)
Insert into BusinessFinancialIndexScore(ScoreID, IndexID, IndustryID, ScaleID, FromValue, ToValue, FixedValue, LevelID) 
values (6, '001002', '01', 'L', 50, 100, Null, 80)
Insert into BusinessFinancialIndexScore(ScoreID, IndexID, IndustryID, ScaleID, FromValue, ToValue, FixedValue, LevelID) 
values (7, '001002', '02', 'L', 0, 50, Null, 60)
Insert into BusinessFinancialIndexScore(ScoreID, IndexID, IndustryID, ScaleID, FromValue, ToValue, FixedValue, LevelID) 
values (8, '001002', '02', 'L', 50, 100, Null, 100)
Insert into BusinessFinancialIndexScore(ScoreID, IndexID, IndustryID, ScaleID, FromValue, ToValue, FixedValue, LevelID) 
values (9, '002001', '01', 'L', 0, 50, Null, 40)
Insert into BusinessFinancialIndexScore(ScoreID, IndexID, IndustryID, ScaleID, FromValue, ToValue, FixedValue, LevelID) 
values (10, '002001', '01', 'L', 50, 100, Null, 80)
Insert into BusinessFinancialIndexScore(ScoreID, IndexID, IndustryID, ScaleID, FromValue, ToValue, FixedValue, LevelID) 
values (11, '002001', '02', 'L', 0, 50, Null, 60)
Insert into BusinessFinancialIndexScore(ScoreID, IndexID, IndustryID, ScaleID, FromValue, ToValue, FixedValue, LevelID) 
values (12, '002001', '02', 'L', 50, 100, Null, 100)
Insert into BusinessFinancialIndexScore(ScoreID, IndexID, IndustryID, ScaleID, FromValue, ToValue, FixedValue, LevelID) 
values (13, '002002', '01', 'L', 0, 50, Null, 40)
Insert into BusinessFinancialIndexScore(ScoreID, IndexID, IndustryID, ScaleID, FromValue, ToValue, FixedValue, LevelID) 
values (14, '002002', '01', 'L', 50, 100, Null, 80)
Insert into BusinessFinancialIndexScore(ScoreID, IndexID, IndustryID, ScaleID, FromValue, ToValue, FixedValue, LevelID) 
values (15, '002002', '02', 'L', 0, 50, Null, 60)
Insert into BusinessFinancialIndexScore(ScoreID, IndexID, IndustryID, ScaleID, FromValue, ToValue, FixedValue, LevelID) 
values (16, '002002', '02', 'L', 50, 100, Null, 100)
SET Identity_insert BusinessFinancialIndexScore off

SET Identity_insert BusinessFinancialIndexProportion on
Insert into BusinessFinancialIndexProportion(ProportionID, IndustryID, IndexID, Proportion) values (1, '01', '001001', 10)
Insert into BusinessFinancialIndexProportion(ProportionID, IndustryID, IndexID, Proportion) values (2, '01', '001002', 20)
Insert into BusinessFinancialIndexProportion(ProportionID, IndustryID, IndexID, Proportion) values (3, '01', '002001', 30)
Insert into BusinessFinancialIndexProportion(ProportionID, IndustryID, IndexID, Proportion) values (4, '01', '002002', 40)
Insert into BusinessFinancialIndexProportion(ProportionID, IndustryID, IndexID, Proportion) values (5, '02', '001001', 10)
Insert into BusinessFinancialIndexProportion(ProportionID, IndustryID, IndexID, Proportion) values (6, '02', '001002', 20)
Insert into BusinessFinancialIndexProportion(ProportionID, IndustryID, IndexID, Proportion) values (7, '02', '002001', 30)
Insert into BusinessFinancialIndexProportion(ProportionID, IndustryID, IndexID, Proportion) values (8, '02', '002002', 40)
SET Identity_insert BusinessFinancialIndexProportion off

-- Chỉ tiêu phi tài chính
Insert into BusinessNonFinancialIndex values ('001', N'Chỉ tiêu 001', null, null, 'C', 0)
Insert into BusinessNonFinancialIndex values ('001001', N'Chỉ tiêu 001001', null, null, 'C', 1)
Insert into BusinessNonFinancialIndex values ('001002', N'Chỉ tiêu 001002', null, null, 'C', 1)
Insert into BusinessNonFinancialIndex values ('002', N'Chỉ tiêu 002', null, null, 'C', 0)
Insert into BusinessNonFinancialIndex values ('002001', N'Chỉ tiêu 002001', null, null, 'C', 1)
Insert into BusinessNonFinancialIndex values ('002002', N'Chỉ tiêu 002002', null, null, 'C', 1)

Insert into BusinessNonFinancialIndexLevels values (20, 20)
Insert into BusinessNonFinancialIndexLevels values (40, 40)
Insert into BusinessNonFinancialIndexLevels values (60, 60)
Insert into BusinessNonFinancialIndexLevels values (80, 80)
Insert into BusinessNonFinancialIndexLevels values (100, 100)

SET Identity_insert BusinessNonFinancialIndexScore on
Insert into BusinessNonFinancialIndexScore(ScoreID, IndexID, IndustryID, FromValue, ToValue, FixedValue, LevelID) 
values (1, '001001', '01', Null, Null, N'Từ 0 đến 50', 40)
Insert into BusinessNonFinancialIndexScore(ScoreID, IndexID, IndustryID, FromValue, ToValue, FixedValue, LevelID) 
values (2, '001001', '01', Null, Null, N'Từ 50 đến 100', 80)
Insert into BusinessNonFinancialIndexScore(ScoreID, IndexID, IndustryID, FromValue, ToValue, FixedValue, LevelID) 
values (3, '001001', '02', Null, Null, N'Từ 0 đến 50', 60)
Insert into BusinessNonFinancialIndexScore(ScoreID, IndexID, IndustryID, FromValue, ToValue, FixedValue, LevelID) 
values (4, '001001', '02', Null, Null, N'Từ 50 đến 100', 100)
Insert into BusinessNonFinancialIndexScore(ScoreID, IndexID, IndustryID, FromValue, ToValue, FixedValue, LevelID) 
values (5, '001002', '01', Null, Null, N'Từ 0 đến 50', 40)
Insert into BusinessNonFinancialIndexScore(ScoreID, IndexID, IndustryID, FromValue, ToValue, FixedValue, LevelID) 
values (6, '001002', '01', Null, Null, N'Từ 50 đến 100', 80)
Insert into BusinessNonFinancialIndexScore(ScoreID, IndexID, IndustryID, FromValue, ToValue, FixedValue, LevelID) 
values (7, '001002', '02', Null, Null, N'Từ 0 đến 50', 60)
Insert into BusinessNonFinancialIndexScore(ScoreID, IndexID, IndustryID, FromValue, ToValue, FixedValue, LevelID) 
values (8, '001002', '02', Null, Null, N'Từ 50 đến 100', 100)
Insert into BusinessNonFinancialIndexScore(ScoreID, IndexID, IndustryID, FromValue, ToValue, FixedValue, LevelID) 
values (9, '002001', '01', Null, Null, N'Từ 0 đến 50', 40)
Insert into BusinessNonFinancialIndexScore(ScoreID, IndexID, IndustryID, FromValue, ToValue, FixedValue, LevelID) 
values (10, '002001', '01', Null, Null, N'Từ 50 đến 100', 80)
Insert into BusinessNonFinancialIndexScore(ScoreID, IndexID, IndustryID, FromValue, ToValue, FixedValue, LevelID) 
values (11, '002001', '02', Null, Null, N'Từ 0 đến 50', 60)
Insert into BusinessNonFinancialIndexScore(ScoreID, IndexID, IndustryID, FromValue, ToValue, FixedValue, LevelID) 
values (12, '002001', '02', Null, Null, N'Từ 50 đến 100', 100)
Insert into BusinessNonFinancialIndexScore(ScoreID, IndexID, IndustryID, FromValue, ToValue, FixedValue, LevelID) 
values (13, '002002', '01', Null, Null, N'Từ 0 đến 50', 40)
Insert into BusinessNonFinancialIndexScore(ScoreID, IndexID, IndustryID, FromValue, ToValue, FixedValue, LevelID) 
values (14, '002002', '01', Null, Null, N'Từ 50 đến 100', 80)
Insert into BusinessNonFinancialIndexScore(ScoreID, IndexID, IndustryID, FromValue, ToValue, FixedValue, LevelID) 
values (15, '002002', '02', Null, Null, N'Từ 0 đến 50', 60)
Insert into BusinessNonFinancialIndexScore(ScoreID, IndexID, IndustryID, FromValue, ToValue, FixedValue, LevelID) 
values (16, '002002', '02', Null, Null, N'Từ 50 đến 100', 100)
SET Identity_insert BusinessNonFinancialIndexScore off

SET Identity_insert BusinessNFIProportionByType on
Insert into BusinessNFIProportionByType(ProportionID, IndexID, TypeID, Proportion) values (1, '001', 'LH01', 30)
Insert into BusinessNFIProportionByType(ProportionID, IndexID, TypeID, Proportion) values (2, '002', 'LH01', 70)
SET Identity_insert BusinessNFIProportionByType off

SET Identity_insert BusinessNFIProportionByIndustry on
Insert into BusinessNFIProportionByIndustry(ProportionID, IndexID, IndustryID, Proportion) values (1, '001001', '01', 50)
Insert into BusinessNFIProportionByIndustry(ProportionID, IndexID, IndustryID, Proportion) values (2, '001002', '01', 50)
Insert into BusinessNFIProportionByIndustry(ProportionID, IndexID, IndustryID, Proportion) values (3, '002001', '01', 50)
Insert into BusinessNFIProportionByIndustry(ProportionID, IndexID, IndustryID, Proportion) values (4, '002002', '01', 50)
Insert into BusinessNFIProportionByIndustry(ProportionID, IndexID, IndustryID, Proportion) values (5, '001001', '02', 50)
Insert into BusinessNFIProportionByIndustry(ProportionID, IndexID, IndustryID, Proportion) values (6, '001002', '02', 50)
Insert into BusinessNFIProportionByIndustry(ProportionID, IndexID, IndustryID, Proportion) values (7, '002001', '02', 50)
Insert into BusinessNFIProportionByIndustry(ProportionID, IndexID, IndustryID, Proportion) values (8, '002002', '02', 50)
SET Identity_insert BusinessNFIProportionByIndustry off

SET Identity_insert BusinessNFIProportionCalculated on
Insert into BusinessNFIProportionCalculated(ProportionID, IndexID, IndustryID, TypeID, Proportion) 
values (1, '001001', '01', 'LH01', 15)
Insert into BusinessNFIProportionCalculated(ProportionID, IndexID, IndustryID, TypeID, Proportion) 
values (2, '001002', '01', 'LH01', 15)
Insert into BusinessNFIProportionCalculated(ProportionID, IndexID, IndustryID, TypeID, Proportion) 
values (3, '002001', '01', 'LH01', 35)
Insert into BusinessNFIProportionCalculated(ProportionID, IndexID, IndustryID, TypeID, Proportion) 
values (4, '002002', '01', 'LH01', 35)
Insert into BusinessNFIProportionCalculated(ProportionID, IndexID, IndustryID, TypeID, Proportion) 
values (5, '001001', '02', 'LH01', 15)
Insert into BusinessNFIProportionCalculated(ProportionID, IndexID, IndustryID, TypeID, Proportion) 
values (6, '001002', '02', 'LH01', 15)
Insert into BusinessNFIProportionCalculated(ProportionID, IndexID, IndustryID, TypeID, Proportion) 
values (7, '002001', '02', 'LH01', 35)
Insert into BusinessNFIProportionCalculated(ProportionID, IndexID, IndustryID, TypeID, Proportion) 
values (8, '002002', '02', 'LH01', 35)
SET Identity_insert BusinessNFIProportionCalculated off

-- Danh sách khách hàng
SET Identity_insert CustomersBusinesses on
Insert into CustomersBusinesses(BusinessID, CIF, CustomerName, BranchID) values (1, '11111', 'BMS1', '010')
Insert into CustomersBusinesses(BusinessID, CIF, CustomerName, BranchID) values (2, '22222', 'BMS2', '010')
Insert into CustomersBusinesses(BusinessID, CIF, CustomerName, BranchID) values (3, '33333', 'BMS3', '020')
Insert into CustomersBusinesses(BusinessID, CIF, CustomerName, BranchID) values (4, '44444', 'BMS4', '020')
SET Identity_insert CustomersBusinesses off

-- Khách hàng được chấm điểm tín dụng
SET Identity_insert CustomersBusinessRanking on
-- Khách hàng số 1
Insert into CustomersBusinessRanking(ID,BusinessID,PeriodID,CreditDepartment,TaxCode,CustomerGroup,IndustryID,TypeID,AuditedStatus,TotalDebt,LoanTermID,CustomerTypeID,ScaleID,FinancialScore,NonFinancialScore,RankID,ClusterRankID,UserID,DateModified)
values (1, 1, '01', N'Phòng Doanh nghiệp', '111', '1', '01', 'LH01', '1', 111111111, '01', '01', 'L', 19.2, 42, '04', Null, 'LONGNT', '04-15-2011')
-- Khách hàng số 2
Insert into CustomersBusinessRanking(ID,BusinessID,PeriodID,CreditDepartment,TaxCode,CustomerGroup,IndustryID,TypeID,AuditedStatus,TotalDebt,LoanTermID,CustomerTypeID,ScaleID,FinancialScore,NonFinancialScore,RankID,ClusterRankID,UserID,DateModified)
values (2, 2, '01', N'Phòng Doanh nghiệp', '222', '2', '02', 'LH01', '0', 222222222, '02', '01', 'L', 33.6, 48, '02', Null, 'LONGNT', '04-15-2011')
SET Identity_insert CustomersBusinessRanking off

SET Identity_insert CustomersBusinessScale on
-- Khách hàng số 1
Insert into CustomersBusinessScale(ID, RankingID, CriteriaID, Value, Score) values (1, 1, '01', 40, 40)
Insert into CustomersBusinessScale(ID, RankingID, CriteriaID, Value, Score) values (2, 1, '02', 60, 80)
-- Khách hàng số 2
Insert into CustomersBusinessScale(ID, RankingID, CriteriaID, Value, Score) values (3, 2, '01', 40, 40)
Insert into CustomersBusinessScale(ID, RankingID, CriteriaID, Value, Score) values (4, 2, '02', 60, 80)
SET Identity_insert CustomersBusinessScale off

SET Identity_insert CustomersBusinessFinancialIndex on
-- Khách hàng số 1
Insert into CustomersBusinessFinancialIndex(ID, RankingID, IndexID, Value, LevelID) values (1, 1, '001001', '40', 40)
Insert into CustomersBusinessFinancialIndex(ID, RankingID, IndexID, Value, LevelID) values (2, 1, '001002', '60', 80)
Insert into CustomersBusinessFinancialIndex(ID, RankingID, IndexID, Value, LevelID) values (3, 1, '002001', '40', 40)
Insert into CustomersBusinessFinancialIndex(ID, RankingID, IndexID, Value, LevelID) values (4, 1, '002002', '60', 80)
-- Khách hàng số 2
Insert into CustomersBusinessFinancialIndex(ID, RankingID, IndexID, Value, LevelID) values (5, 2, '001001', '40', 60)
Insert into CustomersBusinessFinancialIndex(ID, RankingID, IndexID, Value, LevelID) values (6, 2, '001002', '60', 100)
Insert into CustomersBusinessFinancialIndex(ID, RankingID, IndexID, Value, LevelID) values (7, 2, '002001', '40', 60)
Insert into CustomersBusinessFinancialIndex(ID, RankingID, IndexID, Value, LevelID) values (8, 2, '002002', '60', 100)
SET Identity_insert CustomersBusinessFinancialIndex off

SET Identity_insert CustomersBusinessNonFinancialIndex on
-- Khách hàng số 1
Insert into CustomersBusinessNonFinancialIndex(ID, RankingID, IndexID, Value, LevelID) 
values (1, 1, '001001', N'Từ 0 đến 50', 40)
Insert into CustomersBusinessNonFinancialIndex(ID, RankingID, IndexID, Value, LevelID) 
values (2, 1, '001002', N'Từ 50 đến 100', 80)
Insert into CustomersBusinessNonFinancialIndex(ID, RankingID, IndexID, Value, LevelID) 
values (3, 1, '002001', N'Từ 0 đến 50', 40)
Insert into CustomersBusinessNonFinancialIndex(ID, RankingID, IndexID, Value, LevelID) 
values (4, 1, '002002', N'Từ 50 đến 100', 80)
-- Khách hàng số 2
Insert into CustomersBusinessNonFinancialIndex(ID, RankingID, IndexID, Value, LevelID) 
values (5, 2, '001001', N'Từ 0 đến 50', 60)
Insert into CustomersBusinessNonFinancialIndex(ID, RankingID, IndexID, Value, LevelID) 
values (6, 2, '001002', N'Từ 50 đến 100', 100)
Insert into CustomersBusinessNonFinancialIndex(ID, RankingID, IndexID, Value, LevelID) 
values (7, 2, '002001', N'Từ 0 đến 50', 60)
Insert into CustomersBusinessNonFinancialIndex(ID, RankingID, IndexID, Value, LevelID) 
values (8, 2, '002002', N'Từ 50 đến 100', 100)
SET Identity_insert CustomersBusinessNonFinancialIndex off
