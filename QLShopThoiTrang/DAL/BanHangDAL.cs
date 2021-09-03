using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Data.Entity.Core.Objects;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLShopThoiTrang.DAL
{
    public class BanHangDAL
    {
        QLShopThoiTrangEntities dtct = new QLShopThoiTrangEntities();
        DataHelper dh = new DataHelper(@"Data Source=LD\SQLEXPRESS;Initial Catalog=QLShopThoiTrang;Integrated Security=True");

        public IEnumerable<object> LayDSSPTrongKho()
        {
            List<SanPham> l = dtct.SanPhams.SqlQuery("select * from SanPham where MaSP IN( select masp from ChiTietNhap group by MaSP)").ToList<SanPham>();

            var list = from x in l
                       select new { x.MaSP, x.TenSP, x.ChatLieu, x.MoTa, x.MaLoai };
            return list.ToList();
        }
        public IEnumerable<object> LayDanhSachGioHang()
        {
            List<GioHang> l = dtct.GioHangs.SqlQuery("select * from GioHang").ToList<GioHang>();

            var list = from x in l
                       select new { x.MaSP, x.SoLuong, x.DonGia };
            return list.ToList();
        }
        public List<GioHang> DSGioHang()
        {
            return dtct.GioHangs.SqlQuery("SELECT * FROM GioHang").ToList<GioHang>();
        }
        public IEnumerable<object> DSHDBan()
        {
            List<BanHang> l = dtct.BanHangs.SqlQuery($"SELECT * FROM BanHang").ToList<BanHang>();

            var list = from x in l
                       select new { x.MaDonBan, x.MaKH, x.MaNV, x.NgayBan };
            return list.ToList();
        }
        public IEnumerable<object> DSCTHDBan()
        {
            List<ChiTietBan> l = dtct.ChiTietBans.SqlQuery($"SELECT * FROM ChiTietBan").ToList<ChiTietBan>();

            var list = from x in l
                       select new { x.MaDonBan, x.MaSP, x.SoLuong, x.DonGia};
            return list.ToList();
        }
        public IEnumerable<object> TimHDBan(string madonban)
        {
            List<BanHang> l = dtct.BanHangs.SqlQuery($"SELECT * FROM BanHang WHERE MaDonBan = '{madonban}'").ToList<BanHang>();

            var list = from x in l
                       select new { x.MaDonBan, x.MaKH, x.MaNV, x.NgayBan};
            return list.ToList();
        }

        public IEnumerable<object> TimCTHDBan(string madonban)
        {
            List<ChiTietBan> l = dtct.ChiTietBans.SqlQuery($"SELECT * FROM ChiTietBan WHERE MaDonBan = '{madonban}'").ToList<ChiTietBan>();

            var list = from x in l
                       select new { x.MaDonBan, x.MaSP, x.SoLuong, x.DonGia };
            return list.ToList();
        }
        public string LayDonGia(string masp, string cot)
        {
            return dh.LayDonGia(masp, cot);
        }
        public string sumNhap(string masp, string cot)
        {
            return dh.sumNhap(masp, cot);
        }
        public string sumBan(string masp, string cot)
        {
            return dh.sumBan(masp, cot);
        }
        public double reloadTotalPrice()
        {
            double totalPrice = 0;
            foreach (GioHang g in DSGioHang())
            {
                totalPrice += (g.SoLuong * g.DonGia);
            }
            return totalPrice;
        }
        public void AddGioHang(GioHang gh)
        {
           GioHang g = dtct.GioHangs.Find(gh.MaSP);
            if (g != null)
            {
                if(g.SoLuong < LayTonKho(g.MaSP))
                {
                    g.SoLuong = g.SoLuong + 1;
                }else
                {
                    MessageBox.Show("Sản phẩm đã hết hàng");
                }
                //dtct.Entry(g).State = EntityState.Modified;
            }
            else 
            {
                dtct.GioHangs.Add(gh);
            }
            dtct.SaveChanges();
        }
        public void EditGioHang(GioHang gh)
        {
            GioHang g = dtct.GioHangs.Find(gh.MaSP);

            if (g != null)
            {
                g.SoLuong = gh.SoLuong;
                g.DonGia = gh.DonGia;

                dtct.SaveChanges();
            }
        }
        public void DeleteGioHang(string masp)
        {
            GioHang n = dtct.GioHangs.Find(masp);
            dtct.GioHangs.Remove(n);
            
            dtct.SaveChanges();
        }
        public void DeleteAllGioHang()
        {
            var all = from c in dtct.GioHangs select c;
            dtct.GioHangs.RemoveRange(all);
            dtct.SaveChanges();
        }
        public string SinhMaDonBan()
        {
            return dh.MaKeTiep(dh.LayMaCuoi("BanHang", "MaDonBan"), "DB");
        }
        public string SinhMaKH()
        {
            return dh.MaKeTiep(dh.LayMaCuoi("KhachHang", "MaKH"), "KH");
        }
        public IEnumerable<object> TimSPDeBan(string masp)
        {
            List<SanPham> l = dtct.SanPhams.SqlQuery($"select s.MaSP, s.TenSP, s.ChatLieu, s.MoTa, s.MaLoai FROM ChiTietNhap ctn LEFT JOIN SanPham s ON s.MaSP = ctn.MaSP WHERE ctn.MaSP = ${masp} GROUP BY s.MaSP, s.TenSP, s.ChatLieu, s.MoTa, s.MaLoai").ToList<SanPham>();

            var list = from x in l
                       select new { x.MaSP, x.TenSP, x.ChatLieu, x.MoTa
                       , x.MaLoai};
            return list.ToList();
        }
        public void AddKH(KhachHang kh)
        {
            dtct.KhachHangs.Add(kh);

            dtct.SaveChanges();
        }
        public void AddHDBan(BanHang b)
        {
            dtct.BanHangs.Add(b);

            dtct.SaveChanges();
        }
        public void AddCTHDBan(ChiTietBan ctb)
        {
            dtct.ChiTietBans.Add(ctb);

            dtct.SaveChanges();
        }
        //Danh sách sản phẩm
        public DataTable TKDSSP()
        {
            string query = "SELECT * FROM SanPham where MaSP IN (Select MaSP from ChiTietNhap)";
            DataTable dt = dh.LayDatatable(query);
            DataColumn nhap = new DataColumn("SoLuongNhap");
            DataColumn ban = new DataColumn("SoLuongBan");
            DataColumn ton = new DataColumn("SoLuongTon");
            dt.Columns.Add(nhap);
            dt.Columns.Add(ban);
            dt.Columns.Add(ton);
            int soluongton = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string a = sumNhap(dt.Rows[i]["MaSP"].ToString() ,"TongSoLuongNhap");
                string b = sumBan(dt.Rows[i]["MaSP"].ToString(), "TongSoLuongBan");
                int slnhap, slxuat;

                slnhap = (a == null || a == "") ? 0 : int.Parse(a);
                slxuat = (b == null || b == "") ? 0 : int.Parse(b);


                soluongton = slnhap - slxuat;
                dt.Rows[i]["SoLuongNhap"] = slnhap;
                dt.Rows[i]["SoLuongBan"] = slxuat;
                dt.Rows[i]["SoLuongTon"] = soluongton;
            }
            return dt;
        }
        //Bán chạy
        public DataTable LaySPBanChayNhat()
        {
            DataTable dt = TKDSSP().Clone();
            dt.Columns["SoLuongBan"].DataType = Type.GetType("System.Int32");

            foreach (DataRow dr in TKDSSP().Rows)
            {
                if(dr["SoLuongBan"].ToString() != "0")
                {
                    dt.Rows.Add(dr.ItemArray);
                }
            }
            dt.AcceptChanges();
            DataView dv = dt.DefaultView;
            dv.Sort = "SoLuongBan desc";
            DataTable sortedDT = dv.ToTable();

            return sortedDT;
        }
        //Chưa Bán được
        public DataTable LaySPChuaBanDuoc()
        {
            DataTable dt = TKDSSP().Clone();

            foreach (DataRow dr in TKDSSP().Rows)
            {
                if (dr["SoLuongBan"].ToString() == "0")
                {
                    dt.Rows.Add(dr.ItemArray);
                }
            }
            dt.AcceptChanges();

            return dt;
        }
        //Tồn kho
        public int LayTonKho(string masp)
        {
            string query = "SELECT * FROM SanPham where MaSP IN (Select MaSP from ChiTietNhap)";
            DataTable dt = dh.LayDatatable(query);
            //DataColumn nhap = new DataColumn("nhap");
            //DataColumn ban = new DataColumn("ban");
            DataColumn ton = new DataColumn("ton");
            //dt.Columns.Add(nhap);
            //dt.Columns.Add(ban);
            dt.Columns.Add(ton);
            int soluongton = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string a = sumNhap(masp, "TongSoLuongNhap");
                string b = sumBan(masp, "TongSoLuongBan");
                int slnhap, slxuat;
              
                slnhap = (a == null || a == "") ? 0 : int.Parse(a);
                slxuat = (b == null || b == "") ? 0 : int.Parse(b);
               
             
                soluongton = slnhap - slxuat;
                //dt.Rows[i]["nhap"] = slnhap;
                //dt.Rows[i]["ban"] = slxuat;
                dt.Rows[i]["ton"] = soluongton;
            }
            return soluongton;
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
