﻿namespace CSDLPT.ReportTN
{
    partial class frmCau11
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
            this.lblReportKQ = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnXemRP = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbCoso = new System.Windows.Forms.ComboBox();
            this.dateKT = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dateBD = new System.Windows.Forms.DateTimePicker();
            this.grbReport = new System.Windows.Forms.GroupBox();
            this.rp11 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.lblError = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.grbReport.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblReportKQ
            // 
            this.lblReportKQ.Font = new System.Drawing.Font("Segoe UI Black", 14.25F, System.Drawing.FontStyle.Bold);
            this.lblReportKQ.ForeColor = System.Drawing.Color.Red;
            this.lblReportKQ.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblReportKQ.Location = new System.Drawing.Point(45, 9);
            this.lblReportKQ.Name = "lblReportKQ";
            this.lblReportKQ.Size = new System.Drawing.Size(1114, 38);
            this.lblReportKQ.TabIndex = 5;
            this.lblReportKQ.Text = "DANH SÁCH ĐĂNG KÝ THI TRẮC NGHIỆM ";
            this.lblReportKQ.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblReportKQ.Click += new System.EventHandler(this.lblReportKQ_Click);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.lblError);
            this.panel1.Controls.Add(this.btnXemRP);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.cmbCoso);
            this.panel1.Controls.Add(this.dateKT);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.dateBD);
            this.panel1.Location = new System.Drawing.Point(39, 105);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(200, 389);
            this.panel1.TabIndex = 6;
            // 
            // btnXemRP
            // 
            this.btnXemRP.BackColor = System.Drawing.Color.LightSalmon;
            this.btnXemRP.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnXemRP.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnXemRP.Location = new System.Drawing.Point(32, 284);
            this.btnXemRP.Name = "btnXemRP";
            this.btnXemRP.Size = new System.Drawing.Size(130, 37);
            this.btnXemRP.TabIndex = 6;
            this.btnXemRP.Text = "XEM BÁO CÁO";
            this.btnXemRP.UseVisualStyleBackColor = false;
            this.btnXemRP.Click += new System.EventHandler(this.btnXemRP_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(20, 171);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 15);
            this.label3.TabIndex = 5;
            this.label3.Text = "CƠ SỞ:";
            // 
            // cmbCoso
            // 
            this.cmbCoso.FormattingEnabled = true;
            this.cmbCoso.Items.AddRange(new object[] {
            "CS1",
            "CS2",
            "TẤT CẢ"});
            this.cmbCoso.Location = new System.Drawing.Point(23, 206);
            this.cmbCoso.Name = "cmbCoso";
            this.cmbCoso.Size = new System.Drawing.Size(153, 21);
            this.cmbCoso.TabIndex = 4;
            this.cmbCoso.SelectedIndexChanged += new System.EventHandler(this.cmbCoso_SelectedIndexChanged);
            // 
            // dateKT
            // 
            this.dateKT.CustomFormat = "yyyy-MM-dd";
            this.dateKT.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateKT.Location = new System.Drawing.Point(23, 132);
            this.dateKT.Name = "dateKT";
            this.dateKT.Size = new System.Drawing.Size(153, 20);
            this.dateKT.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(20, 95);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "ĐẾN NGÀY:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(20, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "TỪ NGÀY:";
            // 
            // dateBD
            // 
            this.dateBD.CustomFormat = "yyyy-MM-dd";
            this.dateBD.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateBD.Location = new System.Drawing.Point(23, 51);
            this.dateBD.Name = "dateBD";
            this.dateBD.Size = new System.Drawing.Size(153, 20);
            this.dateBD.TabIndex = 0;
            // 
            // grbReport
            // 
            this.grbReport.Controls.Add(this.rp11);
            this.grbReport.Font = new System.Drawing.Font("Segoe UI Black", 9F, System.Drawing.FontStyle.Bold);
            this.grbReport.Location = new System.Drawing.Point(260, 82);
            this.grbReport.Name = "grbReport";
            this.grbReport.Size = new System.Drawing.Size(958, 412);
            this.grbReport.TabIndex = 7;
            this.grbReport.TabStop = false;
            this.grbReport.Text = "KẾT QUẢ BÁO CÁO";
            // 
            // rp11
            // 
            this.rp11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rp11.LocalReport.ReportEmbeddedResource = "CSDLPT.rpCau11.rdlc";
            this.rp11.Location = new System.Drawing.Point(3, 20);
            this.rp11.Name = "rp11";
            this.rp11.ServerReport.BearerToken = null;
            this.rp11.Size = new System.Drawing.Size(952, 389);
            this.rp11.TabIndex = 0;
            // 
            // lblError
            // 
            this.lblError.AutoSize = true;
            this.lblError.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblError.ForeColor = System.Drawing.Color.Red;
            this.lblError.Location = new System.Drawing.Point(49, 253);
            this.lblError.Name = "lblError";
            this.lblError.Size = new System.Drawing.Size(93, 15);
            this.lblError.TabIndex = 7;
            this.lblError.Text = "Không có dữ liệu!";
            // 
            // frmCau11
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1230, 637);
            this.Controls.Add(this.grbReport);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lblReportKQ);
            this.Name = "frmCau11";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmCau11";
            this.Load += new System.EventHandler(this.frmCau11_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.grbReport.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblReportKQ;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dateBD;
        private System.Windows.Forms.GroupBox grbReport;
        private System.Windows.Forms.Button btnXemRP;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbCoso;
        private System.Windows.Forms.DateTimePicker dateKT;
        private Microsoft.Reporting.WinForms.ReportViewer rp11;
        private System.Windows.Forms.Label lblError;
    }
}