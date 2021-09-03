using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLShopThoiTrang.DAL
{
    public class NhanVienDAL
    {
        QLShopThoiTrangEntities dtct = new QLShopThoiTrangEntities();
        DataHelper dh = new DataHelper(@"Data Source=LD\SQLEXPRESS;Initial Catalog=QLShopThoiTrang;Integrated Security=True");
        public IEnumerable<object> LayDanhSachNV()
        {
            List<NhanVien> l = dtct.NhanViens.SqlQuery("select * from NhanVien where MaNV not in (select userID from Users where userRole = N'Quản lý')").ToList<NhanVien>();

            var list = from x in l
                       select new { x.MaNV, x.TenNV, x.SDT, x.DiaChi};
            return list.ToList();
        }
        //Tìm nhân viên theo họ tên
        public IEnumerable<object> LayNhanVienTheoTen(string hoten)
        {
            //List<NhanVien> l = dtct.NhanViens.SqlQuery("exec LayNhanVienTheoTen @TenNV", new SqlParameter("@TenNV", hoten)).ToList<NhanVien>();
            List<NhanVien> l = dtct.NhanViens.SqlQuery($"SELECT * FROM NhanVien WHERE TenNV LIKE '%{hoten}%'").ToList<NhanVien>();
            var list = from x in l
                       select new { x.MaNV, x.TenNV, x.SDT, x.DiaChi };
            return list.ToList();
        }
        public IEnumerable<object> TimNV(string manv)
        {
            List<NhanVien> l = dtct.NhanViens.SqlQuery($"select * from NhanVien where MaNV = '{manv}'").ToList<NhanVien>();

            var list = from x in l
                       select new { x.MaNV, x.TenNV, x.SDT, x.DiaChi };
            return list.ToList();
        }
        public IEnumerable<object> TimHDNhapCuaNV(string manv)
        {
            List<NhapHang> l = dtct.NhapHangs.SqlQuery($"select * from NhapHang where MaNV = '{manv}'").ToList<NhapHang>();

            var list = from x in l
                       select new { x.MaDonNhap, x.MaNCC, x.MaNV, x.NgayNhap };
            return list.ToList();
        }
        public IEnumerable<object> TimHDBanCuaNV(string manv)
        {
            List<BanHang> l = dtct.BanHangs.SqlQuery($"select * from BanHang where MaNV = '{manv}'").ToList<BanHang>();

            var list = from x in l
                       select new { x.MaDonBan, x.MaKH, x.MaNV, x.NgayBan };
            return list.ToList();
        }
        public User LayTK(string userID)
        {
            User u = dtct.Users.Find(userID);

            return u;
        }
        public void AddUser(User u)
        {
            dtct.Users.Add(u);

            dtct.SaveChanges();
        }

        public void Add(NhanVien nv)
        {
            dtct.NhanViens.Add(nv);

            dtct.SaveChanges();
        }
        public void Edit(NhanVien nv)
        {
            NhanVien n = dtct.NhanViens.Find(nv.MaNV);

            if (n != null)
            {
                n.TenNV = nv.TenNV;
                n.DiaChi = nv.DiaChi;
                n.SDT= nv.SDT;

                dtct.SaveChanges();
            }
        }
        public void Delete(string manv)
        {
            NhanVien n = dtct.NhanViens.Find(manv);
            dtct.NhanViens.Remove(n);

            dtct.SaveChanges();
        }
        public void DeleteUser(string userid)
        {
            User n = dtct.Users.Find(userid);
            dtct.Users.Remove(n);

            dtct.SaveChanges();
        }
        public string SinhMaNV()
        {
            return dh.MaKeTiep(dh.LayMaCuoiNV("NhanVien", "MaNV"), "NV");
        }
        public string SinhMaQL()
        {
            return dh.MaKeTiep(dh.LayMaCuoiQL("NhanVien", "MaNV"), "QL");
        }
        public DataTable TKNV(string sql)
        {
            DataTable dt = dh.LayDatatable(sql);
            return dt;
        }
    }
}
