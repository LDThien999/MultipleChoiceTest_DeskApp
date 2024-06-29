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

            textBox1.ReadOnly = true;
            textBoxTenMon.ReadOnly = true;

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

        //Text - Binding
        private void BindingTextMONHOC(DataGridView gridview)
        {
            textBox1.DataBindings.Clear();//clearBinding
            textBoxTenMon.DataBindings.Clear();
            textBox1.DataBindings.Add("Text", gridview.DataSource, "MAMH"); //Binding textBox
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
            string pattern = @"^[a-zA-Z0-9\sđĐảẢấẤầẦậẬẩẨắẮằẰặẶẫẪéÉèÈẻẺẽẼêÊếẾềỀệỆểỂọỌốỐồỒộỘổỔơƠớỚờỜợỢởỞịỊủỦũŨúÚưỨỨựỰửỬừỪýÝỳỲỷỶỹỸ\s]+$";
            if (Regex.IsMatch(input, pattern))
            {
                return true;
            }
            else
            {
                return false;
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


        private void tabPage1_Click(object sender, EventArgs e)
        {

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

            string trimmedTextBox1 = Regex.Replace(textBox1.Text, @"\s+", "");
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
            textBox1.Clear();
            textBoxTenMon.Clear();
            textBox1.ReadOnly = false;
            textBoxTenMon.ReadOnly = false;
        }

        private void btnSave_Add_Click(object sender, EventArgs e)
        {
            string mamonhoc = textBox1.Text;
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
                textBox1.ReadOnly = true;
                textBoxTenMon.ReadOnly = true;
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

        // BUTTON CHINH SUA
        private void btnChinhSua_Click(object sender, EventArgs e)
        {
            textBoxTenMon.ReadOnly = false;
            btnChinhSua.Visible = false;
            btnSave.Visible = true;
            btnXoa.Enabled = false;
            btnThemMon.Enabled = false;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            string mamonhoc = textBox1.Text;
            string tenmonhoc = textBoxTenMon.Text;
            if (CheckRegexName(tenmonhoc) == true)
            {
                if (CheckSP_MonHoc("null", tenmonhoc))
                {
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
            }
            else
            {
                MessageBox.Show("Lỗi: Dữ liệu nhập không hợp lệ!");
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
            LoadDataIntoDataGridView("KHOA", dataGridViewKHOA);
            LoadDataIntoDataGridView("LOP", dataGridViewLOP);
            BindingTextKHOA(dataGridViewKHOA);
            BindingTextLOP(dataGridViewLOP);
            LoadDataIntoComboBoxMAKH_LOP();

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
                    ComboBoxKhoaLop.Enabled = true;
                    textBoxMaKhoa_Khoa.ReadOnly = true;
                    textBoxTenKhoa.ReadOnly = true;
                    textBoxMaLop.ReadOnly = true;
                    textBoxTenLop.ReadOnly = true;
                    dataGridViewKHOA.Enabled = true;
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
                        ComboBoxKhoaLop.Enabled = true;
                        textBoxMaKhoa_Khoa.ReadOnly = true;
                        textBoxTenKhoa.ReadOnly = true;
                        textBoxMaLop.ReadOnly = true;
                        textBoxTenLop.ReadOnly = true;
                        dataGridViewLOP.Enabled = true;
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
                string tenkhoa = textBoxTenKhoa.Text;
                if (CheckRegexName(textBoxTenKhoa.Text) == true)
                {
                    if (CheckSP_KhoaTonTai("null", tenkhoa))
                    {
                        string sqlQuery = "UPDATE KHOA SET TENKH = @InputData2 WHERE MAKH = @InputData1";
                        using (SqlCommand command = new SqlCommand(sqlQuery, Program.conn))
                        {
                            command.Parameters.AddWithValue("@InputData1", textBoxMaKhoa_Khoa.Text);
                            command.Parameters.AddWithValue("@InputData2", textBoxTenKhoa.Text);
                            int rowsAffected = command.ExecuteNonQuery();
                            try
                            {
                                if (rowsAffected > 0)
                                {
                                    MessageBox.Show("Chỉnh sửa dữ liệu thành công!");
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
                }
                else
                {
                    MessageBox.Show("Lỗi: Dữ liệu nhập không hợp lệ!");
                }
            }
            else
            {
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
                            string sqlQuery = "UPDATE LOP SET TENLOP = @InputData2, MAKH = @InputData3 WHERE MALOP = @InputData1";
                            using (SqlCommand command = new SqlCommand(sqlQuery, Program.conn))
                            {
                                command.Parameters.AddWithValue("@InputData1", textBoxMaLop.Text);
                                command.Parameters.AddWithValue("@InputData2", textBoxTenLop.Text);
                                command.Parameters.AddWithValue("@InputData3", makhoa_lop);
                                int rowsAffected = command.ExecuteNonQuery();
                                try
                                {
                                    if (rowsAffected > 0)
                                    {
                                        MessageBox.Show("Chỉnh sửa dữ liệu thành công!");
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

                }
                else

                    MessageBox.Show("Lỗi: Dữ liệu nhập không hợp lệ!");

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
