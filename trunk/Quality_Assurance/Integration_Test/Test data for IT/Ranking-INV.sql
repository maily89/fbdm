USE Banking

DECLARE @BRANCH INT		SET @BRANCH = 170
DECLARE @INDUSTRY INT	SET @INDUSTRY = 01
DECLARE @CIF INT		SET @CIF = 3

SELECT x.ChiNhanh, x.Nganh,x.DiemThanNhan,x.DiemKNtraNo,
	   x.DiemThanNhan_TyTrong+x.DiemKNtraNo_TyTrong as Score,
	   x.DiemTSDB, x.XepLoaiTSDB, x.DanhGiaTSDB, 
       x.XepLoaiCaNHan, t.XepLoai, t.NhomRR, X.MaDanhGia
FROM XepHangCN as x, ThDiemXepLoaiCN as t
WHERE x.ChiNhanh = @BRANCH and SoCIF = @CIF and
		t.maso = x.XepLoaiCaNHan

SELECT d.ChiNhanh, d.Nganh, d.SoCIF, d.Chitieu, c.Ten, d.LuaChon, d.Mucdiem, 
	   p.Tyle, d.Mucdiem*p.Tyle as Calculated
FROM DiemCN as d, CtieuCNNganh as p, CtieuCN as c
WHERE ChiNhanh = @BRANCH and SoCIF = @CIF and
	  (p.Nganh = d.Nganh and p.ChiTieu = d.Chitieu) and
	  (c.maso = d.Chitieu)

SELECT d.ChiNhanh, d.Nganh, d.SoCIF, d.Chitieu, c.Ten, d.LuaChon, d.Mucdiem
FROM DiemTSDBCN as d, CtieuTSDBCN as c
WHERE d.Chinhanh = @BRANCH and 
	  d.Nganh = @INDUSTRY and 
	  d.SoCIF = @CIF and
	  (c.maso = d.Chitieu)