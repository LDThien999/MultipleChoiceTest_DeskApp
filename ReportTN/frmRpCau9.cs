using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using TNCSDLPT;

namespace CSDLPT.ReportTN
{
    public partial class frmRpCau9 : Form
    {
        public frmRpCau9()
        {
            InitializeComponent();
        }

        private void lblReportKQ_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void grbReport_Enter(object sender, EventArgs e)
        {

        }

        private void cmbLop_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmbMonhoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void frmRpCau9_Load(object sender, EventArgs e)
        {
            lblError.Visible = false;
            // do du lieu vao cb mon hoc
            String statementCmbMonhoc = "select mamh from MONHOC";
            Program.myReader = Program.ExecSqlDataReader(statementCmbMonhoc);
            if (Program.myReader == null)
                return;
            while (Program.myReader.Read())
            {
                cmbMonhoc.Items.Add(Program.myReader.GetString(0));
            }
            Program.myReader.Close();

        }

        private void btnXemRP_Click(object sender, EventArgs e)
        {
            
            SqlCommand command = new SqlCommand();
            command.CommandText = "EXEC LINK0.TRACNGHIEM1.DBO.sp_GetBaoCaoTheoSV '" + txtMaSV.Text + "', '" + cmbMonhoc.Text + "', '" + cmbLan.Text + "'";
            command.Connection = Program.conn;
            System.Data.DataSet ds = new System.Data.DataSet();
            SqlDataAdapter dap = new SqlDataAdapter(command);
            dap.Fill(ds);

            rp9.ProcessingMode = ProcessingMode.Local;
            rp9.LocalReport.ReportPath = "rpCau9.rdlc";

            if (ds.Tables[0].Rows.Count > 0)
            {
                ReportDataSource rds = new ReportDataSource();
                rds.Name = "Cau9";
                rds.Value = ds.Tables[0];
                rp9.LocalReport.DataSources.Clear();
                rp9.LocalReport.DataSources.Add(rds);
                rp9.RefreshReport();
                lblError.Visible = false;
            }
            else
            {
                rp9.LocalReport.DataSources.Clear();
                rp9.RefreshReport();
                lblError.Visible = true;
            }
        }

        private void cmbMonhoc_Click(object sender, EventArgs e)
        {
           
           
        }

        private void cmbMonhoc_MouseClick(object sender, MouseEventArgs e)
        {
         
        }
    }
}
