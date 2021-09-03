using QLShopThoiTrang.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLShopThoiTrang.BUS
{
    public class SanPhamBUS
    {
        SanPhamDAL spDAL = new SanPhamDAL();
        public IEnumerable<object> TimSP(string masp)
        {
            return spDAL.TimSP(masp);
        }
        public IEnumerable<object> LayDanhSachSanPham()
        {
            return spDAL.LayDanhSachSanPham();
        }
        public void Add(SanPham sp)
        {
            spDAL.Add(sp);
        }
        public void Edit(SanPham sp)
        {
            spDAL.Edit(sp);
        }
        public void Delete(string masp)
        {
            spDAL.Delete(masp);
        }
        public string SinhMaSanPham()
        {
            return spDAL.SinhMaSanPham();
        }
    }
}
