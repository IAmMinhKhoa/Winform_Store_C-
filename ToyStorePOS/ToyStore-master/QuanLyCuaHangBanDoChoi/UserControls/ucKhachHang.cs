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
using DTO;
using QuanLyCuaHangBanDoChoi.Forms;
using System.Text.RegularExpressions;

namespace QuanLyCuaHangBanDoChoi.UserControls
{
    public partial class ucKhachHang : UserControl
    {
        public ucKhachHang()
        {
            InitializeComponent();
        }

        private void ucKhachHang_Load(object sender, EventArgs e)
        {
            LoadDgvKhachHang();
            cboGioiTinh.SelectedIndex = 0;
            btnXoa.Enabled = false;
            btnXoa.BackColor = Color.Gray;
            btnCapNhat.Enabled = false;
            btnCapNhat.BackColor = Color.Gray;
            btnThem.Enabled = true;
            btnThem.BackColor = Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(166)))), ((int)(((byte)(0)))));
        }

        private void LoadDgvKhachHang()
        {
            dgvKhachHang.DataSource = KhachHangBL.GetInstance.GetDanhSachKhachHang();
            dgvKhachHang.ClearSelection();
        }

        private void btnThemKH_Click(object sender, EventArgs e)  //Thêm khách hàng
        {
            txtDoanhSo.Text = "0";

            if (CheckControls())
            {
                if (txtTen.Text.Length < 50)
                {
                    if(txtSoDienThoai.Text.Length == 10)
                    {
                        if (int.TryParse(txtSoDienThoai.Text, out int parsedSoDienThoai))
                        {
                            if (txtDiaChi.Text.Length <= 200)
                            {
                                if(ValidateEmail(txtEmail.Text))
                                {
                                    KhachHangDTO khDTO = new KhachHangDTO();
                                    khDTO.tenkh = FormatStringInput(txtTen.Text);
                                    khDTO.diachi = FormatStringInput(txtDiaChi.Text);
                                    if (cboGioiTinh.Text == "Nam")
                                        khDTO.gioitinh = true;
                                    else
                                        khDTO.gioitinh = false;
                                    khDTO.ngaydangky = dateNgayDangKy.Value;
                                    khDTO.email = txtEmail.Text;
                                    khDTO.sdt = txtSoDienThoai.Text;


                                    if (KhachHangBL.GetInstance.ThemKhachHang(khDTO))
                                    {
                                        LoadDgvKhachHang();
                                        LamMoi();
                                        MessageBox.Show("Thêm KH thành công");
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
                                frm.lblThongBao.Text = "Địa chỉ tối đa 200 ký tự!";
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
                frm.lblThongBao.Text = "Bạn chưa nhập đủ thông tin khách hàng";
                frm.ShowDialog();
            }
        }

        private void LamMoi()
        {
            txtTen.Clear();
            txtEmail.Clear();
            txtDiaChi.Clear();
            txtDoanhSo.Clear();
            txtSoDienThoai.Clear();
            cboGioiTinh.SelectedIndex = 0;
            dateNgayDangKy.Value = DateTime.Now;
            btnXoa.Enabled = false;
            btnXoa.BackColor = Color.Gray;
            btnCapNhat.Enabled = false;
            btnCapNhat.BackColor = Color.Gray;
            btnThem.Enabled = true;
            btnThem.BackColor = Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(166)))), ((int)(((byte)(0)))));
            ResetColorControls();
        }

        private void ResetColorControls()
        {
            foreach (Control ctrl in pnlThongTinKhachHang.Controls)
            {
                if (ctrl is TextBox)
                {
                    if (ctrl.BackColor == Color.OrangeRed)
                    {
                        ctrl.BackColor = Color.White;
                    }
                }
            }
        }

        private bool CheckControls()
        {
            int r = 0;
            foreach (Control ctrl in pnlThongTinKhachHang.Controls)
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
            if (r == 0)
                return true;
            return false;
        }

        private void dateNgayDangKy_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dgvKhachHang_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvKhachHang.SelectedRows.Count == 1)
                {
                    ResetColorControls();
                    DataGridViewRow dr = dgvKhachHang.SelectedRows[0];
                    makh = int.Parse(dr.Cells["Mã KH"].Value.ToString().Trim());
                    txtTen.Text = dr.Cells["Tên KH"].Value.ToString().Trim();
                    cboGioiTinh.SelectedItem = dr.Cells["Giới Tính"].Value.ToString();
                    dateNgayDangKy.Value = Convert.ToDateTime(dr.Cells["Ngày Đăng Ký"].Value);
                    txtEmail.Text = dr.Cells["Email"].Value.ToString().Trim();
                    txtSoDienThoai.Text = dr.Cells["SĐT"].Value.ToString().Trim();
                    txtDiaChi.Text = dr.Cells["Địa Chỉ"].Value.ToString().Trim();
                    txtDoanhSo.Text = ConvertTien(Convert.ToDouble(dr.Cells["Doanh Số"].Value.ToString().Trim()));
                }
            }
            catch (Exception)
            {
                return;
            }
            btnXoa.Enabled = true;
            btnXoa.BackColor = Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(58)))), ((int)(((byte)(58)))));
            btnCapNhat.Enabled = true;
            btnCapNhat.BackColor = Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(145)))), ((int)(((byte)(249)))));
            btnThem.Enabled = false;
            btnThem.BackColor = Color.Gray;
        }
        private string ConvertTien(double gia)
        {
            string giaban = gia.ToString();
            string result = "";
            int d = 0;
            for (int i = giaban.Length - 1; i >= 0; i--)
            {
                d++;
                result += giaban[i];
                if (d == 3 && i != 0)
                {
                    result += ',';
                    d = 0;
                }
            }
            char[] charArray = result.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
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
                            if (txtDiaChi.Text.Length <= 200)
                            {
                                if(ValidateEmail(txtEmail.Text))
                                {
                                    KhachHangDTO khDTO = new KhachHangDTO();
                                    khDTO.makh = makh;
                                    khDTO.tenkh = FormatStringInput(txtTen.Text);
                                    khDTO.diachi = FormatStringInput(txtDiaChi.Text);
                                    if (cboGioiTinh.Text == "Nam")
                                        khDTO.gioitinh = true;
                                    else
                                        khDTO.gioitinh = false;
                                    khDTO.ngaydangky = dateNgayDangKy.Value;
                                    khDTO.email = txtEmail.Text;
                                    khDTO.sdt = txtSoDienThoai.Text;

                                    if (KhachHangBL.GetInstance.SuaThongTinKhachHang(khDTO))
                                    {
                                        LoadDgvKhachHang();
                                        LamMoi();
                                        MessageBox.Show("Cập nhật thành công");
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
                                frm.lblThongBao.Text = "Địa chỉ tối đa 200 ký tự!";
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
                frm.lblThongBao.Text = "Bạn chưa nhập đủ thông tin khách hàng";
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

        int makh = 0;  //Kiểm soát khách hàng đang đc chọn để cập nhật,xoá 
        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (KhachHangBL.GetInstance.XoaKhachHang(makh.ToString()))
            {
                LamMoi();
                LoadDgvKhachHang();
                MessageBox.Show("Xóa thành công");
            }
            else
            {
                frmThongBao frm = new frmThongBao();
                frm.lblThongBao.Text = "Xóa khách hàng thất bại!";
                frm.ShowDialog();
            }
        }

        private void btnLamMoiThongTin_Click(object sender, EventArgs e)
        {
            LamMoi();
        }

        private void txtTenNV_TextChanged(object sender, EventArgs e)  // Xử lý khi nhập vào textbox tìm kiếm tên KH
        {
            if (txtTenKH.Text != "")
            {
                dgvKhachHang.DataSource = KhachHangBL.GetInstance.GetDanhSachKhachHangTimKiem(txtTenKH.Text);
                dgvKhachHang.ClearSelection();
            }
            else
            {
                LoadDgvKhachHang();
            }
        }
        

        private void txtDoanhSo_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
