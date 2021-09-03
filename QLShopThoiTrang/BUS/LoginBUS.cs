using QLShopThoiTrang.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLShopThoiTrang.BUS
{
    public class LoginBUS
    {
        LoginDAL lgDAL = new LoginDAL();
        public bool CheckLogin(string username, string password)
        {
            return lgDAL.CheckLogin(username, password);
        }
        public void RePassWord(User u)
        {
            lgDAL.RePassWord(u);
        }
        public bool CheckPass(string userid, string password)
        {
            return lgDAL.CheckPass(userid, password);
        }
    }
}
