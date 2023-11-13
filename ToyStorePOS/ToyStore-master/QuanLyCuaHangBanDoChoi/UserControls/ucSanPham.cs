using System;
using System.Drawing;
using System.Windows.Forms;
using DTO;

namespace QuanLyCuaHangBanDoChoi.UserControls
{
    public partial class ucSanPham : UserControl
    {
        public SanPhamDTO spDTO;
        public ucSanPham()
        {
            InitializeComponent();
            spDTO = new SanPhamDTO();
          
        }

        private void ucSanPham_MouseEnter(object sender, EventArgs e)
        {
            this.BackColor = Color.LightCyan;
            
        }

        private void ucSanPham_Leave(object sender, EventArgs e)
        {

        }

        private void ucSanPham_MouseLeave(object sender, EventArgs e)
        {
            this.BackColor = Color.White;
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            /*if (picSP.Top > 2)
            {
                picSP.Top--;
            }
            else
            {
                timer1.Stop();
            }*/
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
           /* if (picSP.Top < 5)
            {
                picSP.Top++;
            }
            else
            {
                timer2.Stop();
            }*/
        }

        private void ucSanPham_Load(object sender, EventArgs e)
        {
            panel1.BringToFront();
           // pictureBox2.BringToFront();
        }

        private void lblKM_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void picSP_Click(object sender, EventArgs e)
        {

        }
    }
}
