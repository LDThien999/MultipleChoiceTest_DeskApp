using CSDLPT.thi_dangkythi;
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

using TNCSDLPT;

namespace CSDLPT
{
    public partial class frmChuanBiThi : Form
    {
        String  createMaKhoiPhuc()
        {
            Random random = new Random();
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            string randomCode = new string(Enumerable.Repeat(chars, 10)
              .Select(s => s[random.Next(s.Length)]).ToArray());
            return randomCode;
   
        }
 
        class ThongTinDangKy
        {
            public String maMh;
            public String maLop;
            public String trinhDo;
            public String ngayThi;
            public String lan;
            public String soCauThi;
            public String thoiGian;
            public String trangthai;
            public String maKhoiPhuc;

            public ThongTinDangKy(string maMh, string maLop, string trinhDo, string ngayThi, string lan, string soCauThi, string thoiGian, string maKhoiPhuc)
            {
                this.maMh = maMh;
                this.maLop = maLop;
                this.trinhDo = trinhDo;
                this.ngayThi = ngayThi;
                this.lan = lan;
                this.soCauThi = soCauThi;
                this.thoiGian = thoiGian;
                this.maKhoiPhuc = maKhoiPhuc;
            }

        }
        //tao hai stack undo va redo
        Stack<ThongTinDangKy> undoStack = new Stack<ThongTinDangKy>();
        Stack<ThongTinDangKy> redoStack = new Stack<ThongTinDangKy>();

        void checkUnRedo()
        {
            if (undoStack.Count > 0)
                btnKhoiPhuc.Enabled = true;
            else
                btnKhoiPhuc.Enabled = false;

            if (redoStack.Count > 0)
                btnRedo.Enabled = true;
            else
                btnRedo.Enabled = false;
        }

        void luuThongTin(String a)
        {
            String maKhoiPhuc = createMaKhoiPhuc();
            ThongTinDangKy info = new ThongTinDangKy(cmbMonThi.Text, cmbLopThi.Text, cmbTrinhDo.Text, dateNgayThi.Text, cmbLanThi.Text, txtSoCH.Text, txtThoiGian.Text,maKhoiPhuc);
            info.trangthai = a;
            undoStack.Push(info);
        }
        ThongTinDangKy newInfo;
        ThongTinDangKy luuThongTinNew(String a)
        {
            String maKhoiPhuc = createMaKhoiPhuc();
            ThongTinDangKy info = new ThongTinDangKy(cmbMonThi.Text, cmbLopThi.Text, cmbTrinhDo.Text, dateNgayThi.Text, cmbLanThi.Text, txtSoCH.Text, txtThoiGian.Text,maKhoiPhuc);
            info.trangthai = a; 
            return info;
        }





        SqlDataAdapter adapter = new SqlDataAdapter();
        DataTable table = new DataTable();
        SqlCommand command;
        void loadData()
        {
            try
            {
                command = Program.conn.CreateCommand();
                command.CommandText = "SELECT MAGV,MAMH,MALOP,TRINHDO,NGAYTHI,LAN,SOCAUTHI,THOIGIAN,MAKHOIPHUC FROM GIAOVIEN_DANGKY" +
                    " where MAGV = '" + Program.userName + "'";
                adapter.SelectCommand = command;
                table.Clear();
                adapter.Fill(table);
                dtgvShowDangKy.DataSource = table;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
        }
        public frmChuanBiThi()
        {
            InitializeComponent();
        }

        private void lblChuanBiThi_Click(object sender, EventArgs e)
        {

        }

        private void grbThongTinCBTHI_Enter(object sender, EventArgs e)
        {

        }
        void xuLyComboBoxLanThi()
        {
            
            if (cmbMonThi.SelectedItem == null || cmbLopThi.SelectedItem == null)
            {
                cmbLanThi.Items.Clear();
                cmbLanThi.Items.Add("1");
                cmbLanThi.Items.Add("2");
            }
            // neu ca 2 cmb tren deu co noi dung
            else if(cmbMonThi.SelectedItem != null && cmbLopThi.SelectedItem != null)
            {
                int soLanThi;
                String com = "sp_CheckLanThi" ;
                using (SqlCommand command = new SqlCommand(com, Program.conn))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@malop", cmbLopThi.Text);
                    command.Parameters.AddWithValue("@mamh", cmbMonThi.Text);
                    // Tạo parameter để nhận giá trị trả về từ stored procedure
                    SqlParameter returnParameter = command.Parameters.Add("@ReturnVal", SqlDbType.Int);
                    returnParameter.Direction = ParameterDirection.ReturnValue;

                    // Thực thi stored procedure
                    command.ExecuteNonQuery();

                    // Lấy giá trị trả về từ parameter
                    soLanThi = (int)returnParameter.Value;

                   
                }
                if(soLanThi == 0)
                {
                    cmbLanThi.Items.Clear();
                    cmbLanThi.Items.Add("1");
                }
                else if(soLanThi == 1)
                {
                    cmbLanThi.Items.Clear();
                    cmbLanThi.Items.Add("2");
                }
                else
                {
                    cmbLanThi.Items.Clear();
                }
            }
        }

        private void frmChuanBiThi_Load(object sender, EventArgs e)
        {
            // an hien cac chuc nang
            panChonLua.Enabled = false;
            checkUnRedo();
            picSave.Visible = false;
            btnChinhSua.Visible = false;
            btnOutChinhSua.Visible = false;
            pictureBox1.Visible = false;
            btnDangKyThi.Visible = false;

            picOut.Visible = false;
            btnThoatAdd.Visible = false;
            // cho thong tin giang vien vao grpbox thong tin
            String statement = "select MAKH from GIAOVIEN";
            Program.myReader = Program.ExecSqlDataReader(statement);
            if (Program.myReader == null)
                return;
            Program.myReader.Read();
            lblCoSo.Text = Program.brand2;
            lblMa.Text = Program.userName;
            lblName.Text = Program.staff;
            lblKhoa.Text = Program.myReader.GetString(0);
            Program.myReader.Close();
            // ket noi csdl voi datagridview
            loadData();
            // Do du lieu vao cmbMonthi
            String statementCmbMonhoc = "select MAMH from MONHOC";
            Program.myReader = Program.ExecSqlDataReader(statementCmbMonhoc);
            if (Program.myReader == null)
                return;
            while (Program.myReader.Read())
            {
                cmbMonThi.Items.Add(Program.myReader.GetString(0));
            }
            Program.myReader.Close();
            //do du lieu vao cmblopThi
            String statementCmbLopThi = "select MALOP from LOP";
            Program.myReader = Program.ExecSqlDataReader(statementCmbLopThi);
            if (Program.myReader == null)
                return;
            while (Program.myReader.Read())
            {
                cmbLopThi.Items.Add(Program.myReader.GetString(0));
            }
            Program.myReader.Close();
            
            


        }

        private void lblMa_Click(object sender, EventArgs e)
        {

        }

        private void dtgvShowDangKy_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnDangKyThi_Click(object sender, EventArgs e)
        {   
            if(cmbMonThi.Text == "" || cmbLopThi.Text == "" || cmbLanThi.Text =="" || cmbTrinhDo.Text == "" || dateNgayThi.Text == "")
            {
                MessageBox.Show("Không được để trống Môn thi, lớp thi, ngày thi hoặc lần thi hay trình độ thi!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            
            else if (txtThoiGian.Text == "" || txtSoCH.Text == "")
            {
                MessageBox.Show("Không được để trống thời gian hoặc số câu hỏi!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            else if (IsNumeric(txtSoCH.Text) == false || IsNumeric(txtThoiGian.Text) == false)
            {
                MessageBox.Show("Thời gian và số câu hỏi phải là số!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (int.Parse(txtThoiGian.Text) < 15 || int.Parse(txtThoiGian.Text) > 60)
            {
                MessageBox.Show("Thời gian phải nằm trong đoạn [15,60]!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (int.Parse(txtSoCH.Text) < 10 || int.Parse(txtSoCH.Text) > 100)
            {
                MessageBox.Show("Số câu hỏi đăng ký cần nằm trong đoạn [10,100]!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                String maKhoiPhuc = createMaKhoiPhuc();
                int val;
                String com = "sp_XuatCauHoi";
                using (SqlCommand command = new SqlCommand(com, Program.conn))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@SOCH", txtSoCH.Text);
                    command.Parameters.AddWithValue("@MAMH", cmbMonThi.Text);
                    command.Parameters.AddWithValue("@TRINHDO", cmbTrinhDo.Text);
                    command.Parameters.AddWithValue("@MALOP", cmbLopThi.Text);
                    command.Parameters.AddWithValue("@NGAYTHI", dateNgayThi.Text);
                    command.Parameters.AddWithValue("@LAN", cmbLanThi.Text);
                    command.Parameters.AddWithValue("@THOIGIAN", txtThoiGian.Text);
                    command.Parameters.AddWithValue("@MAKHOIPHUC", maKhoiPhuc);
                    command.Parameters.AddWithValue("@MAGV", Program.userName);

                    // Tạo parameter để nhận giá trị trả về từ stored procedure
                    SqlParameter returnParameter = command.Parameters.Add("@ReturnVal", SqlDbType.Int);
                    returnParameter.Direction = ParameterDirection.ReturnValue;

                    // Thực thi stored procedure
                    command.ExecuteNonQuery();

                    // Lấy giá trị trả về từ parameter
                    val = (int)returnParameter.Value;


                }

                try
                {
                    if (val == 0)
                    {
                        MessageBox.Show("Không đủ số câu để tạo đề!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    else
                    {
                        MessageBox.Show("Đăng ký thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        picAdd.Visible = true;
                        btnAddThi.Visible = true;
                        //an nut thoatAdd
                        picOut.Visible = false;
                        btnThoatAdd.Visible = false;
                        pictureBox1.Visible = false;
                        btnDangKyThi.Visible = false;
                        // hien het cac button khac va datagridview
                        luuThongTin("them");
                        redoStack.Clear();
                        //luuThongTinNew("them");
                        pictureBox2.Visible = true;
                        btnXoa.Visible = true;
                        btnTienHanhChinhSua.Enabled = true;
                        btnKhoiPhuc.Enabled = true;
                        btnRedo.Enabled = true;
                        dtgvShowDangKy.Enabled = true;
                        panChonLua.Enabled = false;
                        loadData();
                        checkUnRedo();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

            }
        }
            

        private void dtgvShowDangKy_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            cmbMonThi.Enabled = false;
            cmbLanThi.Enabled = false;
            cmbLopThi.Enabled = false;
            int vitri;
            vitri = dtgvShowDangKy.CurrentRow.Index;
            cmbMonThi.Text = dtgvShowDangKy.Rows[vitri].Cells[1].Value.ToString();
            cmbLopThi.Text = dtgvShowDangKy.Rows[vitri].Cells[2].Value.ToString();
            cmbTrinhDo.Text = dtgvShowDangKy.Rows[vitri].Cells[3].Value.ToString();
            dateNgayThi.Text = dtgvShowDangKy.Rows[vitri].Cells[4].Value.ToString();
            cmbLanThi.Items.Clear();
            cmbLanThi.Items.Add("2");
            cmbLanThi.Items.Add("1");
            cmbLanThi.Items.Add("2");
            cmbLanThi.Text = dtgvShowDangKy.Rows[vitri].Cells[5].Value.ToString();
            txtSoCH.Text = dtgvShowDangKy.Rows[vitri].Cells[6].Value.ToString();
            txtThoiGian.Text = dtgvShowDangKy.Rows[vitri].Cells[7].Value.ToString();
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                dtgvShowDangKy.Rows[e.RowIndex].Selected = true;
            }
            Program.soCauHoi = int.Parse(txtSoCH.Text);
            Program.tgianLam = int.Parse(txtThoiGian.Text);
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            DateTime dayNow = DateTime.Now;
            if (dayNow >= DateTime.Parse(dateNgayThi.Text))
            {
                MessageBox.Show("Đang trong thời gian thi! Không thể xóa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                DialogResult result = MessageBox.Show("Bạn thật sự muốn hủy đăng ký?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (result == DialogResult.OK)
                {
                    try
                    {
                        //command = Program.conn.CreateCommand();
                        //command.CommandText = "delete from GIAOVIEN_DANGKY where MAMH='" +
                        //cmbMonThi.Text + "' and MALOP='" + cmbLopThi.Text + "' and LAN=" + cmbLanThi.Text;
                        //luuThongTin("xoa");
                        //command.ExecuteNonQuery();
                        command = Program.conn.CreateCommand();
                        command.CommandText = "exec sp_xoaDangKyThi '" + cmbLopThi.Text + "','" + cmbMonThi.Text + "'," + cmbLanThi.Text;
                        luuThongTin("xoa");
                        command.ExecuteNonQuery();

                        MessageBox.Show("Hủy đăng ký thành công!", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                        loadData();
                        redoStack.Clear();
                        checkUnRedo();
                        panChonLua.Enabled = false;

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                        return;
                    }
                }
            }



        }
        static bool IsNumeric(string str)
        {
            foreach (char c in str)
            {
                if (!char.IsDigit(c))
                {
                    return false;
                }
            }
            return true;
        }
        private void btnChinhSua_Click(object sender, EventArgs e)
        {
            if (cmbMonThi.Text == "" || cmbLopThi.Text == "" || cmbLanThi.Text == "" || cmbTrinhDo.Text == "" || dateNgayThi.Text == "")
            {
                MessageBox.Show("Không được để trống Môn thi, lớp thi, ngày thi hoặc lần thi hay trình độ thi!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            else if (txtThoiGian.Text == "" || txtSoCH.Text == "")
            {
                MessageBox.Show("Không được để trống thời gian hoặc số câu hỏi!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            else if (IsNumeric(txtSoCH.Text) == false || IsNumeric(txtThoiGian.Text) == false)
            {
                MessageBox.Show("Thời gian và số câu hỏi phải là số!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (int.Parse(txtThoiGian.Text) < 15 || int.Parse(txtThoiGian.Text) > 60)
            {
                MessageBox.Show("Thời gian phải nằm trong đoạn [15,60]!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (int.Parse(txtSoCH.Text) < 10 || int.Parse(txtSoCH.Text) > 100)
            {
                MessageBox.Show("Số câu hỏi đăng ký cần nằm trong đoạn [10,100]!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                try
                {
                    String maKhoiPhuc = createMaKhoiPhuc();
                    int val;
                    String com = "sp_ChinhSuaDKThi";
                    using (SqlCommand command = new SqlCommand(com, Program.conn))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@SOCH", txtSoCH.Text);
                        command.Parameters.AddWithValue("@MAMH", cmbMonThi.Text);
                        command.Parameters.AddWithValue("@TRINHDO", cmbTrinhDo.Text);
                        command.Parameters.AddWithValue("@MALOP", cmbLopThi.Text);
                        command.Parameters.AddWithValue("@NGAYTHI", dateNgayThi.Text);
                        command.Parameters.AddWithValue("@LAN", cmbLanThi.Text);
                        command.Parameters.AddWithValue("@THOIGIAN", txtThoiGian.Text);
                        command.Parameters.AddWithValue("@MAKHOIPHUC", maKhoiPhuc);
                        command.Parameters.AddWithValue("@MAGV", Program.userName);

                        // Tạo parameter để nhận giá trị trả về từ stored procedure
                        SqlParameter returnParameter = command.Parameters.Add("@ReturnVal", SqlDbType.Int);
                        returnParameter.Direction = ParameterDirection.ReturnValue;

                        // Thực thi stored procedure
                        command.ExecuteNonQuery();

                        // Lấy giá trị trả về từ parameter
                        val = (int)returnParameter.Value;
                    }
                        if (val==0)
                        {
                            MessageBox.Show("Không đủ số câu tạo đề!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }

                        else
                        {
                        //command = Program.conn.CreateCommand();
                        //command.CommandText = "update GIAOVIEN_DANGKY set TRINHDO='" + cmbTrinhDo.Text
                        //    + "', NGAYTHI='" + dateNgayThi.Text + "', SOCAUTHI='"
                        //    + txtSoCH.Text + "', THOIGIAN ='" + txtThoiGian.Text
                        //    + "' where MAMH ='" + cmbMonThi.Text + "' and MALOP ='" + cmbLopThi.Text + "' and LAN ='" + cmbLanThi.Text + "'";
                        //command.ExecuteNonQuery();
                           
                             btnAddThi.Enabled = true;

                            picOut.Visible = false;
                            btnOutChinhSua.Visible = false;

                            pictureBox2.Visible = true;
                            btnXoa.Visible = true;

                            // hien het cac button khac va datagridview

                            picSave.Visible = false;
                            btnChinhSua.Visible = false;
                            picEdit.Visible = true;
                            btnTienHanhChinhSua.Visible = true;
                            btnKhoiPhuc.Enabled = true;
                            btnRedo.Enabled = true;
                            MessageBox.Show("Lưu thành công!", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                            redoStack.Clear();
                            newInfo = luuThongTinNew("chinhsua");
                            loadData();
                            checkUnRedo();
                            panChonLua.Enabled = false;
                        }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                    return;
                }
            }
        }
            

        private void btnAddThi_Click(object sender, EventArgs e)
        {
            panChonLua.Enabled = true;
            //clear het cac box
            cmbMonThi.SelectedItem = null;
            cmbLopThi.SelectedItem = null;
            cmbLanThi.SelectedItem = null;
            cmbTrinhDo.SelectedItem = null;
            txtSoCH.Text = "";
            txtThoiGian.Text = "";

            picAdd.Visible = false;
            btnAddThi.Visible = false;

            pictureBox1.Visible = true;
            btnDangKyThi.Visible = true;

            picOut.Visible = true;
            btnThoatAdd.Visible = true;

            btnTienHanhChinhSua.Enabled = false;
            pictureBox2.Visible = false;
            btnXoa.Visible = false;

            cmbMonThi.Enabled = true;
            cmbLopThi.Enabled = true;
            cmbLanThi.Enabled = true;
            // an het cac button khac va datagridview


            btnKhoiPhuc.Enabled = false;
            btnRedo.Enabled = false;
            dtgvShowDangKy.Enabled = false;



            
            
        }

        private void btnThoatAdd_Click(object sender, EventArgs e)
        {
            panChonLua.Enabled = false;
            
            picAdd.Visible = true;
            btnAddThi.Visible = true;

            pictureBox1.Visible = false;
            btnDangKyThi.Visible = false;

            picOut.Visible = false;
            btnThoatAdd.Visible = false;

            pictureBox2.Visible = true;
            btnXoa.Visible = true;

            cmbMonThi.Enabled = false;
            cmbLopThi.Enabled = false;
            cmbLanThi.Enabled = false;
            // hien het cac button khac va datagridview

            btnTienHanhChinhSua.Enabled = true;
            btnKhoiPhuc.Enabled = true;
            btnRedo.Enabled = true;
            dtgvShowDangKy.Enabled = true;

            //clear het cac box
            cmbMonThi.Text = "";
            cmbLopThi.Text = "";
            cmbLanThi.Text = "";
            cmbTrinhDo.Text = "";
            txtSoCH.Text = "";
            txtThoiGian.Text = "";
        }

        private void btnTienHanhChinhSua_Click(object sender, EventArgs e)
        {
            panChonLua.Enabled = true;
           
            picEdit.Visible = false;
            btnTienHanhChinhSua.Visible = false;

            picSave.Visible = true;
            btnChinhSua.Visible = true;

            btnAddThi.Enabled = false;
            pictureBox2.Visible = false;
            btnXoa.Visible = false;
            picOut.Visible = true;
            btnOutChinhSua.Visible = true;
            btnRedo.Enabled = false;
            btnKhoiPhuc.Enabled = false;

            luuThongTin("chinhsua");
            


        }

        private void btnOutChinhSua_Click(object sender, EventArgs e)
        {
            panChonLua.Enabled = false;
            
            btnAddThi.Enabled = true;

            picOut.Visible = false;
            btnOutChinhSua.Visible = false;

            pictureBox2.Visible = true;
            btnXoa.Visible = true;

            // hien het cac button khac va datagridview

            picSave.Visible = false;
            btnChinhSua.Visible = false;
            picEdit.Visible = true;
            btnTienHanhChinhSua.Visible = true;
            btnKhoiPhuc.Enabled = true;
            btnRedo.Enabled = true;


            //clear het cac box
            cmbMonThi.Text = "";
            cmbLopThi.Text = "";
            cmbLanThi.Text = "";
            cmbTrinhDo.Text = "";
            txtSoCH.Text = "";
            txtThoiGian.Text = "";
            undoStack.Pop();
            checkUnRedo();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có muốn thoát?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                undoStack.Clear();
                redoStack.Clear();
                this.Close();
            }
        }
        void loadThongTin(ThongTinDangKy temp)
        {
            cmbMonThi.Text = temp.maMh;
            cmbLopThi.Text = temp.maLop;
            cmbLanThi.Text = temp.lan;
            cmbTrinhDo.Text = temp.trinhDo;
            dateNgayThi.Text = temp.ngayThi;
            txtSoCH.Text = temp.soCauThi;
            txtThoiGian.Text = temp.thoiGian;
        }

        void themDangKy()
        {
            String maKhoiPhuc = createMaKhoiPhuc();
            String com = "sp_XuatCauHoi";
            using (SqlCommand command = new SqlCommand(com, Program.conn))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@SOCH", txtSoCH.Text);
                command.Parameters.AddWithValue("@MAMH", cmbMonThi.Text);
                command.Parameters.AddWithValue("@TRINHDO", cmbTrinhDo.Text);
                command.Parameters.AddWithValue("@MALOP", cmbLopThi.Text);
                command.Parameters.AddWithValue("@NGAYTHI", dateNgayThi.Text);
                command.Parameters.AddWithValue("@LAN", cmbLanThi.Text);
                command.Parameters.AddWithValue("@THOIGIAN", txtThoiGian.Text);
                command.Parameters.AddWithValue("@MAKHOIPHUC", maKhoiPhuc);
                command.Parameters.AddWithValue("@MAGV", Program.userName);

                // Tạo parameter để nhận giá trị trả về từ stored procedure
                SqlParameter returnParameter = command.Parameters.Add("@ReturnVal", SqlDbType.Int);
                returnParameter.Direction = ParameterDirection.ReturnValue;

                // Thực thi stored procedure
                command.ExecuteNonQuery();

            }
        }
        void xoaDangKy()
        {
            command = Program.conn.CreateCommand();
            //command.CommandText = "delete from GIAOVIEN_DANGKY where MAMH='" +
            //cmbMonThi.Text + "' and MALOP='" + cmbLopThi.Text + "' and LAN=" + cmbLanThi.Text;
            command.CommandText = "exec sp_xoaDangKyThi '" + cmbLopThi.Text + "','" + cmbMonThi.Text + "'," + cmbLanThi.Text;
            command.ExecuteNonQuery();
        }
        void chinhSuaDangKy()
        {
            //command = Program.conn.CreateCommand();
            //command.CommandText = "update GIAOVIEN_DANGKY set TRINHDO='" + cmbTrinhDo.Text
            //    + "', NGAYTHI='" + dateNgayThi.Text + "', SOCAUTHI='"
            //    + txtSoCH.Text + "', THOIGIAN ='" + txtThoiGian.Text
            //    + "' where MAMH ='" + cmbMonThi.Text + "' and MALOP ='" + cmbLopThi.Text + "' and LAN ='" + cmbLanThi.Text + "'";
            //command.ExecuteNonQuery();
            String maKhoiPhuc = createMaKhoiPhuc();
            int val;
            String com = "sp_ChinhSuaDKThi";
            using (SqlCommand command = new SqlCommand(com, Program.conn))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@SOCH", txtSoCH.Text);
                command.Parameters.AddWithValue("@MAMH", cmbMonThi.Text);
                command.Parameters.AddWithValue("@TRINHDO", cmbTrinhDo.Text);
                command.Parameters.AddWithValue("@MALOP", cmbLopThi.Text);
                command.Parameters.AddWithValue("@NGAYTHI", dateNgayThi.Text);
                command.Parameters.AddWithValue("@LAN", cmbLanThi.Text);
                command.Parameters.AddWithValue("@THOIGIAN", txtThoiGian.Text);
                command.Parameters.AddWithValue("@MAKHOIPHUC", maKhoiPhuc);
                command.Parameters.AddWithValue("@MAGV", Program.userName);

                // Tạo parameter để nhận giá trị trả về từ stored procedure
                SqlParameter returnParameter = command.Parameters.Add("@ReturnVal", SqlDbType.Int);
                returnParameter.Direction = ParameterDirection.ReturnValue;

                // Thực thi stored procedure
                command.ExecuteNonQuery();
            }
        }
        private void btnKhoiPhuc_Click(object sender, EventArgs e)
        {
            ThongTinDangKy temp = undoStack.Pop();
            loadThongTin(temp);
            if (temp.trangthai == "xoa")
            {
                themDangKy();
                loadData();
                redoStack.Push(temp);
            }
            else if(temp.trangthai =="them")
            {
                xoaDangKy();
                loadData();
                redoStack.Push(temp);
            }
            else if(temp.trangthai=="chinhsua")
            {
                chinhSuaDangKy();
                loadData();
                redoStack.Push(newInfo);
                newInfo = temp;
            }
            checkUnRedo();

        }

        private void btnRedo_Click(object sender, EventArgs e)
        {
            ThongTinDangKy temp = redoStack.Pop();
            loadThongTin(temp);

            if (temp.trangthai == "xoa")
            {
                xoaDangKy();
                loadData();
                undoStack.Push(temp);
            }
            else if (temp.trangthai == "them")
            {
                themDangKy();
                loadData();
                undoStack.Push(temp);
            }
            else if (temp.trangthai == "chinhsua")
            {
                
                chinhSuaDangKy();
                loadData();
                undoStack.Push(newInfo);
                newInfo = temp;
            }
            checkUnRedo();
        }

        private void txtSoCH_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void aas_Click(object sender, EventArgs e)
        {

        }

        private void cmbLanThi_MouseClick(object sender, MouseEventArgs e)
        {
            xuLyComboBoxLanThi();
        }

        private void dtgvShowDangKy_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Program.myReader = Program.ExecSqlDataReader("select MADK FROM GIAOVIEN_DANGKY WHERE MALOP='"+cmbLopThi.Text+"' AND MAMH='"
                + cmbMonThi.Text +"' AND LAN='"+cmbLanThi.Text+"'" );
            if (Program.myReader == null)
                return;
            Program.myReader.Read();
            Program.maDe = Program.myReader.GetInt32(0);
            Program.myReader.Close();
            frmLamBaiThi f = new frmLamBaiThi();   
            f.ShowDialog();
        }
    }
}

