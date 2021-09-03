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
    public class BanHangBUS
    {
        BanHangDAL bDAL = new BanHangDAL();
        public IEnumerable<object> LayDSSPTrongKho()
        {
            return bDAL.LayDSSPTrongKho();
        }
        public IEnumerable<object> LayDanhSachGioHang()
        {
            return bDAL.LayDanhSachGioHang();
        }
        public IEnumerable<object> DSHDBan()
        {
            return bDAL.DSHDBan();
        }
        public IEnumerable<object> DSCTHDBan()
        {
            return bDAL.DSCTHDBan();
        }
        public IEnumerable<object> TimHDBan(string madonban)
        {
            return bDAL.TimHDBan(madonban);
        }

        public IEnumerable<object> TimCTHDBan(string madonban)
        {
            return bDAL.TimCTHDBan(madonban);
        }
        public List<GioHang> DSGioHang()
        {
            return bDAL.DSGioHang();
        }
        public void AddGioHang(GioHang gh)
        {
            bDAL.AddGioHang(gh);
        }
        public string LayDonGia(string masp, string cot)
        {
            return bDAL.LayDonGia(masp, cot);
        }
        public void EditGioHang(GioHang gh)
        {
            bDAL.EditGioHang(gh);
        }
        public void DeleteGioHang(string masp)
        {
            bDAL.DeleteGioHang(masp);
        }
        public void DeleteAllGioHang()
        {
            bDAL.DeleteAllGioHang();
        }
        public string SinhMaDonBan()
        {
            return bDAL.SinhMaDonBan();
        }
        public string SinhMaKH()
        {
            return bDAL.SinhMaKH();
        }
        public double reloadTotalPrice()
        {
            return bDAL.reloadTotalPrice();
        }
        public IEnumerable<object> TimSPDeBan(string masp)
        {
            return bDAL.TimSPDeBan(masp);
        }
        public void AddHDBan(BanHang b)
        {
            bDAL.AddHDBan(b); 
        }
        public void AddKH(KhachHang kh)
        {
            bDAL.AddKH(kh);
        }
        public void AddCTHDBan(ChiTietBan ctb)
        {
            bDAL.AddCTHDBan(ctb);
        }
        public int LayTonKho(string masp)
        {
            return bDAL.LayTonKho(masp);
        }
        public string TKTheoNgay(string Bang1, string Bang2, string Ma, string ngay, DateTimePicker d, string cot)
        {
            return bDAL.TKTheoNgay(Bang1, Bang2, Ma, ngay, d, cot);
        }
        public DataTable HDTheoNgay(string Bang1, string Bang2, string Ma, string ngay, DateTimePicker d)
        {
            return bDAL.HDTheoNgay(Bang1, Bang2, Ma, ngay, d);
        }
        public string TKTheoThang(string Bang1, string Bang2, string Ma, string ngay, string thang, string nam, string cot)
        {
            return bDAL.TKTheoThang(Bang1, Bang2, Ma, ngay, thang, nam, cot);
        }
        public DataTable HDTheoThang(string Bang1, string Bang2, string Ma, string ngay, string thang, string nam)
        {
            return bDAL.HDTheoThang(Bang1, Bang2, Ma, ngay, thang, nam);
        }
        public string TKTheoNam(string Bang1, string Bang2, string Ma, string ngay, string nam, string cot)
        {
            return bDAL.TKTheoNam(Bang1, Bang2, Ma, ngay, nam, cot);
        }
        public DataTable HDTheoNam(string Bang1, string Bang2, string Ma, string ngay, string nam)
        {
            return bDAL.HDTheoNam(Bang1, Bang2, Ma, ngay, nam);
        }
        //Danh sách sản phẩm
        public DataTable TKDSSP()
        {
            return bDAL.TKDSSP();
        }
        //Bán chạy
        public DataTable LaySPBanChayNhat()
        {
            return bDAL.LaySPBanChayNhat();
        }

        //Chưa bán được
        public DataTable LaySPChuaBanDuoc()
        {
            return bDAL.LaySPChuaBanDuoc();
        }
    }
}
