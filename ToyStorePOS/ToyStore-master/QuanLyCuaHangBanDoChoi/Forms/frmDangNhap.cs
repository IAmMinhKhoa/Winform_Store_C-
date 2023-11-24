using BusinessLogicLayer;
using System;
using System.Drawing;
using System.Windows.Forms;
using Tulpep.NotificationWindow;

namespace QuanLyCuaHangBanDoChoi.Forms
{
    public partial class frmDangNhap : Form
    {


        public frmDangNhap()
        {
            InitializeComponent();

        }

        public static int Quyen;
        public static int TenDangNhap;

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void frmDangNhap_Load(object sender, EventArgs e)
        {
            txtMatKhau.PasswordChar = '●';

            txtTenDangNhap.Visible = true;
            txtMatKhau.Visible = true;
            btnShowPass.Visible = true;
            txtTenDangNhap.Focus();



            lbDangNhap.ForeColor = Color.FromArgb(240, 240, 240);
            lbMatKhau.ForeColor = Color.FromArgb(240, 240, 240);
        }
        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            XuLyDangNhap();
        }
        
        private void XuLyDangNhap()
        {
            Cursor = Cursors.AppStarting;
            if (TaiKhoanBL.GetInstance.CheckLogin(txtTenDangNhap.Text, txtMatKhau.Text) == true)
            {
                btnDangNhap.BackColor = Color.FromArgb(0, 100, 0);
                TenDangNhap = int.Parse(txtTenDangNhap.Text);
                Quyen = TaiKhoanBL.GetInstance.GetMaQuyen(int.Parse(txtTenDangNhap.Text));
                txtMatKhau.Text = "";
                txtTenDangNhap.Text = "";
                Cursor = Cursors.Default;
             
                frmChinh frm = new frmChinh();
                frm.Show();
                this.Hide();
            }
            else
            {
                Cursor = Cursors.Default;
                frmThongBao frm = new frmThongBao();
                frm.lblThongBao.Text = "Tên đăng nhập hoặc mật khẩu không đúng!\nVui lòng nhập lại...";
                frm.Show();
            }
        }

        private void txtTenDangNhap_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                XuLyDangNhap();
            }
        }

        private void txtMatKhau_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                XuLyDangNhap();
            }
        }

      

        private void btnShowPass_Click(object sender, EventArgs e)
        {
            if (btnShowPass.ImageIndex == 0)
            {
                btnShowPass.ImageIndex = 1;
                txtMatKhau.Focus();
            }
            else
            {
                btnShowPass.ImageIndex = 0;
                txtMatKhau.Focus();
            }
            if (txtMatKhau.PasswordChar == '●')
            {
                txtMatKhau.PasswordChar = '\0';
            }
            else
            {
                txtMatKhau.PasswordChar = '●';
            }
        }

        private void btnShowPass_MouseHover(object sender, EventArgs e)
        {
            if (btnShowPass.ImageIndex == 0)
                toolTip1.Show("Hiện mật khẩu", btnShowPass);
            else
                toolTip1.Show("Ẩn mật khẩu", btnShowPass);
        }

        private void txtMatKhau_TextChanged(object sender, EventArgs e)
        {
            if (txtTenDangNhap.Text != "" && txtMatKhau.Text != "")
            {
                btnDangNhap.BackColor = Color.FromArgb(255, 122, 0);
            }
            else
            {
                btnDangNhap.BackColor = Color.DimGray;
            }
        }

        private void txtTenDangNhap_TextChanged(object sender, EventArgs e)
        {
            if (txtTenDangNhap.Text != "" && txtMatKhau.Text != "")
            {
                btnDangNhap.BackColor = Color.FromArgb(255, 122, 0);
            }
            else
            {
                btnDangNhap.BackColor = Color.DimGray;
            }
        }
    }
}

