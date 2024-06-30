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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRpKQ));
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
            resources.ApplyResources(this.grbReport, "grbReport");
            this.grbReport.Controls.Add(this.rp);
            this.grbReport.Name = "grbReport";
            this.grbReport.TabStop = false;
            // 
            // rp
            // 
            resources.ApplyResources(this.rp, "rp");
            this.rp.Cursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.rp.LocalReport.ReportEmbeddedResource = "CSDLPT.rpXemBD.rdlc";
            this.rp.Name = "rp";
            this.rp.ServerReport.BearerToken = null;
            // 
            // lblReportKQ
            // 
            resources.ApplyResources(this.lblReportKQ, "lblReportKQ");
            this.lblReportKQ.ForeColor = System.Drawing.Color.Red;
            this.lblReportKQ.Name = "lblReportKQ";
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.lblError);
            this.panel1.Controls.Add(this.btnXemRP);
            this.panel1.Controls.Add(this.lblLan);
            this.panel1.Controls.Add(this.cmbLan);
            this.panel1.Controls.Add(this.lblMonhoc);
            this.panel1.Controls.Add(this.cmbMonhoc);
            this.panel1.Controls.Add(this.lblLop);
            this.panel1.Controls.Add(this.cmbLop);
            this.panel1.Name = "panel1";
            // 
            // lblError
            // 
            resources.ApplyResources(this.lblError, "lblError");
            this.lblError.ForeColor = System.Drawing.Color.Red;
            this.lblError.Name = "lblError";
            // 
            // btnXemRP
            // 
            resources.ApplyResources(this.btnXemRP, "btnXemRP");
            this.btnXemRP.BackColor = System.Drawing.Color.Salmon;
            this.btnXemRP.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnXemRP.Name = "btnXemRP";
            this.btnXemRP.UseVisualStyleBackColor = false;
            this.btnXemRP.Click += new System.EventHandler(this.btnXemRP_Click);
            // 
            // lblLan
            // 
            resources.ApplyResources(this.lblLan, "lblLan");
            this.lblLan.Name = "lblLan";
            this.lblLan.Click += new System.EventHandler(this.label2_Click);
            // 
            // cmbLan
            // 
            resources.ApplyResources(this.cmbLan, "cmbLan");
            this.cmbLan.FormattingEnabled = true;
            this.cmbLan.Items.AddRange(new object[] {
            resources.GetString("cmbLan.Items"),
            resources.GetString("cmbLan.Items1")});
            this.cmbLan.Name = "cmbLan";
            // 
            // lblMonhoc
            // 
            resources.ApplyResources(this.lblMonhoc, "lblMonhoc");
            this.lblMonhoc.Name = "lblMonhoc";
            // 
            // cmbMonhoc
            // 
            resources.ApplyResources(this.cmbMonhoc, "cmbMonhoc");
            this.cmbMonhoc.FormattingEnabled = true;
            this.cmbMonhoc.Name = "cmbMonhoc";
            // 
            // lblLop
            // 
            resources.ApplyResources(this.lblLop, "lblLop");
            this.lblLop.Name = "lblLop";
            // 
            // cmbLop
            // 
            resources.ApplyResources(this.cmbLop, "cmbLop");
            this.cmbLop.FormattingEnabled = true;
            this.cmbLop.Name = "cmbLop";
            // 
            // frmRpKQ
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lblReportKQ);
            this.Controls.Add(this.grbReport);
            this.Name = "frmRpKQ";
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