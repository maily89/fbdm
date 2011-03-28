USE Banking


-------BEGIN: Declare constants-------
DECLARE @PERIOD INT				SET @PERIOD = 01
DECLARE @BRANCH INT				SET @BRANCH = 010
DECLARE @CIF INT				SET @CIF = 2
DECLARE @TYPE NVARCHAR(10)		SET @TYPE = 'LH01'
DECLARE @INDUSTRY INT			SET @INDUSTRY = 34
-------END: Declare constants-------


-------BEGIN: Select NFI input-------
SELECT a.KyBC, a.ChiNhanh, a.SoCIF, dn.TenKH, dn.LoaiHinhDN, a.Chitieu, ptc.Ten, a.Nganh, a.LuaChon, a.Mucdiem, a.Diem, a.Status
FROM DiemPhiTC as a, CtieuPhiTC as ptc, XepHangDN as dn
WHERE a.KyBC = @PERIOD and
	  a.ChiNhanh = @BRANCH and
	  a.SoCIF = @CIF and
	  a.Chitieu = ptc.Maso and
	  (a.KyBC = dn.KyBC and a.ChiNhanh = dn.ChiNhanh and a.SoCIF = dn.SoCIF)
-------END: Select NFI input-------


-------BEGIN: Select FI input-------
SELECT b.KyBC, b.ChiNhanh, b.SoCIF, b.Chitieu, tc.Ten, b.Nganh, b.LuaChon, b.Mucdiem, b.Diem
FROM DiemTC as b, CtieuTC as tc
WHERE b.KyBC= @PERIOD and 
	  b.ChiNhanh = @BRANCH and
	  b.SoCIF = @CIF and
	  b.Chitieu = tc.Maso
-------END: Select FI input-------


-------BEGIN: Select NFI Proportion-------
SELECT * FROM TiTrongLHDN
WHERE LHDN = @TYPE
-------END: Select NFI Proportion-------


-------BEGIN: Select FI Proportion-------
SELECT * FROM CtieuTCNganhTD
WHERE Nganh = @INDUSTRY
-------END: Select FI Proportion-------


-------BEGIN: -------

-------END: -------


-------BEGIN: -------

-------END: -------