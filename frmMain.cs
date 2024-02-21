using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Linq.Expressions;
using TNCSDLPT;

namespace CSDLPT
{
    public partial class frmMain : Form
    {
       
        private SqlConnection conn_publisher = new SqlConnection();

        private void LayDSPM(String cmd)
        {
            DataTable dt = new DataTable();
            if(conn_publisher.State==ConnectionState.Closed) conn_publisher.Open(); 
            SqlDataAdapter da = new SqlDataAdapter(cmd, conn_publisher);
            da.Fill(dt);
            conn_publisher.Close();
            Program.bds_dspm.DataSource = dt;
            cmbCoso.DataSource = Program.bds_dspm;
            cmbCoso.DisplayMember = "TENCS";
            cmbCoso.ValueMember = "TENSERVER";


        }
       

        public frmMain()
        {
            InitializeComponent();
        }
        // kiem tra co mo form nay hai lan khong

        private Form CheckExists(Type ftype)
        {
            foreach (Form f in this.MdiChildren)
                if (f.GetType() == ftype)
                    return f;
            return null;
        }
        private int KetNoiCSDLGOC()
        {
            if (conn_publisher != null && conn_publisher.State == ConnectionState.Open)
                conn_publisher.Close();
            try
            {
                //Program.connstr = "Data Source=" + Program.serverName + ";Initial Catalog=" +
                // Program.database + ";User ID=" +
                //Program.currentLogin + ";password=" + Program.loginPassword + ";Encrypt=False";
                conn_publisher.ConnectionString = Program.connstr_publisher;
                conn_publisher.Open();    
                return 1;
            }

            catch (Exception e)
            {
                MessageBox.Show("Lỗi qqkết nối cơ sở dữ liệu.\nBạn xem lại user name và password.\n " + e.Message, "", MessageBoxButtons.OK);
                return 0;
            }
        }
        private void frmMain_Load(object sender, EventArgs e)
        {
           
            if (KetNoiCSDLGOC() == 0) return;
            LayDSPM("SELECT * FROM Get_Subscribes");
            cmbCoso.SelectedIndex = 1; cmbCoso.SelectedIndex = 0;
        }


       
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            DialogResult tb = MessageBox.Show("Bạn chắc chắn muốn thoát?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (tb == DialogResult.OK)
                Application.Exit();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Program.serverName = cmbCoso.SelectedValue.ToString();
            }
            catch (Exception) { }

        }
            

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {

        }

        private void btn_openlogin_Click(object sender, EventArgs e)
        {
           
            }

        private void button1_Click(object sender, EventArgs e)
        {
       
            if (txtTAIKHOAN.Text.Trim() == "" || txtMATKHAU.Text.Trim() == "")
            {
                MessageBox.Show("Tài khoản & mật khẩu không thể bỏ trống", "Thông Báo", MessageBoxButtons.OK);
                return;
            }
            Program.loginName = txtTAIKHOAN.Text.Trim();
            Program.loginPassword = txtMATKHAU.Text.Trim();
            if (Program.KetNoi() == 0)
                return;
            MessageBox.Show("Đăng nhập thành công!");
            Program.brand = cmbCoso.SelectedIndex;
            Program.currentLogin = Program.loginName;
            Program.currentPassword = Program.loginPassword;
            String statement = "EXEC sp_LayThongTinGiaoVien '" + Program.loginName + "'";
            Program.myReader = Program.ExecSqlDataReader(statement);
            if (Program.myReader == null)
                return;
            
            Program.myReader.Read();
            Program.userName = Program.myReader.GetString(0);// lấy userName
            if (Convert.IsDBNull(Program.userName))
            {
                MessageBox.Show("Tài khoản này không có quyền truy cập \n Hãy thử tài khoản khác", "Thông Báo", MessageBoxButtons.OK);
            }



            Program.staff = Program.myReader.GetString(1);
            Program.role = Program.myReader.GetString(2);

            Program.myReader.Close();
            Program.conn.Close();
            MessageBox.Show("Mã giảng viên: "+Program.userName+"\n Họ tên: "+ Program.staff+"\n Nhóm: "+ Program.role);
        }
    }
    }

