using QLShopThoiTrang.BUS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLShopThoiTrang.GUI
{
    public partial class frmLogin : Form
    {
        LoginBUS lgBUS = new LoginBUS();
        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            frmMain fMain = new frmMain();

            if (lgBUS.CheckLogin(txtaccount.Text, txtPassWord.Text))
            {
                fMain.Show();

                this.Hide();
            } else
            {
                MessageBox.Show("Tài khoản hoặc mật khẩu chưa chính xác!");
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn thoát không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
        }
    }
}
