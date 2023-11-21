using BusinessLogicLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyCuaHangBanDoChoi.UserControls
{
    public partial class ucHoaDon : UserControl
    {
        int sohd,masp;

        private void lsvCTHD_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void usHoaDon_Load(object sender, EventArgs e)
        {
            DataTable dt = HoaDonBL.GetInstance.LayDanhSachHoaDon();
            dgvHoaDon.DataSource = dt;
            DataTable data = CTHDBL.GetInstance.LayDanhSachChiTietHoaDonTheoMaHD(1);
            dgvHTCTHD.DataSource = data;
        }

        private void dgvHoaDon_Click(object sender, EventArgs e)
        {
            try
            {
                DataGridViewCell clickedCell = null;

                // Tìm ô được nhấp chuột
                foreach (DataGridViewCell cell in dgvHoaDon.SelectedCells)
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

                    DataGridViewRow row = dgvHoaDon.Rows[rowIndex];
                    object sohdCellValue = row.Cells["Số Hóa Đơn"].Value;

                    // Kiểm tra và sử dụng giá trị "Số Hóa Đơn"
                    if (sohdCellValue != null)
                    {
                        int sohd = Convert.ToInt32(sohdCellValue);
                        // Sử dụng giá trị "Số Hóa Đơn"
                        DataTable dt = CTHDBL.GetInstance.LayDanhSachChiTietHoaDonTheoMaHD(sohd);
                        dgvHTCTHD.DataSource = dt;
                    }

                    // Chọn hàng chứa ô được nhấp
                    dgvHoaDon.CurrentCell = clickedCell;
                    row.Selected = true;
                }
            }
            catch (Exception)
            {
                return;
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                {
                    DataGridViewRow row = dgvHoaDon.Rows[e.RowIndex];
                    object sohdCellValue = row.Cells["Số Hóa Đơn"].Value;

                    // Kiểm tra và sử dụng giá trị "Số Hóa Đơn"
                    if (sohdCellValue != null)
                    {
                        int sohd = Convert.ToInt32(sohdCellValue);
                        // Sử dụng giá trị "Số Hóa Đơn"
                        DataTable dt = CTHDBL.GetInstance.LayDanhSachChiTietHoaDonTheoMaHD(sohd);
                        dgvHTCTHD.DataSource = dt;
                    }

                    // Chọn hàng chứa ô được nhấp
                    DataGridViewCell cell = dgvHoaDon.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    dgvHoaDon.CurrentCell = cell;
                    row.Selected = true;
                }
            }
            catch (Exception)
            {
                return;
            }
        }
        private void SelectRowByCell(DataGridViewCell cell)
        {
            if (cell != null && cell.RowIndex >= 0)
            {
                DataGridViewRow row = dgvHoaDon.Rows[cell.RowIndex];
                dgvHoaDon.CurrentCell = cell;
                row.Selected = true;
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            DataTable dt = HoaDonBL.GetInstance.LayDanhSachHoaDon();
            dgvHoaDon.DataSource = dt;
            DataTable data = CTHDBL.GetInstance.LayDanhSachChiTietHoaDonTheoMaHD(1);
            dgvHTCTHD.DataSource = data;
        }

        private void dgvHoaDon_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void lblDSHoaDon_Click(object sender, EventArgs e)
        {

        }

        public ucHoaDon()
        {
            InitializeComponent();
        }

    }
}
