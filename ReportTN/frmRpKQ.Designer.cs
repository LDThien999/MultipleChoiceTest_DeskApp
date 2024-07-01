namespace CSDLPT.ReportTN
{
    partial class frmRpKQ
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
            this.grbReport = new System.Windows.Forms.GroupBox();
            this.rp = new Microsoft.Reporting.WinForms.ReportViewer();
            this.lblReportKQ = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblError = new System.Windows.Forms.Label();
            this.btnXemRP = new System.Windows.Forms.Button();
            this.lblLan = new System.Windows.Forms.Label();
            this.cmbLan = new System.Windows.Forms.ComboBox();
            this.lblMonhoc = new System.Windows.Forms.Label();
            this.cmbMonhoc = new System.Windows.Forms.ComboBox();
            this.lblLop = new System.Windows.Forms.Label();
            this.cmbLop = new System.Windows.Forms.ComboBox();
            this.grbReport.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // grbReport
            // 
            this.grbReport.Controls.Add(this.rp);
            this.grbReport.Font = new System.Drawing.Font("Segoe UI Black", 9F, System.Drawing.FontStyle.Bold);
            this.grbReport.Location = new System.Drawing.Point(31, 300);
            this.grbReport.Margin = new System.Windows.Forms.Padding(4);
            this.grbReport.Name = "grbReport";
            this.grbReport.Padding = new System.Windows.Forms.Padding(4);
            this.grbReport.Size = new System.Drawing.Size(1489, 457);
            this.grbReport.TabIndex = 0;
            this.grbReport.TabStop = false;
            this.grbReport.Text = "KẾT QUẢ BÁO CÁO";
            // 
            // rp
            // 
            this.rp.AutoSize = true;
            this.rp.Cursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.rp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rp.LocalReport.ReportEmbeddedResource = "CSDLPT.rpXemBD.rdlc";
            this.rp.Location = new System.Drawing.Point(4, 25);
            this.rp.Margin = new System.Windows.Forms.Padding(4);
            this.rp.Name = "rp";
            this.rp.ServerReport.BearerToken = null;
            this.rp.Size = new System.Drawing.Size(1481, 428);
            this.rp.TabIndex = 0;
            // 
            // lblReportKQ
            // 
            this.lblReportKQ.Font = new System.Drawing.Font("Segoe UI Black", 14.25F, System.Drawing.FontStyle.Bold);
            this.lblReportKQ.ForeColor = System.Drawing.Color.Red;
            this.lblReportKQ.Location = new System.Drawing.Point(95, 30);
            this.lblReportKQ.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblReportKQ.Name = "lblReportKQ";
            this.lblReportKQ.Size = new System.Drawing.Size(1485, 47);
            this.lblReportKQ.TabIndex = 1;
            this.lblReportKQ.Text = "XEM BẢNG ĐIỂM MÔN HỌC";
            this.lblReportKQ.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.lblError);
            this.panel1.Controls.Add(this.btnXemRP);
            this.panel1.Controls.Add(this.lblLan);
            this.panel1.Controls.Add(this.cmbLan);
            this.panel1.Controls.Add(this.lblMonhoc);
            this.panel1.Controls.Add(this.cmbMonhoc);
            this.panel1.Controls.Add(this.lblLop);
            this.panel1.Controls.Add(this.cmbLop);
            this.panel1.Location = new System.Drawing.Point(40, 111);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1476, 162);
            this.panel1.TabIndex = 2;
            // 
            // lblError
            // 
            this.lblError.AutoSize = true;
            this.lblError.Font = new System.Drawing.Font("Times New Roman", 9F);
            this.lblError.ForeColor = System.Drawing.Color.Red;
            this.lblError.Location = new System.Drawing.Point(717, 84);
            this.lblError.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblError.Name = "lblError";
            this.lblError.Size = new System.Drawing.Size(113, 17);
            this.lblError.TabIndex = 8;
            this.lblError.Text = "Không có dữ liệu!";
            // 
            // btnXemRP
            // 
            this.btnXemRP.BackColor = System.Drawing.Color.Salmon;
            this.btnXemRP.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold);
            this.btnXemRP.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnXemRP.Location = new System.Drawing.Point(704, 106);
            this.btnXemRP.Margin = new System.Windows.Forms.Padding(4);
            this.btnXemRP.Name = "btnXemRP";
            this.btnXemRP.Size = new System.Drawing.Size(156, 37);
            this.btnXemRP.TabIndex = 6;
            this.btnXemRP.Text = "Xem báo cáo";
            this.btnXemRP.UseVisualStyleBackColor = false;
            this.btnXemRP.Click += new System.EventHandler(this.btnXemRP_Click);
            // 
            // lblLan
            // 
            this.lblLan.AutoSize = true;
            this.lblLan.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold);
            this.lblLan.Location = new System.Drawing.Point(1110, 33);
            this.lblLan.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblLan.Name = "lblLan";
            this.lblLan.Size = new System.Drawing.Size(59, 17);
            this.lblLan.TabIndex = 5;
            this.lblLan.Text = "Lần thi:";
            this.lblLan.Click += new System.EventHandler(this.label2_Click);
            // 
            // cmbLan
            // 
            this.cmbLan.FormattingEnabled = true;
            this.cmbLan.Items.AddRange(new object[] {
            "1",
            "2"});
            this.cmbLan.Location = new System.Drawing.Point(1183, 31);
            this.cmbLan.Margin = new System.Windows.Forms.Padding(4);
            this.cmbLan.Name = "cmbLan";
            this.cmbLan.Size = new System.Drawing.Size(227, 24);
            this.cmbLan.TabIndex = 4;
            // 
            // lblMonhoc
            // 
            this.lblMonhoc.AutoSize = true;
            this.lblMonhoc.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold);
            this.lblMonhoc.Location = new System.Drawing.Point(575, 34);
            this.lblMonhoc.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMonhoc.Name = "lblMonhoc";
            this.lblMonhoc.Size = new System.Drawing.Size(71, 17);
            this.lblMonhoc.TabIndex = 3;
            this.lblMonhoc.Text = "Môn học:";
            // 
            // cmbMonhoc
            // 
            this.cmbMonhoc.Font = new System.Drawing.Font("Times New Roman", 8.25F);
            this.cmbMonhoc.FormattingEnabled = true;
            this.cmbMonhoc.Location = new System.Drawing.Point(672, 31);
            this.cmbMonhoc.Margin = new System.Windows.Forms.Padding(4);
            this.cmbMonhoc.Name = "cmbMonhoc";
            this.cmbMonhoc.Size = new System.Drawing.Size(227, 24);
            this.cmbMonhoc.TabIndex = 2;
            // 
            // lblLop
            // 
            this.lblLop.AutoSize = true;
            this.lblLop.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold);
            this.lblLop.Location = new System.Drawing.Point(24, 34);
            this.lblLop.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblLop.Name = "lblLop";
            this.lblLop.Size = new System.Drawing.Size(39, 17);
            this.lblLop.TabIndex = 1;
            this.lblLop.Text = "Lớp:";
            // 
            // cmbLop
            // 
            this.cmbLop.Font = new System.Drawing.Font("Times New Roman", 8.25F);
            this.cmbLop.FormattingEnabled = true;
            this.cmbLop.Location = new System.Drawing.Point(80, 31);
            this.cmbLop.Margin = new System.Windows.Forms.Padding(4);
            this.cmbLop.Name = "cmbLop";
            this.cmbLop.Size = new System.Drawing.Size(227, 24);
            this.cmbLop.TabIndex = 0;
            // 
            // frmRpKQ
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(1640, 784);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lblReportKQ);
            this.Controls.Add(this.grbReport);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmRpKQ";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BÁO CÁO";
            this.Load += new System.EventHandler(this.frmRpKQ_Load);
            this.grbReport.ResumeLayout(false);
            this.grbReport.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grbReport;
        private Microsoft.Reporting.WinForms.ReportViewer rp;
        private System.Windows.Forms.Label lblReportKQ;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblLop;
        private System.Windows.Forms.ComboBox cmbLop;
        private System.Windows.Forms.Label lblLan;
        private System.Windows.Forms.ComboBox cmbLan;
        private System.Windows.Forms.Label lblMonhoc;
        private System.Windows.Forms.ComboBox cmbMonhoc;
        private System.Windows.Forms.Button btnXemRP;
        private System.Windows.Forms.Label lblError;
    }
}