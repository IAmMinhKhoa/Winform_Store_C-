using BusinessLogicLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyCuaHangBanDoChoi.UserControls
{
    public partial class ucPhieuNhap : UserControl
    {
        public ucPhieuNhap()
        {
            InitializeComponent();
        }

        private void dgvPhieuNhap_Click(object sender, EventArgs e)
        {
            try
            {
                DataGridViewCell clickedCell = null;

                // Tìm ô được nhấp chuột
                foreach (DataGridViewCell cell in dgvPhieuNhap.SelectedCells)
                {
                    if (cell.Selected)
                    {
                        clickedCell = cell;
                        break;
                    }
                }

                if (clickedCell != null)
                {
                    int rowIndex = clickedCell.RowIndex;
                    int columnIndex = clickedCell.ColumnIndex;

                    DataGridViewRow row = dgvPhieuNhap.Rows[rowIndex];
                    object MaPNCellValue = row.Cells["Mã Phiếu"].Value;

                    // Kiểm tra và sử dụng giá trị "Số Hóa Đơn"
                    if (MaPNCellValue != null)
                    {
                        int MaPN = Convert.ToInt32(MaPNCellValue);
                        // Sử dụng giá trị "Số Hóa Đơn"
                        DataTable dt = CTPNBL.GetInstance.GetDanhSachChiTietPhieuNhap(MaPN);
                        dgvHTCTPN.DataSource = dt;
                    }

                    // Chọn hàng chứa ô được nhấp
                    dgvPhieuNhap.CurrentCell = clickedCell;
                    row.Selected = true;
                }
            }
            catch (Exception)
            {
                return;
            }
        }

        private void ucChiTietPhieuNhap_Load(object sender, EventArgs e)
        {
            DataTable dt = PhieuNhapBL.GetInstance.GetDanhSachPhieuNhap2();
            dgvPhieuNhap.DataSource = dt;
            DataTable data = CTPNBL.GetInstance.GetDanhSachChiTietPhieuNhap(1);
            dgvHTCTPN.DataSource = data;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            DataTable dt = PhieuNhapBL.GetInstance.GetDanhSachPhieuNhap2();
            dgvPhieuNhap.DataSource = dt;
            DataTable data = CTPNBL.GetInstance.GetDanhSachChiTietPhieuNhap(1);
            dgvHTCTPN.DataSource = data;
        }
    }
}
