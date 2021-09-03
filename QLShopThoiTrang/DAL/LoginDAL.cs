using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLShopThoiTrang.DAL
{
    public class LoginDAL
    {
        DataHelper dh = new DataHelper(@"Data Source=LD\SQLEXPRESS;Initial Catalog=QLShopThoiTrang;Integrated Security=True");
        QLShopThoiTrangEntities dtct = new QLShopThoiTrangEntities();
        public static string userID { get; set; }
        public static string userName { get; set; }
        public static string role { get; set; }
        public bool CheckLogin(string username, string password)
        {
            List<User> l = new List<User>();
            List<NhanVien> l_nv = new List<NhanVien>();

            l = dtct.Users.SqlQuery("SELECT * FROM Users").ToList<User>();
         


            foreach (User x in l)
            {
                if(username == x.userName && password == x.pass)
                {
                    l_nv = dtct.NhanViens.SqlQuery($"SELECT * FROM NhanVien where MaNV = '{x.userID}'").ToList<NhanVien>();
                    foreach(NhanVien nv in l_nv)
                    {
                        userName = nv.TenNV;
                    }

                    userID = x.userID;
                    role = x.userRole;
                    return true;   
                }
            }
            return false;
        }
        public bool CheckPass(string userid, string password)
        {
            List<User> l = new List<User>();
            l = dtct.Users.SqlQuery("SELECT * FROM Users").ToList<User>();

            foreach (User x in l)
            {
                if (userid == x.userID && password == x.pass)
                {
                    return true;
                }
            }
            return false;
        }
        public void RePassWord(User u)
        {
            User n = dtct.Users.Find(u.userID);

            if (n != null)
            {
                n.pass = u.pass;

                dtct.SaveChanges();
            }
        }
    }
}
