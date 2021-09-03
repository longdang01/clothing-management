using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLShopThoiTrang.DAL
{
    public class NhaCungCapDAL
    {
        DataHelper dh = new DataHelper(@"Data Source=LD\SQLEXPRESS;Initial Catalog=QLShopThoiTrang;Integrated Security=True");

        QLShopThoiTrangEntities dtct = new QLShopThoiTrangEntities();
        public IEnumerable<object> LayDanhSachNCC()
        {
            List<NCC> l = dtct.NCCs.SqlQuery("SELECT * FROM NCC").ToList<NCC>();

            var list = from x in l
                       select new { x.MaNCC, x.TenNCC };
            return list.ToList();
        }
       
        public void Add(NCC ncc)
        {
            dtct.NCCs.Add(ncc);

            dtct.SaveChanges();
        }
        public void Edit(NCC ncc)
        {
            NCC n = dtct.NCCs.Find(ncc.MaNCC);

            if (n != null)
            {
                n.TenNCC = ncc.TenNCC;

                dtct.SaveChanges();
            }
        }
        public void Delete(string maNCC)
        {
            NCC n = dtct.NCCs.Find(maNCC);
            dtct.NCCs.Remove(n);
            
            dtct.SaveChanges();
        }
        public string SinhMaNCC()
        {
            return dh.MaKeTiep(dh.LayMaCuoi("NCC", "MaNCC"), "NCC");
        }
    }
}
