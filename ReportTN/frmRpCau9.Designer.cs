namespace CSDLPT.ReportTN
{
    partial class frmRpCau9
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtMaSV = new System.Windows.Forms.TextBox();
            this.btnXemRP = new System.Windows.Forms.Button();
            this.lblLan = new System.Windows.Forms.Label();
            this.cmbLan = new System.Windows.Forms.ComboBox();
            this.lblMonhoc = new System.Windows.Forms.Label();
            this.cmbMonhoc = new System.Windows.Forms.ComboBox();
            this.lblLop = new System.Windows.Forms.Label();
            this.lblReportKQ = new System.Windows.Forms.Label();
            this.grbReport = new System.Windows.Forms.GroupBox();
            this.rp9 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.lblError = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.grbReport.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.lblError);
            this.panel1.Controls.Add(this.txtMaSV);
            this.panel1.Controls.Add(this.btnXemRP);
            this.panel1.Controls.Add(this.lblLan);
            this.panel1.Controls.Add(this.cmbLan);
            this.panel1.Controls.Add(this.lblMonhoc);
            this.panel1.Controls.Add(this.cmbMonhoc);
            this.panel1.Controls.Add(this.lblLop);
            this.panel1.Location = new System.Drawing.Point(32, 89);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1173, 132);
            this.panel1.TabIndex = 5;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // txtMaSV
            // 
            this.txtMaSV.Location = new System.Drawing.Point(103, 26);
            this.txtMaSV.Name = "txtMaSV";
            this.txtMaSV.Size = new System.Drawing.Size(155, 20);
            this.txtMaSV.TabIndex = 7;
            // 
            // btnXemRP
            // 
            this.btnXemRP.BackColor = System.Drawing.Color.Salmon;
            this.btnXemRP.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold);
            this.btnXemRP.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnXemRP.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnXemRP.Location = new System.Drawing.Point(528, 86);
            this.btnXemRP.Name = "btnXemRP";
            this.btnXemRP.Size = new System.Drawing.Size(117, 30);
            this.btnXemRP.TabIndex = 6;
            this.btnXemRP.Text = "Xem báo cáo";
            this.btnXemRP.UseVisualStyleBackColor = false;
            this.btnXemRP.Click += new System.EventHandler(this.btnXemRP_Click);
            // 
            // lblLan
            // 
            this.lblLan.AutoSize = true;
            this.lblLan.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold);
            this.lblLan.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblLan.Location = new System.Drawing.Point(867, 27);
            this.lblLan.Name = "lblLan";
            this.lblLan.Size = new System.Drawing.Size(49, 15);
            this.lblLan.TabIndex = 5;
            this.lblLan.Text = "Lần thi:";
            // 
            // cmbLan
            // 
            this.cmbLan.FormattingEnabled = true;
            this.cmbLan.Items.AddRange(new object[] {
            "1",
            "2"});
            this.cmbLan.Location = new System.Drawing.Point(922, 25);
            this.cmbLan.Name = "cmbLan";
            this.cmbLan.Size = new System.Drawing.Size(171, 21);
            this.cmbLan.TabIndex = 4;
            // 
            // lblMonhoc
            // 
            this.lblMonhoc.AutoSize = true;
            this.lblMonhoc.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold);
            this.lblMonhoc.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblMonhoc.Location = new System.Drawing.Point(431, 28);
            this.lblMonhoc.Name = "lblMonhoc";
            this.lblMonhoc.Size = new System.Drawing.Size(56, 15);
            this.lblMonhoc.TabIndex = 3;
            this.lblMonhoc.Text = "Môn học:";
            // 
            // cmbMonhoc
            // 
            this.cmbMonhoc.Font = new System.Drawing.Font("Times New Roman", 8.25F);
            this.cmbMonhoc.FormattingEnabled = true;
            this.cmbMonhoc.Location = new System.Drawing.Point(504, 25);
            this.cmbMonhoc.Name = "cmbMonhoc";
            this.cmbMonhoc.Size = new System.Drawing.Size(171, 22);
            this.cmbMonhoc.TabIndex = 2;
            this.cmbMonhoc.SelectedIndexChanged += new System.EventHandler(this.cmbMonhoc_SelectedIndexChanged);
            this.cmbMonhoc.Click += new System.EventHandler(this.cmbMonhoc_Click);
            this.cmbMonhoc.MouseClick += new System.Windows.Forms.MouseEventHandler(this.cmbMonhoc_MouseClick);
            // 
            // lblLop
            // 
            this.lblLop.AutoSize = true;
            this.lblLop.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold);
            this.lblLop.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblLop.Location = new System.Drawing.Point(18, 28);
            this.lblLop.Name = "lblLop";
            this.lblLop.Size = new System.Drawing.Size(79, 15);
            this.lblLop.TabIndex = 1;
            this.lblLop.Text = "Mã sinh viên:";
            // 
            // lblReportKQ
            // 
            this.lblReportKQ.Font = new System.Drawing.Font("Segoe UI Black", 14.25F, System.Drawing.FontStyle.Bold);
            this.lblReportKQ.ForeColor = System.Drawing.Color.Red;
            this.lblReportKQ.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblReportKQ.Location = new System.Drawing.Point(73, 23);
            this.lblReportKQ.Name = "lblReportKQ";
            this.lblReportKQ.Size = new System.Drawing.Size(1114, 38);
            this.lblReportKQ.TabIndex = 4;
            this.lblReportKQ.Text = "XEM KẾT QUẢ THI";
            this.lblReportKQ.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblReportKQ.Click += new System.EventHandler(this.lblReportKQ_Click);
            // 
            // grbReport
            // 
            this.grbReport.Controls.Add(this.rp9);
            this.grbReport.Font = new System.Drawing.Font("Segoe UI Black", 9F, System.Drawing.FontStyle.Bold);
            this.grbReport.Location = new System.Drawing.Point(25, 243);
            this.grbReport.Name = "grbReport";
            this.grbReport.Size = new System.Drawing.Size(1180, 371);
            this.grbReport.TabIndex = 3;
            this.grbReport.TabStop = false;
            this.grbReport.Text = "KẾT QUẢ BÁO CÁO";
            this.grbReport.Enter += new System.EventHandler(this.grbReport_Enter);
            // 
            // rp9
            // 
            this.rp9.AutoSize = true;
            this.rp9.Cursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.rp9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rp9.LocalReport.ReportEmbeddedResource = "CSDLPT.rpCau9.rdlc";
            this.rp9.Location = new System.Drawing.Point(3, 20);
            this.rp9.Name = "rp9";
            this.rp9.ServerReport.BearerToken = null;
            this.rp9.Size = new System.Drawing.Size(1174, 348);
            this.rp9.TabIndex = 0;
            // 
            // lblError
            // 
            this.lblError.AutoSize = true;
            this.lblError.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblError.ForeColor = System.Drawing.Color.Red;
            this.lblError.Location = new System.Drawing.Point(539, 68);
            this.lblError.Name = "lblError";
            this.lblError.Size = new System.Drawing.Size(93, 15);
            this.lblError.TabIndex = 8;
            this.lblError.Text = "Không có dữ liệu!";
            // 
            // frmRpCau9
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1230, 637);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lblReportKQ);
            this.Controls.Add(this.grbReport);
            this.Name = "frmRpCau9";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmRpCau9";
            this.Load += new System.EventHandler(this.frmRpCau9_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.grbReport.ResumeLayout(false);
            this.grbReport.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnXemRP;
        private System.Windows.Forms.Label lblLan;
        private System.Windows.Forms.ComboBox cmbLan;
        private System.Windows.Forms.Label lblMonhoc;
        private System.Windows.Forms.ComboBox cmbMonhoc;
        private System.Windows.Forms.Label lblLop;
        private System.Windows.Forms.Label lblReportKQ;
        private System.Windows.Forms.GroupBox grbReport;
        private Microsoft.Reporting.WinForms.ReportViewer rp9;
        private System.Windows.Forms.TextBox txtMaSV;
        private System.Windows.Forms.Label lblError;
    }
}