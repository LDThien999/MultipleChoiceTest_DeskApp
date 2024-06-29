using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using TNCSDLPT;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace CSDLPT.feature
{
    public partial class FeatureForm : Form
    {
        private SqlCommand sqlCommand;
        private SqlDataAdapter adapter = new SqlDataAdapter();
        private DataTable table = new DataTable();
        List<String> listComboBox = new List<string>();

        private string coso;
        private string originalTextTenLop;

        class ThongTinMon
        {
            public string maMH { get; set; }
            public string tenMH { get; set; }
            public string TrangThai { get; set; }

            public ThongTinMon(string maMH, string tenMH, string TrangThai)
            {
                this.maMH = maMH;
                this.tenMH = tenMH;
                this.TrangThai = TrangThai;
            }
        }

        class ThongTinKhoa
        {
            public string maKH { get; set; }
            public string tenKH { get; set; }
            public string maCS { get; set; }
            public string TrangThai { get; set; }

            public ThongTinKhoa(string maMK, string tenKH, string maCS, string TrangThai)
            {
                this.maKH = maMK;
                this.tenKH = tenKH;
                this.maCS = maCS;
                this.TrangThai = TrangThai;
            }
        }

        class ThongTinLop
        {
            public string maLOP { get; set; }
            public string tenLOP { get; set; }
            public string maKH { get; set; }
            public string TrangThai { get; set; }

            public ThongTinLop(string maLOP, string tenLOP, string maKH, string TrangThai)
            {
                this.maLOP = maLOP;
                this.tenLOP = tenLOP;
                this.maKH = maKH;
                this.TrangThai = TrangThai;
            }
        }

        // Tạo hai stack undo và redo
        Stack<ThongTinMon> undoStack = new Stack<ThongTinMon>();
        Stack<ThongTinMon> redoStack = new Stack<ThongTinMon>();

        Stack<ThongTinKhoa> undoKHOAStack = new Stack<ThongTinKhoa>();
        Stack<ThongTinKhoa> redoKHOAStack = new Stack<ThongTinKhoa>();

        Stack<ThongTinLop> undoLOPStack = new Stack<ThongTinLop>();
        Stack<ThongTinLop> redoLOPStack = new Stack<ThongTinLop>();

        public FeatureForm()
        {
            InitializeComponent();
            this.FormClosing += FeatureForm_FormClosing;

        }

        private void FeatureForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn đóng form không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.No)
                {
                    e.Cancel = true;
                }
                else
                {
                    undoStack.Clear();
                    redoStack.Clear();
                    undoKHOAStack.Clear();
                    redoKHOAStack.Clear();
                    undoLOPStack.Clear();
                    redoLOPStack.Clear();
                    frmMain main = new frmMain();
                    main.Show();
                }
            }
        }

        private void FeatureForm_Load(object sender, EventArgs e)
        {
            tabControl1 = new TabControl();
            LoadDataIntoDataGridView("MONHOC", dataGridView1);
            BindingTextMONHOC(dataGridView1);
            checkUnRedoMonHoc();

            if (Program.role != "COSO")
            {
                panelFunc.Enabled = false;
                panelFuncKHOALOP.Enabled = false;
            }

            btnXoa.Enabled = false;
            btnChinhSua.Enabled = false;
            btnSave.Visible = false;
            btnSave.Enabled = false;
            btnSave_Add.Visible = false;
            btnThoatAdd.Visible = false;

            txtMaH.ReadOnly = true;
            textBoxTenMon.ReadOnly = true;

            undoStack.Clear();
            redoStack.Clear();
            undoKHOAStack.Clear();
            redoKHOAStack.Clear();
            undoLOPStack.Clear();
            redoLOPStack.Clear();

            string statement = "select MAKH from GIAOVIEN";
            Program.myReader = Program.ExecSqlDataReader(statement);
            if (Program.myReader == null)
                return;
            Program.myReader.Read();
            lblCoSo.Text = Program.brand2;
            lblMa.Text = Program.userName;
            lblName.Text = Program.staff;
            lblKhoa.Text = Program.myReader.GetString(0);
            Program.myReader.Close();

            LoadTabPage2();
        }



        void checkUnRedoMonHoc()
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

        void checkUnRedoKHOALOP()
        {
            btnKhoiPhuc_KhoaLop.Enabled = false;
            btnRedo_KhoaLop.Enabled=false;
            string selectedString = ComboBoxKhoaLop.SelectedIndex.ToString();
            if (selectedString == "0") //KHOA
            {
                if (undoKHOAStack.Count > 0)
                    btnKhoiPhuc_KhoaLop.Enabled = true;
                else
                    btnKhoiPhuc_KhoaLop.Enabled = false;

                if (redoKHOAStack.Count > 0)
                    btnRedo_KhoaLop.Enabled = true;
                else
                    btnRedo_KhoaLop.Enabled = false;
            }
            else
            {
                if (undoLOPStack.Count > 0)
                    btnKhoiPhuc_KhoaLop.Enabled = true;
                else
                    btnKhoiPhuc_KhoaLop.Enabled = false;

                if (redoLOPStack.Count > 0)
                    btnRedo_KhoaLop.Enabled = true;
                else
                    btnRedo_KhoaLop.Enabled = false;
            }
        }

        //Text - Binding
        private void BindingTextMONHOC(DataGridView gridview)
        {
            txtMaH.DataBindings.Clear();//clearBinding
            textBoxTenMon.DataBindings.Clear();
            txtMaH.DataBindings.Add("Text", gridview.DataSource, "MAMH"); //Binding textBox
            textBoxTenMon.DataBindings.Add("Text", gridview.DataSource, "TENMH");
        }

        //Regex
        private bool CheckRegexID(string input)
        {
            string pattern = @"^[a-zA-Z0-9]+$";
            if (Regex.IsMatch(input, pattern))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private bool CheckRegexName(string input)
        {
            string pattern = @"^\s*$";
            if (Regex.IsMatch(input, pattern))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        //GridView - LoadData
        public void LoadDataIntoDataGridView(String selectedTable, DataGridView gridview)
        {
            gridview.DataSource = null;
            gridview.Rows.Clear();
            gridview.Columns.Clear();

            sqlCommand = Program.conn.CreateCommand();
            sqlCommand.CommandText = "SELECT * FROM " + selectedTable; ;
            adapter.SelectCommand = sqlCommand;
            DataTable table = new DataTable();
            try
            {
                adapter.Fill(table);
                table.Columns.Remove("rowguid"); //loai bo dong rowguid
                gridview.DataSource = table; //add data tu table vao gridView
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            gridview.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            gridview.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            gridview.ReadOnly = true;


        }




        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            CheckTextBoxChange_MONHOC();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            CheckTextBoxChange_MONHOC();
        }

        private void CheckTextBoxChange_MONHOC()
        {
            btnXoa.Enabled = false;
            btnSave.Enabled = false;
            btnSave_Add.Enabled = false;

            string trimmedTextBox1 = Regex.Replace(txtMaH.Text, @"\s+", "");
            string trimmedTextBox2 = Regex.Replace(textBoxTenMon.Text, @"\s+", "");
            if (trimmedTextBox1 != "" && trimmedTextBox2 != "")
            {
                btnSave_Add.Enabled = true;
                btnSave.Enabled = true;
            }
        }

        //BUTTON ADD
        private void btnThemMon_Click(object sender, EventArgs e)
        {
            btnThemMon.Visible = false;
            btnSave_Add.Visible = true;
            btnChinhSua.Enabled = false;
            btnXoa.Enabled = false;
            dataGridView1.Enabled = false;
            txtMaH.Clear();
            textBoxTenMon.Clear();
            txtMaH.ReadOnly = false;
            textBoxTenMon.ReadOnly = false;
            btnThoatAdd.Visible = true;
            btnThoatAdd.Enabled = true;

            btnKhoiPhuc.Enabled = false;
            btnRedo.Enabled = false;
        }

        private void btnSave_Add_Click(object sender, EventArgs e)
        {
            string mamonhoc = txtMaH.Text;
            string tenmonhoc = textBoxTenMon.Text;
            if (CheckRegexID(mamonhoc) == true
                && CheckRegexName(tenmonhoc) == true
                && mamonhoc.Length <= 5)
            {
                if (CheckSP_MonHoc(mamonhoc, tenmonhoc) == true)
                {
                    string sqlQuery = "INSERT INTO MONHOC (MAMH, TENMH) VALUES (@InputData1, @InputData2)";

                    using (SqlCommand command = new SqlCommand(sqlQuery, Program.conn))
                    {
                        command.Parameters.AddWithValue("@InputData1", mamonhoc);
                        command.Parameters.AddWithValue("@InputData2", tenmonhoc);

                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {

                            MessageBox.Show("Thêm dữ liệu thành công!");
                            ThongTinMon info = new ThongTinMon(mamonhoc, tenmonhoc, "them");

                            undoStack.Push(info);
                            checkUnRedoMonHoc();
                        }
                        else
                        {
                            MessageBox.Show("Không thể thêm dữ liệu!");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Lỗi: Đã tồn tại mã môn hoặc tên môn!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                LoadDataIntoDataGridView("MONHOC", dataGridView1);
                BindingTextMONHOC(dataGridView1);
                btnSave_Add.Visible = false;
                btnThemMon.Visible = true;
                btnChinhSua.Enabled = true;
                btnXoa.Enabled = true;
                dataGridView1.Enabled = true;
                txtMaH.ReadOnly = true;
                textBoxTenMon.ReadOnly = true;
                btnThoatAdd.Visible = false;
                checkUnRedoMonHoc();
            }
            else
            {
                MessageBox.Show("Lỗi: Dữ liệu nhập không hợp lệ!");
            }


        }

        //BUTTON DELETE
        private void btnXoa_Click_1(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa không?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dataGridView1.SelectedCells.Count > 0)
            {
                int selectedRowIndex = dataGridView1.SelectedCells[0].RowIndex;
                string primaryKeyValue = dataGridView1.Rows[selectedRowIndex].Cells[0].Value.ToString();
                string tenmonhoc = textBoxTenMon.Text;
                if (CheckSP_MonHocExistInOtherTable(primaryKeyValue) == true)
                {
                    string deleteQuery = "DELETE FROM MONHOC WHERE MAMH = @MAMH";
                    SqlCommand deleteCommand = new SqlCommand(deleteQuery, Program.conn);
                    deleteCommand.Parameters.AddWithValue("@MAMH", primaryKeyValue);
                    if (result == DialogResult.Yes)
                    {
                        try
                        {
                            int rowsAffected = deleteCommand.ExecuteNonQuery();
                            if (rowsAffected > 0)
                            {
                                LoadDataIntoDataGridView("MONHOC", dataGridView1);
                                BindingTextMONHOC(dataGridView1);
                                MessageBox.Show("Xóa hàng thành công!");

                                ThongTinMon info = new ThongTinMon(primaryKeyValue, tenmonhoc, "xoa");

                                undoStack.Push(info);
                                checkUnRedoMonHoc();
                            }
                            else
                            {
                                MessageBox.Show("Không có hàng nào được xóa!");
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Lỗi xảy ra khi xóa hàng: " + ex.Message);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Không thể xóa môn học! \n Môn học đã được đăng ký");
                }
            }

        }

        //BUTTON THOAT
        private void btnThoatAdd_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn thoát?", "Xác nhận thoát", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                btnThemMon.Visible = true;
                btnThemMon.Enabled = true;
                btnSave_Add.Visible = false;
                btnChinhSua.Enabled = false;
                btnChinhSua.Visible = true;
                btnSave.Visible = false;
                btnXoa.Enabled = false;
                btnXoa.Visible = true;
                dataGridView1.Enabled = true;
                txtMaH.Clear();
                textBoxTenMon.Clear();
                txtMaH.ReadOnly = true;
                textBoxTenMon.ReadOnly = true;

                LoadDataIntoDataGridView("MONHOC", dataGridView1);
                BindingTextMONHOC(dataGridView1);
                checkUnRedoMonHoc();
            }
        }

        // BUTTON CHINH SUA
        private void btnChinhSua_Click(object sender, EventArgs e)
        {
            textBoxTenMon.ReadOnly = false;
            btnChinhSua.Visible = false;
            btnSave.Visible = true;
            btnXoa.Enabled = false;
            btnThemMon.Enabled = false;
            btnThoatAdd.Visible = true;
            btnThoatAdd.Enabled = true;

            btnKhoiPhuc.Enabled = false;
            btnRedo.Enabled = false;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            string mamonhoc = txtMaH.Text;
            string tenmonhoc = textBoxTenMon.Text;
            if (CheckRegexName(tenmonhoc) == true)
            {
                if (CheckSP_MonHoc("null", tenmonhoc))
                {
                    ThongTinMon infoBeforeUpdate = GetThongTinMonFromDatabase(mamonhoc);

                    string sqlQuery = "UPDATE MONHOC SET TENMH = @InputData2 WHERE MAMH = @InputData1";
                    using (SqlCommand command = new SqlCommand(sqlQuery, Program.conn))
                    {
                        command.Parameters.AddWithValue("@InputData1", mamonhoc);
                        command.Parameters.AddWithValue("@InputData2", tenmonhoc);
                        int rowsAffected = command.ExecuteNonQuery();
                        try
                        {
                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Chỉnh sửa dữ liệu thành công!");
                                undoStack.Push(infoBeforeUpdate);
                            }
                            else
                            {
                                MessageBox.Show("Chỉnh sửa dữ liệu thất bại!");
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Đã xảy ra lỗi!");
                        }
                    }

                }
                else
                {
                    MessageBox.Show("Tên môn học đã tồn tại!");
                }
                LoadDataIntoDataGridView("MONHOC", dataGridView1);
                BindingTextMONHOC(dataGridView1);
                dataGridView1.Enabled = true;
                btnChinhSua.Visible = true;
                btnSave.Visible = false;
                btnThemMon.Enabled = true;
                textBoxTenMon.ReadOnly = true;
                btnThoatAdd.Visible = false;
                checkUnRedoMonHoc();
            }
            else
            {
                MessageBox.Show("Lỗi: Dữ liệu nhập không hợp lệ!");
            }
        }

        //BUTTON UNDO
        private void btnKhoiPhuc_Click(object sender, EventArgs e)
        {


            if (undoStack.Count > 0)
            {
                ThongTinMon temp = undoStack.Pop();

                if (temp.TrangThai == "xoa")
                {
                    themMonHoc(temp);
                    temp.TrangThai = "them";
                    redoStack.Push(temp);
                }
                else if (temp.TrangThai == "them")
                {
                    xoaMonHoc(temp);
                    temp.TrangThai = "xoa";
                    redoStack.Push(temp);
                }
                else if (temp.TrangThai == "chinhsua")
                {
                    redoStack.Push(GetThongTinMonFromDatabase(temp.maMH));
                    chinhSuaMonHoc(temp);
                }
            }
            LoadDataIntoDataGridView("MONHOC", dataGridView1);
            BindingTextMONHOC(dataGridView1);
            checkUnRedoMonHoc();

        }

        //BUTTON REDO
        private void btnRedo_Click(object sender, EventArgs e)
        {
            if (redoStack.Count > 0)
            {
                ThongTinMon temp = redoStack.Pop();

                if (temp.TrangThai == "xoa")
                {
                    themMonHoc(temp);
                    temp.TrangThai = "them";
                    undoStack.Push(temp);
                }
                else if (temp.TrangThai == "them")
                {
                    xoaMonHoc(temp);
                    temp.TrangThai = "xoa";
                    undoStack.Push(temp);
                }
                else if (temp.TrangThai == "chinhsua")
                {
                    undoStack.Push(GetThongTinMonFromDatabase(temp.tenMH));
                    chinhSuaMonHoc(temp);
                }
            }
            LoadDataIntoDataGridView("MONHOC", dataGridView1);
            BindingTextMONHOC(dataGridView1);
            checkUnRedoMonHoc();
        }

        private void themMonHoc(ThongTinMon info)
        {
            string sqlQuery = "INSERT INTO MONHOC (MAMH, TENMH) VALUES (@maMH, @tenMH);";
            using (SqlCommand command = new SqlCommand(sqlQuery, Program.conn))
            {
                command.Parameters.AddWithValue("@maMH", info.maMH);
                command.Parameters.AddWithValue("@tenMH", info.tenMH);

                command.ExecuteNonQuery();
            }
        }

        private void xoaMonHoc(ThongTinMon info)
        {
            string sqlQuery = "DELETE FROM MONHOC WHERE MAMH = @maMH;";
            using (SqlCommand command = new SqlCommand(sqlQuery, Program.conn))
            {
                command.Parameters.AddWithValue("@maMH", info.maMH);

                command.ExecuteNonQuery();
            }
        }

        private void chinhSuaMonHoc(ThongTinMon info)
        {
            string sqlQuery = "UPDATE MONHOC SET TENMH = @tenMH WHERE MAMH = @maMH;";
            using (SqlCommand command = new SqlCommand(sqlQuery, Program.conn))
            {
                command.Parameters.AddWithValue("@maMH", info.maMH);
                command.Parameters.AddWithValue("@tenMH", info.tenMH);

                command.ExecuteNonQuery();
            }
        }



        ThongTinMon GetThongTinMonFromDatabase(string maMH)
        {
            using (SqlCommand command = new SqlCommand("SELECT MAMH, TENMH FROM MONHOC WHERE MAMH = @maMH", Program.conn))
            {
                command.Parameters.AddWithValue("@maMH", maMH);
                try
                {
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        string maMonHoc = reader["MAMH"].ToString();
                        string tenMonHoc = reader["TENMH"].ToString();

                        reader.Close();

                        ThongTinMon thongTinMon = new ThongTinMon(maMonHoc, tenMonHoc, "chinhsua");
                        return thongTinMon;
                    }

                    reader.Close();
                    return null; // Trả về null nếu không tìm thấy dữ liệu
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: get data from database " + ex.Message);
                    return null;
                }
            }
        }


        //SP - Kiem tra mon co ton tai o bang GV_DK va BODE
        private bool CheckSP_MonHocExistInOtherTable(string mamonhoc)
        {
            System.Console.WriteLine(mamonhoc);
            using (SqlCommand command = new SqlCommand("SP_CheckMonHoc_ExistOnOthersTables", Program.conn))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@MAMH", mamonhoc);

                // trả về 1: đã tồn tại, trả về 0: có thể thêm
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    if (reader.GetString(0) == "0")
                    {
                        reader.Close();
                        return true;
                    }
                }
                reader.Close();
                return false;
            }
        }
        //CHECK SP_MonHocExist
        private bool CheckSP_MonHoc(string mamonhoc, string tenmonhoc)
        {
            System.Console.WriteLine(mamonhoc);
            using (SqlCommand command = new SqlCommand("SP_CheckMonHocExist", Program.conn))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@MAMH", mamonhoc);
                command.Parameters.AddWithValue("@TENMH", tenmonhoc);

                // trả về 1: đã tồn tại, trả về 0: có thể thêm
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    if (reader.GetString(0) == "0")
                    {
                        reader.Close();
                        return true;
                    }
                }
                reader.Close();
                return false;
            }
        }



        private void DataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            btnXoa.Enabled = true;
            btnChinhSua.Enabled = true;
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                dataGridView1.Rows[e.RowIndex].Selected = true;
            }
        }


        //==========================KHOA - LOP=============================
        private void LoadTabPage2()
        {
            if(Program.role != "COSO")
            {
                panelFuncKHOALOP.Enabled = false;
                //panelKhoa.Enabled = false;
                //panelLop.Enabled = false;
            }

            LoadDataIntoDataGridView("KHOA", dataGridViewKHOA);
            LoadDataIntoDataGridView("LOP", dataGridViewLOP);
            BindingTextKHOA(dataGridViewKHOA);
            BindingTextLOP(dataGridViewLOP);
            LoadDataIntoComboBoxMAKH_LOP();
            checkUnRedoKHOALOP();

            string statement = "select MAKH from GIAOVIEN";
            Program.myReader = Program.ExecSqlDataReader(statement);
            if (Program.myReader == null)
                return;
            Program.myReader.Read();
            lblTeacherSite.Text = Program.brand2;
            lblTeacherId.Text = Program.userName;
            lblTeacherName.Text = Program.staff;
            lblTeacherDepartment.Text = Program.myReader.GetString(0);
            Program.myReader.Close();


            panelLop.Visible = false;

            dataGridViewLOP.Enabled = false;

            btnThem_KhoaLop.Enabled = false;
            btnSua_KhoaLop.Enabled = false;
            btnXoa_KhoaLop.Enabled = false;
            btnSave_Add_KHOALOP.Visible = false;
            btnSave_Change_KHOALOP.Visible = false;
            btnThoatKHOALOP.Visible = false;

            textBoxMaKhoa_Khoa.ReadOnly = true;
            textBoxTenKhoa.ReadOnly = true;
            textBoxMaLop.ReadOnly = true;
            textBoxTenLop.ReadOnly = true;

            labelKhoa.Visible = true;

            comboBoxCOSO.FormattingEnabled = false;
            comboBoxMaKhoa_Lop.FormattingEnabled = false;

            //---------DEFAULT = KHOA------------
            ComboBoxKhoaLop.SelectedIndex = 0;
            panelKhoa.Visible = true;
            dataGridViewKHOA.Enabled = true;


            coso = "CS" + (Program.brand + 1).ToString();
            comboBoxCOSO.Items.Add(coso);

            if (Program.role == "TRUONG")
                panelFuncKHOALOP.Enabled = false;

        }





        //COMBOBOX CHANGE
        private void ComboBoxKhoaLop_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedString = ComboBoxKhoaLop.SelectedIndex.ToString();
            if (selectedString == "0") //KHOA
            {
                btnThem_KhoaLop.Enabled = true;

                labelKhoa.Visible = true;
                labelLop.Visible = false;
                dataGridViewKHOA.Enabled = true;
                dataGridViewKHOA.Visible = true;
                dataGridViewLOP.Enabled = false;
                dataGridViewLOP.Enabled = false;

                panelKhoa.Visible = true;
                panelLop.Visible = false;
                LoadDataIntoDataGridView("KHOA", dataGridViewKHOA);
                LoadDataIntoDataGridView("LOP", dataGridViewLOP);
                BindingTextKHOA(dataGridViewKHOA);
                BindingTextLOP(dataGridViewLOP);
                LoadDataIntoComboBoxMAKH_LOP();
                checkUnRedoKHOALOP();
            }
            else //LOP
            {
                btnThem_KhoaLop.Enabled = true;

                labelKhoa.Visible = false;
                labelLop.Visible = true;
                dataGridViewKHOA.Enabled = false;
                dataGridViewKHOA.Visible = false;
                dataGridViewLOP.Enabled = true;
                dataGridViewLOP.Enabled = true;

                panelKhoa.Visible = false;
                panelLop.Visible = true;
                LoadDataIntoDataGridView("KHOA", dataGridViewKHOA);
                LoadDataIntoDataGridView("LOP", dataGridViewLOP);
                BindingTextKHOA(dataGridViewKHOA);
                BindingTextLOP(dataGridViewLOP);
                LoadDataIntoComboBoxMAKH_LOP();
                checkUnRedoKHOALOP();

            }
        }

        private void LoadDataIntoComboBoxMAKH_LOP()
        {
            comboBoxMaKhoa_Lop.Items.Clear();
            sqlCommand = Program.conn.CreateCommand();
            sqlCommand.CommandText = "SELECT DISTINCT MAKH FROM KHOA";
            adapter.SelectCommand = sqlCommand;
            DataTable table = new DataTable();
            try
            {
                adapter.Fill(table);

                foreach (DataRow row in table.Rows)
                {
                    comboBoxMaKhoa_Lop.Items.Add(row["MAKH"].ToString());
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        //TETXBOX_CHANGE
        private bool StrimmedText(string input)
        {
            string trimmedText = Regex.Replace(input, @"\s+", "");
            if (trimmedText == "")
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        private void CheckTextBoxChange_KHOA()
        {
            btnXoa_KhoaLop.Enabled = false;
            btnSave_Add_KHOALOP.Enabled = false;
            btnSave_Change_KHOALOP.Enabled = false;

            if (StrimmedText(textBoxMaKhoa_Khoa.Text) == true
                && StrimmedText(textBoxTenKhoa.Text) == true)
            {
                btnSave_Add_KHOALOP.Enabled = true;
                btnSave_Change_KHOALOP.Enabled = true;
            }
        }
        private void CheckTextBoxChange_LOP()
        {
            btnXoa_KhoaLop.Enabled = false;
            btnSave_Add_KHOALOP.Enabled = false;
            btnSave_Change_KHOALOP.Enabled = false;

            if (StrimmedText(textBoxMaLop.Text) == true
                && StrimmedText(textBoxTenLop.Text) == true)
            {
                btnSave_Add_KHOALOP.Enabled = true;
                btnSave_Change_KHOALOP.Enabled = true;
            }
        }

        private void textBoxTenKhoa_TextChanged(object sender, EventArgs e)
        {
            CheckTextBoxChange_KHOA();
        }

        private void textBoxMaCoSo_TextChanged(object sender, EventArgs e)
        {
            CheckTextBoxChange_KHOA();
        }
        private void textBoxMaKhoa_Khoa_TextChanged(object sender, EventArgs e)
        {
            CheckTextBoxChange_KHOA();
        }
        private void textBoxMaLop_TextChanged(object sender, EventArgs e)
        {
            CheckTextBoxChange_LOP();
        }
        private void textBoxTenLop_TextChanged(object sender, EventArgs e)
        {
            CheckTextBoxChange_LOP();
        }


        //BINDING-TEXT
        private void BindingTextKHOA(DataGridView gridview)
        {
            textBoxMaKhoa_Khoa.DataBindings.Clear();//clearBinding
            textBoxTenKhoa.DataBindings.Clear();
            comboBoxCOSO.DataBindings.Clear();
            textBoxMaKhoa_Khoa.DataBindings.Add("Text", gridview.DataSource, "MAKH"); //Binding textBox
            textBoxTenKhoa.DataBindings.Add("Text", gridview.DataSource, "TENKH");
            comboBoxCOSO.DataBindings.Add("Text", gridview.DataSource, "MACS");

        }

        private void BindingTextLOP(DataGridView gridview)
        {
            textBoxMaLop.DataBindings.Clear();//clearBinding
            textBoxTenLop.DataBindings.Clear();
            comboBoxMaKhoa_Lop.DataBindings.Clear();
            textBoxMaLop.DataBindings.Add("Text", gridview.DataSource, "MALOP"); //Binding textBox
            textBoxTenLop.DataBindings.Add("Text", gridview.DataSource, "TENLOP");
            comboBoxMaKhoa_Lop.DataBindings.Add("Text", gridview.DataSource, "MAKH");

        }

        //CLICK-CELLS
        private void DataGridViewKHOA_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            btnXoa_KhoaLop.Enabled = true;
            btnSua_KhoaLop.Enabled = true;
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                dataGridViewKHOA.Rows[e.RowIndex].Selected = true;
            }
        }

        private void DataGridViewLOP_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            btnXoa_KhoaLop.Enabled = true;
            btnSua_KhoaLop.Enabled = true;
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                dataGridViewLOP.Rows[e.RowIndex].Selected = true;
                originalTextTenLop = dataGridViewLOP.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
            }
        }


        //BUTTON ADD KHOA-LOP
        private void btnThem_KhoaLop_Click(object sender, EventArgs e)
        {
            ComboBoxKhoaLop.Enabled = false;
            btnThem_KhoaLop.Visible = false;
            btnSave_Add_KHOALOP.Visible = true;
            btnSua_KhoaLop.Enabled = false;
            btnXoa_KhoaLop.Enabled = false;
            btnThoatKHOALOP.Visible = true;
            btnThoatKHOALOP.Enabled = true;

            btnKhoiPhuc_KhoaLop.Enabled = false;
            btnRedo_KhoaLop.Enabled=false;

            if (ComboBoxKhoaLop.SelectedIndex.ToString() == "0")
            {
                textBoxMaKhoa_Khoa.Clear();
                textBoxTenKhoa.Clear();
                textBoxMaKhoa_Khoa.ReadOnly = false;
                textBoxTenKhoa.ReadOnly = false;
                dataGridViewKHOA.Enabled = false;
            }
            else
            {
                textBoxMaLop.Clear();
                textBoxTenLop.Clear();
                textBoxMaLop.ReadOnly = false;
                textBoxTenLop.ReadOnly = false;
                dataGridViewLOP.Enabled = false;
            }

        }

        private void btnSave_Add_KHOALOP_Click(object sender, EventArgs e)
        {
            if (ComboBoxKhoaLop.SelectedIndex.ToString() == "0")
            {
                string makhoa = textBoxMaKhoa_Khoa.Text;
                string tenkhoa = textBoxTenKhoa.Text;

                if (CheckRegexID(makhoa) == true
                    && CheckRegexName(tenkhoa) == true)
                {

                    if (CheckSP_KhoaTonTai(makhoa, tenkhoa) == true)
                    {
                        string sqlQuery = "INSERT INTO KHOA (MAKH, TENKH, MACS) VALUES (@InputData1, @InputData2, @InputData3)";

                        using (SqlCommand command = new SqlCommand(sqlQuery, Program.conn))
                        {
                            command.Parameters.AddWithValue("@InputData1", makhoa);
                            command.Parameters.AddWithValue("@InputData2", tenkhoa);
                            command.Parameters.AddWithValue("@InputData3", coso);

                            int rowsAffected = command.ExecuteNonQuery();
                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Thêm dữ liệu thành công!");

                                ThongTinKhoa info = new ThongTinKhoa(makhoa, tenkhoa,coso,"them");
                                undoKHOAStack.Push(info);
                                checkUnRedoKHOALOP();
                            }
                            else
                            {
                                MessageBox.Show("Không thể thêm dữ liệu!");
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Lỗi: Đã tồn tại mã khoa hoặc tên khoa \n(Cơ sở này hoặc cơ sở khác) !", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    LoadDataIntoDataGridView("KHOA", dataGridViewKHOA);
                    BindingTextKHOA(dataGridViewKHOA);
                    btnSave_Add_KHOALOP.Visible = false;
                    btnThem_KhoaLop.Visible = true;
                    btnThoatKHOALOP.Visible = false;
                    ComboBoxKhoaLop.Enabled = true;
                    textBoxMaKhoa_Khoa.ReadOnly = true;
                    textBoxTenKhoa.ReadOnly = true;
                    textBoxMaLop.ReadOnly = true;
                    textBoxTenLop.ReadOnly = true;
                    dataGridViewKHOA.Enabled = true;
                    checkUnRedoKHOALOP();
                }
                else
                {
                    MessageBox.Show("Lỗi: Dữ liệu không hợp lệ!");
                }
            }
            else
            {
                string malop = textBoxMaLop.Text;
                string tenlop = textBoxTenLop.Text;
                string makhoa_lop = comboBoxMaKhoa_Lop.SelectedItem.ToString();

                if (CheckRegexID(malop) == true
                    && CheckRegexName(tenlop) == true
                    && malop.Length <= 15)
                {
                    if (CheckSP_Lop_TonTaiKhoa(makhoa_lop) == true)
                    {

                        if (CheckSP_LopTonTai(malop, tenlop) == true)
                        {
                            string sqlQuery = "INSERT INTO LOP (MALOP, TENLOP, MAKH) VALUES (@InputData1, @InputData2, @InputData3)";

                            using (SqlCommand command = new SqlCommand(sqlQuery, Program.conn))
                            {
                                command.Parameters.AddWithValue("@InputData1", malop);
                                command.Parameters.AddWithValue("@InputData2", tenlop);
                                command.Parameters.AddWithValue("@InputData3", makhoa_lop);

                                int rowsAffected = command.ExecuteNonQuery();
                                if (rowsAffected > 0)
                                {
                                    MessageBox.Show("Thêm dữ liệu thành công!");

                                    ThongTinLop info = new ThongTinLop(malop, tenlop, makhoa_lop, "them");
                                    undoLOPStack.Push(info);
                                    checkUnRedoKHOALOP();
                                }
                                else
                                {
                                    MessageBox.Show("Không thể thêm dữ liệu!");
                                }

                            }
                        }
                        else
                        {
                            MessageBox.Show("Lỗi: Đã tồn tại mã lớp ở cơ sở này hoặc cơ sở khác!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        LoadDataIntoDataGridView("LOP", dataGridViewLOP);
                        BindingTextLOP(dataGridViewLOP);
                        btnSave_Add_KHOALOP.Visible = false;
                        btnThem_KhoaLop.Visible = true;
                        btnThoatKHOALOP.Visible = false;
                        ComboBoxKhoaLop.Enabled = true;
                        textBoxMaKhoa_Khoa.ReadOnly = true;
                        textBoxTenKhoa.ReadOnly = true;
                        textBoxMaLop.ReadOnly = true;
                        textBoxTenLop.ReadOnly = true;
                        dataGridViewLOP.Enabled = true;
                        checkUnRedoKHOALOP();

                    }
                    else
                    {
                        MessageBox.Show("Lỗi: Mã khoa không tồn tại!");
                    }
                }
                else
                {
                    MessageBox.Show("Lỗi: Dữ liệu không hợp lệ!");
                }
            }
        }



        //BUTTON DELETE KHOA-LOP
        private void btnXoa_KhoaLop_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa không?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (ComboBoxKhoaLop.SelectedIndex.ToString() == "0")
            {
                if (dataGridViewKHOA.SelectedCells.Count > 0)
                {
                    int selectedRowIndex = dataGridViewKHOA.SelectedCells[0].RowIndex;
                    string primaryKeyValue = dataGridViewKHOA.Rows[selectedRowIndex].Cells[0].Value.ToString();
                    string tenkhoa = textBoxTenKhoa.Text;
                    string macs = comboBoxCOSO.Text;
                    if (CHECKSP_KiemTraKhoa_TonTai_GV_LOP(primaryKeyValue) == true)
                    {
                        if (result == DialogResult.Yes)
                        {
                            string deleteQuery = "DELETE FROM KHOA WHERE MAKH = @MAKH";
                            SqlCommand deleteCommand = new SqlCommand(deleteQuery, Program.conn);
                            deleteCommand.Parameters.AddWithValue("@MAKH", primaryKeyValue);
                            try
                            {
                                int rowsAffected = deleteCommand.ExecuteNonQuery();
                                if (rowsAffected > 0)
                                {
                                    LoadDataIntoDataGridView("KHOA", dataGridViewKHOA);
                                    BindingTextKHOA(dataGridViewKHOA);
                                    MessageBox.Show("Xóa hàng thành công!");

                                    ThongTinKhoa info = new ThongTinKhoa(primaryKeyValue, tenkhoa, macs, "xoa");
                                    undoKHOAStack.Push(info);
                                    checkUnRedoKHOALOP();

                                }
                                else
                                {
                                    MessageBox.Show("Không có hàng nào được xóa!");
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Lỗi xảy ra khi xóa hàng: " + ex.Message);
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Không thể xóa! \n Có thể xung đột dữ liệu");
                    }
                }
            }
            else
            {
                if (dataGridViewLOP.SelectedCells.Count > 0)
                {
                    int selectedRowIndex = dataGridViewLOP.SelectedCells[0].RowIndex;
                    string primaryKeyValue = dataGridViewLOP.Rows[selectedRowIndex].Cells[0].Value.ToString();
                    string tenlop = textBoxTenLop.Text;
                    string maKH = comboBoxMaKhoa_Lop.Text;
                    if (CHECKSP_KiemTraLop_TonTaiSV(primaryKeyValue) == true)
                    {
                        string deleteQuery = "DELETE FROM LOP WHERE MALOP = @MALOP";
                        SqlCommand deleteCommand = new SqlCommand(deleteQuery, Program.conn);
                        deleteCommand.Parameters.AddWithValue("@MALOP", primaryKeyValue);
                        try
                        {
                            int rowsAffected = deleteCommand.ExecuteNonQuery();
                            if (rowsAffected > 0)
                            {
                                LoadDataIntoDataGridView("LOP", dataGridViewLOP);
                                BindingTextLOP(dataGridViewLOP);
                                MessageBox.Show("Xóa hàng thành công!");

                                ThongTinLop info = new ThongTinLop(primaryKeyValue, tenlop, maKH, "xoa");
                                undoLOPStack.Push(info);
                                checkUnRedoKHOALOP();

                            }
                            else
                            {
                                MessageBox.Show("Không có hàng nào được xóa!");
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Lỗi xảy ra khi xóa hàng: " + ex.Message);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Không thể xóa! \n Có thể xung đột dữ liệu");
                    }
                }
            }
        }


        private bool CHECKSP_KiemTraKhoa_TonTai_GV_LOP(string makhoa)
        {
            System.Console.WriteLine(makhoa);
            using (SqlCommand command = new SqlCommand("sp_KiemTraKhoa_TonTai_GV-LOP", Program.conn))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@MAKH", makhoa);

                // trả về 1: có LỚP hoặc SV có MAKH, trả về 0: khoa rỗng có thể xóa
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    if (reader.GetString(0) == "0")
                    {
                        reader.Close();
                        return true;
                    }
                }
                reader.Close();
                return false;
            }
        }
        private bool CHECKSP_KiemTraLop_TonTaiSV(string malop)
        {
            System.Console.WriteLine(malop);
            using (SqlCommand command = new SqlCommand("sp_KiemTraLop_TonTaiSV", Program.conn))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@MALOP", malop);

                // trả về 1: có SV thuộc LỚP này, trả về 0: lớp rỗng có thể xóa
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    if (reader.GetString(0) == "0")
                    {
                        reader.Close();
                        return true;
                    }
                }
                reader.Close();
                return false;
            }
        }

        //BUTTON CHINH SUA KHOA-LOP
        private void btnSua_KhoaLop_Click(object sender, EventArgs e)
        {
            btnThem_KhoaLop.Enabled = false;
            btnXoa_KhoaLop.Enabled = false;
            btnSua_KhoaLop.Visible = false;
            btnSave_Change_KHOALOP.Visible = true;
            ComboBoxKhoaLop.Enabled = false;
            btnThoatKHOALOP.Visible = true;
            btnThoatKHOALOP.Enabled = true;

            btnKhoiPhuc_KhoaLop.Enabled = false;
            btnRedo_KhoaLop.Enabled =false;

            if (ComboBoxKhoaLop.SelectedIndex.ToString() == "0")
            {
                textBoxMaKhoa_Khoa.ReadOnly = true;
                textBoxTenKhoa.ReadOnly = false;
                dataGridViewKHOA.Enabled = false;
            }
            else
            {
                textBoxMaLop.ReadOnly = true;
                textBoxTenLop.ReadOnly = false;
                dataGridViewLOP.Enabled = false;
            }
        }


        private void btnSave_Change_KHOALOP_Click(object sender, EventArgs e)
        {
            if (ComboBoxKhoaLop.SelectedIndex.ToString() == "0")
            {
                string makh = textBoxMaKhoa_Khoa.Text;
                string tenkhoa = textBoxTenKhoa.Text;
                string macs = comboBoxCOSO.Text;
                if (CheckRegexName(textBoxTenKhoa.Text) == true)
                {
                    if (CheckSP_KhoaTonTai("null", tenkhoa))
                    {
                        ThongTinKhoa infoBeforeUpdate = GetKhoaInfoFromDatabase(makh);

                        string sqlQuery = "UPDATE KHOA SET TENKH = @InputData2 WHERE MAKH = @InputData1";
                        using (SqlCommand command = new SqlCommand(sqlQuery, Program.conn))
                        {
                            command.Parameters.AddWithValue("@InputData1", makh);
                            command.Parameters.AddWithValue("@InputData2", tenkhoa);
                            int rowsAffected = command.ExecuteNonQuery();
                            try
                            {
                                if (rowsAffected > 0)
                                {
                                    MessageBox.Show("Chỉnh sửa dữ liệu thành công!");
                                    undoKHOAStack.Push(infoBeforeUpdate);
                                    checkUnRedoKHOALOP();
                                }
                                else
                                {
                                    MessageBox.Show("Chỉnh sửa dữ liệu thất bại!");
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Đã xảy ra lỗi!");
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Lỗi: Đã tồn tại tên khoa!");
                    }
                    LoadDataIntoDataGridView("KHOA", dataGridViewKHOA);
                    BindingTextKHOA(dataGridViewKHOA);
                    dataGridViewKHOA.Enabled = true;
                    textBoxTenKhoa.ReadOnly = true;
                    ComboBoxKhoaLop.Enabled = true;
                    btnSave_Change_KHOALOP.Visible = false;
                    btnSua_KhoaLop.Visible = true;
                    btnThem_KhoaLop.Enabled = true;
                    btnThoatKHOALOP.Visible = false;
                    checkUnRedoKHOALOP();


                }
                else
                {
                    MessageBox.Show("Lỗi: Dữ liệu nhập không hợp lệ!");
                }
            }
            else
            {
                string malop = textBoxMaLop.Text;
                string makhoa_lop = comboBoxMaKhoa_Lop.SelectedItem.ToString();
                string tenlop = textBoxTenLop.Text;
                if (CheckRegexName(tenlop) == true)
                {
                    if (CheckSP_Lop_TonTaiKhoa(makhoa_lop) == true)
                    {
                        if (tenlop == originalTextTenLop)
                        {
                            tenlop = "null";
                        }
                        if (CheckSP_LopTonTai("null", tenlop))
                        {
                            ThongTinLop infoBeforeUpdate = GetLopInfoFromDatabase(malop);

                            string sqlQuery = "UPDATE LOP SET TENLOP = @InputData2, MAKH = @InputData3 WHERE MALOP = @InputData1";
                            using (SqlCommand command = new SqlCommand(sqlQuery, Program.conn))
                            {
                                command.Parameters.AddWithValue("@InputData1", malop);
                                command.Parameters.AddWithValue("@InputData2", tenlop);
                                command.Parameters.AddWithValue("@InputData3", makhoa_lop);
                                int rowsAffected = command.ExecuteNonQuery();
                                try
                                {
                                    if (rowsAffected > 0)
                                    {
                                        MessageBox.Show("Chỉnh sửa dữ liệu thành công!");
                                        undoLOPStack.Push(infoBeforeUpdate);
                                        checkUnRedoKHOALOP();
                                    }
                                    else
                                    {
                                        MessageBox.Show("Chỉnh sửa dữ liệu thất bại!");
                                    }
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show("Đã xảy ra lỗi!");
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("Tên lớp đã tồn tại!");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Mã Khoa không hợp lệ!");
                    }
                    LoadDataIntoDataGridView("LOP", dataGridViewLOP);
                    BindingTextLOP(dataGridViewLOP);
                    dataGridViewLOP.Enabled = true;
                    textBoxTenLop.ReadOnly = true;
                    ComboBoxKhoaLop.Enabled = true;
                    btnSave_Change_KHOALOP.Visible = false;
                    btnSua_KhoaLop.Visible = true;
                    btnThem_KhoaLop.Enabled = true;
                    btnThoatKHOALOP.Visible = false;
                    checkUnRedoKHOALOP();


                }
                else

                    MessageBox.Show("Lỗi: Dữ liệu nhập không hợp lệ!");

            }
        }

        //BUTTON THOAT KHOA LOP
        private void btnThoatKHOALOP_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn thoát?", "Xác nhận thoát", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                btnThem_KhoaLop.Enabled = true;
                btnThem_KhoaLop.Visible = true;
                btnSave_Add_KHOALOP.Visible = false;

                btnXoa_KhoaLop.Enabled = false;
                btnXoa_KhoaLop.Visible = true;

                btnSua_KhoaLop.Visible = true;
                btnSua_KhoaLop.Enabled = false;
                btnSave_Change_KHOALOP.Visible = false;

                ComboBoxKhoaLop.Enabled = true;
                btnThoatKHOALOP.Visible = false;
                if (ComboBoxKhoaLop.SelectedIndex.ToString() == "0")
                {
                    textBoxMaKhoa_Khoa.ReadOnly = true;
                    textBoxTenKhoa.ReadOnly = true;
                    dataGridViewKHOA.Enabled = true;
                    LoadDataIntoDataGridView("KHOA", dataGridViewKHOA);
                    BindingTextKHOA(dataGridViewKHOA);
                }
                else
                {
                    textBoxMaLop.ReadOnly = true;
                    textBoxTenLop.ReadOnly = true;
                    dataGridViewLOP.Enabled = true;
                    LoadDataIntoDataGridView("LOP", dataGridViewLOP);
                    BindingTextLOP(dataGridViewLOP);
                }
                checkUnRedoKHOALOP();
            }
        }

        //BUTTON UNDO KHOA LOP
        private void btnKhoiPhuc_KhoaLop_Click(object sender, EventArgs e)
        {
            string selectedString = ComboBoxKhoaLop.SelectedIndex.ToString();
            if (selectedString == "0") //KHOA
            {
                if (undoKHOAStack.Count > 0)
                {
                    ThongTinKhoa temp = undoKHOAStack.Pop();

                    if (temp.TrangThai == "xoa")
                    {
                        themKhoa(temp);
                        temp.TrangThai = "them";
                        redoKHOAStack.Push(temp);
                    }
                    else if (temp.TrangThai == "them")
                    {
                        xoaKhoa(temp);
                        temp.TrangThai = "xoa";
                        redoKHOAStack.Push(temp);
                    }
                    else if (temp.TrangThai == "chinhsua")
                    {
                        redoKHOAStack.Push(GetKhoaInfoFromDatabase((temp.maKH)));
                        chinhSuaKhoa(temp);
                    }
                }
                LoadDataIntoDataGridView("KHOA", dataGridViewKHOA);
                BindingTextKHOA(dataGridViewKHOA);
            }
            else
            {
                if (undoLOPStack.Count > 0)
                {
                    ThongTinLop temp = undoLOPStack.Pop();

                    if (temp.TrangThai == "xoa")
                    {
                        themLop(temp);
                        temp.TrangThai = "them";
                        redoLOPStack.Push(temp);
                    }
                    else if (temp.TrangThai == "them")
                    {
                        xoaLop(temp);
                        temp.TrangThai = "xoa";
                        redoLOPStack.Push(temp);
                    }
                    else if (temp.TrangThai == "chinhsua")
                    {
                        redoLOPStack.Push(GetLopInfoFromDatabase((temp.maLOP)));
                        chinhSuaLop(temp);
                    }
                }
                LoadDataIntoDataGridView("LOP", dataGridViewLOP);
                BindingTextLOP(dataGridViewLOP);
            }
            checkUnRedoKHOALOP();
        }

        //BUTTON REDO KHOA LOP
        private void btnRedo_KhoaLop_Click(object sender, EventArgs e)
        {
        string selectedString = ComboBoxKhoaLop.SelectedIndex.ToString();
            if (selectedString == "0") //KHOA
            {
                if (redoKHOAStack.Count > 0)
                {
                    ThongTinKhoa temp = redoKHOAStack.Pop();

                    if (temp.TrangThai == "xoa")
                    {
                        themKhoa(temp);
                        temp.TrangThai = "them";
                        undoKHOAStack.Push(temp);
                    }
                    else if (temp.TrangThai == "them")
                    {
                        xoaKhoa(temp);
                        temp.TrangThai = "xoa";
                        undoKHOAStack.Push(temp);
                    }
                    else if (temp.TrangThai == "chinhsua")
                    {
                        undoKHOAStack.Push(GetKhoaInfoFromDatabase((temp.maKH)));
                        chinhSuaKhoa(temp);
                    }
                }
                LoadDataIntoDataGridView("KHOA", dataGridViewKHOA);
                BindingTextKHOA(dataGridViewKHOA);
            }
            else
            {
                if (redoLOPStack.Count > 0)
                {
                    ThongTinLop temp = redoLOPStack.Pop();

                    if (temp.TrangThai == "xoa")
                    {
                        themLop(temp);
                        temp.TrangThai = "them";
                        undoLOPStack.Push(temp);
                    }
                    else if (temp.TrangThai == "them")
                    {
                        xoaLop(temp);
                        temp.TrangThai = "xoa";
                        undoLOPStack.Push(temp);
                    }
                    else if (temp.TrangThai == "chinhsua")
                    {
                        undoLOPStack.Push(GetLopInfoFromDatabase((temp.maLOP)));
                        chinhSuaLop(temp);
                    }
                }
                LoadDataIntoDataGridView("LOP", dataGridViewLOP);
                BindingTextLOP(dataGridViewLOP);
            }
            checkUnRedoKHOALOP();
        }

        private void themKhoa(ThongTinKhoa info)
        {
            string sqlQuery = "INSERT INTO KHOA (MAKH, TENKH, MACS) VALUES (@maKH, @tenKH, @maCS);";
            using (SqlCommand command = new SqlCommand(sqlQuery, Program.conn))
            {
                command.Parameters.AddWithValue("@maKH", info.maKH);
                command.Parameters.AddWithValue("@tenKH", info.tenKH);
                command.Parameters.AddWithValue("@maCS", info.maCS);

                command.ExecuteNonQuery();
            }
        }

        private void xoaKhoa(ThongTinKhoa info)
        {
            string sqlQuery = "DELETE FROM KHOA WHERE MAKH = @maKH;";
            using (SqlCommand command = new SqlCommand(sqlQuery, Program.conn))
            {
                command.Parameters.AddWithValue("@maKH", info.maKH);

                command.ExecuteNonQuery();
            }
        }

        private void chinhSuaKhoa(ThongTinKhoa info)
        {
            string sqlQuery = "UPDATE KHOA SET TENKH = @tenKH, MACS = @maCS WHERE MAKH = @maKH;";
            using (SqlCommand command = new SqlCommand(sqlQuery, Program.conn))
            {
                command.Parameters.AddWithValue("@maKH", info.maKH);
                command.Parameters.AddWithValue("@tenKH", info.tenKH);
                command.Parameters.AddWithValue("@maCS", info.maCS);

                command.ExecuteNonQuery();
            }
        }

        ThongTinKhoa GetKhoaInfoFromDatabase(string maKH)
        {
            using (SqlCommand command = new SqlCommand("SELECT MAKH, TENKH, MACS FROM KHOA WHERE MAKH = @maKH", Program.conn))
            {
                command.Parameters.AddWithValue("@maKH", maKH);
                try
                {
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        string maKHDb = reader["MAKH"].ToString();
                        string tenKH = reader["TENKH"].ToString();
                        string maCS = reader["MACS"].ToString();

                        reader.Close();

                        ThongTinKhoa info = new ThongTinKhoa(maKHDb, tenKH, maCS, "chinhsua");
                        return info;
                    }

                    reader.Close();
                    return null; // Trả về null nếu không tìm thấy dữ liệu
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: get data from database " + ex.Message);
                    return null;
                }
            }
        }



        private void themLop(ThongTinLop info)
        {
            string sqlQuery = "INSERT INTO LOP (MALOP, TENLOP, MAKH) VALUES (@maLop, @tenLop, @maKH);";
            using (SqlCommand command = new SqlCommand(sqlQuery, Program.conn))
            {
                command.Parameters.AddWithValue("@maLop", info.maLOP);
                command.Parameters.AddWithValue("@tenLop", info.tenLOP);
                command.Parameters.AddWithValue("@maKH", info.maKH);

                command.ExecuteNonQuery();
            }
        }

        private void xoaLop(ThongTinLop info)
        {
            string sqlQuery = "DELETE FROM LOP WHERE MALOP = @maLop;";
            using (SqlCommand command = new SqlCommand(sqlQuery, Program.conn))
            {
                command.Parameters.AddWithValue("@maLop", info.maLOP);

                command.ExecuteNonQuery();
            }
        }

        private void chinhSuaLop(ThongTinLop info)
        {
            string sqlQuery = "UPDATE LOP SET TENLOP = @tenLop, MAKH = @maKH WHERE MALOP = @maLop;";
            using (SqlCommand command = new SqlCommand(sqlQuery, Program.conn))
            {
                command.Parameters.AddWithValue("@maLop", info.maLOP);
                command.Parameters.AddWithValue("@tenLop", info.tenLOP);
                command.Parameters.AddWithValue("@maKH", info.maKH);

                command.ExecuteNonQuery();
            }
        }

        ThongTinLop GetLopInfoFromDatabase(string maLop)
        {
            using (SqlCommand command = new SqlCommand("SELECT MALOP, TENLOP, MAKH FROM LOP WHERE MALOP = @maLop", Program.conn))
            {
                command.Parameters.AddWithValue("@maLop", maLop);
                try
                {
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        string maLopDb = reader["MALOP"].ToString();
                        string tenLop = reader["TENLOP"].ToString();
                        string maKH = reader["MAKH"].ToString();

                        reader.Close();

                        ThongTinLop info = new ThongTinLop(maLopDb, tenLop, maKH, "chinhsua");
                        return info;
                    }

                    reader.Close();
                    return null; // Trả về null nếu không tìm thấy dữ liệu
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: get data from database " + ex.Message);
                    return null;
                }
            }
        }




        //CHECK SP KHOA-LOP
        private bool CheckSP_Lop_TonTaiKhoa(string makhoa)
        {
            System.Console.WriteLine(makhoa);
            using (SqlCommand command = new SqlCommand("Sp_KiemTraLop_TonTaiKHOA", Program.conn))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@MAKH", makhoa);


                // trả về 1: có khoa tồn tại -> có thể thêm
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    if (reader.GetString(0) == "1")
                    {
                        reader.Close();
                        return true;
                    }
                }
                reader.Close();
                return false;
            }
        }

        private bool CheckSP_KhoaTonTai(string makhoa, string tenkhoa)
        {
            System.Console.WriteLine(makhoa);
            using (SqlCommand command = new SqlCommand("SP_CheckKhoaExist", Program.conn))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@MAKH", makhoa);
                command.Parameters.AddWithValue("@TENKH", tenkhoa);


                // trả về 1: đã tồn tại, trả về 0: có thể thêm
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    if (reader.GetString(0) == "0")
                    {
                        reader.Close();
                        return true;
                    }
                }
                reader.Close();
                return false;
            }
        }

        private bool CheckSP_LopTonTai(string malop, string tenlop)
        {
            System.Console.WriteLine(malop);
            using (SqlCommand command = new SqlCommand("sp_KiemTraLopTonTai", Program.conn))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@MALOP", malop);
                command.Parameters.AddWithValue("@TENLOP", tenlop);


                // trả về 1: đã tồn tại, trả về 0: có thể thêm
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    if (reader.GetString(0) == "0")
                    {
                        reader.Close();
                        return true;
                    }
                }
                reader.Close();
                return false;
            }
        }

        
    }
}
