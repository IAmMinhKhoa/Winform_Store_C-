namespace QuanLyCuaHangBanDoChoi.Forms
{
    partial class frmChonNCC
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button9 = new System.Windows.Forms.Button();
            this.btnChon = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.cboNCC = new System.Windows.Forms.ComboBox();
            this.label24 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button9
            // 
            this.button9.FlatAppearance.BorderSize = 0;
            this.button9.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Red;
            this.button9.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button9.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button9.ForeColor = System.Drawing.Color.White;
            this.button9.Image = global::QuanLyCuaHangBanDoChoi.Properties.Resources.icons8_close_window_32px;
            this.button9.Location = new System.Drawing.Point(669, 2);
            this.button9.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(48, 51);
            this.button9.TabIndex = 20;
            this.button9.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button9.UseVisualStyleBackColor = true;
            this.button9.Click += new System.EventHandler(this.button9_Click);
            // 
            // btnChon
            // 
            this.btnChon.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(166)))), ((int)(((byte)(0)))));
            this.btnChon.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnChon.FlatAppearance.BorderSize = 0;
            this.btnChon.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnChon.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnChon.ForeColor = System.Drawing.Color.White;
            this.btnChon.Image = global::QuanLyCuaHangBanDoChoi.Properties.Resources.icons8_ok_32px;
            this.btnChon.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnChon.Location = new System.Drawing.Point(558, 169);
            this.btnChon.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnChon.Name = "btnChon";
            this.btnChon.Size = new System.Drawing.Size(144, 57);
            this.btnChon.TabIndex = 17;
            this.btnChon.Text = "Chọn";
            this.btnChon.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnChon.UseVisualStyleBackColor = false;
            this.btnChon.Click += new System.EventHandler(this.btnChon_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.LightGray;
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Image = global::QuanLyCuaHangBanDoChoi.Properties.Resources.icons8_add_32px;
            this.button1.Location = new System.Drawing.Point(482, 68);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(56, 48);
            this.button1.TabIndex = 57;
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // cboNCC
            // 
            this.cboNCC.BackColor = System.Drawing.Color.White;
            this.cboNCC.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.cboNCC.ForeColor = System.Drawing.Color.Black;
            this.cboNCC.FormattingEnabled = true;
            this.cboNCC.Location = new System.Drawing.Point(254, 66);
            this.cboNCC.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cboNCC.Name = "cboNCC";
            this.cboNCC.Size = new System.Drawing.Size(220, 41);
            this.cboNCC.TabIndex = 56;
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label24.ForeColor = System.Drawing.Color.Black;
            this.label24.Location = new System.Drawing.Point(90, 71);
            this.label24.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(160, 29);
            this.label24.TabIndex = 58;
            this.label24.Text = "Nhà cung cấp";
            this.label24.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // frmChonNCC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightGray;
            this.ClientSize = new System.Drawing.Size(720, 245);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.cboNCC);
            this.Controls.Add(this.label24);
            this.Controls.Add(this.button9);
            this.Controls.Add(this.btnChon);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "frmChonNCC";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmChonNCC";
            this.Load += new System.EventHandler(this.frmChonNCC_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.Button btnChon;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox cboNCC;
        private System.Windows.Forms.Label label24;
    }
}