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
            this.lblError = new System.Windows.Forms.Label();
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
            this.panel1.Location = new System.Drawing.Point(43, 110);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1563, 162);
            this.panel1.TabIndex = 5;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // lblError
            // 
            this.lblError.AutoSize = true;
            this.lblError.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblError.ForeColor = System.Drawing.Color.Red;
            this.lblError.Location = new System.Drawing.Point(719, 84);
            this.lblError.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblError.Name = "lblError";
            this.lblError.Size = new System.Drawing.Size(113, 17);
            this.lblError.TabIndex = 8;
            this.lblError.Text = "Không có dữ liệu!";
            // 
            // txtMaSV
            // 
            this.txtMaSV.Location = new System.Drawing.Point(137, 32);
            this.txtMaSV.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtMaSV.Name = "txtMaSV";
            this.txtMaSV.Size = new System.Drawing.Size(205, 22);
            this.txtMaSV.TabIndex = 7;
            // 
            // btnXemRP
            // 
            this.btnXemRP.BackColor = System.Drawing.Color.Salmon;
            this.btnXemRP.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold);
            this.btnXemRP.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnXemRP.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnXemRP.Location = new System.Drawing.Point(704, 106);
            this.btnXemRP.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
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
            this.lblLan.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblLan.Location = new System.Drawing.Point(1156, 33);
            this.lblLan.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblLan.Name = "lblLan";
            this.lblLan.Size = new System.Drawing.Size(59, 17);
            this.lblLan.TabIndex = 5;
            this.lblLan.Text = "Lần thi:";
            // 
            // cmbLan
            // 
            this.cmbLan.FormattingEnabled = true;
            this.cmbLan.Items.AddRange(new object[] {
            "1",
            "2"});
            this.cmbLan.Location = new System.Drawing.Point(1229, 31);
            this.cmbLan.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmbLan.Name = "cmbLan";
            this.cmbLan.Size = new System.Drawing.Size(227, 24);
            this.cmbLan.TabIndex = 4;
            // 
            // lblMonhoc
            // 
            this.lblMonhoc.AutoSize = true;
            this.lblMonhoc.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold);
            this.lblMonhoc.ImeMode = System.Windows.Forms.ImeMode.NoControl;
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
            this.cmbMonhoc.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmbMonhoc.Name = "cmbMonhoc";
            this.cmbMonhoc.Size = new System.Drawing.Size(227, 24);
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
            this.lblLop.Location = new System.Drawing.Point(24, 34);
            this.lblLop.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblLop.Name = "lblLop";
            this.lblLop.Size = new System.Drawing.Size(98, 17);
            this.lblLop.TabIndex = 1;
            this.lblLop.Text = "Mã sinh viên:";
            // 
            // lblReportKQ
            // 
            this.lblReportKQ.Font = new System.Drawing.Font("Segoe UI Black", 14.25F, System.Drawing.FontStyle.Bold);
            this.lblReportKQ.ForeColor = System.Drawing.Color.Red;
            this.lblReportKQ.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblReportKQ.Location = new System.Drawing.Point(97, 28);
            this.lblReportKQ.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblReportKQ.Name = "lblReportKQ";
            this.lblReportKQ.Size = new System.Drawing.Size(1485, 47);
            this.lblReportKQ.TabIndex = 4;
            this.lblReportKQ.Text = "XEM KẾT QUẢ THI";
            this.lblReportKQ.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblReportKQ.Click += new System.EventHandler(this.lblReportKQ_Click);
            // 
            // grbReport
            // 
            this.grbReport.Controls.Add(this.rp9);
            this.grbReport.Font = new System.Drawing.Font("Segoe UI Black", 9F, System.Drawing.FontStyle.Bold);
            this.grbReport.Location = new System.Drawing.Point(33, 299);
            this.grbReport.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.grbReport.Name = "grbReport";
            this.grbReport.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.grbReport.Size = new System.Drawing.Size(1573, 457);
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
            this.rp9.Location = new System.Drawing.Point(4, 25);
            this.rp9.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.rp9.Name = "rp9";
            this.rp9.ServerReport.BearerToken = null;
            this.rp9.Size = new System.Drawing.Size(1565, 428);
            this.rp9.TabIndex = 0;
            // 
            // frmRpCau9
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1640, 784);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lblReportKQ);
            this.Controls.Add(this.grbReport);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
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