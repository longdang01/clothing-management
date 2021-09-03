using QLShopThoiTrang.BUS;
using QLShopThoiTrang.DAL;
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
    public partial class frmMain : Form
    {
        NhaCungCapBUS nccBUS = new NhaCungCapBUS();
        PhieuNhapBUS pBUS = new PhieuNhapBUS();
        SanPhamBUS spBUS = new SanPhamBUS();
        LoaiSPBUS lBUS = new LoaiSPBUS();
        BanHangBUS bBUS = new BanHangBUS();
        NhanVienBUS nvBUS = new NhanVienBUS();
        LoginBUS lgBUS = new LoginBUS();

        public frmMain()
        {
            InitializeComponent();
        }
       
        private void frmMain_Load(object sender, EventArgs e)
        {
            lblUSName.Text = LoginDAL.userName;
            txtMaNV.Text = LoginDAL.userID;
            txtMaNVHDB.Text = LoginDAL.userID;

            if(LoginDAL.role == "Quản lý")
            {
                MessageBox.Show("Bạn đã đăng nhập với tư cách là quản lý");
            } else
            {
                ((Control)this.tabSP).Enabled = false;
                ((Control)this.tabLSP).Enabled = false;
                ((Control)this.TabNCC).Enabled = false;
                ((Control)this.NhanVienTab).Enabled = false;
                ((Control)this.ThongKeTab).Enabled = false;
            }

            dtpkNgayLap.Value = DateTime.Now;
            dtpkNgayBan.Value = DateTime.Now;
            dtpkdoanhthutheongay.Value = DateTime.Now;

            cboRole.SelectedIndex = 0;

            cboNCC.DataSource = nccBUS.LayDanhSachNCC();
            cboNCC.ValueMember = "MaNCC";
            cboNCC.DisplayMember = "TenNCC";

            cboMaLoai.DataSource = lBUS.LayDanhSachLoaiSP();
            cboMaLoai.ValueMember = "MaLoai";
            cboMaLoai.DisplayMember = "TenLoai";

            txtMaPhieuNhap.Text = pBUS.SinhMaPhieuNhap();
            txtMaDonBan.Text = bBUS.SinhMaDonBan();
            txtMaKH.Text = bBUS.SinhMaKH();
            txtMaSanPham.Text = spBUS.SinhMaSanPham();
            txtMaLoaiSP.Text = lBUS.SinhMaLoaiSP();
            txtMaNCC.Text = nccBUS.SinhMaNCC();
            txtMaNhanVien.Text = nvBUS.SinhMaQL();

            dtgvSPDeBan.DataSource = bBUS.LayDSSPTrongKho();
            dtgvNCCLIST.DataSource = nccBUS.LayDanhSachNCC();
            dtgvSP.DataSource = pBUS.LayDanhSachSP();
            dtgvDanhSachSanPham.DataSource = spBUS.LayDanhSachSanPham();
            dtgvDanhSachLSP.DataSource = lBUS.LayDanhSachLoaiSP();
            dtgvDSNhanVien.DataSource = nvBUS.LayDanhSachNV();
        }

        private void dtgvNCCLIST_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtMaNCC.Text = dtgvNCCLIST.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtTenNCC.Text = dtgvNCCLIST.Rows[e.RowIndex].Cells[1].Value.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            txtMaNCC.Clear();
            txtTenNCC.Clear();
            txtTenNCC.Focus();
            txtMaNCC.Text = nccBUS.SinhMaNCC();
        }

        private void btnThemNCC_Click(object sender, EventArgs e)
        {
            NCC n = new NCC();
            if(txtMaNCC.Text != "" && txtTenNCC.Text != "")
            {
                n.MaNCC = txtMaNCC.Text;
                n.TenNCC = txtTenNCC.Text;

                nccBUS.Add(n);
                txtTenNCC.Clear();
                txtMaNCC.Text = nccBUS.SinhMaNCC();

                dtgvNCCLIST.DataSource = null;
                dtgvNCCLIST.DataSource = nccBUS.LayDanhSachNCC();

                var lnv = nccBUS.LayDanhSachNCC();
                dtgvNCCLIST.CurrentCell = dtgvNCCLIST.Rows[lnv.ToList().Count - 1].Cells[0];
            } else
            {
                MessageBox.Show("Hãy nhập đầy đủ dữ liệu");
            }
        }

        private void btnCapNhatNCC_Click(object sender, EventArgs e)
        {
            if (txtMaNCC.Text == "")
            {
                MessageBox.Show("Hãy chọn nhà cung cấp cần sửa");
            }
            else
            {
                NCC n = new NCC();
                n.MaNCC = txtMaNCC.Text;
                n.TenNCC = txtTenNCC.Text;

                nccBUS.Edit(n);

                dtgvNCCLIST.DataSource = null;
                dtgvNCCLIST.DataSource = nccBUS.LayDanhSachNCC();
            }
        }

        private void btnXoaNCC_Click(object sender, EventArgs e)
        {
            if (txtMaNCC.Text == "")
            {
                MessageBox.Show("Hãy nhập mã nhà cung cấp cần xóa");
            }
            else
            {
                string maNcc = txtMaNCC.Text;

                nccBUS.Delete(maNcc);

                dtgvNCCLIST.DataSource = null;
                dtgvNCCLIST.DataSource = nccBUS.LayDanhSachNCC();
            }
        }

        private void btnTimKiemPhieuNhap_Click(object sender, EventArgs e)
        {
            if(txtTimKiemMaPhieuNhap.Text != "")
            {
                dtgvHoaDonNhap.DataSource = null;
                dtgvChiTietHDNhap.DataSource = null;

                dtgvHoaDonNhap.DataSource = pBUS.TimHDNhap(txtTimKiemMaPhieuNhap.Text);
                dtgvChiTietHDNhap.DataSource = pBUS.TimCTHDNhap(txtTimKiemMaPhieuNhap.Text);

            }
            else
            {
                MessageBox.Show("Hãy nhập vào mã phiếu nhập");
            }
        }

        private void dtgvSP_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtMaSPForNhapHang.Text = dtgvSP.Rows[e.RowIndex].Cells[0].Value.ToString();
        }

        private void btnNhapHang_Click(object sender, EventArgs e)
        {
            NhapHang n = new NhapHang();
        
            if (txtMaNV.Text != "" && txtMaPhieuNhap.Text != "")
            {
                n.MaDonNhap= txtMaPhieuNhap.Text;
                n.MaNCC = cboNCC.SelectedValue.ToString();
                n.MaNV = txtMaNV.Text;
                n.NgayNhap = dtpkNgayLap.Value.Date;
                pBUS.AddHDNhap(n);

                if (pBUS.DSNhapKho().Count < 1)
                {
                    MessageBox.Show("Hãy chọn sản phẩm để nhập");
                }else
                {
                    foreach(NhapKho k in pBUS.DSNhapKho())
                    {
                        ChiTietNhap ctn = new ChiTietNhap();
                        ctn.MaDonNhap = txtMaPhieuNhap.Text;
                        ctn.MaSP = k.MaSP;
                        ctn.SoLuong = k.SoLuong;
                        ctn.DonGia = k.DonGia;
                        pBUS.AddCTHDNhap(ctn);
                    }
                }
                pBUS.DeleteAllNhapKho();
                numAmount.Value = 1;
                txtPrice.Clear();

                lblTongTienNhap.Text = pBUS.reloadTotalPrice().ToString();

                dtgvNhapKho.DataSource = null;

                txtMaPhieuNhap.Text = pBUS.SinhMaPhieuNhap();
                MessageBox.Show("Nhập hàng xong");
            }
            else
            {
                MessageBox.Show("Hãy nhập đầy đủ dữ liệu");
            }
        }
        private void btnThemNhapKho_Click(object sender, EventArgs e)
        {
            if(txtMaSPForNhapHang.Text != "")
            {
                NhapKho nk = new NhapKho();
                nk.MaSP = txtMaSPForNhapHang.Text;
                nk.SoLuong = Convert.ToInt32(numAmount.Value);
                nk.DonGia = Convert.ToDouble(txtPrice.Text);
                pBUS.AddNhapKho(nk);
                lblTongTienNhap.Text = pBUS.reloadTotalPrice().ToString();

                dtgvNhapKho.DataSource = null;
                dtgvNhapKho.DataSource = pBUS.LayDanhSachNhapKho();
            }
            else
            {
                MessageBox.Show("Hãy chọn sản phẩm để nhập");
            }
        }

        private void dtgvNhapKho_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtMaSPForNhapHang.Text = dtgvNhapKho.Rows[e.RowIndex].Cells[0].Value.ToString();
            numAmount.Value = Convert.ToDecimal(dtgvNhapKho.Rows[e.RowIndex].Cells[1].Value.ToString());
            txtPrice.Text = dtgvNhapKho.Rows[e.RowIndex].Cells[2].Value.ToString();
        }

        private void btnTimSanPhamDeNhap_Click(object sender, EventArgs e)
        {
            if(txtMaSPTimKiem.Text == "")
            {
                MessageBox.Show("Hãy nhập mã sản phẩm để tìm kiếm");
            } else
            {
                dtgvSP.DataSource = null;
                dtgvSP.DataSource = spBUS.TimSP(txtMaSPTimKiem.Text);
            }
        }

        private void btnXoaNhapHang_Click(object sender, EventArgs e)
        {
            if (txtMaSPForNhapHang.Text == "")
            {
                MessageBox.Show("Hãy chọn sản phẩm để xóa");
            }
            else
            {
                pBUS.DeleteNhapKho(txtMaSPForNhapHang.Text);
                lblTongTienNhap.Text = pBUS.reloadTotalPrice().ToString();

                dtgvNhapKho.DataSource = null;
                dtgvNhapKho.DataSource = pBUS.LayDanhSachNhapKho();
            }
        }

        private void btnCapNhatNhapHang_Click(object sender, EventArgs e)
        {
            NhapKho k = new NhapKho();
            if(txtMaSPForNhapHang.Text == "")
            {
                MessageBox.Show("Hãy chọn sản phẩm để sửa");
            }else
            {
                k.MaSP = txtMaSPForNhapHang.Text;
                k.SoLuong = Convert.ToInt32(numAmount.Value);
                k.DonGia = Convert.ToDouble(txtPrice.Text);

                pBUS.EditPriceAndAmount(k);
                lblTongTienNhap.Text = pBUS.reloadTotalPrice().ToString();

                dtgvNhapKho.DataSource = null;
                dtgvNhapKho.DataSource = pBUS.LayDanhSachNhapKho();
            }
        }

        private void btnthemVaoGioHang_Click(object sender, EventArgs e)
        {
            if (bBUS.LayTonKho(txtMaSPForBanHang.Text) != 0)
            {
                GioHang gh = new GioHang();
                if (txtMaSPForBanHang.Text != "")
                {
                    int markup = 30;

                    gh.MaSP = txtMaSPForBanHang.Text;
                    gh.SoLuong = Convert.ToInt32(numSoLuongBan.Value);
                    gh.DonGia = Math.Round((Convert.ToDouble(bBUS.LayDonGia(txtMaSPForBanHang.Text, "DonGia")) / (100 - markup)) * 100, 0);

                    bBUS.AddGioHang(gh);
                    lbltotalPrice.Text = bBUS.reloadTotalPrice().ToString();

                    dtgvGioHang.DataSource = null;
                    dtgvGioHang.DataSource = bBUS.LayDanhSachGioHang();
                }
                else
                {
                    MessageBox.Show("Hãy chọn sản phẩm để bán");
                }
            } else
            {
                MessageBox.Show("Sản phẩm đã hết hàng");
            }
        }

        private void dtgvSPDeBan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtMaSPForBanHang.Text = dtgvSPDeBan.Rows[e.RowIndex].Cells[0].Value.ToString();
            lblTonKho.Text = bBUS.LayTonKho(txtMaSPForBanHang.Text).ToString();
        }

        private void dtgvGioHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtMaSPForBanHang.Text = dtgvGioHang.Rows[e.RowIndex].Cells[0].Value.ToString();
            numSoLuongBan.Value = Convert.ToDecimal(dtgvGioHang.Rows[e.RowIndex].Cells[1].Value.ToString());
            txtGiaBan.Text = dtgvGioHang.Rows[e.RowIndex].Cells[2].Value.ToString();
        }

        private void btnSuaGioHang_Click(object sender, EventArgs e)
        {
            GioHang g = new GioHang();
            if (txtMaSPForBanHang.Text == "")
            {
                MessageBox.Show("Hãy chọn sản phẩm để sửa");
            }
            else
            {
                g.MaSP = txtMaSPForBanHang.Text;
                g.SoLuong = Convert.ToInt32(numSoLuongBan.Value);
                g.DonGia = Convert.ToDouble(txtGiaBan.Text);

                if(g.SoLuong > bBUS.LayTonKho(g.MaSP))
                {
                    MessageBox.Show($"Sản phẩm trong kho chỉ còn {bBUS.LayTonKho(g.MaSP)}");
                } else
                {
                    bBUS.EditGioHang(g);
                    lbltotalPrice.Text = bBUS.reloadTotalPrice().ToString();
                    dtgvGioHang.DataSource = null;
                    dtgvGioHang.DataSource = bBUS.LayDanhSachGioHang();
                }
            }
        }

        private void btnXoaGioHang_Click(object sender, EventArgs e)
        {
            if (txtMaSPForBanHang.Text == "")
            {
                MessageBox.Show("Hãy chọn sản phẩm để xóa");
            }
            else
            {
                bBUS.DeleteGioHang(txtMaSPForBanHang.Text);
                lbltotalPrice.Text = bBUS.reloadTotalPrice().ToString();
                dtgvGioHang.DataSource = null;
                dtgvGioHang.DataSource = bBUS.LayDanhSachGioHang();
            }
        }

        private void btnLamMoiGioHang_Click(object sender, EventArgs e)
        {
            bBUS.DeleteAllGioHang();
            numSoLuongBan.Value = 1;
            txtGiaBan.Clear();

            lbltotalPrice.Text = bBUS.reloadTotalPrice().ToString();
            
            dtgvGioHang.DataSource = null;
            //dtgvGioHang.DataSource = bBUS.LayDanhSachGioHang();
        }

        private void btnTimKiemSPDeBan_Click(object sender, EventArgs e)
        {
            if(txtMaSPTKDonBan.Text != null)
            {
                dtgvSPDeBan.DataSource = null;
                dtgvSPDeBan.DataSource = bBUS.TimSPDeBan(txtMaSPTKDonBan.Text);
            } else
            {
                MessageBox.Show("Hãy nhập mã sản phẩm để tìm kiếm");
            }
        }

        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            BanHang n = new BanHang();
            KhachHang kh = new KhachHang();

            if (txtMaNV.Text != "" && txtMaDonBan.Text != "" && txtMaSPForBanHang.Text != "")
            {
                n.MaDonBan = txtMaDonBan.Text;
                n.MaKH = txtMaKH.Text;
                kh.MaKH = txtMaKH.Text;

                n.MaNV = txtMaNVHDB.Text;
                n.NgayBan = dtpkNgayBan.Value.Date;

                bBUS.AddKH(kh);
                bBUS.AddHDBan(n);

                if (bBUS.DSGioHang().Count < 1)
                {
                    MessageBox.Show("Hãy chọn sản phẩm để bán");
                }
                else
                {
                    foreach (GioHang k in bBUS.DSGioHang())
                    {
                        ChiTietBan ctb = new ChiTietBan();
                        ctb.MaDonBan = txtMaDonBan.Text;
                        ctb.MaSP = k.MaSP;
                        ctb.SoLuong = k.SoLuong;
                        ctb.DonGia = k.DonGia;
                        bBUS.AddCTHDBan(ctb);
                    }
                }
                bBUS.DeleteAllGioHang();
                numSoLuongBan.Value = 1;
                txtGiaBan.Clear();

                lbltotalPrice.Text = bBUS.reloadTotalPrice().ToString();

                dtgvGioHang.DataSource = null;

                txtMaDonBan.Text = bBUS.SinhMaDonBan();
                txtMaKH.Text = bBUS.SinhMaKH();
                MessageBox.Show("Thanh toán thành công");
            }
            else
            {
                MessageBox.Show("Hãy nhập đầy đủ dữ liệu");
            }
        }

        private void btnTimKiemHDBan_Click(object sender, EventArgs e)
        {
            if (txtXemHDBan.Text != "")
            {
                dtgvHDBan.DataSource = null;
                dtgvCTHDBan.DataSource = null;

                dtgvHDBan.DataSource = bBUS.TimHDBan(txtXemHDBan.Text);
                dtgvCTHDBan.DataSource = bBUS.TimCTHDBan(txtXemHDBan.Text);

            }
            else
            {
                MessageBox.Show("Hãy nhập vào mã đơn bán để tìm kiếm");
            }
        }

        private void btnLamMoiHDNhap_Click(object sender, EventArgs e)
        {
            pBUS.DeleteAllNhapKho();
            numAmount.Value = 1;
            txtPrice.Clear();

            lblTongTienNhap.Text = pBUS.reloadTotalPrice().ToString();

            dtgvNhapKho.DataSource = null;
        }

        private void btnThemMoiSanPham_Click(object sender, EventArgs e)
        {
            txtTenSanPham.Clear();
            txtTenSanPham.Focus();
            txtChatLieu.Clear();
            txtMoTaSP.Clear();
            txtMaSanPham.Text = spBUS.SinhMaSanPham();
        }

        private void btnThemSanPham_Click(object sender, EventArgs e)
        {
            SanPham n = new SanPham();
            if (txtMaSanPham.Text != "" && txtTenSanPham.Text != "" && txtChatLieu.Text != "" &&
                txtMoTaSP.Text != "" && cboMaLoai.SelectedValue.ToString() != "")
            {
                n.MaSP = txtMaSanPham.Text;
                n.TenSP = txtTenSanPham.Text;
                n.ChatLieu = txtChatLieu.Text;
                n.MoTa = txtMoTaSP.Text;
                n.MaLoai = cboMaLoai.SelectedValue.ToString();

                spBUS.Add(n);
                txtTenSanPham.Clear();
                txtChatLieu.Clear();
                txtMoTaSP.Clear();
                txtMaSanPham.Text = spBUS.SinhMaSanPham();

                dtgvDanhSachSanPham.DataSource = null;
                dtgvDanhSachSanPham.DataSource = spBUS.LayDanhSachSanPham();

                dtgvSP.DataSource = null;
                dtgvSP.DataSource = pBUS.LayDanhSachSP();

                var lnv = spBUS.LayDanhSachSanPham();
                dtgvDanhSachSanPham.CurrentCell = dtgvDanhSachSanPham.Rows[lnv.ToList().Count - 1].Cells[0];
            }
            else
            {
                MessageBox.Show("Hãy nhập đầy đủ dữ liệu");
            }
        }

        private void btnSuaSanPham_Click(object sender, EventArgs e)
        {
            if (txtMaSanPham.Text == "")
            {
                MessageBox.Show("Hãy chọn sản phẩm cần cập nhật");
            }
            else
            {
                SanPham n = new SanPham();
                n.MaSP = txtMaSanPham.Text;
                n.TenSP = txtTenSanPham.Text;
                n.ChatLieu = txtChatLieu.Text;
                n.MoTa = txtMoTaSP.Text;
                n.MaLoai = cboMaLoai.SelectedValue.ToString();

                spBUS.Edit(n);

                dtgvDanhSachSanPham.DataSource = null;
                dtgvDanhSachSanPham.DataSource = spBUS.LayDanhSachSanPham();
            }
        }

        private void btnXoaSanPham_Click(object sender, EventArgs e)
        {
            if (txtMaSanPham.Text == "")
            {
                MessageBox.Show("Hãy chọn sản phẩm cần xóa");
            }
            else
            {
                string masp = txtMaSanPham.Text;

                spBUS.Delete(masp);

                dtgvSP.DataSource = null;
                dtgvSP.DataSource = pBUS.LayDanhSachSP();

                dtgvDanhSachSanPham.DataSource = null;
                dtgvDanhSachSanPham.DataSource = spBUS.LayDanhSachSanPham();
            }
        }

        private void dtgvDanhSachSanPham_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtMaSanPham.Text = dtgvDanhSachSanPham.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtTenSanPham.Text = dtgvDanhSachSanPham.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtChatLieu.Text = dtgvDanhSachSanPham.Rows[e.RowIndex].Cells[2].Value.ToString();
            txtMoTaSP.Text = dtgvDanhSachSanPham.Rows[e.RowIndex].Cells[3].Value.ToString();
            cboMaLoai.SelectedValue = dtgvDanhSachSanPham.Rows[e.RowIndex].Cells[4].Value.ToString();

        }

        private void btnThemMoiLSP_Click(object sender, EventArgs e)
        {
            txtTenLoaiSP.Clear();
            txtTenLoaiSP.Focus();
            txtMaLoaiSP.Text = lBUS.SinhMaLoaiSP();
        }

        private void btnThemLSP_Click(object sender, EventArgs e)
        {
            LoaiSanPham n = new LoaiSanPham();
            if (txtMaLoaiSP.Text != "" && txtTenLoaiSP.Text != "")
            {
                n.MaLoai = txtMaLoaiSP.Text;
                n.TenLoai = txtTenLoaiSP.Text;

                lBUS.Add(n);
                txtTenLoaiSP.Clear();
                txtMaLoaiSP.Text = lBUS.SinhMaLoaiSP();

                dtgvDanhSachLSP.DataSource = null;
                dtgvDanhSachLSP.DataSource = lBUS.LayDanhSachLoaiSP();

                var lnv = lBUS.LayDanhSachLoaiSP();
                dtgvDanhSachLSP.CurrentCell = dtgvDanhSachLSP.Rows[lnv.ToList().Count - 1].Cells[0];
            }
            else
            {
                MessageBox.Show("Hãy nhập đầy đủ dữ liệu");
            }
        }

        private void btnSuaLSP_Click(object sender, EventArgs e)
        {
            if (txtMaLoaiSP.Text == "")
            {
                MessageBox.Show("Hãy chọn loại sản phẩm cần cập nhật");
            }
            else
            {
                LoaiSanPham n = new LoaiSanPham();
                n.MaLoai = txtMaLoaiSP.Text;
                n.TenLoai = txtTenLoaiSP.Text;

                lBUS.Edit(n);

                dtgvDanhSachLSP.DataSource = null;
                dtgvDanhSachLSP.DataSource = lBUS.LayDanhSachLoaiSP();
            }
        }

        private void btnXoaLSP_Click(object sender, EventArgs e)
        {
            if (txtMaLoaiSP.Text == "")
            {
                MessageBox.Show("Hãy chọn loại sản phẩm cần xóa");
            }
            else
            {
                string maloai = txtMaLoaiSP.Text;

                lBUS.Delete(maloai);

                dtgvDanhSachLSP.DataSource = null;
                dtgvDanhSachLSP.DataSource = lBUS.LayDanhSachLoaiSP();
            }
        }

        private void dtgvDanhSachLSP_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtMaLoaiSP.Text = dtgvDanhSachLSP.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtTenLoaiSP.Text = dtgvDanhSachLSP.Rows[e.RowIndex].Cells[1].Value.ToString();
        }

        private void btnXemDSHDBan_Click(object sender, EventArgs e)
        {
            dtgvHDBan.DataSource = bBUS.DSHDBan();
            dtgvCTHDBan.DataSource = bBUS.DSCTHDBan();
        }

        private void btnXemAllDSHDNhap_Click(object sender, EventArgs e)
        {
            dtgvHoaDonNhap.DataSource = pBUS.DSHDNhap();
            dtgvChiTietHDNhap.DataSource = pBUS.DSCTHDNhap();
        }

        private void btnThemMoiNV_Click(object sender, EventArgs e)
        {
            txtMaNhanVien.Text = nvBUS.SinhMaNV();
            txtTenNhanVien.Clear();
            txtTenNhanVien.Focus();
            txtSDT.Clear();
            rtbDiaChi.Clear();
            txtTaiKhoan.Clear();
            txtMatKhau.Clear();
            txtTaiKhoan.ReadOnly = false;
            txtMatKhau.ReadOnly = false;
            txtTaiKhoan.Enabled = true;
            txtMatKhau.Enabled = true;
            cboRole.Enabled = true;
        }

        private void btnThemNV_Click(object sender, EventArgs e)
        {
            NhanVien n = new NhanVien();
            User u = new User();
            if (txtMaNhanVien.Text != "" && txtTenNhanVien.Text != "" && txtSDT.Text != ""
                 && rtbDiaChi.Text != "" && txtTaiKhoan.Text != "" && txtMatKhau.Text != "")
            {
                u.userID = txtMaNhanVien.Text;
                u.userName = txtTaiKhoan.Text;
                u.pass = txtMatKhau.Text;
                u.userRole = cboRole.Text;

                n.MaNV = txtMaNhanVien.Text;
                n.TenNV = txtTenNhanVien.Text;
                n.SDT = txtSDT.Text;
                n.DiaChi = rtbDiaChi.Text;

                nvBUS.AddUser(u);
                nvBUS.Add(n);

                txtMaNhanVien.Text = nvBUS.SinhMaNV();
                txtTenNhanVien.Clear();
                txtTenNhanVien.Focus();
                txtSDT.Clear();
                rtbDiaChi.Clear();
                txtTaiKhoan.Clear();
                txtMatKhau.Clear();

                dtgvDSNhanVien.DataSource = null;
                dtgvDSNhanVien.DataSource = nvBUS.LayDanhSachNV();

                var lnv = nvBUS.LayDanhSachNV();
                dtgvDSNhanVien.CurrentCell = dtgvDSNhanVien.Rows[lnv.ToList().Count - 1].Cells[0];
            }
            else
            {
                MessageBox.Show("Hãy nhập đầy đủ dữ liệu");
            }
        }

        private void btnSuaNV_Click(object sender, EventArgs e)
        {
            if (txtMaNhanVien.Text == "")
            {
                MessageBox.Show("Hãy chọn nhân viên cần sửa");
            }
            else
            {
                NhanVien n = new NhanVien();
                n.MaNV = txtMaNhanVien.Text;
                n.TenNV = txtTenNhanVien.Text;
                n.SDT = txtSDT.Text;
                n.DiaChi = rtbDiaChi.Text;

                nvBUS.Edit(n);

                dtgvDSNhanVien.DataSource = null;
                dtgvDSNhanVien.DataSource = nvBUS.LayDanhSachNV();
            }
        }

        private void btnXoaNV_Click(object sender, EventArgs e)
        {
            if (txtMaNhanVien.Text == "")
            {
                MessageBox.Show("Hãy chọn nhân viên cần xóa");
            }
            else
            {
                string manv = txtMaNhanVien.Text;

                nvBUS.Delete(manv);
                nvBUS.DeleteUser(manv);

                dtgvDSNhanVien.DataSource = null;
                dtgvDSNhanVien.DataSource = nvBUS.LayDanhSachNV();
            }
        }

        private void dtgvDSNhanVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtTaiKhoan.ReadOnly = true;
            txtMatKhau.ReadOnly = true;
            txtTaiKhoan.Enabled = false;
            txtMatKhau.Enabled = false;
            cboRole.Enabled = false;

            txtMaNhanVien.Text = dtgvDSNhanVien.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtTenNhanVien.Text = dtgvDSNhanVien.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtSDT.Text = dtgvDSNhanVien.Rows[e.RowIndex].Cells[2].Value.ToString();
            rtbDiaChi.Text = dtgvDSNhanVien.Rows[e.RowIndex].Cells[3].Value.ToString();

            User x = nvBUS.LayTK(txtMaNhanVien.Text);
            txtTaiKhoan.Text = x.userName;
            txtMatKhau.Text = x.pass;
            cboRole.Text = x.userRole;

            dtgvHDNhapCuaNV.DataSource = nvBUS.TimHDNhapCuaNV(txtMaNhanVien.Text);
            dtgvHDBanCuaNV.DataSource = nvBUS.TimHDBanCuaNV(txtMaNhanVien.Text);
        }

        private void btnTimNVTheoMaQLNV_Click(object sender, EventArgs e)
        {
            if(txtTimNVQLNV.Text != "")
            {
                dtgvDSNhanVien.DataSource = null;
                dtgvDSNhanVien.DataSource = nvBUS.TimNV(txtTimNVQLNV.Text);
            } else
            {
                MessageBox.Show("Hãy nhập dữ liệu để tìm kiếm");
            }
        }

        private void btnTimNVTheoTenQLNV_Click(object sender, EventArgs e)
        {
            if (txtTimNVQLNV.Text != "")
            {
                dtgvDSNhanVien.DataSource = null;
                dtgvDSNhanVien.DataSource = nvBUS.LayNhanVienTheoTen(txtTimNVQLNV.Text);
            }
            else
            {
                MessageBox.Show("Hãy nhập dữ liệu để tìm kiếm");
            }
        }
        private void raddoanhthungay_CheckedChanged(object sender, EventArgs e)
        {
            dtpkdoanhthutheongay.Enabled = true;
            cbodoanhthutheothang.Enabled = false;
            txtdoanhthutheonam.Enabled = false;
        }

        private void raddoanhthuthang_CheckedChanged(object sender, EventArgs e)
        {
            dtpkdoanhthutheongay.Enabled = false;
            cbodoanhthutheothang.Enabled = true;
            txtdoanhthutheonam.Enabled = true;
        }

        private void raddoanhthunam_CheckedChanged(object sender, EventArgs e)
        {
            dtpkdoanhthutheongay.Enabled = false;
            cbodoanhthutheothang.Enabled = false;
            txtdoanhthutheonam.Enabled = true;
        }

        private void btnXemDoanhThu_Click(object sender, EventArgs e)
        {
            if(raddoanhthungay.Checked == true)
            {
                lblDoanhThu.Text = bBUS.TKTheoNgay("BanHang", "ChiTietBan", "MaDonBan", "NgayBan", dtpkdoanhthutheongay, "TongDoanhThu");
                dtgvHDDoanhThu.DataSource = bBUS.HDTheoNgay("ChiTietBan", "BanHang", "MaDonBan", "NgayBan", dtpkdoanhthutheongay); 
            } else if(raddoanhthuthang.Checked == true)
            {   
                lblDoanhThu.Text = bBUS.TKTheoThang("BanHang", "ChiTietBan", "MaDonBan", "NgayBan", cbodoanhthutheothang.Text, txtdoanhthutheonam.Text, "TongDoanhThu");
                dtgvHDDoanhThu.DataSource = bBUS.HDTheoThang("ChiTietBan", "BanHang", "MaDonBan", "NgayBan", cbodoanhthutheothang.Text, txtdoanhthutheonam.Text); 
            } else if (raddoanhthunam.Checked == true)
            {
                lblDoanhThu.Text = bBUS.TKTheoNam("BanHang", "ChiTietBan", "MaDonBan", "NgayBan", txtdoanhthutheonam.Text, "TongDoanhThu");
                dtgvHDDoanhThu.DataSource = bBUS.HDTheoNam("ChiTietBan", "BanHang", "MaDonBan", "NgayBan", txtdoanhthutheonam.Text);
            }
        }

        private void radTKNKNgay_CheckedChanged(object sender, EventArgs e)
        {
            dtpkTKNKNgay.Enabled = true;
            cboTKNKThang.Enabled = false;
            txtTKNKNam.Enabled = false;
        }

        private void radTkNKThang_CheckedChanged(object sender, EventArgs e)
        {
            dtpkTKNKNgay.Enabled = false;
            cboTKNKThang.Enabled = true;
            txtTKNKNam.Enabled = true;
        }

        private void radTKNKNam_CheckedChanged(object sender, EventArgs e)
        {
            dtpkTKNKNgay.Enabled = false;
            cboTKNKThang.Enabled = false;
            txtTKNKNam.Enabled = true;
        }

        private void btnXemTongChi_Click(object sender, EventArgs e)
        {
            if (radTKNKNgay.Checked == true)
            {
                lblTongChi.Text = pBUS.TKTheoNgay("NhapHang", "ChiTietNhap", "MaDonNhap", "NgayNhap", dtpkTKNKNgay, "TongChi");
                dtgvTKHDNK.DataSource = pBUS.HDTheoNgay("ChiTietNhap", "NhapHang", "MaDonNhap", "NgayNhap", dtpkTKNKNgay);
            }
            else if (radTkNKThang.Checked == true)
            {
                lblTongChi.Text = pBUS.TKTheoThang("NhapHang", "ChiTietNhap", "MaDonNhap", "NgayNhap", cboTKNKThang.Text, txtTKNKNam.Text, "TongChi");
                dtgvTKHDNK.DataSource = pBUS.HDTheoThang("ChiTietNhap", "NhapHang", "MaDonNhap", "NgayNhap", cboTKNKThang.Text, txtTKNKNam.Text);
            }
            else if (radTKNKNam.Checked == true)
            {
                lblTongChi.Text = pBUS.TKTheoNam("NhapHang", "ChiTietNhap", "MaDonNhap", "NgayNhap", txtTKNKNam.Text, "TongChi");
                dtgvTKHDNK.DataSource = pBUS.HDTheoNam("ChiTietNhap", "NhapHang", "MaDonNhap", "NgayNhap", txtTKNKNam.Text);
            }
        }

        private void radTatCaSP_CheckedChanged(object sender, EventArgs e)
        {
            dtgvTKSanPham.DataSource = null;
            dtgvTKSanPham.DataSource = bBUS.TKDSSP();
        }

        private void radSPBanChay_CheckedChanged(object sender, EventArgs e)
        {
            dtgvTKSanPham.DataSource = null;
            dtgvTKSanPham.DataSource = bBUS.LaySPBanChayNhat();
        }

        private void radSanPhamChuaBanDc_CheckedChanged(object sender, EventArgs e)
        {
            dtgvTKSanPham.DataSource = null;
            dtgvTKSanPham.DataSource = bBUS.LaySPChuaBanDuoc();

        }

        private void radTKNVTheoThang_CheckedChanged(object sender, EventArgs e)
        {
            cboTKNVThang.Enabled = true;
            txtTKNVNam.Enabled = true;
        }

        private void radTKNVTheoNam_CheckedChanged(object sender, EventArgs e)
        {
            cboTKNVThang.Enabled = false;
            txtTKNVNam.Enabled = true;
        }

        private void btnTKNV_Click(object sender, EventArgs e)
        {
            if(radTKNVTheoThang.Checked == true)
            {
                if (radNVBanHang.Checked == true)
                {
                    dtgvTKNV.DataSource = null;
                    dtgvTKNV.DataSource = nvBUS.TKNV($"select b.MaNV, ctb.* from BanHang b left join ChiTietBan ctb on ctb.MaDonBan = b.MaDonBan where Month(b.NgayBan) = {int.Parse(cboTKNVThang.Text)} and YEAR(b.NgayBan) = {int.Parse(txtTKNVNam.Text)}");
                }
                if (radNVBanNhieuSPNhat.Checked == true)
                {
                    dtgvTKNV.DataSource = null;
                    dtgvTKNV.DataSource = nvBUS.TKNV($"select * from NhanVien where MaNV in (select top 1 b.MaNV from BanHang b left join ChiTietBan ctb on ctb.MaDonBan = b.MaDonBan where Month(b.NgayBan) = {int.Parse(cboTKNVThang.Text)} and YEAR(b.NgayBan) = {int.Parse(txtTKNVNam.Text)} group by b.MaNV order by SUM(ctb.SoLuong) desc)");
                }
                if (radNVKhongBanDcSP.Checked == true)
                {
                    dtgvTKNV.DataSource = null;
                    dtgvTKNV.DataSource = nvBUS.TKNV($"select * from NhanVien where MaNV not in (select MaNV from BanHang where Month(NgayBan) = {int.Parse(cboTKNVThang.Text)} and Year(NgayBan) = {int.Parse(txtTKNVNam.Text)})");
                }
            } else if(radTKNVTheoNam.Checked == true)
            {
                if (radNVBanHang.Checked == true)
                {
                    dtgvTKNV.DataSource = null;
                    dtgvTKNV.DataSource = nvBUS.TKNV($"select b.MaNV, ctb.* from BanHang b left join ChiTietBan ctb on ctb.MaDonBan = b.MaDonBan where YEAR(b.NgayBan) = { int.Parse(txtTKNVNam.Text)}");
                }
                if (radNVBanNhieuSPNhat.Checked == true)
                {
                    dtgvTKNV.DataSource = null;
                    dtgvTKNV.DataSource = nvBUS.TKNV($"select * from NhanVien where MaNV in (select top 1 b.MaNV from BanHang b left join ChiTietBan ctb on ctb.MaDonBan = b.MaDonBan where YEAR(b.NgayBan) = {int.Parse(txtTKNVNam.Text)} group by b.MaNV order by SUM(ctb.SoLuong) desc)");
                }
                if (radNVKhongBanDcSP.Checked == true)
                {
                    dtgvTKNV.DataSource = null;
                    dtgvTKNV.DataSource = nvBUS.TKNV($"select * from NhanVien where MaNV not in (select MaNV from BanHang where Year(NgayBan) = {int.Parse(txtTKNVNam.Text)})");
                }
            }
        }

        private void cboRole_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cboRole.Text == "Quản lý")
            {
                txtMaNhanVien.Text = nvBUS.SinhMaQL();
            } else if(cboRole.Text == "Nhân viên")
            {
                txtMaNhanVien.Text = nvBUS.SinhMaNV();
            }
        }

        private void btnlogout_Click(object sender, EventArgs e)
        {
            this.Close();
            frmLogin fLogin = new frmLogin();
            fLogin.Show();
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn thoát không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        private void btnrepass_Click(object sender, EventArgs e)
        {
            if(lgBUS.CheckPass(LoginDAL.userID, txtoldpass.Text))
            {
                User u = new User();
                u.userID = LoginDAL.userID;
                u.pass = txtnewpass.Text;
                if(txtnewpass.Text == txtverifypass.Text)
                {
                    lgBUS.RePassWord(u);
                    MessageBox.Show("Đổi mật khẩu thành công");
                }else
                {
                    MessageBox.Show("Nhập lại mật khẩu mới chưa chính xác");
                }
            } else
            {
                MessageBox.Show("Mật khẩu cũ chưa chính xác");
            }
        }
    }
}
