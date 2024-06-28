using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
using CSDLPT;

namespace TNCSDLPT
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        public static SqlConnection conn = new SqlConnection();
        public static String connstr;
        public static String connstr_publisher = @"Data Source=LUONGDATTHIEN\SQLSEVER;Initial Catalog=TRACNGHIEM1;Integrated Security=True;Encrypt=False";

        public static SqlDataReader myReader;
        public static String serverName = "";
        public static String userName = "";
        public static String loginName = "";
        public static String loginPassword = "";

        public static String database = "TRACNGHIEM1";
        public static String remoteLogin = "HTKN";
        public static String remotePassword = "123456";

        public static String currentLogin = "";//mloginDN
        public static String currentPassword = "";//passwordDN

        public static String role = "";// mGroup
        public static String staff = "";//mHoten
        public static int brand = 0;//mCoso
        public static String brand2 = ""; //cosoString

        public static String MaSV = ""; //luu ma sinh vien dang nhap

        public static int mChinhanh = 0;

        public static BindingSource bds_dspm = new BindingSource();  // giữ bdsPM khi đăng nhập
        
        public static frmMain frmChinh;

        public static bool bienDangNhap = false ;
        public static String passWordSV = "";
        public static int lan;
        public static int tgianLam;
        public static String monThi;
        public static String maMonThi;// thong tin de lam bai thi
        public static String trinhDo;
        public static int soCauHoi;
        public static int maDe;
        public static String maLop;


        public static float diemSo = 0;
        public static String min;
        public static String sec;

        public static int khoiPhuc = 0;

        public static int KetNoi()
        {
            if (Program.conn != null && Program.conn.State == ConnectionState.Open)
                Program.conn.Close();
            try
            {
                Program.connstr = "Data Source=" + Program.serverName +";Initial Catalog=" +
                      Program.database + ";User ID=" +
                      Program.loginName + ";password=" + Program.loginPassword;
                Program.conn.ConnectionString = Program.connstr;
                Program.conn.Open();
                return 1;
            }

            catch (Exception e)
            {
                MessageBox.Show("Lỗi kết nối cơ sở dữ liệu.\nBạn xem lại user name và password.\n " + e.Message, "", MessageBoxButtons.OK);
                return 0;
            }
        }
        public static SqlDataReader ExecSqlDataReader(String strLenh)
        {
            SqlDataReader myreader;
            SqlCommand sqlcmd = new SqlCommand(strLenh, Program.conn);
            sqlcmd.CommandType = CommandType.Text;
            if (Program.conn.State == ConnectionState.Closed) Program.conn.Open();
            try
            {
                myreader = sqlcmd.ExecuteReader(); return myreader;

            }
            catch (SqlException ex)
            {
                Program.conn.Close();
                MessageBox.Show(ex.Message);
                return null;
            }
        }
   
        public static DataTable ExecSqlDataTable(String cmd)
        {
            DataTable dt = new DataTable();
            if (Program.conn.State == ConnectionState.Closed) Program.conn.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd, conn);
            try
            {
                da.Fill(dt); conn.Close();
                return dt;
            }
            catch (SqlException ex)
            {
                Program.conn.Close();
                MessageBox.Show(ex.Message);
                return null;
            }

        }

        public static int ExecSqlNonQuery(String strlenh)
        {
            SqlCommand Sqlcmd = new SqlCommand(strlenh, conn);
            Sqlcmd.CommandType = CommandType.Text;
            Sqlcmd.CommandTimeout = 600;// 10 phut 
            if (conn.State == ConnectionState.Closed) conn.Open();
            try
            {
                Sqlcmd.ExecuteNonQuery(); conn.Close();
                return 0;
            }
            catch (SqlException ex)
            {
                if (ex.Message.Contains("Error converting data type varchar to int"))
                    MessageBox.Show("Bạn format Cell lại cột \"Ngày Thi\" qua kiểu Number hoặc mở File Excel.");
                else MessageBox.Show(ex.Message);
                conn.Close();
                return ex.State; // trang thai lỗi gởi từ RAISERROR trong SQL Server qua
            }
        }
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            frmChinh = new frmMain();
            Application.Run(frmChinh);
        }
    }
}