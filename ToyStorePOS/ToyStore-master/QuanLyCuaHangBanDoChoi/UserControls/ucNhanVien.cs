using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BusinessLogicLayer;
using System.IO;
using QuanLyCuaHangBanDoChoi.Forms;
using DTO;
using System.Text.RegularExpressions;

namespace QuanLyCuaHangBanDoChoi.UserControls
{
    public partial class ucNhanVien : UserControl
    {
        public ucNhanVien()
        {
            InitializeComponent();
        }

        private void ucNhanVien_Load(object sender, EventArgs e)
        {
            if (frmDangNhap.Quyen == 1)
            {
                LoadCboLoaiNV();
                LoadCboLocLoaiNV();
                LoadDataGridViewTheoBoLoc();
                cboGioiTinh.SelectedIndex = 0;
                btnSaThai.Enabled = false;
                btnSaThai.BackColor = Color.Gray;
                btnCapNhat.Enabled = false;
                btnCapNhat.BackColor = Color.Gray;
                btnThem.Enabled = true;
                btnThem.BackColor = Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(166)))), ((int)(((byte)(0)))));
            }
            else
            {
                dgvNhanVien.DataSource = NhanVienBL.GetInstance.GetDanhSachNhanVienTheoMa(frmDangNhap.TenDangNhap);
                panelBoLoc.Enabled = false;
                btnSaThai.Enabled = false;
                btnThem.Enabled = false;
                LoadCboLoaiNV();
                cboLoai.Enabled = false;
                cboGioiTinh.SelectedIndex = 0;
                txtTen.Enabled = false;
                cboGioiTinh.Enabled = false;
                dateNgaySinh.Enabled = false;
            }
        }

        private void LoadDataGridViewTheoBoLoc()
        {
            dgvNhanVien.DataSource = NhanVienBL.GetInstance.GetDanhSachNhanVienTheoBoLoc(txtTenNV.Text.Trim(), cboLocLoaiNhanVien.SelectedValue.ToString().Trim());
            dgvNhanVien.ClearSelection();
        }

        private void LoadCboLocLoaiNV()
        {
            DataTable dt = LoaiNhanVienBL.GetInstance.GetDanhSachLoaiNhanVien();
            DataRow dr = dt.NewRow();
            dr["Mã Loại NV"] = "-1";
            dr["Tên Loại NV"] = "Tất cả";
            dt.Rows.Add(dr);
            cboLocLoaiNhanVien.DataSource = dt;
            cboLocLoaiNhanVien.DisplayMember = "Tên Loại NV";
            cboLocLoaiNhanVien.ValueMember = "Mã Loại NV";
            cboLocLoaiNhanVien.SelectedIndex = cboLocLoaiNhanVien.Items.Count - 1;
        }

        private void LoadCboGioiTinh()
        {
        }

        private void LoadCboLoaiNV()
        {
            cboLoai.DataSource = LoaiNhanVienBL.GetInstance.GetDanhSachLoaiNhanVien();
            cboLoai.DisplayMember = "Tên Loại NV";
            cboLoai.ValueMember = "Mã Loại NV";
        }
        int manv = 0;
        private void dgvNhanVien_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvNhanVien.SelectedRows.Count == 1)
                {
                    ResetColorControls();
                    DataGridViewRow dr = dgvNhanVien.SelectedRows[0];
                    manv = int.Parse(dr.Cells["Mã NV"].Value.ToString().Trim());
                    txtTen.Text = dr.Cells["Tên NV"].Value.ToString().Trim();
                    cboLoai.SelectedValue = dr.Cells["Mã Loại NV"].Value.ToString();
                    cboGioiTinh.SelectedItem = dr.Cells["Giới Tính"].Value.ToString();
                    dateNgaySinh.Value = Convert.ToDateTime(dr.Cells["Ngày Sinh"].Value);
                    txtEmail.Text = dr.Cells["Email"].Value.ToString().Trim();
                    txtSoDienThoai.Text = dr.Cells["SĐT"].Value.ToString().Trim();
                    MemoryStream ms = new MemoryStream((byte[])dgvNhanVien.CurrentRow.Cells["Hình Ảnh"].Value‌​);
                    picHinhAnh.Image = Image.FromStream(ms);


                    txtMatKhau.Text = "It's a secret bro";
                    txtMatKhau.Enabled = false;
                }
            }
            catch (Exception)
            {
                return;
            }
            btnSaThai.Enabled = true;
            btnSaThai.BackColor = Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(58)))), ((int)(((byte)(58)))));
            btnCapNhat.Enabled = true;
            btnCapNhat.BackColor = Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(145)))), ((int)(((byte)(249)))));
            btnThem.Enabled = false;
            btnThem.BackColor = Color.Gray;
        }

        private void ResetColorControls()
        {
            foreach (Control ctrl in pnlThongTinNhanVien.Controls)
            {
                if (ctrl is TextBox)
                {
                    if (ctrl.BackColor == Color.OrangeRed)
                    {
                        ctrl.BackColor = Color.White;
                    }
                }
            }
            if (picHinhAnh.BackColor == Color.OrangeRed)
            {
                picHinhAnh.BackColor = Color.White;
            }
        }

        private void btnThemNV_Click(object sender, EventArgs e)
        {
            if (CheckControls())
            {
                if (txtTen.Text.Length < 50)
                {
                    if (txtMatKhau.Text.Length > 5)
                    {
                        if(txtSoDienThoai.Text.Length == 10)
                        {
                            if (int.TryParse(txtSoDienThoai.Text, out int parsedSoDienThoai))
                            {
                                if(ValidateEmail(txtEmail.Text))
                                {
                                    if (CheckDate())
                                    {
                                        NhanVienDTO nvDTO = new NhanVienDTO();
                                        nvDTO.tennv = FormatStringInput(txtTen.Text);
                                        nvDTO.matkhau = txtMatKhau.Text;
                                        nvDTO.maloainv = int.Parse(cboLoai.SelectedValue.ToString().Trim());
                                        if (cboGioiTinh.Text == "Nam")
                                            nvDTO.gioitinh = true;
                                        else
                                            nvDTO.gioitinh = false;
                                        nvDTO.ngaysinh = dateNgaySinh.Value;
                                        nvDTO.email = txtEmail.Text;
                                        nvDTO.sdt = txtSoDienThoai.Text;
                                        Image img = picHinhAnh.Image;
                                        nvDTO.hinhanh = ImageToByteArray(img);

                                        cboLocLoaiNhanVien.SelectedIndex = cboLoai.SelectedIndex;

                                        if (NhanVienBL.GetInstance.ThemNhanVien(nvDTO))
                                        {
                                            LoadDataGridViewTheoBoLoc();
                                            LamMoi();
                                            MessageBox.Show("Vừa thêm 1 nole vào công ty");
                                        }
                                    }
                                    else
                                    {
                                        frmThongBao frm = new frmThongBao();
                                        frm.lblThongBao.Text = "Mã nhân viên đã tồn tại";
                                        frm.ShowDialog();
                                    }
                                }
                                else
                                {
                                    frmThongBao frm = new frmThongBao();
                                    frm.lblThongBao.Text = "Định dạng Email không hợp lệ!";
                                    frm.ShowDialog();
                                }
                            }
                            else
                            {
                                frmThongBao frm = new frmThongBao();
                                frm.lblThongBao.Text = "Số điện thoại phải là số!";
                                frm.ShowDialog();
                            }
                        }
                        else
                        {
                            frmThongBao frm = new frmThongBao();
                            frm.lblThongBao.Text = "Số điện thoại phải 10 số!";
                            frm.ShowDialog();
                        }    
                    }
                    else
                    {
                        frmThongBao frm = new frmThongBao();
                        frm.lblThongBao.Text = "MẬT KHẨU TRÊN 5 KÍ TỰ BRO ?";
                        frm.ShowDialog();
                    }

                }
                else
                {
                    frmThongBao frm = new frmThongBao();
                    frm.lblThongBao.Text = "Họ tên chỉ được tối đa 50 ký tự!";
                    frm.ShowDialog();
                }
            }
            else
            {
                frmThongBao frm = new frmThongBao();
                frm.lblThongBao.Text = "Bạn chưa nhập đủ thông tin nhân viên";
                frm.ShowDialog();
            }
        }

        private void LamMoi()
        {
            txtMatKhau.Clear();
            txtMatKhau.Enabled = true;

            txtTen.Clear();
            txtEmail.Clear();
            txtSoDienThoai.Clear();
            cboGioiTinh.SelectedIndex = 0;
            if (cboLoai.Items.Count > 0)
                cboLoai.SelectedIndex = 0;
            dateNgaySinh.Value = DateTime.Now;
            picHinhAnh.Image = null;
            btnSaThai.Enabled = false;
            btnSaThai.BackColor = Color.Gray;
            btnCapNhat.Enabled = false;
            btnCapNhat.BackColor = Color.Gray;
            btnThem.Enabled = true;
            btnThem.BackColor = Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(166)))), ((int)(((byte)(0)))));
            ResetColorControls();
        }

        private byte[] ImageToByteArray(Image imageIn)
        {
            using (var ms = new MemoryStream())
            {
                imageIn.Save(ms, imageIn.RawFormat);
                return ms.ToArray();
            }
        }

        private bool CheckDate()
        {
            if (dateNgaySinh.Value >= DateTime.Now)
            {
                return false;
            }
            return true;
        }

        private bool CheckControls()
        {
            int r = 0;
            foreach (Control ctrl in pnlThongTinNhanVien.Controls)
            {
                if (ctrl is TextBox)
                {
                    if (ctrl.Text == "")
                    {
                        ctrl.BackColor = Color.OrangeRed;
                        r = 1;
                    }
                }
            }
            if (picHinhAnh.Image == null)
            {
                r = 1;
                picHinhAnh.BackColor = Color.OrangeRed;
            }
            if (r == 0)
                return true;
            return false;
        }

        private void picHinhAnh_Click(object sender, EventArgs e)
        {
            if (picHinhAnh.BackColor == Color.OrangeRed)
            {
                picHinhAnh.BackColor = Color.White;
            }
            frmLoadImage frm = new frmLoadImage();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                picHinhAnh.Image = frm.img;
            }
        }

        private void btnLamMoiThongTin_Click(object sender, EventArgs e)
        {
            LamMoi();
        }

        private void btnThoiViec_Click(object sender, EventArgs e)
        {
            if (NhanVienBL.GetInstance.ThoiViecNhanVien(manv.ToString()))
            {
                LamMoi();
                LoadDataGridViewTheoBoLoc();
                MessageBox.Show("Đuổi việc thằng nhóc này thành công");
            }
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            if (CheckControls())
            {
                if (txtTen.Text.Length < 50)
                {
                    if(txtSoDienThoai.Text.Length == 10) 
                    {
                        if (int.TryParse(txtSoDienThoai.Text, out int parsedSoDienThoai))
                        {
                            if (ValidateEmail(txtEmail.Text))
                            {
                                if (CheckDate())
                                {
                                    NhanVienDTO nvDTO = new NhanVienDTO();
                                    nvDTO.manv = manv;
                                    nvDTO.tennv = FormatStringInput(txtTen.Text);
                                    nvDTO.maloainv = int.Parse(cboLoai.SelectedValue.ToString().Trim());
                             
                                    if (cboGioiTinh.Text == "Nam")
                                        nvDTO.gioitinh = true;
                                    else
                                        nvDTO.gioitinh = false;
                                    nvDTO.ngaysinh = dateNgaySinh.Value;
                                    nvDTO.email = txtEmail.Text;
                                    nvDTO.sdt = txtSoDienThoai.Text;
                                    Image img = picHinhAnh.Image;
                                    nvDTO.hinhanh = ImageToByteArray(img);
                                    if (frmDangNhap.Quyen == 1)
                                        cboLocLoaiNhanVien.SelectedIndex = cboLoai.SelectedIndex;

                                    if (NhanVienBL.GetInstance.SuaThongTinNhanVien(nvDTO))
                                    {
                                        if (frmDangNhap.Quyen == 1)
                                            LoadDataGridViewTheoBoLoc();
                                        LamMoi();
                                        MessageBox.Show("Cập nhật thành công");
                                    }
                                }
                                else
                                {
                                    frmThongBao frm = new frmThongBao();
                                    frm.lblThongBao.Text = "Ngày sinh không hợp lệ!";
                                    frm.ShowDialog();
                                }
                            }
                            else
                            {
                                frmThongBao frm = new frmThongBao();
                                frm.lblThongBao.Text = "Định dạng Email không hợp lệ!";
                                frm.ShowDialog();
                            }
                        }
                        else
                        {
                            frmThongBao frm = new frmThongBao();
                            frm.lblThongBao.Text = "Số điện thoại phải là số!";
                            frm.ShowDialog();
                        }
                    }
                    else
                    {
                        frmThongBao frm = new frmThongBao();
                        frm.lblThongBao.Text = "Số điện thoại phải 10 số!";
                        frm.ShowDialog();
                    }        
                }
                else
                {
                    frmThongBao frm = new frmThongBao();
                    frm.lblThongBao.Text = "Họ tên chỉ được tối đa 50 ký tự!";
                    frm.ShowDialog();
                }
            }
            else
            {
                frmThongBao frm = new frmThongBao();
                frm.lblThongBao.Text = "Bạn chưa nhập đủ thông tin nhân viên";
                frm.ShowDialog();
            }
        }

        private bool ValidateEmail(string email)
        {
            // Biểu thức chính quy cho định dạng email
            string emailPattern = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";

            // Kiểm tra định dạng email
            if (Regex.IsMatch(email, emailPattern))
            {
                return true; // Email hợp lệ
            }
            else
            {
                return false; // Email không hợp lệ
            }
        }

        private string FormatStringInput(string name)
        {
            // Chuyển tất cả các ký tự thành chữ thường
            name = name.ToLower();

            // Chuyển đổi các ký tự đầu của từ thành chữ hoa
            name = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(name);

            // Loại bỏ các khoảng trắng không cần thiết
            name = string.Join(" ", name.Split(default(string[]), StringSplitOptions.RemoveEmptyEntries));

            return name;
        }

        private void txtTen_Click(object sender, EventArgs e)
        {
            if (txtTen.BackColor == Color.OrangeRed)
            {
                txtTen.BackColor = Color.White;
            }
        }

        private void cboLoai_Click(object sender, EventArgs e)
        {
            if (cboLoai.BackColor == Color.OrangeRed)
            {
                cboLoai.BackColor = Color.White;
            }
        }

        private void cboGioiTinh_Click(object sender, EventArgs e)
        {
            if (cboGioiTinh.BackColor == Color.OrangeRed)
            {
                cboGioiTinh.BackColor = Color.White;
            }
        }

        private void txtEmail_Click(object sender, EventArgs e)
        {
            if (txtEmail.BackColor == Color.OrangeRed)
            {
                txtEmail.BackColor = Color.White;
            }
        }

        private void txtSoDienThoai_Click(object sender, EventArgs e)
        {
            if (txtSoDienThoai.BackColor == Color.OrangeRed)
            {
                txtSoDienThoai.BackColor = Color.White;
            }
        }

        private void btnApDung_Click(object sender, EventArgs e)
        {
            LoadDataGridViewTheoBoLoc();
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtTenNV.Text = "";
            cboLocLoaiNhanVien.SelectedIndex = cboLocLoaiNhanVien.Items.Count - 1;
        }
        

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void dgvNhanVien_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
