using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLShopThoiTrang.DAL
{
    public class SanPhamDAL
    {
        QLShopThoiTrangEntities dtct = new QLShopThoiTrangEntities();
        DataHelper dh = new DataHelper(@"Data Source=LD\SQLEXPRESS;Initial Catalog=QLShopThoiTrang;Integrated Security=True");

        public IEnumerable<object> TimSP(string masp)
        {
            List<SanPham> l = dtct.SanPhams.SqlQuery($"SELECT * FROM SanPham WHERE MaSP = '{masp}'").ToList<SanPham>();

            var list = from x in l
                       select new { x.MaSP, x.TenSP, x.ChatLieu, x.MoTa, x.MaLoai };
            return list.ToList();
        }
        public IEnumerable<object> LayDanhSachSanPham()
        {
            List<SanPham> l = dtct.SanPhams.SqlQuery("SELECT * FROM SanPham").ToList<SanPham>();

            var list = from x in l
                       select new { x.MaSP, x.TenSP, x.ChatLieu, x.MoTa, x.MaLoai};
            return list.ToList();
        }

        public void Add(SanPham sp)
        {
            dtct.SanPhams.Add(sp);

            dtct.SaveChanges();
        }
        public void Edit(SanPham sp)
        {
            SanPham n = dtct.SanPhams.Find(sp.MaSP);

            if (n != null)
            {
                n.TenSP = sp.TenSP;
                n.ChatLieu = sp.ChatLieu;
                n.MoTa = sp.MoTa;
                n.MaLoai = sp.MaLoai;

                dtct.SaveChanges();
            }
        }
        public void Delete(string masp)
        {
            SanPham n = dtct.SanPhams.Find(masp);
            dtct.SanPhams.Remove(n);

            dtct.SaveChanges();
        }
        public string SinhMaSanPham()
        {
            return dh.MaKeTiep(dh.LayMaCuoi("SanPham", "MaSP"), "SP");
        }
    }
}
