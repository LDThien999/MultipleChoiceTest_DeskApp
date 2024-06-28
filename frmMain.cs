using CSDLPT.feature;
using CSDLPT.Login_Signup;
using CSDLPT.PerFeat;
using CSDLPT.thi_dangkythi;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Runtime.Remoting.Messaging;
using System.Windows.Forms;
using TNCSDLPT;
using Microsoft.Reporting.WinForms;
using Microsoft.ReportingServices.Diagnostics.Internal;
using CSDLPT.ReportTN;
using CSDLPT.addQuestion;

namespace CSDLPT
{
    public partial class frmMain : Form
    {
        private String userName, password;
        
        public frmMain()
        {
            InitializeComponent();

        }

        private void trangChủToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        /*private void tínhNăngToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }*/
        private void thayĐổiThôngTinToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FeatureForm featureForm = new FeatureForm();
            this.Hide();
            featureForm.ShowDialog();
        }


        private void btnDangKyMain_Click(object sender, EventArgs e)
        {

        }

        private void grbThongTin_Enter(object sender, EventArgs e)
        {

        }


        private void lblThongTinMa_Click(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }


        private void tabPage2_Click(object sender, EventArgs e)
        {

        }



        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn đăng xuất?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result == DialogResult.OK)
            {
                Program.bienDangNhap = false;
                //frmMain f = new frmMain();
                //this.Hide();
                //f.ShowDialog();
                Program.conn.Close();
                toolStripContainer1_ContentPanel_Load(sender, e);

            }
        }


        private void toolStripContainer1_ContentPanel_Load(object sender, EventArgs e)
        {
            panThongTinTaiKhoan.Visible = false;
            if (Program.bienDangNhap == true)
            {
                mnuCVGV.Enabled = true;
                tsmnuThongTinCaNhan.Enabled = true;
                picOutSide.Visible = false;
                btnDangKyMain.Visible = false;
                btnDangNhapMain.Visible = false;
                mnuTrangChuDangXuat.Enabled = true;
                lblMa.Text = Program.userName;
                lblTen.Text = Program.staff;
                lblNhom.Text = Program.role;
                grbThongTin.Visible = true;
                if (Program.role == "Sinh viên")
                {
                    tslblThi.Enabled = true;
                    btnLamBaiThi.Enabled = true;
                    picStudent.Visible = true;
                    picTeacher.Visible = false;
                    grbThongTin.BackColor = Color.LemonChiffon;
                }
                else
                {
                    picStudent.Visible = false;
                    picTeacher.Visible = true;
                    tslblThi.Enabled = false;
                    btnLamBaiThi.Enabled = false;
                    grbThongTin.BackColor = Color.PeachPuff;
                }
                 
                if (Program.role == "GIANGVIEN")
                {
                    tsmnuDangKyThi.Enabled = true;
                }
                else
                    tsmnuDangKyThi.Enabled = false;
                if(Program.role == "COSO")
                {
                    tsmnuCongViecGiaoVu.Enabled = true;
                    tsmnuQuanLySinhVien.Enabled = true;
                    tsmnuQuanLyGiaoVien.Enabled = true;
                    btnDangKyMain.Visible = true;
                }
                else
                {
                    tsmnuQuanLySinhVien.Enabled = false;
                    tsmnuCongViecGiaoVu.Enabled = false;
                    tsmnuQuanLyGiaoVien.Enabled = false;
                }
                if (Program.role == "TRUONG")
                {
                    btnDangKyMain.Visible = true;
                    mnuXemRP.Enabled = true;
                }
                else
                {
                    mnuXemRP.Enabled = false;
                }
                lblTraCuuDiemthi.Visible = true;
                btnTraCuuDiem.Visible = true;
                tslblThi.Visible = true;
                btnLamBaiThi.Visible = true;
                //if (program.role.trim() == "sinh viên")
                //    tsbtnthi.enabled = true;
                //else
                //    tsbtnthi.enabled = false;
                //tsbtnthithu.enabled = true;
                //tsbtnthongtinct.enabled = true;
                //tssbtntracuu.enabled = true;


            }
            else
            {
                tsmnuThongTinCaNhan.Enabled = false;
                picOutSide.Visible = true;
                tsmnuCongViecGiaoVu.Enabled = false;
                mnuTrangChuDangXuat.Enabled = false;
                btnDangKyMain.Enabled = false;
                btnDangNhapMain.Visible = true;
                grbThongTin.Visible = false;
                tsmnuDangKyThi.Enabled = false;
                lblTraCuuDiemthi.Visible = false;
                btnTraCuuDiem.Visible = false;
                tslblThi.Visible = false;
                btnLamBaiThi.Visible=false;
                //tsbtnThi.Enabled = false;
                //tsbtnThiThu.Enabled = false;
                //tsbtnThongTinCT.Enabled = false;
                //tssbtnTraCuu.Enabled = false;
                mnuXemRP.Enabled = false;
                mnuCVGV.Enabled = false;
            }
        }

        private void btnDangNhapMain_Click(object sender, EventArgs e)
        {
            frmDangNhap formDangNhap = new frmDangNhap();
            this.Hide();
            formDangNhap.ShowDialog();
        }

        /*private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }*/

        private void frmMain_Load(object sender, EventArgs e)
        {

        }

        private void đăngToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void đăngKýMônThiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmChuanBiThi formCBT = new frmChuanBiThi();
            formCBT.ShowDialog();   


        }

        private void grbThongTin_Enter_1(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void toolStripContainer1_TopToolStripPanel_Click(object sender, EventArgs e)
        {

        }

        private void btnLamBaiThi_Click(object sender, EventArgs e)
        {
            frmThi formThi = new frmThi();
            formThi.ShowDialog();
        }

        private void tsmnuQuanLySinhVien_Click(object sender, EventArgs e)
        {
            frmSV f = new frmSV();
            f.ShowDialog();
        }

        private void tsmnuQuanLyGiaoVien_Click(object sender, EventArgs e)
        {
            frmGV f = new frmGV();
            f.ShowDialog();
        }

        private void btnDangKyMain_Click_1(object sender, EventArgs e)
        {
            frmDangKy f = new frmDangKy();
            f.ShowDialog();

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            panThayDoiMatKhau.Visible = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panThayDoiMatKhau.Visible = false;
        }
        SqlDataAdapter adapter = new SqlDataAdapter();
        DataTable table = new DataTable();
        SqlCommand command1;
        void loadData()
        {
            try
            {
                command1 = Program.conn.CreateCommand();
                command1.CommandText = "select mabd, bangdiem.mamh, mh.TENMH,MALOP, lan, ngaythi, THOIGIANCONLAI from bangdiem inner join (select mamh, tenmh from monhoc) as mh" +
                    " on bangdiem.mamh = mh.MAMH inner join (select masv,malop from sinhvien) as sv on sv.masv = bangdiem.masv" +
                    " where bangdiem.masv = '" + Program.userName+"' and diem is null";
                adapter.SelectCommand = command1;
                table.Clear();
                adapter.Fill(table);
                dtgvBaiThiCoSuCo.DataSource = table;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
        private void tsmnuThongTinCaNhan_Click(object sender, EventArgs e)
        {
            picMain.Visible = false;
            panThongTinTaiKhoan.Visible = true;
            panThayDoiMatKhau.Visible = false;
            if (Program.role == "Sinh viên")
            {
                panSuCoBaiThi.Visible = true;
                btnPhucHoiBaiThi.Enabled = false;
                txtUserName.Text = Program.MaSV;
                txtLoginName.Text = Program.MaSV;
                loadData();

            }
            else
            {
                panSuCoBaiThi.Visible = false;
                txtUserName.Text = Program.userName;
                txtLoginName.Text = Program.loginName;
            }
               

        }

        private void btnX_ThoatAccount_Click(object sender, EventArgs e)
        {
            panThongTinTaiKhoan.Visible=false;
            picMain.Visible = true;
            txtLoginName.Text = "";
            txtUserName.Text = "";
            txtPassCu.Text = "";
            txtPassMoi.Text = "";
            txtXacNhanPassMoi.Text = "";
        }

        private void txtPassCu_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnXacNhanDoiMK_Click(object sender, EventArgs e)
        {
            if(txtPassCu.Text =="" || txtPassMoi.Text =="" || txtXacNhanPassMoi.Text == "")
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if((txtPassCu.Text.Trim() != Program.loginPassword) && Program.role != "Sinh viên")
            {
                MessageBox.Show("mật khẩu cũ không chính xác!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if ((txtPassCu.Text.Trim() != Program.passWordSV) && Program.role == "Sinh viên")
            {
                MessageBox.Show("mật khẩu cũ không chính xác!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if(txtPassMoi.Text.Trim() == txtPassCu.Text.Trim())
            {
                MessageBox.Show("mật khẩu mới trùng với mật khẩu cũ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if(txtPassMoi.Text.Trim() != txtXacNhanPassMoi.Text.Trim())
            {
                MessageBox.Show("mật khẩu xác nhận không trùng khớp!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if(Program.role == "Sinh viên")
            {
                SqlCommand command = new SqlCommand();
                try
                {
                    command = Program.conn.CreateCommand();
                    command.CommandText = "update SINHVIEN set PASSWORD='" + txtPassMoi.Text + "' " +
                        "where masv = '" + txtUserName.Text + "'";
                    command.ExecuteNonQuery();
                    MessageBox.Show("Thay đổi thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else if(Program.role != "Sinh viên")
            {
                SqlConnection connMainCS = new SqlConnection();
                if (Program.brand2 == "CƠ SỞ 1")
                    connMainCS.ConnectionString = @"Data Source=DUONG\MSSQLSERVER01;Initial Catalog=TRACNGHIEM;Integrated Security=True;Encrypt=False";
                else if(Program.brand2 == "CƠ SỞ 2")
                    connMainCS.ConnectionString = @"Data Source=DUONG\MSSQLSERVER02;Initial Catalog=TRACNGHIEM;Integrated Security=True;Encrypt=False";

                SqlCommand command = new SqlCommand();
                try
                {
                    if (connMainCS.State == ConnectionState.Closed) connMainCS.Open();
                    command = connMainCS.CreateCommand();
                    command.CommandText = "ALTER LOGIN " + Program.loginName + " WITH PASSWORD = '"+ txtPassMoi.Text + "'";
                    command.ExecuteNonQuery();
                    MessageBox.Show("Thay đổi thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    connMainCS.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

        }

        private void dtgvBaiThiCoSuCo_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int vitri;
            vitri = dtgvBaiThiCoSuCo.CurrentRow.Index;
            Program.khoiPhuc = int.Parse(dtgvBaiThiCoSuCo.Rows[vitri].Cells[0].Value.ToString());
            Program.monThi = dtgvBaiThiCoSuCo.Rows[vitri].Cells[2].Value.ToString();
            Program.maMonThi = dtgvBaiThiCoSuCo.Rows[vitri].Cells[1].Value.ToString();
            Program.lan = int.Parse(dtgvBaiThiCoSuCo.Rows[vitri].Cells[4].Value.ToString());
            Program.maLop = dtgvBaiThiCoSuCo.Rows[vitri].Cells[3].Value.ToString();
            Program.tgianLam = int.Parse(dtgvBaiThiCoSuCo.Rows[vitri].Cells[6].Value.ToString());


            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                dtgvBaiThiCoSuCo.Rows[e.RowIndex].Selected = true;
                btnPhucHoiBaiThi.Enabled = true;
            }
        }
        bool checkMaKhoiPhuc(String a)
        {
          
            Program.myReader = Program.ExecSqlDataReader("select MAKHOIPHUC FROM GIAOVIEN_DANGKY WHERE MAMH = '"+Program.maMonThi+"' AND MALOP ='"
                +Program.maLop+"' and LAN ="+Program.lan);
            Program.myReader.Read();
            if(a == Program.myReader.GetString(0))
            {
                Program.myReader.Close();
                return true;
            }
            else
            {
                Program.myReader.Close();
                return false;
            }
             
        }
        private void btnPhucHoiBaiThi_Click(object sender, EventArgs e)
        {
            if(txtKhoiPhuc.Text != "" && checkMaKhoiPhuc(txtKhoiPhuc.Text)==true )
            {
                String statementCmbMonhoc = "select count(mabd) from CT_BAITHI" +
                " where mabd = " + Program.khoiPhuc;
                Program.myReader = Program.ExecSqlDataReader(statementCmbMonhoc);
                if (Program.myReader == null)
                    return;
                Program.myReader.Read();
                Program.soCauHoi = Program.myReader.GetInt32(0);
                Program.myReader.Close();
                frmLamBaiThi f = new frmLamBaiThi();
                f.ShowDialog();
            }
            else
            {
                MessageBox.Show("Mã khôi phục không trùng khớp!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void panSuCoBaiThi_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panThongTinTaiKhoan_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
    
        }

        private void viewrpBD_Click(object sender, EventArgs e)
        {
            frmRpKQ f = new frmRpKQ();
            f.ShowDialog();
        }

        private void viewrpKQ_Click(object sender, EventArgs e)
        {
            frmRpCau9 f = new frmRpCau9();
            f.ShowDialog();
        }

        private void viewrpDSDK_Click(object sender, EventArgs e)
        {
            frmCau11 f = new frmCau11();
            f.ShowDialog();
        }

        private void dtgvBaiThiCoSuCo_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void tsmnuThuVienCH_Click(object sender, EventArgs e)
        {
            frmAddQues f = new frmAddQues();
            f.ShowDialog();
        }

        private void mnuTrangChuThoat_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn chắc chắn muốn thoát?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result == DialogResult.OK)
            {
                Application.Exit();
            }
        }

    }

}
