using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TNCSDLPT;

namespace CSDLPT.Login_Signup
{
    public partial class frmDangKy : Form
    {
        public frmDangKy()
        {
            InitializeComponent();
        }
        SqlDataAdapter adapter = new SqlDataAdapter();
        DataTable table1 = new DataTable();
        DataTable table2 = new DataTable();
        SqlCommand command;
        String roleDk;
        String ten;
        void loadData()
        {
            if(radGiangVien.Checked == true)
            {
                try
                {
                    command = Program.conn.CreateCommand();
                    command.CommandText =
                    "select MAGV, HOTEN = HO+' '+TEN, MAKH from GIAOVIEN where MAGV not in (select MAGV from GIAOVIEN where CONVERT(nvarchar,MAGV)" +
                        " in (select name from sys.sysusers))";
                    adapter.SelectCommand = command;
                    table1.Clear();
                    adapter.Fill(table1);
                    dtgvGiaoVienChuaCoTK.DataSource = table1;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else if(radSinhVien.Checked == true)
            {
                command = Program.conn.CreateCommand();
                command.CommandText = "select MASV,HOTEN = HO+' '+TEN, MALOP from SINHVIEN where SINHVIEN.PASSWORD is null";
                adapter.SelectCommand = command;
                table2.Clear();
                adapter.Fill(table2);
                dtgvGiaoVienChuaCoTK.DataSource = table2;
            }
            
        }
            

            private void button1_Click(object sender, EventArgs e)
            {
                
            }

            private void frmDangKy_Load(object sender, EventArgs e)
            {
                if(radGiangVien.Checked == false && radSinhVien .Checked == false)
            {
                btnKiemTra.Visible = false;
                btnGoiY.Visible = false;
            }
            
                panSearch.Visible = false;
                picNotSee1.Visible = false;
                picNotSee2.Visible = false;
                if(Program.role == "COSO")
            {
                radSinhVien.Enabled = true;
                radNhomGiangVien.Enabled = true;
                radTruong.Enabled = false;
            }
                else if(Program.role == "TRUONG")
            {
                radSinhVien.Enabled = false;
                radNhomGiangVien.Enabled = false;
                radTruong.Enabled = true;
            }
            }

            private void btnX_thoatSearch_Click(object sender, EventArgs e)
            {
                panSearch.Visible = false;
                this.Size = new Size(584, 611);
                this.Location = new Point((Screen.PrimaryScreen.WorkingArea.Width - this.Width) / 2,
                       (Screen.PrimaryScreen.WorkingArea.Height - this.Height) / 2);

        }

        private void btnKiemTra_Click(object sender, EventArgs e)
        {
            panSearch.Visible = true;
            this.Size = new Size(1063, 660);
            this.Location = new Point((Screen.PrimaryScreen.WorkingArea.Width - this.Width) / 2,
                           (Screen.PrimaryScreen.WorkingArea.Height - this.Height) / 2);
            if (radGiangVien.Checked == true)
            {
                lblLine.BackColor = Color.LightSteelBlue;
                lblSearchSV.Visible = false;
                lblSearchGV.Visible = true;
                loadData();
            }
            else if(radSinhVien.Checked == true)
            {
                lblLine.BackColor = Color.Coral;
                lblSearchSV.Visible = true;
                lblSearchGV.Visible = false;
                loadData(); 
            }
            
        }

        private void dtgvGiaoVienChuaCoTK_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int vitri;
            vitri = dtgvGiaoVienChuaCoTK.CurrentRow.Index;
            txtUserName_MaGV.Text = dtgvGiaoVienChuaCoTK.Rows[vitri].Cells[0].Value.ToString();
            ten = dtgvGiaoVienChuaCoTK.Rows[vitri].Cells[1].Value.ToString();
            if (radSinhVien.Checked == true)
            {
                txtLoginnam.Text = txtUserName_MaGV.Text;
            }
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                dtgvGiaoVienChuaCoTK.Rows[e.RowIndex].Selected = true;
            }
        }

        private void picSee1_Click(object sender, EventArgs e)
        {
            picNotSee1.Visible = true;
            picSee1.Visible = false;
            txtMATKHAU.UseSystemPasswordChar = false;
        }

        private void picSee2_Click(object sender, EventArgs e)
        {
            picNotSee2.Visible = true;
            picSee2.Visible = false;
            txtXacNhanMK.UseSystemPasswordChar=false;
        }

        private void picNotSee1_Click(object sender, EventArgs e)
        {
            picNotSee1.Visible = false;
            picSee1.Visible = true;
            txtMATKHAU.UseSystemPasswordChar = true;
        }

        private void picNotSee2_Click(object sender, EventArgs e)
        {
            picNotSee2.Visible = false;
            picSee2.Visible = true;
            txtXacNhanMK.UseSystemPasswordChar = true;
        }

        private void radGiangVien_CheckedChanged(object sender, EventArgs e)
        {
            btnKiemTra.Visible = true;
            btnGoiY.Visible = true;
            radGiangVien.ForeColor = Color.Red;
            radSinhVien.ForeColor = Color.Black;
            txtUserName_MaGV.Enabled = true;
            txtLoginnam.Enabled = true;
            grbNhom.Enabled = true;
            btnGoiY.Visible = true;
            txtUserName_MaGV.Text = "";
            txtLoginnam.Text = "";
        }

        private void radTruong_CheckedChanged(object sender, EventArgs e)
        {
            radTruong.ForeColor = Color.Red;
            radCoso.ForeColor = Color.Black;
            radNhomGiangVien.ForeColor= Color.Black;
            roleDk = radTruong.Text;
        }

        private void radSinhVien_CheckedChanged(object sender, EventArgs e)
        {
            btnKiemTra.Visible = true;
            btnGoiY.Visible = true;
            radGiangVien.ForeColor = Color.Black;
            radSinhVien.ForeColor = Color.Red;
            txtUserName_MaGV.Enabled = false;
            txtLoginnam.Enabled = false;
            grbNhom.Enabled = false;
            btnGoiY.Visible = false;
            txtUserName_MaGV.Text = "";
            txtLoginnam.Text = "";
        }

        private void radCoso_CheckedChanged(object sender, EventArgs e)
        {
            radTruong.ForeColor = Color.Black;
            radCoso.ForeColor = Color.Red;
            radNhomGiangVien.ForeColor = Color.Black;
            roleDk = radCoso.Text;
        }

        private void radNhomGiangVien_CheckedChanged(object sender, EventArgs e)
        {
            radTruong.ForeColor = Color.Black;
            radCoso.ForeColor = Color.Black;
            radNhomGiangVien.ForeColor = Color.Red;
            roleDk = radNhomGiangVien.Text;
        }

        private void btn_exit_Click(object sender, EventArgs e)
        {
            DialogResult r = MessageBox.Show("Bạn có thất sự muốn thoát?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (r == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void btn_DangkY_Click(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            if (radGiangVien.Checked  == false && radSinhVien.Checked == false)
            {
                errorProvider1.SetError(grbDoiTuong, "Vui lòng chọn đối tượng!");
            }
            if (txtLoginnam.Text == "")
                errorProvider1.SetError(btnGoiY, "không để login name trống!");
            if (txtUserName_MaGV.Text == "")
                errorProvider1.SetError(btnKiemTra, "Không để username trống!");
            if (txtMATKHAU.Text == "")
                errorProvider1.SetError(txtMATKHAU, "Không để mật khẩu trống!");
            if (txtXacNhanMK.Text == "")
                errorProvider1.SetError(txtXacNhanMK, "Hãy xác nhận lại mật khẩu!");
            if (radCoso.Checked == false && radTruong.Checked == false && radNhomGiangVien.Checked == false && radSinhVien.Checked == false)
                errorProvider1.SetError(grbNhom, "Vui lòng chọn nhóm quyền !");
            if ((radGiangVien.Checked == false && radSinhVien.Checked == false) || txtLoginnam.Text == "" || txtUserName_MaGV.Text == "" || txtMATKHAU.Text == "" || txtXacNhanMK.Text == "" || ((radCoso.Checked == false && radTruong.Checked == false && radNhomGiangVien.Checked == false) && radSinhVien.Checked == false))
                return; 
            else if ((txtMATKHAU.Text == txtXacNhanMK.Text) && radGiangVien.Checked == true )
            {
                int val;
                String com = "sp_TaoTaiKhoan";
                using (SqlCommand command = new SqlCommand(com, Program.conn))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@LGNAME", txtLoginnam.Text);
                    command.Parameters.AddWithValue("@PASS", txtMATKHAU.Text);
                    command.Parameters.AddWithValue("@USERNAME", txtUserName_MaGV.Text);
                    command.Parameters.AddWithValue("@ROLE", roleDk);
                    // Tạo parameter để nhận giá trị trả về từ stored procedure
                    SqlParameter returnParameter = command.Parameters.Add("@ReturnVal", SqlDbType.Int);
                    returnParameter.Direction = ParameterDirection.ReturnValue;

                    // Thực thi stored procedure
                    command.ExecuteNonQuery();

                    // Lấy giá trị trả về từ parameter
                    val = (int)returnParameter.Value;


                }
                if (val == 0)
                    MessageBox.Show("Đăng ký thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else if (val == 1)
                    MessageBox.Show("LoginName bị trùng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else if (val == 2)
                    MessageBox.Show("UserName bị trùng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if ((txtMATKHAU.Text == txtXacNhanMK.Text) && radTruong.Checked == true)
            {
                int val;
                String com = "sp_TaoTaiKhoan";
                using (SqlCommand command = new SqlCommand(com, Program.conn))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@LGNAME", txtLoginnam.Text);
                    command.Parameters.AddWithValue("@PASS", txtMATKHAU.Text);
                    command.Parameters.AddWithValue("@USERNAME", txtUserName_MaGV.Text);
                    command.Parameters.AddWithValue("@ROLE", roleDk);
                    // Tạo parameter để nhận giá trị trả về từ stored procedure
                    SqlParameter returnParameter = command.Parameters.Add("@ReturnVal", SqlDbType.Int);
                    returnParameter.Direction = ParameterDirection.ReturnValue;

                    // Thực thi stored procedure
                    command.ExecuteNonQuery();

                    // Lấy giá trị trả về từ parameter
                    val = (int)returnParameter.Value;


                }
                if (val == 0)
                {
                    //String com1 = "link1.TRACNGHIEM.DBO.sp_TaoTaiKhoan";
                    //using (SqlCommand command = new SqlCommand(com1, Program.conn))
                    //{
                    //    command.CommandType = CommandType.StoredProcedure;
                    //    command.Parameters.AddWithValue("@LGNAME", txtLoginnam.Text);
                    //    command.Parameters.AddWithValue("@PASS", txtMATKHAU.Text);
                    //    command.Parameters.AddWithValue("@USERNAME", txtUserName_MaGV.Text);
                    //    command.Parameters.AddWithValue("@ROLE", roleDk);
                    //    // Tạo parameter để nhận giá trị trả về từ stored procedure
                    //    SqlParameter returnParameter = command.Parameters.Add("@ReturnVal", SqlDbType.Int);
                    //    returnParameter.Direction = ParameterDirection.ReturnValue;

                    //    // Thực thi stored procedure
                    //    command.ExecuteNonQuery();




                    //}
                    SqlCommand command = new SqlCommand();
                    command = Program.conn.CreateCommand();
                    command.CommandText ="exec LINK1.TRACNGHIEM.DBO.sp_TaoTaiKhoan '"+ txtLoginnam.Text + "', '"+ txtMATKHAU.Text+ "', '" + txtUserName_MaGV.Text
                        + "', '" + roleDk + "'";
                    
                    command.ExecuteNonQuery();
                    MessageBox.Show("Đăng ký thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                   
                else if (val == 1)
                    MessageBox.Show("LoginName bị trùng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else if (val == 2)
                    MessageBox.Show("UserName bị trùng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if ((txtMATKHAU.Text == txtXacNhanMK.Text) && radSinhVien.Checked == true)
            {
                try
                {
                    command = Program.conn.CreateCommand();
                    command.CommandText = "update SINHVIEN set PASSWORD='" + txtMATKHAU.Text + "' " +
                        "where masv = '" + txtUserName_MaGV.Text + "'";
                    command.ExecuteNonQuery();
                    MessageBox.Show("Đăng ký thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }


            }
            else
            {
                MessageBox.Show("Mật khẩu xác nhận không trùng khớp!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void lblLine_Click(object sender, EventArgs e)
        {

        }
        static string layTenGoiNho(string fullName)
        {
            string[] words = fullName.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            string initials = string.Join("", words.Select(word => word[0]));
            return initials.ToUpper();
        }
        private void btnGoiY_Click(object sender, EventArgs e)
        {
            txtLoginnam.Text = layTenGoiNho(ten);
        }
    }
    }    
