using System;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.IO;
using BusinessLogicLayer;
using DTO;
using QuanLyCuaHangBanDoChoi.Forms;
using System.Net.Mail;
using System.Net;
using System.Collections.Generic;

namespace QuanLyCuaHangBanDoChoi.UserControls
{
    public partial class ucBanSanPham : UserControl
    {
        public ucBanSanPham()
        {
            InitializeComponent();
        }

        private void ucBanHang_Load(object sender, EventArgs e)
        {
            LoadCboLocLoaiSP();
            LoadCboLocNCC();
            LoadDanhSachSanPhamTheoBoLoc();
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
        int sumpage = 0;
        private void LoadDanhSachSanPhamTheoBoLoc()
        {
            flowLayoutPanelSanPham.Controls.Clear();
            DataTable dt = SanPhamBL.GetInstance.GetDanhSachSanPhamTheoBoLoc(txtTenSP.Text.Trim(), cboLocLoaiSP.SelectedValue.ToString().Trim(), cboLocNCC.SelectedValue.ToString().Trim());


            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ucSanPham sp = new ucSanPham();
                sp.spDTO.masp = int.Parse(dt.Rows[i].ItemArray[0].ToString());
                sp.spDTO.tensp = dt.Rows[i].ItemArray[1].ToString();
                sp.spDTO.maloaisp = dt.Rows[i].ItemArray[2].ToString();
                sp.spDTO.dvt = dt.Rows[i].ItemArray[3].ToString();
                sp.spDTO.mancc = int.Parse(dt.Rows[i].ItemArray[4].ToString());
                sp.spDTO.soluong = int.Parse(dt.Rows[i].ItemArray[7].ToString());
                sp.spDTO.giaban = decimal.Parse(dt.Rows[i].ItemArray[10].ToString());
                sp.spDTO.khuyenmai = int.Parse(dt.Rows[i].ItemArray[11].ToString());
                sp.spDTO.hinhanh = (byte[])dt.Rows[i].ItemArray[12];
                MemoryStream ms = new MemoryStream(sp.spDTO.hinhanh);
                sp.picSP.Image = Image.FromStream(ms);

                sp.lblTenSP.Text = sp.spDTO.tensp;
                if (sp.spDTO.khuyenmai == 0)
                {
                    sp.lblGiaKM.Text = "Giá: " + (sp.spDTO.giaban) + " ₫";
                    sp.lblGiaGoc.Visible = false;
                  
                    sp.panel1.Visible = false;
                }
                else
                {
                    sp.lblKM.Text = "-" + sp.spDTO.khuyenmai + "%";
                    sp.lblGiaGoc.Text = (sp.spDTO.giaban) + " ₫";
                    //sp.spDTO.giaban = sp.spDTO.giaban - ((sp.spDTO.giaban * sp.spDTO.khuyenmai) / 100);
                    sp.lblGiaKM.Text = "Giá: " + (sp.spDTO.giaban - (sp.spDTO.giaban * sp.spDTO.khuyenmai) / 100) + " ₫";
                    //sp.pictureBox2.Click += PictureBox2_Click;
                }
                if (sp.spDTO.soluong <= 0)
                {
                    sp.lblSanCo.Text = "Hết hàng!";
                }
                else
                    sp.lblSanCo.Text = "Sẵn có: " + sp.spDTO.soluong.ToString() + " " + sp.spDTO.dvt;
                sp.lblGiaGoc.Font = new Font("UTM Avo", 10, FontStyle.Strikeout);
             

                //add event buy to image of prodcut card
                sp.picSP.Click += PicSP_Click;
             

   
              
                flowLayoutPanelSanPham.Controls.Add(sp);
            }
        }

       

        private void PictureBox2_Click1(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
        bool hd = false;
        public static int SOHD = 0;
        public static decimal ThanhTien = 0;
        
        private void ThemHoaDon()
        {
            HoaDonDTO hddTO = new HoaDonDTO();
            if (txtTenKH.Text == "")
                hddTO.MaKH = 1;
            else
                hddTO.MaKH = makh;
            hddTO.MaNV = frmDangNhap.TenDangNhap;
            hddTO.NgayLap = DateTime.Now;
            hddTO.ThanhTien = 0;
            hddTO.DaThanhToan = false;
            bool s = HoaDonBL.GetInstance.ThemHoaDon(hddTO);
            SOHD = HoaDonBL.GetInstance.GetSOHDMAX();

            btnHuy.BackColor = Color.OrangeRed;

            MessageBox.Show("Đã tạo đơn hàng gòi nè");

            txtSDT.Enabled = false;
        }

       

        private void ThemCTDH()
        {
            DataTable dt = new DataTable();
            foreach (DataGridViewColumn col in dgvCTHD.Columns)
            {
                dt.Columns.Add(col.Name);
            }

            foreach (DataGridViewRow row in dgvCTHD.Rows)
            {
                DataRow dRow = dt.NewRow();
                foreach (DataGridViewCell cell in row.Cells)
                {
                    dRow[cell.ColumnIndex] = cell.Value;
                }
                dt.Rows.Add(dRow);
            }
            if (CTHDBL.GetInstance.ThemCTHD(dt, SOHD, decimal.Parse(ThanhTien.ToString())))
            {
                CapNhatSoLuong();
                SOHD_Report = SOHD;
                CapNhatTienKhachHang();
                ClearThongTinHD();
                SOHD = 0;
            }
        }

        private void ClearThongTinHD()
        {
            dgvCTHD.Rows.Clear();
            lblTongTien.Text = "0";
            txtTienKHTra.Text = "";
            txtTienThua.Text = "";
            txtSDT.Text = "";
            txtTenKH.Text = "";
            btnThanhToan.BackColor = Color.Gray;
            btnHuy.BackColor = Color.Gray;
        }

        private void CapNhatTienKhachHang()
        {

            //HoaDonBL.GetInstance.CapNhatSoLuongTienKhachHang(SOHD_Report, decimal.Parse(txtTienKHTra.Text), decimal.Parse(txtTienThua.Text));
            decimal tienKHTra;
            decimal tienThua;

            if (decimal.TryParse(txtTienKHTra.Text, out tienKHTra) && decimal.TryParse(txtTienThua.Text, out tienThua))
            {
                HoaDonBL.GetInstance.CapNhatSoLuongTienKhachHang(SOHD_Report, tienKHTra, tienThua);
            }
            else
            {
                MessageBox.Show("Số tiền nhập không hợp lệ. Vui lòng kiểm tra lại.");
            }



        }

        public static int SOHD_Report = 0;
        private void CapNhatSoLuong()
        {
            for (int i = 0; i < dgvCTHD.Rows.Count; i++)
            {
                DataGridViewRow r = dgvCTHD.Rows[i];
                SanPhamBL.GetInstance.CapNhatSoLuongKhiBanHang(int.Parse(r.Cells[0].Value.ToString()), int.Parse(r.Cells[4].Value.ToString()));
            }
        }

      


        //EVENT KHI CLICK VÀO ẢNH CARD PRODUCT
        private void PicSP_Click(object sender, EventArgs e)
        {
            decimal tong = 0;
            bool check_Out_Stock = false;
            PictureBox p = (PictureBox)sender;
            ucSanPham u = (ucSanPham)p.Parent;
           // u.Select();
            if (u.spDTO.soluong > 0)
            {
                Console.WriteLine(u.spDTO.soluong+"sl");
                for (int i = 0; i < dgvCTHD.Rows.Count; i++)
                {
                   
                    DataGridViewRow r = dgvCTHD.Rows[i];
                    if (int.Parse(r.Cells[0].Value.ToString()) == u.spDTO.masp)
                    {
                        if(int.Parse(r.Cells[4].Value.ToString())>= u.spDTO.soluong)
                        {
                            MessageBox.Show("SỐ LƯỢNG MUA LỚN HƠN TRONG KHO! TRỜI ƠI KHỔ QUÁ MUA ÍT THÔI");
                            check_Out_Stock = true;
                        }
                        else
                        {
                            dgvCTHD.Rows[i].Cells[4].Value = int.Parse(r.Cells[4].Value.ToString()) + 1;
                            dgvCTHD.Rows[i].Selected = true;
                            decimal giagoc = decimal.Parse(r.Cells[2].Value.ToString());
                            int km = int.Parse(r.Cells[3].Value.ToString());
                            decimal giakm = giagoc - ((giagoc * km) / 100);
                            tong = giakm * decimal.Parse(r.Cells[4].Value.ToString());
                            dgvCTHD.Rows[i].Cells[5].Value = tong;
                            LoadTongHoaDon();
                           
                            
                            return;
                        }

                        
                    }
                }
                if(!check_Out_Stock)
                {
                    Console.WriteLine("chay duoi nay");
                    tong = u.spDTO.giaban - ((u.spDTO.giaban * u.spDTO.khuyenmai) / 100);
                    dgvCTHD.Rows.Insert(dgvCTHD.Rows.Count, u.spDTO.masp, u.spDTO.tensp, u.spDTO.giaban, u.spDTO.khuyenmai, 1, tong, "-", "+");
                    dgvCTHD.Rows[dgvCTHD.Rows.Count - 1].Selected = true;
                    LoadTongHoaDon();
                    Console.WriteLine(hd+"duoi");
                    if (hd != true)
                    {
                        ThemHoaDon();
                        hd = true;
                        txtSDT.Enabled = false;
                    }
                   
                }
               
            }
        }

       

       

        private void btnApDung_Click(object sender, EventArgs e)
        {
            LoadDanhSachSanPhamTheoBoLoc();
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtTenSP.Text = "";
            cboLocLoaiSP.SelectedIndex = cboLocLoaiSP.Items.Count - 1;
            cboLocNCC.SelectedIndex = cboLocNCC.Items.Count - 1;
        }
        int makh = 0;

        public object NSXDL { get; private set; }
        string tenkh = "";

        private void txtTenDangNhap_TextChanged(object sender, EventArgs e)
        {
            txtTenKH.Text = KhachHangBL.GetInstance.GetTenKhachHang(txtSDT.Text);
            if (txtTenKH.Text == "")
            {
                picThanhCong.Visible = false;
            }
            else
            {
                tenkh = KhachHangBL.GetInstance.GetTenMaKH(txtSDT.Text);
                makh = int.Parse(KhachHangBL.GetInstance.GetTenMaKH(txtSDT.Text));
                picThanhCong.Visible = true;
            }
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void dgvCTHD_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }


        //2 button - + trong CTHD mỗi row
        private void dgvCTHD_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 6)
            {
                DataGridViewRow r = dgvCTHD.SelectedRows[0];
                for (int i = 0; i < dgvCTHD.Rows.Count; i++)
                {
                    if (r.Cells[0].Value.ToString() == dgvCTHD.Rows[i].Cells[0].Value.ToString())
                    {
                        if (int.Parse(r.Cells[4].Value.ToString()) == 1)
                        {
                            dgvCTHD.Rows.Remove(r);
                            LoadTongHoaDon();
                            return;
                        }
                        dgvCTHD.Rows[i].Cells[4].Value = int.Parse(r.Cells[4].Value.ToString()) - 1;
                        decimal giagoc = decimal.Parse(r.Cells[2].Value.ToString());
                        int km = int.Parse(r.Cells[3].Value.ToString());
                        decimal giakm = giagoc - ((giagoc * km) / 100);
                        decimal tong = giakm * decimal.Parse(r.Cells[4].Value.ToString());
                        dgvCTHD.Rows[i].Cells[5].Value = tong;
                        LoadTongHoaDon();

                        if (txtTienKHTra.Text != "")
                        {
                            if (ThanhTien < decimal.Parse(txtTienKHTra.Text))
                                txtTienThua.Text = ((Math.Abs(ThanhTien - decimal.Parse(txtTienKHTra.Text)))) + "";
                            else
                            {
                                txtTienThua.Text = "Không đủ";
                                btnThanhToan.BackColor = Color.Gray;
                            }
                        }

                        return;
                    }
                }
            }
            if (e.ColumnIndex == 7)
            {
                DataGridViewRow r = dgvCTHD.SelectedRows[0];
                for (int i = 0; i < dgvCTHD.Rows.Count; i++)
                {
                    if (r.Cells[0].Value.ToString() == dgvCTHD.Rows[i].Cells[0].Value.ToString())
                    {
                        dgvCTHD.Rows[i].Cells[4].Value = int.Parse(r.Cells[4].Value.ToString()) + 1;
                        decimal giagoc = decimal.Parse(r.Cells[2].Value.ToString());
                        int km = int.Parse(r.Cells[3].Value.ToString());
                        decimal giakm = giagoc - ((giagoc * km) / 100);
                        decimal tong = giakm * decimal.Parse(r.Cells[4].Value.ToString());
                        dgvCTHD.Rows[i].Cells[5].Value = tong;
                        LoadTongHoaDon();
                        try
                        {
                            if (ThanhTien <= decimal.Parse(txtTienKHTra.Text))
                            {
                                txtTienThua.Text = ((Math.Abs(ThanhTien - decimal.Parse(txtTienKHTra.Text)))) + "";

                            }
                            else
                            {
                                txtTienThua.Text = "Không đủ";
                                btnThanhToan.BackColor = Color.Gray;
                            }
                        }
                        catch (Exception)
                        {
                            return;
                        }

                        return;
                    }
                }
            }
        }

        private void LoadTongHoaDon()
        {
            decimal tong = 0;
            for (int i = 0; i < dgvCTHD.Rows.Count; i++)
            {
                tong += decimal.Parse(dgvCTHD.Rows[i].Cells[5].Value.ToString());
            }
            ThanhTien = tong;
            lblTongTien.Text = (tong) + " ₫";
            if (txtTienKHTra.Text == "")
                return;
            if (tong < decimal.Parse(txtTienKHTra.Text))
            {
                txtTienThua.Text = ((Math.Abs(ThanhTien - decimal.Parse(txtTienKHTra.Text)))) + "";
                btnThanhToan.BackColor = Color.Green;
            }
            else
            {
                txtTienThua.Text = "Không đủ";
                btnThanhToan.BackColor = Color.Gray;
            }

        }

        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            if (btnThanhToan.BackColor == Color.Gray)
                return;
            MessageBox.Show("Đã thanh toán");
            this.Cursor = Cursors.AppStarting;
            if (txtTenKH.Text != "")
                KhachHangBL.GetInstance.CapNhatDoanhSoKhachHang(makh, ThanhTien);
            ThemCTDH();
            InHoaDon();
            LoadDanhSachSanPhamTheoBoLoc();
            hd = false;
            txtSDT.Enabled = true;
            this.Cursor = Cursors.Default;
        }

        private void InHoaDon()
        {
            frmInHoaDon frm = new frmInHoaDon();
            frm.Show();
            frm.TopMost = true;
        }
       
        private void btnHuy_Click(object sender, EventArgs e)
        {
            HoaDonBL.GetInstance.XoaHD(SOHD);
            btnHuy.BackColor = Color.Gray;
            dgvCTHD.Rows.Clear();
            hd = false;
            SOHD = 0;
            txtSDT.Text = "";
            lblTongTien.Text = "0";
            txtTienThua.Text = "";
            txtTienKHTra.Text = "";
            btnThanhToan.BackColor = Color.Gray;
            txtSDT.Enabled = true;

            MessageBox.Show("Hủy thành Kong");
        }

        private void txtTienKHTra_TextChanged(object sender, EventArgs e)
        {
            if (txtTienKHTra.Text != "")
            {
                try
                {
                    System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("en-US");
                    decimal value = decimal.Parse(txtTienKHTra.Text, System.Globalization.NumberStyles.AllowThousands);
                    // decimal value;
                    if (decimal.TryParse(txtTienKHTra.Text, System.Globalization.NumberStyles.AllowThousands, culture, out value))
                    {
                        try
                        {
                            if (ThanhTien <= decimal.Parse(txtTienKHTra.Text))
                            {
                                txtTienThua.Text = Math.Abs((ThanhTien - decimal.Parse(txtTienKHTra.Text))) + "";
                                /* System.Globalization.CultureInfo c = new System.Globalization.CultureInfo("en-US");
                                 decimal v = decimal.Parse(txtTienThua.Text, System.Globalization.NumberStyles.AllowThousands);*/
                                // txtTienThua.Text = String.Format(c, "{0:N0}", v);
                                txtTienThua.Select(txtTienThua.Text.Length, 0);
                                btnThanhToan.BackColor = Color.Green;
                            }
                            else
                            {
                                txtTienThua.Text = "Không đủ";
                                btnThanhToan.BackColor = Color.Gray;
                            }

                        }
                        catch (Exception)
                        {
                            MessageBox.Show("SỐ TIỀN HIỆN TẠI VƯỢT RA KHỎI BỘ NHỚ RỒI, GIÀU DỮ VẬY TRỜI ƠI");
                            return;
                        }
                    }
                    else
                    {
                        txtTienThua.Text = "Sai định dạng rồi cậu ơi";
                        txtTienKHTra.Clear();
                        btnThanhToan.BackColor = Color.Gray;
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("SAI ĐỊNH DẠNG RỒI BRO");
                    txtTienKHTra.Clear();
                }
            }
        }

        private void txtTienThua_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
           /* Cursor = Cursors.AppStarting;
            string pre = "";
            string next = "";
            string str = lblPageNumber.Text;
            bool b = false;
            for (int i = 0; i < str.Length; i++)
            {
                if(str[i] != '/' && b == false)
                {
                    pre += str[i];
                    continue;
                }
                if (str[i] != '/' && b == true)
                {
                    next += str[i];
                    continue;
                }
                if (str[i] == '/')
                {
                    b = true;
                }
            }
            if (pre == next)
                return;

            string n = "";
            string num = lblPageNumber.Text;
            for (int i = 0; i < num.Length; i++)
            {
                if (num[i] == '/')
                {
                    break;
                }
                else
                {
                    n += num[i];
                }
            }
           // LoadDanhSachSanPhamTheoBoLoc(int.Parse(n)+1);
            lblPageNumber.Text= int.Parse(n) + 1+"/"+ sumpage;
            Cursor = Cursors.Default;*/
        }

        private void btnPre_Click(object sender, EventArgs e)
        {
           /* Cursor = Cursors.AppStarting;
            string pre = "";
            string str = lblPageNumber.Text;
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] == '/')
                {
                    break;
                }
                else
                {
                    pre += str[i];
                }
            }
            if (pre == "1")
                return;

            string n = "";
            string num = lblPageNumber.Text;
            for (int i = 0; i < num.Length; i++)
            {
                if (num[i] == '/')
                {
                    break;
                }
                else
                {
                    n += num[i];
                }
            }
            LoadDanhSachSanPhamTheoBoLoc(int.Parse(n) - 1);
            lblPageNumber.Text = int.Parse(n) - 1 + "/" + sumpage;
            Cursor = Cursors.Default;*/
        }




        private void txtTenKH_TextChanged(object sender, EventArgs e)
        {
            if (txtTenKH.Text != "")
            {
                
                MessageBox.Show("HD: " + txtTenKH.Text + "...");
            }
        }
       

        private void btnThemMoi_Click(object sender, EventArgs e)
        {

        }

        private void flowLayoutPanelSanPham_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}

