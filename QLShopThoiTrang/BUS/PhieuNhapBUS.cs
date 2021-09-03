using QLShopThoiTrang.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLShopThoiTrang.BUS
{
    public class PhieuNhapBUS
    {
        PhieuNhapDAL pDAL = new PhieuNhapDAL();
        public IEnumerable<object> LayDanhSachSP()
        {
            return pDAL.LayDanhSachSP();
        }
        public IEnumerable<object> LayDanhSachNhapKho()
        {
            return pDAL.LayDanhSachNhapKho();
        }
        public IEnumerable<object> DSHDNhap()
        {
            return pDAL.DSHDNhap();
        }
        public IEnumerable<object> DSCTHDNhap()
        {
            return pDAL.DSCTHDNhap();
        }
        public List<NhapKho> DSNhapKho()
        {
            return pDAL.DSNhapKho();
        }
        public IEnumerable<object> TimHDNhap(string madonnhap)
        {
            return pDAL.TimHDNhap(madonnhap);
        }

        public IEnumerable<object> TimCTHDNhap(string madonnhap)
        {
            return pDAL.TimCTHDNhap(madonnhap);
        }
        public double reloadTotalPrice()
        {
            return pDAL.reloadTotalPrice();
        }
        public void AddHDNhap(NhapHang n)
        {
            pDAL.AddHDNhap(n);
        }
        public void AddCTHDNhap(ChiTietNhap ctn)
        {
            pDAL.AddCTHDNhap(ctn);
        }
        public void AddNhapKho(NhapKho nk)
        {
            pDAL.AddNhapKho(nk);
        }
        public void DeleteNhapKho(string masp)
        {
            pDAL.DeleteNhapKho(masp);
        }
        public void EditPriceAndAmount(NhapKho nk)
        {
            pDAL.EditPriceAndAmount(nk);
        }
        public string SinhMaPhieuNhap()
        {
            return pDAL.SinhMaPhieuNhap();
        }
        public void DeleteAllNhapKho()
        {
            pDAL.DeleteAllNhapKho();
        }
        public string TKTheoNgay(string Bang1, string Bang2, string Ma, string ngay, DateTimePicker d, string cot)
        {
            return pDAL.TKTheoNgay(Bang1, Bang2, Ma, ngay, d, cot);
        }
        public DataTable HDTheoNgay(string Bang1, string Bang2, string Ma, string ngay, DateTimePicker d)
        {
            return pDAL.HDTheoNgay(Bang1, Bang2, Ma, ngay, d);
        }
        public string TKTheoThang(string Bang1, string Bang2, string Ma, string ngay, string thang, string nam, string cot)
        {
            return pDAL.TKTheoThang(Bang1, Bang2, Ma, ngay, thang, nam, cot);
        }
        public DataTable HDTheoThang(string Bang1, string Bang2, string Ma, string ngay, string thang, string nam)
        {
            return pDAL.HDTheoThang(Bang1, Bang2, Ma, ngay, thang, nam);
        }
        public string TKTheoNam(string Bang1, string Bang2, string Ma, string ngay, string nam, string cot)
        {
            return pDAL.TKTheoNam(Bang1, Bang2, Ma, ngay, nam, cot);
        }
        public DataTable HDTheoNam(string Bang1, string Bang2, string Ma, string ngay, string nam)
        {
            return pDAL.HDTheoNam(Bang1, Bang2, Ma, ngay, nam);
        }
    }
}
