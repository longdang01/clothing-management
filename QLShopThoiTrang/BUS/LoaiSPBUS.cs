using QLShopThoiTrang.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLShopThoiTrang.BUS
{
    public class LoaiSPBUS
    {
        LoaiSPDAL lDAL = new LoaiSPDAL();
        public IEnumerable<object> LayDanhSachLoaiSP()
        {
            return lDAL.LayDanhSachLoaiSP();
        }

        public void Add(LoaiSanPham l)
        {
            lDAL.Add(l);
        }
        public void Edit(LoaiSanPham l)
        {
            lDAL.Edit(l);
        }
        public void Delete(string maloai)
        {
            lDAL.Delete(maloai);
        }
        public string SinhMaLoaiSP()
        {
            return lDAL.SinhMaLoaiSP();
        }
    }
}
