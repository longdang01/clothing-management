using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLShopThoiTrang.DAL
{
    public class LoaiSPDAL
    {
        QLShopThoiTrangEntities dtct = new QLShopThoiTrangEntities();
        DataHelper dh = new DataHelper(@"Data Source=LD\SQLEXPRESS;Initial Catalog=QLShopThoiTrang;Integrated Security=True");
        public IEnumerable<object> LayDanhSachLoaiSP()
        {
            List<LoaiSanPham> l = dtct.LoaiSanPhams.SqlQuery("SELECT * FROM LoaiSanPham").ToList<LoaiSanPham>();

            var list = from x in l
                       select new { x.MaLoai, x.TenLoai };
            return list.ToList();
        }

        public void Add(LoaiSanPham l)
        {
            dtct.LoaiSanPhams.Add(l);

            dtct.SaveChanges();
        }
        public void Edit(LoaiSanPham l)
        {
            LoaiSanPham n = dtct.LoaiSanPhams.Find(l.MaLoai);

            if (n != null)
            {
                n.TenLoai = l.TenLoai;

                dtct.SaveChanges();
            }
        }
        public void Delete(string maloai)
        {
            LoaiSanPham n = dtct.LoaiSanPhams.Find(maloai);
            dtct.LoaiSanPhams.Remove(n);

            dtct.SaveChanges();
        }
        public string SinhMaLoaiSP()
        {
            return dh.MaKeTiep(dh.LayMaCuoi("LoaiSanPham", "MaLoai"), "LSP");
        }
    }
}
