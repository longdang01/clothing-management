using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLShopThoiTrang.DAL
{
    public class DataHelper
    {
        SqlConnection con;
        public DataHelper(string stcon)
        {
            con = new SqlConnection(stcon);
        }
        public void Open()
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
        }
        public void Close()
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }
        public void ExecuteNonQuery(string query)
        {
            Open();
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.ExecuteNonQuery();
            Close();
        }
 
        public DataTable LayDatatable(string sql)
        {
            Open();
            SqlDataAdapter da = new SqlDataAdapter(sql, con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            Close();
            return dt;
        }
        public string LayMaCuoi(string Bang, string Cot)
        {
            //try
            //{
                string sql = "SELECT TOP 1 " + Cot + " FROM " + Bang + " ORDER BY " + Cot + " DESC";

                DataTable dt = LayDatatable(sql);
                return dt.Rows[0][Cot].ToString();
            //}
            //catch (Exception ex)
            //{
            //    return Bang.ToString();
            //}
        }
        public string LayMaCuoiQL(string Bang, string Cot)
        {
            string sql = "SELECT TOP 1 " + Cot + " FROM " + Bang + " where SUBSTRING(MaNV, 1, 2) = 'QL' ORDER BY " + Cot + " DESC";

            DataTable dt = LayDatatable(sql);
            return dt.Rows[0][Cot].ToString();
        }
        public string LayMaCuoiNV(string Bang, string Cot)
        {
            string sql = "SELECT TOP 1 " + Cot + " FROM " + Bang + " where SUBSTRING(MaNV, 1, 2) = 'NV' ORDER BY " + Cot + " DESC";

            DataTable dt = LayDatatable(sql);
            return dt.Rows[0][Cot].ToString();
        }
        public string LayDonGia(string masp, string cot)
        {
            string sql = $"SELECT TOP 1 {cot} FROM ChiTietNhap where MaSP = '{masp}' ORDER BY MaDonNhap DESC";
            DataTable dt = LayDatatable(sql);
            return dt.Rows[0][cot].ToString();
        }
        public string TKTheoNgay(string Bang1, string Bang2, string Ma, string ngay, DateTimePicker d, string cot)
        {
            string sql = $"select {cot} = sum(ctb.SoLuong * ctb.DonGia) from {Bang1} b left join {Bang2} ctb on ctb.{Ma} = b.{Ma} where b.{ngay} = '{d.Value.Date}'";
            DataTable dt = LayDatatable(sql);
            return dt.Rows[0][cot].ToString();
        }
        public DataTable HDTheoNgay(string Bang1, string Bang2, string Ma, string ngay, DateTimePicker d)
        {
            string sql = $"select b.*, ctb.* from {Bang1} ctb left join {Bang2} b on b.{Ma} = ctb.{Ma} where b.{ngay} = '{d.Value.Date}'";
            DataTable dt = LayDatatable(sql);
            return dt;
        }
        public string TKTheoThang(string Bang1, string Bang2, string Ma, string ngay, string thang, string nam, string cot)
        {
            string sql = $"select {cot} = sum(ctb.SoLuong * ctb.DonGia) from {Bang1} b left join {Bang2} ctb on ctb.{Ma} = b.{Ma} where MONTH(b.{ngay}) = '{thang}' and YEAR(b.{ngay}) = '{nam}'";
            DataTable dt = LayDatatable(sql);
            return dt.Rows[0][cot].ToString();
        }
        public DataTable HDTheoThang(string Bang1, string Bang2, string Ma, string ngay, string thang, string nam)
        {
            string sql = $"select b.*, ctb.* from {Bang1} ctb left join {Bang2} b on b.{Ma} = ctb.{Ma} where MONTH(b.{ngay}) = '{thang}' and YEAR(b.{ngay}) = '{nam}'";
            DataTable dt = LayDatatable(sql);
            return dt;
        }
        public string TKTheoNam(string Bang1, string Bang2, string Ma, string ngay, string nam, string cot)
        {
            string sql = $"select {cot} = sum(ctb.SoLuong * ctb.DonGia) from {Bang1} b left join {Bang2} ctb on ctb.{Ma}= b.{Ma} where YEAR(b.{ngay}) = '{nam}'";
            DataTable dt = LayDatatable(sql);
            return dt.Rows[0][cot].ToString();
        }
        public DataTable HDTheoNam(string Bang1, string Bang2, string Ma, string ngay, string nam)
        {
            string sql = $"select b.*, ctb.* from {Bang1} ctb left join {Bang2} b on b.{Ma} = ctb.{Ma} where YEAR(b.{ngay}) = '{nam}'";
            DataTable dt = LayDatatable(sql);
            return dt;
        }
        public string sumNhap(string masp, string cot)
        {
            string sql = string.Format($"select {cot} = SUM(SoLuong) from ChiTietNhap where MaSP='{masp}' group by MaSP");
            DataTable dt = LayDatatable(sql);
            return dt.Rows[0][cot].ToString();
        }
        public string sumBan(string masp, string cot)
        {
            string sql = string.Format($"select {cot} = SUM(SoLuong) from ChiTietBan where MaSP='{masp}' group by MaSP");
            DataTable dt = LayDatatable(sql);

            if(dt.Rows.Count > 0)
                return dt.Rows[0][cot].ToString();
            else 
                return "";
        }
        public string MaKeTiep(string macuoi, string tiento)
        {
            int maketiep = int.Parse(macuoi.Remove(0, tiento.Length)) + 1;

            int lengthNumerID = macuoi.Length - tiento.Length;
            string zeroNumber = "";
            for (int i = 1; i < lengthNumerID; i++)
            {
                if (maketiep < Math.Pow(10, i))
                {
                    zeroNumber += "0";
                }
            }
            return tiento + zeroNumber + maketiep.ToString();
        }
        public string SinhMa(string _table, string _colMa, string _tiento)
        {
            return MaKeTiep(LayMaCuoi(_table, _colMa), _tiento);
        }
       
    }
}
