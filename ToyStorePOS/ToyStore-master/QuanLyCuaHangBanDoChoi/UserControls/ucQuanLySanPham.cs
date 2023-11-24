using System;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.IO;
using BusinessLogicLayer;
using QuanLyCuaHangBanDoChoi.Forms;
using DTO;
using System.Xml;

namespace QuanLyCuaHangBanDoChoi.UserControls
{
    public partial class ucQuanLySanPham : UserControl
    {
        public ucQuanLySanPham()
        {
            InitializeComponent();
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void ucSanPham_Load(object sender, EventArgs e)
        {
            LoadCboLoaiSP();

            LoadCboDVT();

            LoadCboNCC();

            LoadCboLocLoaiSP();

            LoadCboLocNCC();

            LoadDataGridViewTheoBoLoc();


            btnNgungKinhDoanh.Enabled = false;
            btnNgungKinhDoanh.BackColor = Color.Gray;

            btnCapNhatSP.Enabled = false;
            btnCapNhatSP.BackColor = Color.Gray;

        }

        private void LoadCboLocLoaiSP()
        {
            DataTable dt = LoaiSanPhamBL.GetInstance.GetDanhSachLoaiSanPham();
            DataRow dr = dt.NewRow();
            dr["Mã Loại SP"] = "-1";
            dr["Tên Loại SP"] = "Tất cả";
            dt.Rows.Add(dr);
            cboLocLoaiSP.DataSource = dt;
            cboLocLoaiSP.DisplayMember = "Tên Loại SP";
            cboLocLoaiSP.ValueMember = "Mã Loại SP";
            cboLocLoaiSP.SelectedIndex = cboLocLoaiSP.Items.Count - 1;
        }

        private void LoadCboDVT()
        {
            cboDVT.SelectedIndex = 0;
        }

        private void LoadCboLoaiSP()
        {
            cboLoai.DataSource = LoaiSanPhamBL.GetInstance.GetDanhSachLoaiSanPham();
            cboLoai.DisplayMember = "Tên Loại SP";
            cboLoai.ValueMember = "Mã Loại SP";
        }

        private void LoadDataGridView()
        {
            DataTable dt = SanPhamBL.GetInstance.GetDanhSachSanPham();
            dgvSanPham.DataSource = dt;
        }

        int masp = 0;
        private void dgvSanPham_Click(object sender, EventArgs e)
        {
            btnNgungKinhDoanh.Enabled = true;
            btnNgungKinhDoanh.BackColor = Color.FromArgb(250, 58, 58);

            btnCapNhatSP.Enabled = true;
            btnCapNhatSP.BackColor = Color.FromArgb(33, 166, 0);

            try
            {
                if (dgvSanPham.SelectedRows.Count == 1)
                {
                    ResetColorControls();
                    DataGridViewRow dr = dgvSanPham.SelectedRows[0];
                    masp = int.Parse(dr.Cells["Mã SP"].Value.ToString().Trim());
                    txtTen.Text = dr.Cells["Tên SP"].Value.ToString().Trim();
                    cboLoai.SelectedValue = dr.Cells["Mã Loại SP"].Value.ToString();
                    cboNCC.SelectedValue = dr.Cells["Mã NCC"].Value.ToString();
                    cboDVT.SelectedItem = dr.Cells["ĐVT"].Value.ToString();
                    dateNgaySX.Value = Convert.ToDateTime(dr.Cells["Ngày SX"].Value);
                    dateNgayHetHan.Value = Convert.ToDateTime(dr.Cells["Ngày Hết Hạn"].Value);
                    txtGiaNhap.Text = dr.Cells["Đơn Giá Nhập"].Value.ToString().Trim();
                    txtLoiNhuan.Text = dr.Cells["Lợi Nhuận"].Value.ToString().Trim();
                    txtGiaBan.Text = dr.Cells["Đơn Giá Bán"].Value.ToString().Trim();
                    txtSoLuong.Text = dr.Cells["Số Lượng"].Value.ToString().Trim();
                    txtKhuyenMai.Text = dr.Cells["Khuyến Mãi"].Value.ToString().Trim();
                    MemoryStream ms = new MemoryStream((byte[])dgvSanPham.CurrentRow.Cells["Hình Ảnh"].Value‌​);
                    picHinhAnh.Image = Image.FromStream(ms);
                }
            }
            catch (Exception)
            {
                return;
            }
        }
 
       

        private void btnLamMoiThongTin_Click(object sender, EventArgs e)
        {
            LamMoi();
        }

        private void LamMoi()
        {
            btnNgungKinhDoanh.Enabled = false;
            btnNgungKinhDoanh.BackColor = Color.Gray;

            btnCapNhatSP.Enabled = false;
            btnCapNhatSP.BackColor = Color.Gray;


            txtTen.Clear();
            txtSoLuong.Clear();
            txtKhuyenMai.Clear();
            txtGiaBan.Clear();
            txtSoLuong.Clear();
            txtGiaNhap.Clear();
            txtLoiNhuan.Clear();
            cboDVT.SelectedIndex = 0;
            if (cboLoai.Items.Count > 0)
                cboLoai.SelectedIndex = 0;
            if (cboNCC.Items.Count > 0)
                cboNCC.SelectedIndex = 0;
            dateNgaySX.Value = DateTime.Now;
            dateNgayHetHan.Value = DateTime.Now;
            picHinhAnh.Image = null;
            ResetColorControls();
        }

        private void ResetColorControls()
        {
            foreach (Control ctrl in pnlThongTinSanPham.Controls)
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

        private void btnXoaSP_Click(object sender, EventArgs e)
        {
            if (CheckControls())
            {
                if (SanPhamBL.GetInstance.NgungKinhDoanhSanPham(masp.ToString()))
                {
                    LamMoi();
                    LoadDataGridViewTheoBoLoc();
                    MessageBox.Show("Ngừng bán cái sản phẩm này");
                }
            }
            else
            {
                frmThongBao frm = new frmThongBao();
                frm.lblThongBao.Text = "Bạn chưa chọn sản phẩm cần ngừng kinh doanh!";
                frm.ShowDialog();
            }
        }
        int i = 0;

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
        public byte[] ImageToByteArray(Image imageIn)
        {
            using (var ms = new MemoryStream())
            {
                imageIn.Save(ms, imageIn.RawFormat);
                return ms.ToArray();
            }
        }
        
        private void btnThemSP_Click(object sender, EventArgs e)
        {
            if (CheckControls())
            {
                if (txtTen.Text.Length < 200)
                {
                    if (int.Parse(txtKhuyenMai.Text) < 100)
                    {
                        if (int.Parse(txtLoiNhuan.Text) > 0)
                        {
                            if (CheckDate())
                            {
                                
                                SanPhamDTO spDTO = new SanPhamDTO();
                                spDTO.tensp = txtTen.Text;
                                spDTO.maloaisp = cboLoai.SelectedValue.ToString().Trim();
                                spDTO.mancc = int.Parse(cboNCC.SelectedValue.ToString().Trim());
                                spDTO.dvt = cboDVT.SelectedItem.ToString();
                                spDTO.ngaysx = dateNgaySX.Value;
                                spDTO.ngayhethan = dateNgayHetHan.Value;
                                spDTO.gianhap = decimal.Parse(txtGiaNhap.Text);
                                spDTO.loinhuan = int.Parse(txtLoiNhuan.Text);

                                spDTO.giaban = decimal.Parse(txtGiaBan.Text);
                          
                                spDTO.khuyenmai = int.Parse(txtKhuyenMai.Text);
                                Image img = picHinhAnh.Image;
                                spDTO.hinhanh = ImageToByteArray(img);

                                cboLocLoaiSP.SelectedIndex = cboLoai.SelectedIndex;
                                cboLocNCC.SelectedIndex = cboNCC.SelectedIndex;

                                if (SanPhamBL.GetInstance.ThemSanPham(spDTO))
                                {
                                    MessageBox.Show("Thêm sản phẩm thành công");
                                    LoadDataGridViewTheoBoLoc();
                                    LamMoi();
                                }
                            }
                        }
                        else
                        {
                            frmThongBao frm = new frmThongBao();
                            frm.lblThongBao.Text = "Lợi nhuận phải lớn hơn 0%!";
                            frm.ShowDialog();
                        }
                    }
                    else
                    {
                        frmThongBao frm = new frmThongBao();
                        frm.lblThongBao.Text = "Khuyến mãi phải nhỏ hơn 100%!";
                        frm.ShowDialog();
                    }
                }
                else
                {
                    frmThongBao frm = new frmThongBao();
                    frm.lblThongBao.Text = "Tên sản phẩm tối đa 200 ký tự!";
                    frm.ShowDialog();
                }
            }
            else
            {
                frmThongBao frm = new frmThongBao();
                frm.lblThongBao.Text = "Bạn chưa nhập đủ thông tin sản phẩm";
                frm.ShowDialog();
            }
        }

        private bool CheckControls()
        {
            int r = 0;
            foreach (Control ctrl in pnlThongTinSanPham.Controls)
            {
                if (ctrl is TextBox)
                {
                    if (ctrl.Text == "" && ctrl.Name != "txtSoLuong" && ctrl.Name != "txtGiaBan")
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

        private void btnCapNhatSP_Click(object sender, EventArgs e)
        {
            if (CheckControls())
            {
                if (txtTen.Text.Length < 200)
                {
                    if (int.Parse(txtKhuyenMai.Text) < 100)
                    {
                        if (int.Parse(txtLoiNhuan.Text) > 0)
                        {
                            if (CheckDate())
                            {
                                SanPhamDTO spDTO = new SanPhamDTO();
                                spDTO.masp = masp;
                                spDTO.tensp = txtTen.Text;
                                spDTO.ngaysx = dateNgaySX.Value;
                                spDTO.ngayhethan = dateNgayHetHan.Value;
                                spDTO.loinhuan = int.Parse(txtLoiNhuan.Text);
                                spDTO.gianhap = decimal.Parse(txtGiaNhap.Text);
                                spDTO.giaban = decimal.Parse(txtGiaBan.Text);
                                spDTO.khuyenmai = int.Parse(txtKhuyenMai.Text);
                                Image img = picHinhAnh.Image;
                                spDTO.hinhanh = ImageToByteArray(img);

                                cboLocLoaiSP.SelectedIndex = cboLoai.SelectedIndex;
                                cboLocNCC.SelectedIndex = cboNCC.SelectedIndex;

                                if (SanPhamBL.GetInstance.SuaSanPham(spDTO))
                                {
                                    LoadDataGridViewTheoBoLoc();
                                    LamMoi();
                                    MessageBox.Show("Cập nhật thành công");
                                }
                                else
                                {
                                    MessageBox.Show("Cập nhật thất bại");
                                }
                            }
                            else
                            {
                                frmThongBao frm = new frmThongBao();
                                frm.lblThongBao.Text = "Ngày hết hạn phải sau ngày sản xuất";
                                frm.ShowDialog();
                            }
                        }
                        else
                        {
                            frmThongBao frm = new frmThongBao();
                            frm.lblThongBao.Text = "Lợi nhuận phải lớn hơn 0%!";
                            frm.ShowDialog();
                        }
                    }
                    else
                    {
                        frmThongBao frm = new frmThongBao();
                        frm.lblThongBao.Text = "Khuyến mãi phải nhỏ hơn 100%!";
                        frm.ShowDialog();
                    }
                }
                else
                {
                    frmThongBao frm = new frmThongBao();
                    frm.lblThongBao.Text = "Tên sản phẩm tối đa 200 ký tự!";
                    frm.ShowDialog();
                }
            }
            else
            {
                frmThongBao frm = new frmThongBao();
                frm.lblThongBao.Text = "Bạn chưa nhập đủ thông tin sản phẩm";
                frm.ShowDialog();
            }
        }

        private bool CheckDate()
        {
            if (dateNgaySX.Value >= dateNgayHetHan.Value)
            {
                MessageBox.Show("WTF BRO? NGÀY SẢN XUẤT SAO LẠI LỚN HƠN HOẶC  BẰNG NGÀY BÁN VẬY ??");
                return false;
            }
            return true;
        }

        private void txtGiaBan_TextChanged(object sender, EventArgs e)
        {
            if (txtGiaBan.Text != "")
            {

               /* try
                {
                    System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("en-US");
                    decimal value = decimal.Parse(txtGiaBan.Text, System.Globalization.NumberStyles.AllowThousands);
                    txtGiaBan.Text = String.Format(culture, "{0:N0}", value);
                    txtGiaBan.Select(txtGiaBan.Text.Length, 0);
                }
                catch (Exception)
                {

                    MessageBox.Show("SAI ĐỊNH DẠNG TIỀN TỆ  HOẶC BỘ NHỚ ĐÃ HẾT");

                }*/

                /* string input = txtGiaBan.Text;

                 if (input.Length > 28)
                 {
                     // Chuỗi nhập vào quá dài, hiển thị thông báo lỗi
                     MessageBox.Show("Giá trị không hợp lệ. Vui lòng nhập số.");
                     txtGiaBan.Clear();
                 }
                 else
                 {
                     if (!decimal.TryParse(input, out decimal dema))
                     {
                         // Hiển thị thông báo lỗi
                         MessageBox.Show("Giá trị không hợp lệ. Vui lòng nhập số.");
                         txtGiaBan.Clear();
                     }
                     else
                     {
                         // Giá trị hợp lệ, tiếp tục xử lý
                         // ...
                     }
                 }*/

            }
        }

        private void txtGiaMua_KeyPress(object sender, KeyPressEventArgs e)
        {
            /*if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
        (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }*/
        }

        private void txtGiaBan_KeyPress(object sender, KeyPressEventArgs e)
        {
           /* if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
        (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }*/
        }

        private void btnApDung_Click(object sender, EventArgs e)
        {
            LoadDataGridViewTheoBoLoc();
        }

        private void LoadDataGridViewTheoBoLoc()
        {
            dgvSanPham.DataSource = SanPhamBL.GetInstance.GetDanhSachSanPhamTheoBoLoc(txtTenSP.Text.Trim(), cboLocLoaiSP.SelectedValue.ToString().Trim(), cboLocNCC.SelectedValue.ToString().Trim());
           
            dgvSanPham.ClearSelection();
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtTenSP.Text = "";
            cboLocLoaiSP.SelectedIndex = cboLocLoaiSP.Items.Count - 1;
            cboLocNCC.SelectedIndex = cboLocNCC.Items.Count - 1;
        }

        private void txtMa_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtTen_TextChanged(object sender, EventArgs e)
        {

        }

        private void cboLoai_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cboDVT_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dateNgaySX_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dateNgayHetHan_ValueChanged(object sender, EventArgs e)
        {

        }

        private void txtTen_Click(object sender, EventArgs e)
        {
            if (txtTen.BackColor == Color.OrangeRed)
            {
                txtTen.BackColor = Color.White;
            }
        }

        private void txtSoLuong_Click(object sender, EventArgs e)
        {
            if (txtSoLuong.BackColor == Color.OrangeRed)
            {
                txtSoLuong.BackColor = Color.White;
            }
        }

        private void txtKhuyenMai_Click(object sender, EventArgs e)
        {





            if (txtKhuyenMai.BackColor == Color.OrangeRed)
            {
                txtKhuyenMai.BackColor = Color.White;
            }
        }

        private void pnlLoaiSP_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pnlLoaiSP_Click(object sender, EventArgs e)
        {
            frmLoaiSanPham frm = new frmLoaiSanPham();
            frm.ShowDialog();
            if (frm.b)
            {
                LoadCboLoaiSP();
                LoadCboLocLoaiSP();
                LoadDataGridViewTheoBoLoc();
            }
        }

     
        private void LoadCboLocNCC()
        {
            DataTable dt = NCCBL.GetInstance.GetDanhSachNCC();
            DataRow dr = dt.NewRow();
            dr["Mã NCC"] = "-1";
            dr["Tên NCC"] = "Tất cả";
            dt.Rows.Add(dr);
            cboLocNCC.DataSource = dt;
            cboLocNCC.DisplayMember = "Tên NCC";
            cboLocNCC.ValueMember = "Mã NCC";
            cboLocNCC.SelectedIndex = cboLocNCC.Items.Count - 1;
        }

        private void LoadCboNCC()
        {
            cboNCC.DataSource = NCCBL.GetInstance.GetDanhSachNCC();
            cboNCC.DisplayMember = "Tên NCC";
            cboNCC.ValueMember = "Mã NCC";
        }

        private void pnlNCC_Click(object sender, EventArgs e)
        {
            frmNCC frm = new frmNCC();
            frm.ShowDialog();
            if (frm.b)
            {
                LoadCboNCC();
                LoadCboLocNCC();
            }
        }

        private void label20_Click(object sender, EventArgs e)
        {

        }

        private void label22_Click(object sender, EventArgs e)
        {

        }

        private void txtGiaNhap_TextChanged(object sender, EventArgs e)
        {
            if (txtGiaNhap.Text != "")
            {


                //decimal value = decimal.Parse(txtGiaNhap.Text, System.Globalization.NumberStyles.AllowThousands);
                //txtGiaNhap.Text = string.Format("{0:N0}", value);
                //txtGiaNhap.Select(txtGiaNhap.Text.Length, 0);

                if (!decimal.TryParse(txtGiaNhap.Text, out decimal dema))
                {
                    // Hiển thị thông báo lỗi
                    MessageBox.Show("Giá trị không hợp lệ. Vui lòng nhập số. <from txtgianhap>");
                    txtGiaNhap.Clear();
                }
                else
                {
                    if (txtLoiNhuan.Text != "")
                    {
                        int n = int.Parse(txtGiaNhap.Text) + ((int.Parse(txtGiaNhap.Text) * int.Parse(txtLoiNhuan.Text) / 100));
                        txtGiaBan.Text = n.ToString();
                    }
                    else
                    {
                        txtGiaBan.Text = txtGiaNhap.Text;
                    }
                }
               


            }
        }

        private void txtLoiNhuan_TextChanged(object sender, EventArgs e)
        {
            
            if (txtLoiNhuan.Text == "")
            {
                txtGiaBan.Clear();
                return;
            }
            if (txtGiaNhap.Text != "")
            {
                if (!int.TryParse(txtLoiNhuan.Text, out int dema))
                {
                    // Hiển thị thông báo lỗi
                    MessageBox.Show("Giá trị không hợp lệ. Vui lòng nhập số. <from txtloinhuan>");
                    txtLoiNhuan.Clear();
                }
                else
                {
                   // Console.WriteLine(int.Parse(txtGiaNhap.Text.Replace(".", "")));
                  // Console.WriteLine(int.Parse(txtLoiNhuan.Text.Replace(",", "")));
                    int n = int.Parse(txtGiaNhap.Text) + ((int.Parse(txtGiaNhap.Text) * int.Parse(txtLoiNhuan.Text) / 100));
                    //int n = int.Parse(txtGiaNhap.Text) + ((int.Parse(txtGiaNhap.Text) * int.Parse(txtLoiNhuan.Text) / 100));
                    txtGiaBan.Text = n.ToString();
                    Console.WriteLine(txtGiaBan.Text+"/"+ txtGiaNhap.Text);
                }
            }
            else
            {
                MessageBox.Show("VUI LÒNG NHẬP GIÁ NHẬP");
                txtLoiNhuan.Clear();
            }
           






            /*if (int.Parse(txtLoiNhuan.Text) > 0 && (int.TryParse(txtLoiNhuan.Text, out int dema)))
            {
                int n = int.Parse(txtGiaNhap.Text.Replace(",", "")) + ((int.Parse(txtGiaNhap.Text.Replace(",", "")) * int.Parse(txtLoiNhuan.Text.Replace(",", "")) / 100));
                txtGiaBan.Text = ConvertTien((double)n);
            }
            else
            {
                txtGiaBan.Clear();
            }*/

        }
       /* private string ConvertTien(double gia)
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
        }*/

        private void txtKhuyenMai_TextChanged(object sender, EventArgs e)
        {
            if (txtKhuyenMai.Text != "")
            {
                if (!int.TryParse(txtKhuyenMai.Text, out int dema))
                {
                    frmThongBao frm = new frmThongBao();
                    
                    MessageBox.Show("SAI ĐỊNH DẠNG KHUYẾN MÃI");
                    txtKhuyenMai.Clear();
                }
            }
            Console.WriteLine("cccc");
        }

        private void cboLocLoaiSP_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void pnlNCC_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dgvSanPham_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
