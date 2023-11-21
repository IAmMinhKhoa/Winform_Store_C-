using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyCuaHangBanDoChoi.Forms
{
    public partial class frmLoadImage : Form
    {
        public Image img = null;
        PictureBox pic = new PictureBox();
        public frmLoadImage()
        {
            InitializeComponent();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            this.Close();
        }
       




        private void btnChon_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Title = "Open Image";
                dlg.Filter = "bmp files (*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";
                DialogResult r = dlg.ShowDialog();

                if (dlg.FileName != null && r != DialogResult.Cancel)
                {
                    pic.Image = null;
                    pic.Image = new Bitmap(dlg.FileName);
                    img = pic.Image;
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                    return;
                }
                if (dlg.FileName == null)
                {
                    return;
                }
            }
        }

        private void lblThongBao_Click(object sender, EventArgs e)
        {

        }

        private void txtURL_TextChanged(object sender, EventArgs e)
        {
           
        }
    }
}
