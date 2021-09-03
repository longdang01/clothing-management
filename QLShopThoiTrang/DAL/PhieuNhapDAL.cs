using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLShopThoiTrang.DAL
{
    public class PhieuNhapDAL
    {
        QLShopThoiTrangEntities dtct = new QLShopThoiTrangEntities();
        DataHelper dh = new DataHelper(@"Data Source=LD\SQLEXPRESS;Initial Catalog=QLShopThoiTrang;Integrated Security=True");
        public IEnumerable<object> LayDanhSachSP()
        {
            List<SanPham> l = dtct.SanPhams.SqlQuery("SELECT * FROM SanPham").ToList<SanPham>();

            var list = from x in l
                       select new { x.MaSP, x.TenSP, x.ChatLieu, x.MoTa, x.MaLoai};
            return list.ToList();
        }
        public IEnumerable<object> LayDanhSachNhapKho()
        {
            //List<NhapKho> l = dtct.NhapKhoes.SqlQuery("select MaSP, COUNT(MaSP) AS [Số lượng], DonGia from NhapKho GROUP BY MaSP, DonGia").ToList<NhapKho>();
            List<NhapKho> l = dtct.NhapKhoes.SqlQuery("select * from NhapKho").ToList<NhapKho>();

            var list = from x in l
                       select new { x.MaSP, x.SoLuong, x.DonGia };
            return list.ToList();
        }
        public IEnumerable<object> DSHDNhap()
        {
            List<NhapHang> l = dtct.NhapHangs.SqlQuery($"SELECT * FROM NhapHang").ToList<NhapHang>();

            var list = from x in l
                       select new { x.MaDonNhap, x.MaNCC, x.MaNV, x.NgayNhap };
            return list.ToList();
        }
        public IEnumerable<object> DSCTHDNhap()
        {
            List<ChiTietNhap> l = dtct.ChiTietNhaps.SqlQuery($"SELECT * FROM ChiTietNhap").ToList<ChiTietNhap>();

            var list = from x in l
                       select new { x.MaDonNhap, x.MaSP, x.SoLuong, x.DonGia };
            return list.ToList();
        }
        public List<NhapKho> DSNhapKho()
        {
            return dtct.NhapKhoes.SqlQuery("SELECT * FROM NhapKho").ToList<NhapKho>();
        }
        public IEnumerable<object> TimHDNhap(string madonnhap)
        {
            List<NhapHang> l = dtct.NhapHangs.SqlQuery($"SELECT * FROM NhapHang WHERE MaDonNhap = '{madonnhap}'").ToList<NhapHang>();

            var list = from x in l
                       select new { x.MaDonNhap, x.MaNCC, x.MaNV, x.NgayNhap};
            return list.ToList();
        }

        public IEnumerable<object> TimCTHDNhap(string madonnhap)
        {
            List<ChiTietNhap> l = dtct.ChiTietNhaps.SqlQuery($"SELECT * FROM ChiTietNhap WHERE MaDonNhap = '{madonnhap}'").ToList<ChiTietNhap>();

            var list = from x in l
                       select new { x.MaDonNhap, x.MaSP, x.SoLuong, x.DonGia };
            return list.ToList();
        }
        public double reloadTotalPrice()
        {
            double totalPrice = 0;
            foreach (NhapKho nk in DSNhapKho())
            {
                totalPrice += (nk.SoLuong * nk.DonGia);
            }
            return totalPrice;
        }
        public void AddNhapKho(NhapKho nk)
        {
            NhapKho n = dtct.NhapKhoes.Find(nk.MaSP);
            if(n != null)
            {
                n.SoLuong = n.SoLuong + 1;
            } else
            {
                dtct.NhapKhoes.Add(nk);
            }
            //dtct.themVaoPhanNhapHang(nk.MaSP, nk.SoLuong, nk.DonGia);
            dtct.SaveChanges();
        }
        public void AddHDNhap(NhapHang n)
        {
            dtct.NhapHangs.Add(n);

            dtct.SaveChanges();
        }
        public void AddCTHDNhap(ChiTietNhap ctn)
        {
            dtct.ChiTietNhaps.Add(ctn);

            dtct.SaveChanges();
        }
        public void DeleteNhapKho(string masp)
        {
            NhapKho n = dtct.NhapKhoes.Find(masp);
            dtct.NhapKhoes.Remove(n);

            dtct.SaveChanges();
        }
        public void DeleteAllNhapKho()
        {
            var all = from c in dtct.NhapKhoes select c;
            dtct.NhapKhoes.RemoveRange(all);
            dtct.SaveChanges();
        }
        public void EditPriceAndAmount(NhapKho nk)
        {
            NhapKho n = dtct.NhapKhoes.Find(nk.MaSP);

            if (n != null)
            {
                n.SoLuong = nk.SoLuong;
                n.DonGia = nk.DonGia;

                dtct.SaveChanges();
            }
        }
        public string SinhMaPhieuNhap()
        {
            return dh.MaKeTiep(dh.LayMaCuoi("NhapHang", "MaDonNhap"), "DN");
        }
        //Doanh thu theo ngày
        public string TKTheoNgay(string Bang1, string Bang2, string Ma, string ngay, DateTimePicker d, string cot)
        {
            string a = dh.TKTheoNgay(Bang1, Bang2, Ma, ngay, d, cot);
            string result = (a == null || a == "") ? "0" : a;
            return result;
        }
        public DataTable HDTheoNgay(string Bang1, string Bang2, string Ma, string ngay, DateTimePicker d)
        {
            return dh.HDTheoNgay(Bang1, Bang2, Ma, ngay, d);
        }
        //Theo Tháng
        public string TKTheoThang(string Bang1, string Bang2, string Ma, string ngay, string thang, string nam, string cot)
        {
            string a = dh.TKTheoThang(Bang1, Bang2, Ma, ngay, thang, nam, cot);
            string result = (a == null || a == "") ? "0" : a;
            return result;
        }
        public DataTable HDTheoThang(string Bang1, string Bang2, string Ma, string ngay, string thang, string nam)
        {
            return dh.HDTheoThang(Bang1, Bang2, Ma, ngay, thang, nam);
        }
        //Theo năm
        public string TKTheoNam(string Bang1, string Bang2, string Ma, string ngay, string nam, string cot)
        {
            string a = dh.TKTheoNam(Bang1, Bang2, Ma, ngay, nam, cot);
            string result = (a == null || a == "") ? "0" : a;
            return result;
        }
        public DataTable HDTheoNam(string Bang1, string Bang2, string Ma, string ngay, string nam)
        {
            return dh.HDTheoNam(Bang1, Bang2, Ma, ngay, nam);
        }
    }
}
