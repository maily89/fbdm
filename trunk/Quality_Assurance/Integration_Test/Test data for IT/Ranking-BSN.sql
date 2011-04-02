USE Banking


-------BEGIN: Declare constants-------
DECLARE @PERIOD INT				SET @PERIOD = 01
DECLARE @BRANCH INT				SET @BRANCH = 040
DECLARE @CIF INT				SET @CIF = 3
-------END: Declare constants-------


-------BEGIN: Select Score infor-------
SELECT x.TenKH, x.Nganh, x.DiemQuyMo, 
       x.DiemTC, x.TytrongTC, x.DiemPhiTC, 
       x.TytrongPhiTC, x.XepHang, t.XepLoai, 
	   x.DiemTC_tytrong+ x.DiemPhiTC_tytrong as Summary
FROM XepHangDN as x, ThangDiem as t
WHERE x.KyBC = @Period and
	  x.ChiNhanh = @Branch and
	  x.SoCIF = @CIF and
	  x.XepHang = t.maso
-------END: Select Score infor-------


-------BEGIN: Select NFI input-------
SELECT a.KyBC, a.ChiNhanh, a.SoCIF, dn.TenKH, dn.LoaiHinhDN, a.Nganh, a.Chitieu, ptc.Ten, a.LuaChon, a.Mucdiem, p.TyleTD,
		a.Mucdiem*p.TyleTD as Calculated
FROM DiemPhiTC as a, CtieuPhiTC as ptc, XepHangDN as dn, CTieuPhiTCNganhLHDN as p
WHERE a.KyBC = @PERIOD and
	  a.ChiNhanh = @BRANCH and
	  a.SoCIF = @CIF and
	  a.Chitieu = ptc.Maso and
	  (a.KyBC = dn.KyBC and a.ChiNhanh = dn.ChiNhanh and a.SoCIF = dn.SoCIF) and
	  (p.Nganh = a.Nganh and p.LHDN = dn.LoaiHinhDN and p.Chitieu = a.Chitieu)
-------END: Select NFI input-------


-------BEGIN: Select FI input-------
SELECT b.KyBC, b.ChiNhanh, b.SoCIF, b.Chitieu, tc.Ten, b.Nganh, b.LuaChon, b.Mucdiem, p.TyleTD,
		b.Mucdiem*p.TyleTD as Calculated
FROM DiemTC as b, CtieuTC as tc, CtieuTCNganhTD as p
WHERE b.KyBC= @PERIOD and 
	  b.ChiNhanh = @BRANCH and
	  b.SoCIF = @CIF and
	  b.Chitieu = tc.Maso and
	  (p.Nganh = b.Nganh and p.Chitieu = b.Chitieu)
-------END: Select FI input-------


-------BEGIN: -------

-------END: -------