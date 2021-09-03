using QLShopThoiTrang.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLShopThoiTrang.BUS
{
    public class NhanVienBUS
    {
        NhanVienDAL nvDAL = new NhanVienDAL();

        public IEnumerable<object> LayDanhSachNV()
        {
            return nvDAL.LayDanhSachNV();
        }
        public IEnumerable<object> LayNhanVienTheoTen(string hoten)
        {
            return nvDAL.LayNhanVienTheoTen(hoten);
        }
        public IEnumerable<object> TimNV(string manv)
        {
            return nvDAL.TimNV(manv);
        }
        public IEnumerable<object> TimHDNhapCuaNV(string manv)
        {
            return nvDAL.TimHDNhapCuaNV(manv);
        }
        public IEnumerable<object> TimHDBanCuaNV(string manv)
        {
            return nvDAL.TimHDBanCuaNV(manv);
        }
        public User LayTK(string userID)
        {
            return nvDAL.LayTK(userID);
        }
        public void AddUser(User u)
        {
            nvDAL.AddUser(u);
        }

        public void Add(NhanVien nv)
        {
            nvDAL.Add(nv);
        }
        public void Edit(NhanVien nv)
        {
            nvDAL.Edit(nv);
        }
        public void Delete(string manv)
        {
            nvDAL.Delete(manv);
        }
        public void DeleteUser(string userid)
        {
            nvDAL.DeleteUser(userid);
        }
        public string SinhMaNV()
        {
            return nvDAL.SinhMaNV();
        }
        public DataTable TKNV(string sql)
        {
            return nvDAL.TKNV(sql);
        }
        public string SinhMaQL()
        {
            return nvDAL.SinhMaQL();
        }

    }
}
