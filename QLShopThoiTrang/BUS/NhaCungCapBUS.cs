using QLShopThoiTrang.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLShopThoiTrang.BUS
{
    public class NhaCungCapBUS
    {
        NhaCungCapDAL nccDAL = new NhaCungCapDAL();
        public IEnumerable<object> LayDanhSachNCC()
        {
            return nccDAL.LayDanhSachNCC();
        }
        public void Add(NCC ncc)
        {
            nccDAL.Add(ncc);
        }
        public void Edit(NCC ncc)
        {
            nccDAL.Edit(ncc);
        }
        public void Delete(string maNCC)
        {
            nccDAL.Delete(maNCC);
        }
        public string SinhMaNCC()
        {
            return nccDAL.SinhMaNCC();
        }
    }
}
