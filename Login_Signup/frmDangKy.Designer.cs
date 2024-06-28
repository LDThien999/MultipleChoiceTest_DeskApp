namespace CSDLPT.Login_Signup
{
    partial class frmDangKy
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDangKy));
            this.lblTieuDeDK = new System.Windows.Forms.Label();
            this.picPTIT = new System.Windows.Forms.PictureBox();
            this.grbDoiTuong = new System.Windows.Forms.GroupBox();
            this.radSinhVien = new System.Windows.Forms.RadioButton();
            this.radGiangVien = new System.Windows.Forms.RadioButton();
            this.picNotSee1 = new System.Windows.Forms.PictureBox();
            this.picSee1 = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtMATKHAU = new System.Windows.Forms.TextBox();
            this.txtUserName_MaGV = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.picNotSee2 = new System.Windows.Forms.PictureBox();
            this.picSee2 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtXacNhanMK = new System.Windows.Forms.TextBox();
            this.txtLoginnam = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.grbNhom = new System.Windows.Forms.GroupBox();
            this.radNhomGiangVien = new System.Windows.Forms.RadioButton();
            this.radCoso = new System.Windows.Forms.RadioButton();
            this.radTruong = new System.Windows.Forms.RadioButton();
            this.btn_DangkY = new System.Windows.Forms.Button();
            this.btn_exit = new System.Windows.Forms.Button();
            this.panInput = new System.Windows.Forms.Panel();
            this.btnGoiY = new System.Windows.Forms.Button();
            this.btnKiemTra = new System.Windows.Forms.Button();
            this.panSearch = new System.Windows.Forms.Panel();
            this.lblSearchSV = new System.Windows.Forms.Label();
            this.dtgvGiaoVienChuaCoTK = new System.Windows.Forms.DataGridView();
            this.btnX_thoatSearch = new System.Windows.Forms.Button();
            this.lblLine = new System.Windows.Forms.Label();
            this.lblSearchGV = new System.Windows.Forms.Label();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.picPTIT)).BeginInit();
            this.grbDoiTuong.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picNotSee1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picSee1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picNotSee2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picSee2)).BeginInit();
            this.grbNhom.SuspendLayout();
            this.panInput.SuspendLayout();
            this.panSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvGiaoVienChuaCoTK)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTieuDeDK
            // 
            this.lblTieuDeDK.AutoSize = true;
            this.lblTieuDeDK.Font = new System.Drawing.Font("Times New Roman", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTieuDeDK.ForeColor = System.Drawing.Color.Red;
            this.lblTieuDeDK.Location = new System.Drawing.Point(121, 9);
            this.lblTieuDeDK.Name = "lblTieuDeDK";
            this.lblTieuDeDK.Size = new System.Drawing.Size(305, 31);
            this.lblTieuDeDK.TabIndex = 11;
            this.lblTieuDeDK.Text = "ĐĂNG KÝ TÀI KHOẢN";
            // 
            // picPTIT
            // 
            this.picPTIT.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.picPTIT.Image = global::CSDLPT.Properties.Resources.PtitLogo;
            this.picPTIT.Location = new System.Drawing.Point(227, 55);
            this.picPTIT.Name = "picPTIT";
            this.picPTIT.Size = new System.Drawing.Size(91, 85);
            this.picPTIT.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picPTIT.TabIndex = 12;
            this.picPTIT.TabStop = false;
            // 
            // grbDoiTuong
            // 
            this.grbDoiTuong.BackColor = System.Drawing.Color.WhiteSmoke;
            this.grbDoiTuong.Controls.Add(this.radSinhVien);
            this.grbDoiTuong.Controls.Add(this.radGiangVien);
            this.grbDoiTuong.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grbDoiTuong.Location = new System.Drawing.Point(85, 146);
            this.grbDoiTuong.Name = "grbDoiTuong";
            this.grbDoiTuong.Size = new System.Drawing.Size(387, 59);
            this.grbDoiTuong.TabIndex = 13;
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
            // picNotSee1
            // 
            this.picNotSee1.Image = ((System.Drawing.Image)(resources.GetObject("picNotSee1.Image")));
            this.picNotSee1.Location = new System.Drawing.Point(415, 99);
            this.picNotSee1.Name = "picNotSee1";
            this.picNotSee1.Size = new System.Drawing.Size(24, 25);
            this.picNotSee1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picNotSee1.TabIndex = 21;
            this.picNotSee1.TabStop = false;
            this.picNotSee1.Click += new System.EventHandler(this.picNotSee1_Click);
            // 
            // picSee1
            // 
            this.picSee1.Image = ((System.Drawing.Image)(resources.GetObject("picSee1.Image")));
            this.picSee1.Location = new System.Drawing.Point(415, 99);
            this.picSee1.Name = "picSee1";
            this.picSee1.Size = new System.Drawing.Size(24, 25);
            this.picSee1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picSee1.TabIndex = 20;
            this.picSee1.TabStop = false;
            this.picSee1.Click += new System.EventHandler(this.picSee1_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(30, 109);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 15);
            this.label3.TabIndex = 18;
            this.label3.Text = "Mật khẩu:";
            // 
            // txtMATKHAU
            // 
            this.txtMATKHAU.Location = new System.Drawing.Point(124, 99);
            this.txtMATKHAU.Name = "txtMATKHAU";
            this.txtMATKHAU.Size = new System.Drawing.Size(268, 20);
            this.txtMATKHAU.TabIndex = 17;
            this.txtMATKHAU.UseSystemPasswordChar = true;
            // 
            // txtUserName_MaGV
            // 
            this.txtUserName_MaGV.Location = new System.Drawing.Point(124, 14);
            this.txtUserName_MaGV.Name = "txtUserName_MaGV";
            this.txtUserName_MaGV.Size = new System.Drawing.Size(268, 20);
            this.txtUserName_MaGV.TabIndex = 16;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(27, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 15);
            this.label2.TabIndex = 15;
            this.label2.Text = "Username:";
            // 
            // picNotSee2
            // 
            this.picNotSee2.Image = ((System.Drawing.Image)(resources.GetObject("picNotSee2.Image")));
            this.picNotSee2.Location = new System.Drawing.Point(414, 144);
            this.picNotSee2.Name = "picNotSee2";
            this.picNotSee2.Size = new System.Drawing.Size(24, 25);
            this.picNotSee2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picNotSee2.TabIndex = 25;
            this.picNotSee2.TabStop = false;
            this.picNotSee2.Click += new System.EventHandler(this.picNotSee2_Click);
            // 
            // picSee2
            // 
            this.picSee2.Image = ((System.Drawing.Image)(resources.GetObject("picSee2.Image")));
            this.picSee2.Location = new System.Drawing.Point(415, 144);
            this.picSee2.Name = "picSee2";
            this.picSee2.Size = new System.Drawing.Size(24, 25);
            this.picSee2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picSee2.TabIndex = 24;
            this.picSee2.TabStop = false;
            this.picSee2.Click += new System.EventHandler(this.picSee2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(31, 149);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 15);
            this.label1.TabIndex = 23;
            this.label1.Text = "Xác nhận:";
            // 
            // txtXacNhanMK
            // 
            this.txtXacNhanMK.Location = new System.Drawing.Point(124, 144);
            this.txtXacNhanMK.Name = "txtXacNhanMK";
            this.txtXacNhanMK.Size = new System.Drawing.Size(268, 20);
            this.txtXacNhanMK.TabIndex = 22;
            this.txtXacNhanMK.UseSystemPasswordChar = true;
            // 
            // txtLoginnam
            // 
            this.txtLoginnam.Location = new System.Drawing.Point(124, 57);
            this.txtLoginnam.Name = "txtLoginnam";
            this.txtLoginnam.Size = new System.Drawing.Size(268, 20);
            this.txtLoginnam.TabIndex = 27;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(19, 59);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(74, 15);
            this.label4.TabIndex = 26;
            this.label4.Text = "LoginName:";
            // 
            // grbNhom
            // 
            this.grbNhom.BackColor = System.Drawing.Color.WhiteSmoke;
            this.grbNhom.Controls.Add(this.radNhomGiangVien);
            this.grbNhom.Controls.Add(this.radCoso);
            this.grbNhom.Controls.Add(this.radTruong);
            this.grbNhom.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grbNhom.Location = new System.Drawing.Point(18, 408);
            this.grbNhom.Name = "grbNhom";
            this.grbNhom.Size = new System.Drawing.Size(511, 59);
            this.grbNhom.TabIndex = 14;
            this.grbNhom.TabStop = false;
            this.grbNhom.Text = "NHÓM";
            // 
            // radNhomGiangVien
            // 
            this.radNhomGiangVien.AutoSize = true;
            this.radNhomGiangVien.Location = new System.Drawing.Point(385, 24);
            this.radNhomGiangVien.Name = "radNhomGiangVien";
            this.radNhomGiangVien.Size = new System.Drawing.Size(113, 21);
            this.radNhomGiangVien.TabIndex = 2;
            this.radNhomGiangVien.TabStop = true;
            this.radNhomGiangVien.Text = "GIANGVIEN";
            this.radNhomGiangVien.UseVisualStyleBackColor = true;
            this.radNhomGiangVien.CheckedChanged += new System.EventHandler(this.radNhomGiangVien_CheckedChanged);
            // 
            // radCoso
            // 
            this.radCoso.AutoSize = true;
            this.radCoso.Location = new System.Drawing.Point(214, 24);
            this.radCoso.Name = "radCoso";
            this.radCoso.Size = new System.Drawing.Size(69, 21);
            this.radCoso.TabIndex = 1;
            this.radCoso.TabStop = true;
            this.radCoso.Text = "COSO";
            this.radCoso.UseVisualStyleBackColor = true;
            this.radCoso.CheckedChanged += new System.EventHandler(this.radCoso_CheckedChanged);
            // 
            // radTruong
            // 
            this.radTruong.AutoSize = true;
            this.radTruong.Location = new System.Drawing.Point(32, 24);
            this.radTruong.Name = "radTruong";
            this.radTruong.Size = new System.Drawing.Size(92, 21);
            this.radTruong.TabIndex = 0;
            this.radTruong.TabStop = true;
            this.radTruong.Text = "TRUONG";
            this.radTruong.UseVisualStyleBackColor = true;
            this.radTruong.CheckedChanged += new System.EventHandler(this.radTruong_CheckedChanged);
            // 
            // btn_DangkY
            // 
            this.btn_DangkY.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btn_DangkY.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_DangkY.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.btn_DangkY.Location = new System.Drawing.Point(127, 497);
            this.btn_DangkY.Name = "btn_DangkY";
            this.btn_DangkY.Size = new System.Drawing.Size(98, 41);
            this.btn_DangkY.TabIndex = 28;
            this.btn_DangkY.Text = "Đăng ký";
            this.btn_DangkY.UseVisualStyleBackColor = false;
            this.btn_DangkY.Click += new System.EventHandler(this.btn_DangkY_Click);
            // 
            // btn_exit
            // 
            this.btn_exit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btn_exit.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_exit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btn_exit.Location = new System.Drawing.Point(358, 497);
            this.btn_exit.Name = "btn_exit";
            this.btn_exit.Size = new System.Drawing.Size(98, 41);
            this.btn_exit.TabIndex = 29;
            this.btn_exit.Text = "Exit";
            this.btn_exit.UseVisualStyleBackColor = false;
            this.btn_exit.Click += new System.EventHandler(this.btn_exit_Click);
            // 
            // panInput
            // 
            this.panInput.Controls.Add(this.btnGoiY);
            this.panInput.Controls.Add(this.btnKiemTra);
            this.panInput.Controls.Add(this.txtUserName_MaGV);
            this.panInput.Controls.Add(this.label2);
            this.panInput.Controls.Add(this.txtMATKHAU);
            this.panInput.Controls.Add(this.label3);
            this.panInput.Controls.Add(this.txtLoginnam);
            this.panInput.Controls.Add(this.picSee1);
            this.panInput.Controls.Add(this.label4);
            this.panInput.Controls.Add(this.picNotSee1);
            this.panInput.Controls.Add(this.picNotSee2);
            this.panInput.Controls.Add(this.txtXacNhanMK);
            this.panInput.Controls.Add(this.picSee2);
            this.panInput.Controls.Add(this.label1);
            this.panInput.Location = new System.Drawing.Point(33, 222);
            this.panInput.Name = "panInput";
            this.panInput.Size = new System.Drawing.Size(483, 180);
            this.panInput.TabIndex = 30;
            // 
            // btnGoiY
            // 
            this.btnGoiY.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnGoiY.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGoiY.Location = new System.Drawing.Point(398, 54);
            this.btnGoiY.Name = "btnGoiY";
            this.btnGoiY.Size = new System.Drawing.Size(75, 23);
            this.btnGoiY.TabIndex = 32;
            this.btnGoiY.Text = "! Gợi ý";
            this.btnGoiY.UseVisualStyleBackColor = false;
            this.btnGoiY.Click += new System.EventHandler(this.btnGoiY_Click);
            // 
            // btnKiemTra
            // 
            this.btnKiemTra.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnKiemTra.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnKiemTra.Location = new System.Drawing.Point(398, 12);
            this.btnKiemTra.Name = "btnKiemTra";
            this.btnKiemTra.Size = new System.Drawing.Size(75, 23);
            this.btnKiemTra.TabIndex = 31;
            this.btnKiemTra.Text = "Search";
            this.btnKiemTra.UseVisualStyleBackColor = false;
            this.btnKiemTra.Click += new System.EventHandler(this.btnKiemTra_Click);
            // 
            // panSearch
            // 
            this.panSearch.BackColor = System.Drawing.Color.White;
            this.panSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panSearch.Controls.Add(this.lblSearchSV);
            this.panSearch.Controls.Add(this.dtgvGiaoVienChuaCoTK);
            this.panSearch.Controls.Add(this.btnX_thoatSearch);
            this.panSearch.Controls.Add(this.lblLine);
            this.panSearch.Controls.Add(this.lblSearchGV);
            this.panSearch.Location = new System.Drawing.Point(535, 66);
            this.panSearch.Name = "panSearch";
            this.panSearch.Size = new System.Drawing.Size(500, 401);
            this.panSearch.TabIndex = 31;
            // 
            // lblSearchSV
            // 
            this.lblSearchSV.AutoSize = true;
            this.lblSearchSV.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSearchSV.ForeColor = System.Drawing.Color.Red;
            this.lblSearchSV.Location = new System.Drawing.Point(135, 43);
            this.lblSearchSV.Name = "lblSearchSV";
            this.lblSearchSV.Size = new System.Drawing.Size(236, 15);
            this.lblSearchSV.TabIndex = 4;
            this.lblSearchSV.Text = "CÁC SINH VIÊN CHƯA CÓ TÀI KHOẢN ";
            // 
            // dtgvGiaoVienChuaCoTK
            // 
            this.dtgvGiaoVienChuaCoTK.AllowUserToAddRows = false;
            this.dtgvGiaoVienChuaCoTK.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dtgvGiaoVienChuaCoTK.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgvGiaoVienChuaCoTK.Location = new System.Drawing.Point(17, 79);
            this.dtgvGiaoVienChuaCoTK.Name = "dtgvGiaoVienChuaCoTK";
            this.dtgvGiaoVienChuaCoTK.Size = new System.Drawing.Size(462, 294);
            this.dtgvGiaoVienChuaCoTK.TabIndex = 3;
            this.dtgvGiaoVienChuaCoTK.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtgvGiaoVienChuaCoTK_CellClick);
            // 
            // btnX_thoatSearch
            // 
            this.btnX_thoatSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnX_thoatSearch.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnX_thoatSearch.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnX_thoatSearch.Location = new System.Drawing.Point(450, -1);
            this.btnX_thoatSearch.Name = "btnX_thoatSearch";
            this.btnX_thoatSearch.Size = new System.Drawing.Size(48, 27);
            this.btnX_thoatSearch.TabIndex = 2;
            this.btnX_thoatSearch.Text = "X";
            this.btnX_thoatSearch.UseVisualStyleBackColor = false;
            this.btnX_thoatSearch.Click += new System.EventHandler(this.btnX_thoatSearch_Click);
            // 
            // lblLine
            // 
            this.lblLine.BackColor = System.Drawing.Color.LightSteelBlue;
            this.lblLine.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblLine.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblLine.Location = new System.Drawing.Point(0, 0);
            this.lblLine.Name = "lblLine";
            this.lblLine.Size = new System.Drawing.Size(498, 26);
            this.lblLine.TabIndex = 1;
            this.lblLine.Click += new System.EventHandler(this.lblLine_Click);
            // 
            // lblSearchGV
            // 
            this.lblSearchGV.AutoSize = true;
            this.lblSearchGV.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSearchGV.ForeColor = System.Drawing.Color.Blue;
            this.lblSearchGV.Location = new System.Drawing.Point(135, 43);
            this.lblSearchGV.Name = "lblSearchGV";
            this.lblSearchGV.Size = new System.Drawing.Size(242, 15);
            this.lblSearchGV.TabIndex = 0;
            this.lblSearchGV.Text = "CÁC NHÂN VIÊN CHƯA CÓ TÀI KHOẢN ";
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // frmDangKy
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(568, 572);
            this.Controls.Add(this.panSearch);
            this.Controls.Add(this.panInput);
            this.Controls.Add(this.btn_exit);
            this.Controls.Add(this.btn_DangkY);
            this.Controls.Add(this.grbNhom);
            this.Controls.Add(this.grbDoiTuong);
            this.Controls.Add(this.picPTIT);
            this.Controls.Add(this.lblTieuDeDK);
            this.Name = "frmDangKy";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmDangKy";
            this.Load += new System.EventHandler(this.frmDangKy_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picPTIT)).EndInit();
            this.grbDoiTuong.ResumeLayout(false);
            this.grbDoiTuong.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picNotSee1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picSee1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picNotSee2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picSee2)).EndInit();
            this.grbNhom.ResumeLayout(false);
            this.grbNhom.PerformLayout();
            this.panInput.ResumeLayout(false);
            this.panInput.PerformLayout();
            this.panSearch.ResumeLayout(false);
            this.panSearch.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvGiaoVienChuaCoTK)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTieuDeDK;
        private System.Windows.Forms.PictureBox picPTIT;
        private System.Windows.Forms.GroupBox grbDoiTuong;
        private System.Windows.Forms.RadioButton radSinhVien;
        private System.Windows.Forms.RadioButton radGiangVien;
        private System.Windows.Forms.PictureBox picNotSee1;
        private System.Windows.Forms.PictureBox picSee1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtMATKHAU;
        private System.Windows.Forms.TextBox txtUserName_MaGV;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox picNotSee2;
        private System.Windows.Forms.PictureBox picSee2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtXacNhanMK;
        private System.Windows.Forms.TextBox txtLoginnam;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox grbNhom;
        private System.Windows.Forms.RadioButton radNhomGiangVien;
        private System.Windows.Forms.RadioButton radCoso;
        private System.Windows.Forms.RadioButton radTruong;
        private System.Windows.Forms.Button btn_DangkY;
        private System.Windows.Forms.Button btn_exit;
        private System.Windows.Forms.Panel panInput;
        private System.Windows.Forms.Button btnKiemTra;
        private System.Windows.Forms.Panel panSearch;
        private System.Windows.Forms.Button btnX_thoatSearch;
        private System.Windows.Forms.Label lblLine;
        private System.Windows.Forms.Label lblSearchGV;
        private System.Windows.Forms.DataGridView dtgvGiaoVienChuaCoTK;
        private System.Windows.Forms.Button btnGoiY;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.Label lblSearchSV;
    }
}