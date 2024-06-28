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
    public partial class frmDangNhap : Form
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
       

        public frmDangNhap()
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
            picNotSee.Visible = false;
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
                Close();
           
            frmMain f = new frmMain();
            f.ShowDialog();
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
            // check truong hop de trong ten dang nhap
            errorProvider1.Clear();
            if (radGiangVien.Checked == false && radSinhVien.Checked == false)
                errorProvider1.SetError(grbDoiTuong, "Vui lòng chọn đối tượng đăng nhập!");
            if (txtTAIKHOAN.Text.Trim() == "")
            {
                errorProvider1.SetError(txtTAIKHOAN, "Không để tên đăng nhập trống!");
       
            }
            if (txtMATKHAU.Text.Trim() == "")
            {
                errorProvider1.SetError(txtMATKHAU, "Không để mật khẩu trống!");
               
            }
            if (txtMATKHAU.Text.Trim() == "" || txtTAIKHOAN.Text.Trim() == "" ||(radGiangVien.Checked == false && radSinhVien.Checked == false))
                return;

            //dang nhap vao csdl

            
            
            if (radGiangVien.Checked == true)
            {
                Program.loginPassword = txtMATKHAU.Text.Trim();
                Program.loginName = txtTAIKHOAN.Text.Trim();
                if (Program.KetNoi() == 0)
                    return;
                try {
                    
                    Program.brand = cmbCoso.SelectedIndex;
                    Program.brand2 = cmbCoso.Text;
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
                    // Program.conn.Close();
                    MessageBox.Show("Đăng nhập thành công!");
                    Program.bienDangNhap = true;
                    frmMain f = new frmMain();
                    this.Hide();
                    f.ShowDialog();
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Sai thông tin đăng nhập!!!"+ex.Message, "Thông báo", MessageBoxButtons.OK);
                    return;
                }
                
            }
            
            if(radSinhVien.Checked == true)
            {
                //tai khoan dang nhap duy nhat cua sinh vien vao co so du lieu
                Program.loginName = "SVPTIT";
                Program.loginPassword = "123456";
                Program.role = "Sinh viên";
                Program.MaSV = txtTAIKHOAN.Text.Trim();
                if (Program.KetNoi() == 0)
                    return;

                
                Program.brand = cmbCoso.SelectedIndex;
                Program.currentLogin = Program.loginName;
                Program.currentPassword = Program.loginPassword;
                
                // check mat khau va username
                if (conn_publisher.State == ConnectionState.Closed) conn_publisher.Open();
                int val;
                String com = "sp_CheckTaiKhoanSV";
                using (SqlCommand command = new SqlCommand(com, conn_publisher))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@masv", txtTAIKHOAN.Text);
                    command.Parameters.AddWithValue("@pass", txtMATKHAU.Text);
                   
                    // Tạo parameter để nhận giá trị trả về từ stored procedure
                    SqlParameter returnParameter = command.Parameters.Add("@ReturnVal", SqlDbType.Int);
                    returnParameter.Direction = ParameterDirection.ReturnValue;

                    // Thực thi stored procedure
                    command.ExecuteNonQuery();

                    // Lấy giá trị trả về từ parameter
                    val = (int)returnParameter.Value;


                }
                // xuat thong tin sv
                String statement = "EXEC sp_LayThongTinSinhVien '" + Program.MaSV + "'";
                Program.myReader = Program.ExecSqlDataReader(statement);
                if (Program.myReader == null)
                    return;
                Program.myReader.Read();
                try
                {
                    Program.userName = Program.myReader.GetString(0);
                    Program.staff = Program.myReader.GetString(1);// lấy userName
                }
                catch(Exception ee)
                {
                    MessageBox.Show(ee.Message);
                }
                //kiem tra dang nhap
                if (val == 1)
                {
                    MessageBox.Show("Đăng nhập thành công!");
                    Program.passWordSV = txtMATKHAU.Text;
                }
                else
                {
                    MessageBox.Show("Sai thông tin đăng nhập!", "Thông báo", MessageBoxButtons.OK);
                    return;
                }
                // kiem tra quyen han truy cap    
                if (Convert.IsDBNull(Program.userName))
                {
                    MessageBox.Show("Tài khoản này không có quyền truy cập \n Hãy thử tài khoản khác", "Thông Báo", MessageBoxButtons.OK);
                }



                
                
                Program.myReader.Close();
                //Program.conn.Close();
                conn_publisher.Close();
                Program.bienDangNhap = true;
                frmMain f = new frmMain();
                this.Hide();
                f.ShowDialog();
            }

        }

        private void radSinhVien_CheckedChanged(object sender, EventArgs e)
        {
            radSinhVien.ForeColor = Color.Red;
            radGiangVien.ForeColor = Color.Black;

        }

        private void radGiangVien_CheckedChanged(object sender, EventArgs e)
        {
            radGiangVien.ForeColor = Color.Red;
            radSinhVien.ForeColor = Color.Black;
        }

        private void picSee_Click(object sender, EventArgs e)
        {
            picSee.Visible = false;
            picNotSee.Visible = true;
            txtMATKHAU.UseSystemPasswordChar = false;
        }

        private void picNotSee_Click(object sender, EventArgs e)
        {
            picNotSee.Visible = false;
            picSee.Visible = true;
            txtMATKHAU.UseSystemPasswordChar = true;
        }

      
    }
    }

