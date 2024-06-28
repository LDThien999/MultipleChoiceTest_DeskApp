namespace CSDLPT
{
    partial class frmDangNhap
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDangNhap));
            this.cmbCoso = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtTAIKHOAN = new System.Windows.Forms.TextBox();
            this.txtMATKHAU = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btn_DangNhap = new System.Windows.Forms.Button();
            this.btn_exit = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.grbDoiTuong = new System.Windows.Forms.GroupBox();
            this.radSinhVien = new System.Windows.Forms.RadioButton();
            this.radGiangVien = new System.Windows.Forms.RadioButton();
            this.lblTieuDeLogin = new System.Windows.Forms.Label();
            this.picPTIT = new System.Windows.Forms.PictureBox();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.picSee = new System.Windows.Forms.PictureBox();
            this.picNotSee = new System.Windows.Forms.PictureBox();
            this.grbDoiTuong.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picPTIT)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picSee)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picNotSee)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbCoso
            // 
            this.cmbCoso.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbCoso.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbCoso.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCoso.FormattingEnabled = true;
            this.cmbCoso.Items.AddRange(new object[] {
            "CƠ SỞ 1",
            "CƠ SỞ 2"});
            this.cmbCoso.Location = new System.Drawing.Point(129, 252);
            this.cmbCoso.Name = "cmbCoso";
            this.cmbCoso.Size = new System.Drawing.Size(314, 25);
            this.cmbCoso.TabIndex = 1;
            this.cmbCoso.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(39, 309);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "Tài Khoản :";
            // 
            // txtTAIKHOAN
            // 
            this.txtTAIKHOAN.Location = new System.Drawing.Point(129, 301);
            this.txtTAIKHOAN.Name = "txtTAIKHOAN";
            this.txtTAIKHOAN.Size = new System.Drawing.Size(314, 25);
            this.txtTAIKHOAN.TabIndex = 3;
            // 
            // txtMATKHAU
            // 
            this.txtMATKHAU.Location = new System.Drawing.Point(129, 349);
            this.txtMATKHAU.Name = "txtMATKHAU";
            this.txtMATKHAU.Size = new System.Drawing.Size(314, 25);
            this.txtMATKHAU.TabIndex = 4;
            this.txtMATKHAU.UseSystemPasswordChar = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(39, 357);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 15);
            this.label3.TabIndex = 5;
            this.label3.Text = "Password :";
            // 
            // btn_DangNhap
            // 
            this.btn_DangNhap.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btn_DangNhap.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_DangNhap.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.btn_DangNhap.Location = new System.Drawing.Point(129, 393);
            this.btn_DangNhap.Name = "btn_DangNhap";
            this.btn_DangNhap.Size = new System.Drawing.Size(98, 41);
            this.btn_DangNhap.TabIndex = 6;
            this.btn_DangNhap.Text = "Login";
            this.btn_DangNhap.UseVisualStyleBackColor = false;
            this.btn_DangNhap.Click += new System.EventHandler(this.button1_Click);
            // 
            // btn_exit
            // 
            this.btn_exit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btn_exit.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_exit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btn_exit.Location = new System.Drawing.Point(313, 393);
            this.btn_exit.Name = "btn_exit";
            this.btn_exit.Size = new System.Drawing.Size(98, 41);
            this.btn_exit.TabIndex = 7;
            this.btn_exit.Text = "Exit";
            this.btn_exit.UseVisualStyleBackColor = false;
            this.btn_exit.Click += new System.EventHandler(this.button2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(43, 260);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 17);
            this.label1.TabIndex = 8;
            this.label1.Text = "Cơ sở :";
            this.label1.Click += new System.EventHandler(this.label1_Click_1);
            // 
            // grbDoiTuong
            // 
            this.grbDoiTuong.BackColor = System.Drawing.Color.WhiteSmoke;
            this.grbDoiTuong.Controls.Add(this.radSinhVien);
            this.grbDoiTuong.Controls.Add(this.radGiangVien);
            this.grbDoiTuong.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grbDoiTuong.Location = new System.Drawing.Point(86, 187);
            this.grbDoiTuong.Name = "grbDoiTuong";
            this.grbDoiTuong.Size = new System.Drawing.Size(387, 59);
            this.grbDoiTuong.TabIndex = 9;
            this.grbDoiTuong.TabStop = false;
            this.grbDoiTuong.Text = "Đối Tượng";
            // 
            // radSinhVien
            // 
            this.radSinhVien.AutoSize = true;
            this.radSinhVien.Location = new System.Drawing.Point(247, 24);
            this.radSinhVien.Name = "radSinhVien";
            this.radSinhVien.Size = new System.Drawing.Size(86, 21);
            this.radSinhVien.TabIndex = 1;
            this.radSinhVien.TabStop = true;
            this.radSinhVien.Text = "Sinh viên";
            this.radSinhVien.UseVisualStyleBackColor = true;
            this.radSinhVien.CheckedChanged += new System.EventHandler(this.radSinhVien_CheckedChanged);
            // 
            // radGiangVien
            // 
            this.radGiangVien.AutoSize = true;
            this.radGiangVien.Location = new System.Drawing.Point(32, 24);
            this.radGiangVien.Name = "radGiangVien";
            this.radGiangVien.Size = new System.Drawing.Size(96, 21);
            this.radGiangVien.TabIndex = 0;
            this.radGiangVien.TabStop = true;
            this.radGiangVien.Text = "Giảng viên";
            this.radGiangVien.UseVisualStyleBackColor = true;
            this.radGiangVien.CheckedChanged += new System.EventHandler(this.radGiangVien_CheckedChanged);
            // 
            // lblTieuDeLogin
            // 
            this.lblTieuDeLogin.AutoSize = true;
            this.lblTieuDeLogin.Font = new System.Drawing.Font("Times New Roman", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTieuDeLogin.ForeColor = System.Drawing.Color.Red;
            this.lblTieuDeLogin.Location = new System.Drawing.Point(112, 34);
            this.lblTieuDeLogin.Name = "lblTieuDeLogin";
            this.lblTieuDeLogin.Size = new System.Drawing.Size(331, 31);
            this.lblTieuDeLogin.TabIndex = 10;
            this.lblTieuDeLogin.Text = "ĐĂNG NHẬP HỆ THỐNG";
            // 
            // picPTIT
            // 
            this.picPTIT.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.picPTIT.Image = global::CSDLPT.Properties.Resources.PtitLogo;
            this.picPTIT.Location = new System.Drawing.Point(228, 79);
            this.picPTIT.Name = "picPTIT";
            this.picPTIT.Size = new System.Drawing.Size(91, 85);
            this.picPTIT.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picPTIT.TabIndex = 11;
            this.picPTIT.TabStop = false;
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // picSee
            // 
            this.picSee.Image = ((System.Drawing.Image)(resources.GetObject("picSee.Image")));
            this.picSee.Location = new System.Drawing.Point(449, 349);
            this.picSee.Name = "picSee";
            this.picSee.Size = new System.Drawing.Size(24, 25);
            this.picSee.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picSee.TabIndex = 12;
            this.picSee.TabStop = false;
            this.picSee.Click += new System.EventHandler(this.picSee_Click);
            // 
            // picNotSee
            // 
            this.picNotSee.Image = ((System.Drawing.Image)(resources.GetObject("picNotSee.Image")));
            this.picNotSee.Location = new System.Drawing.Point(449, 349);
            this.picNotSee.Name = "picNotSee";
            this.picNotSee.Size = new System.Drawing.Size(24, 25);
            this.picNotSee.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picNotSee.TabIndex = 13;
            this.picNotSee.TabStop = false;
            this.picNotSee.Click += new System.EventHandler(this.picNotSee_Click);
            // 
            // frmDangNhap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Menu;
            this.ClientSize = new System.Drawing.Size(541, 487);
            this.Controls.Add(this.picNotSee);
            this.Controls.Add(this.picSee);
            this.Controls.Add(this.picPTIT);
            this.Controls.Add(this.lblTieuDeLogin);
            this.Controls.Add(this.grbDoiTuong);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_exit);
            this.Controls.Add(this.btn_DangNhap);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtMATKHAU);
            this.Controls.Add(this.txtTAIKHOAN);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmbCoso);
            this.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmDangNhap";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LOGIN";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.grbDoiTuong.ResumeLayout(false);
            this.grbDoiTuong.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picPTIT)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picSee)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picNotSee)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ComboBox cmbCoso;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtTAIKHOAN;
        private System.Windows.Forms.TextBox txtMATKHAU;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btn_DangNhap;
        private System.Windows.Forms.Button btn_exit;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox grbDoiTuong;
        private System.Windows.Forms.RadioButton radSinhVien;
        private System.Windows.Forms.RadioButton radGiangVien;
        private System.Windows.Forms.Label lblTieuDeLogin;
        private System.Windows.Forms.PictureBox picPTIT;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.PictureBox picSee;
        private System.Windows.Forms.PictureBox picNotSee;
    }
}

