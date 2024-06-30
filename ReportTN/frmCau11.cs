using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using TNCSDLPT;

namespace CSDLPT.ReportTN
{
    public partial class frmCau11 : Form
    {
        public frmCau11()
        {
            InitializeComponent();
        }

        private void lblReportKQ_Click(object sender, EventArgs e)
        {

        }
       
        

        private void frmCau11_Load(object sender, EventArgs e)
        {
            this.rp11.RefreshReport();
            lblError.Visible = false;
          
        }

        private void cmbCoso_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        String kiemTraCoSo()
        {
            String a;
            if (Program.brand == 0)
                a = "CƠ SỞ 1";
            else
                a = "CƠ SỞ 2";
            return a;
        }
        private void btnXemRP_Click(object sender, EventArgs e)
        {
            if(cmbCoso.Text == "")
            {
                MessageBox.Show("Vui lòng chọn cơ sở!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if(dateBD.Value > dateKT.Value)
            {
                MessageBox.Show("Vui lòng nhập khoảng thời gian hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if(cmbCoso.Text =="TẤT CẢ")
            {
                DateTime today = DateTime.Now;

                // Chuyển đổi ngày hiện tại sang định dạng dd/MM/yyyy
                string formattedDate = today.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                ReportParameterCollection rP = new ReportParameterCollection();
                rP.Add(new ReportParameter("rpParamDateBD", dateBD.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)));
                rP.Add(new ReportParameter("rpParamDateKT", dateBD.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)));
                rP.Add(new ReportParameter("rpParamNguoiBC", Program.staff));
                rP.Add(new ReportParameter("rpParamNgayBC", formattedDate)); 
                rp11.LocalReport.SetParameters(rP);
                SqlCommand command = new SqlCommand();
                command.CommandText = "EXEC sp_Cau11 '" + dateBD.Text + "', '" + dateKT.Text + "'";
                // command.CommandText = "EXEC sp_XemDSDangKy '2024-04-01','2024-04-03'";
                command.Connection = Program.conn;
                System.Data.DataSet ds = new System.Data.DataSet();
                SqlDataAdapter dap = new SqlDataAdapter(command);
                dap.Fill(ds);

                rp11.ProcessingMode = ProcessingMode.Local;
                rp11.LocalReport.ReportPath = "rpCau11.rdlc";

                if (ds.Tables[0].Rows.Count > 0)
                {
                    ReportDataSource rds = new ReportDataSource();
                    rds.Name = "Cau11";
                    rds.Value = ds.Tables[0];
                    rp11.LocalReport.DataSources.Clear();
                    rp11.LocalReport.DataSources.Add(rds);
                    rp11.RefreshReport();
                    lblError.Visible = false;
                }
                else
                {
                    rp11.LocalReport.DataSources.Clear();
                    rp11.RefreshReport();
                    lblError.Visible = true;
                }
            }
            else if(cmbCoso.Text == kiemTraCoSo() )
            {
                DateTime today = DateTime.Now;

                // Chuyển đổi ngày hiện tại sang định dạng dd/MM/yyyy
                string formattedDate = today.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                ReportParameterCollection rP = new ReportParameterCollection();
                rP.Add(new ReportParameter("rpParamDateBD", dateBD.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)));
                rP.Add(new ReportParameter("rpParamDateKT", dateKT.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)));
                rP.Add(new ReportParameter("rpParamNguoiBC", Program.staff));
                rP.Add(new ReportParameter("rpParamNgayBC", formattedDate));
                rp11.LocalReport.SetParameters(rP);
                SqlCommand command = new SqlCommand();
                command.CommandText = "EXEC sp_XemDSDangKy '" + dateBD.Text + "', '" + dateKT.Text + "'";
                // command.CommandText = "EXEC sp_XemDSDangKy '2024-04-01','2024-04-03'";
                command.Connection = Program.conn;
                System.Data.DataSet ds = new System.Data.DataSet();
                SqlDataAdapter dap = new SqlDataAdapter(command);
                dap.Fill(ds);

                rp11.ProcessingMode = ProcessingMode.Local;
                rp11.LocalReport.ReportPath = "rpCau11.rdlc";

                if (ds.Tables[0].Rows.Count > 0)
                {
                    ReportDataSource rds = new ReportDataSource();
                    rds.Name = "Cau11";
                    rds.Value = ds.Tables[0];
                    rp11.LocalReport.DataSources.Clear();
                    rp11.LocalReport.DataSources.Add(rds);
                    rp11.RefreshReport();
                    lblError.Visible = false;
                }
                else
                {
                    rp11.LocalReport.DataSources.Clear();
                    rp11.RefreshReport();
                    lblError.Visible = true;
                }
            }
            else if (cmbCoso.Text != kiemTraCoSo())
            {
                DateTime today = DateTime.Now;

                // Chuyển đổi ngày hiện tại sang định dạng dd/MM/yyyy
                string formattedDate = today.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                ReportParameterCollection rP = new ReportParameterCollection();
                rP.Add(new ReportParameter("rpParamDateBD", dateBD.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)));
                rP.Add(new ReportParameter("rpParamDateKT", dateKT.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)));
                rP.Add(new ReportParameter("rpParamNguoiBC", Program.staff));
                rP.Add(new ReportParameter("rpParamNgayBC", formattedDate));
                rp11.LocalReport.SetParameters(rP);
                SqlCommand command = new SqlCommand();
                command.CommandText = "EXEC LINK1.TRACNGHIEM1.DBO.sp_XemDSDangKy '" + dateBD.Text + "', '" + dateKT.Text + "'";
                // command.CommandText = "EXEC sp_XemDSDangKy '2024-04-01','2024-04-03'";
                command.Connection = Program.conn;
                System.Data.DataSet ds = new System.Data.DataSet();
                SqlDataAdapter dap = new SqlDataAdapter(command);
                dap.Fill(ds);

                rp11.ProcessingMode = ProcessingMode.Local;
                rp11.LocalReport.ReportPath = "rpCau11.rdlc";

                if (ds.Tables[0].Rows.Count > 0)
                {
                    ReportDataSource rds = new ReportDataSource();
                    rds.Name = "Cau11";
                    rds.Value = ds.Tables[0];
                    rp11.LocalReport.DataSources.Clear();
                    rp11.LocalReport.DataSources.Add(rds);
                    rp11.RefreshReport();
                    lblError.Visible = false;
                }
                else
                {
                    rp11.LocalReport.DataSources.Clear();
                    rp11.RefreshReport();
                    lblError.Visible = true;
                }
            }


        }

        private void lblError_Click(object sender, EventArgs e)
        {

        }
    }
}
