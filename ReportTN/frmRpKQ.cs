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
    public partial class frmRpKQ : Form
    {
        public frmRpKQ()
        {
            InitializeComponent();
        }

        private void frmRpKQ_Load(object sender, EventArgs e)
        {
            lblError.Visible = false;
            this.rp.RefreshReport();
            this.rp.RefreshReport();
            // do du lieu vao cmb lop
            String statementCmbLop = "select MALOP from LINK3.TRACNGHIEM1.dbo.LOP";
            Program.myReader = Program.ExecSqlDataReader(statementCmbLop);
            if (Program.myReader == null)
                return;
            while (Program.myReader.Read())
            {
                cmbLop.Items.Add(Program.myReader.GetString(0));
            }
            Program.myReader.Close();
            // do du lieu vao cmb Monhoc
            String statementCmbMonhoc = "select tenmh from MONHOC";
            Program.myReader = Program.ExecSqlDataReader(statementCmbMonhoc);
            if (Program.myReader == null)
                return;
            while (Program.myReader.Read())
            {
                cmbMonhoc.Items.Add(Program.myReader.GetString(0));
            }
            Program.myReader.Close();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void btnXemRP_Click(object sender, EventArgs e)
        {
            if (cmbMonhoc.Text == "" || cmbLop.Text == "" || cmbLan.Text == "")
            {
                MessageBox.Show("Vui lòng không để trống các thông tin trên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
            {
                string mamh = "";
                String statement = "select mamh from monhoc where tenmh= N'" + cmbMonhoc.SelectedItem + "'";
                Program.myReader = Program.ExecSqlDataReader(statement);
                if (Program.myReader == null)
                    return;
                while (Program.myReader.Read())
                {
                    mamh = Program.myReader.GetString(0);
                }
                Program.myReader.Close();
                DateTime today = DateTime.Now;

                // Chuyển đổi ngày hiện tại sang định dạng dd/MM/yyyy
                string formattedDate = today.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                ReportParameterCollection rP = new ReportParameterCollection();

                rP.Add(new ReportParameter("rpParamNguoiBC", Program.staff));
                rP.Add(new ReportParameter("rpParamNgayBC", formattedDate));
                rp.LocalReport.SetParameters(rP);
                //SqlConnection con1 = new SqlConnection();
                // con1.ConnectionString = @"Data Source=DUONG;Initial Catalog=TRACNGHIEM;Integrated Security=True;Encrypt=False";
                SqlCommand command = new SqlCommand();
                command.CommandText = "EXEC LINK0.TRACNGHIEM1.DBO.sp_InBangDiem '" + cmbLop.Text + "', '" + mamh + "', '" + cmbLan.Text + "'";
                command.Connection = Program.conn;
                System.Data.DataSet ds = new System.Data.DataSet();
                SqlDataAdapter dap = new SqlDataAdapter(command);
                dap.Fill(ds);

                rp.ProcessingMode = ProcessingMode.Local;
                rp.LocalReport.ReportPath = "rpXemBD.rdlc";

                if (ds.Tables[0].Rows.Count > 0)
                {
                    ReportDataSource rds = new ReportDataSource();
                    rds.Name = "XemBangDiem";
                    rds.Value = ds.Tables[0];
                    rp.LocalReport.DataSources.Clear();
                    rp.LocalReport.DataSources.Add(rds);
                    rp.RefreshReport();
                    lblError.Visible = false;
                }
                else
                {
                    rp.LocalReport.DataSources.Clear();
                    rp.RefreshReport();
                    lblError.Visible = true;
                }
            }

        }
    }
}
