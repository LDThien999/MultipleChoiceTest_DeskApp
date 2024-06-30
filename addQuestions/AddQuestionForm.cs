using Microsoft.ReportingServices.Diagnostics.Internal;
using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using TNCSDLPT;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace CSDLPT.addQuestion
{
    public partial class frmAddQues : Form
    {
        private string maGV = Program.userName;
        private SqlCommand sqlCommand;
        private SqlDataAdapter adapter = new SqlDataAdapter();
        private DataTable table = new DataTable();

        class ThongTinDangKy
        {
            public string cauhoi { get; set; }
            public string TrinhDo { get; set; }
            public string DapAn { get; set; }
            public string MaMH { get; set; }
            public string NoiDung { get; set; }
            public string A { get; set; }
            public string B { get; set; }
            public string C { get; set; }
            public string D { get; set; }
            public string TrangThai { get; set; }

            public ThongTinDangKy(string cauhoi, string trinhDo, string dapAn, string maMH, string noidung, string a, string b, string c, string d, string trangThai)
            {
                this.cauhoi = cauhoi;
                TrinhDo = trinhDo;
                DapAn = dapAn;
                MaMH = maMH;
                NoiDung = noidung;
                A = a;
                B = b;
                C = c;
                D = d;
                TrangThai = trangThai;
            }
        }


        // Tạo hai stack undo và redo
        Stack<ThongTinDangKy> undoStack = new Stack<ThongTinDangKy>();
        Stack<ThongTinDangKy> redoStack = new Stack<ThongTinDangKy>();

        //Dictionary ho tro undo
        Dictionary<string, ThongTinDangKy> myDictionary = new Dictionary<string, ThongTinDangKy>();



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

        public frmAddQues()
        {
            InitializeComponent();
            this.FormClosing += AddQuestionForm_FormClosing;
        }

        private void AddQuestionForm_FormClosing(object sender, FormClosingEventArgs e)
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
                    myDictionary.Clear();

                    frmMain main = new frmMain();
                    main.Show();
                }
            }
        }

        private void AddQuestionForm_Load(object sender, EventArgs e)
        {
            if (Program.role != "GIANGVIEN")
            {
                panel1.Enabled = false;
            }

            LoadDataIntoDataGridView("BODE", gridViewCauHoi, maGV);
            BindingText(gridViewCauHoi);
            LoadDataIntoComboBox("BODE", "TRINHDO", cbbTrinhDo);
            LoadDataIntoComboBox("MONHOC", "MAMH", cbbMaMH);
            LoadDataIntoComboBox("BODE", "DAP_AN", cbbDapAn);
            checkUnRedo();
            myDictionary.Clear();


            string statement = "select MAKH from GIAOVIEN";
            Program.myReader = Program.ExecSqlDataReader(statement);
            if (Program.myReader == null)
                return;
            Program.myReader.Read();
            lblCoSo.Text = Program.brand2;
            lblMa.Text = maGV;
            lblName.Text = Program.staff;
            lblKhoa.Text = Program.myReader.GetString(0);
            Program.myReader.Close();

            btnAdd.Enabled = true;
            btnSaveAdd.Visible = false;

            btnXoa.Enabled = false;
            btnThoatAdd.Visible = false;

            btnChinhSua.Enabled = false;
            btnSaveChange.Visible = false;

            btnKhoiPhuc.Enabled = false;
            btnRedo.Enabled = false;

            cbbTrinhDo.Enabled = false;
            cbbTrinhDo.SelectedItem = null;

            cbbDapAn.Enabled = false;
            cbbDapAn.SelectedItem = null;

            cbbMaMH.Enabled = false;
            cbbMaMH.SelectedItem = null;

            txtNoiDung.ReadOnly = true;
            txtA.ReadOnly = true;
            txtB.ReadOnly = true;
            txtC.ReadOnly = true;
            txtD.ReadOnly = true;
            BindingText(gridViewCauHoi);

        }

        //CUSTOM TETXBOX
        public void EnableTextBox(System.Windows.Forms.TextBox textBox)
        {
            textBox.ReadOnly = true;
            resetTextbox(textBox);
        }

        public void resetTextbox(System.Windows.Forms.TextBox textBox)
        {
            textBox.ResetText();
        }


        private bool CheckTextBox(string input)
        {
            string pattern = @"^\s*$";

            if (Regex.IsMatch(input, pattern))
            {
                // Nếu chuỗi chỉ gồm khoảng trắng và xuống dòng, trả về false (báo lỗi)
                return false;
            }
            else
            {
                return true;
            }
        }

        //COMBO BOX
        private void LoadDataIntoComboBox(String selectedTable, string selectRow, System.Windows.Forms.ComboBox comboBox)
        {
            comboBox.Items.Clear();
            sqlCommand = Program.conn.CreateCommand();
            sqlCommand.CommandText = "SELECT DISTINCT " + selectRow + " FROM " + selectedTable; ;
            adapter.SelectCommand = sqlCommand;
            DataTable table = new DataTable();
            try
            {
                adapter.Fill(table);

                foreach (DataRow row in table.Rows)
                {
                    comboBox.Items.Add(row[selectRow].ToString());
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        ///-----------GRIDVIEW----------------/
        public void LoadDataIntoDataGridView(String selectedTable, DataGridView gridview, String maGV)
        {
            gridview.DataSource = null;
            gridview.Rows.Clear();
            gridview.Columns.Clear();

            sqlCommand = Program.conn.CreateCommand();
            if (Program.role != "GIANGVIEN")
            {
                sqlCommand.CommandText = "SELECT * FROM " + selectedTable;
            }
            else
            {
                sqlCommand.CommandText = "SELECT * FROM " + selectedTable + " WHERE MAGV = @maGV";
            }
            sqlCommand.Parameters.AddWithValue("@maGV", maGV);
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

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void BindingText(DataGridView gridview)
        {
            txtNoiDung.DataBindings.Clear();//clearBinding
            cbbMaMH.DataBindings.Clear();
            cbbTrinhDo.DataBindings.Clear();
            cbbDapAn.DataBindings.Clear();
            txtA.DataBindings.Clear();
            txtB.DataBindings.Clear();
            txtC.DataBindings.Clear();
            txtD.DataBindings.Clear();

            txtNoiDung.DataBindings.Add("Text", gridview.DataSource, "NOIDUNG"); //Binding textBox
            cbbMaMH.DataBindings.Add("Text", gridview.DataSource, "MAMH");
            cbbTrinhDo.DataBindings.Add("Text", gridview.DataSource, "TRINHDO");
            cbbDapAn.DataBindings.Add("Text", gridview.DataSource, "DAP_AN");
            txtA.DataBindings.Add("Text", gridview.DataSource, "A");
            txtB.DataBindings.Add("Text", gridview.DataSource, "B");
            txtC.DataBindings.Add("Text", gridview.DataSource, "C");
            txtD.DataBindings.Add("Text", gridview.DataSource, "D");
        }

        private void DataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            btnChinhSua.Enabled = true;
            btnXoa.Enabled = true;
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                gridViewCauHoi.Rows[e.RowIndex].Selected = true;
            }
        }


        ///BUTTON ADD
        private void btnAdd_Click(object sender, EventArgs e)
        {
            btnAdd.Enabled = false;
            btnSaveAdd.Visible = true;
            btnSaveAdd.Enabled = true;
            btnSaveChange.Visible = true;
            btnSaveChange.Enabled = false;
            btnChinhSua.Visible = false;
            btnThoatAdd.Visible = true;
            btnThoatAdd.Enabled = true;
            btnXoa.Enabled = false;
            btnXoa.Visible = false;

            btnKhoiPhuc.Enabled = false;
            btnRedo.Enabled = false;

            cbbMaMH.Enabled = true;
            cbbTrinhDo.Enabled = true;
            cbbDapAn.Enabled = true;

            txtNoiDung.ReadOnly = false;
            txtA.ReadOnly = false;
            txtB.ReadOnly = false;
            txtC.ReadOnly = false;
            txtD.ReadOnly = false;

            cbbTrinhDo.SelectedItem = null;
            cbbDapAn.SelectedItem = null;
            cbbMaMH.SelectedItem = null;

            resetTextbox(txtNoiDung);
            resetTextbox(txtA);
            resetTextbox(txtB);
            resetTextbox(txtC);
            resetTextbox(txtD);

            gridViewCauHoi.Enabled = false;
        }

        private void btnSaveAdd_Click(object sender, EventArgs e)
        {
            string noidung = txtNoiDung.Text;
            string a = txtA.Text;
            string b = txtB.Text;
            string c = txtC.Text;
            string d = txtD.Text;

            if (CheckTextBox(noidung) && CheckTextBox(a) && CheckTextBox(b)
                && CheckTextBox(c) && CheckTextBox(d)
                && cbbDapAn.SelectedIndex >= 0 && cbbMaMH.SelectedIndex >= 0
                && cbbTrinhDo.SelectedIndex >= 0)
            {
                string maMH = cbbMaMH.SelectedItem.ToString();
                string trinhdo = cbbTrinhDo.SelectedItem.ToString();
                string dapan = cbbDapAn.SelectedItem.ToString();

                string sqlQuery = "INSERT INTO BODE (MAMH, TRINHDO, NOIDUNG, A, B, C, D, DAP_AN, MAGV) " +
                              "VALUES (@ip1, @ip2, @ip3, @ip4, @ip5, @ip6, @ip7, @ip8, @ip9); " +
                              "SELECT SCOPE_IDENTITY();";

                using (SqlCommand command = new SqlCommand(sqlQuery, Program.conn))
                {
                    command.Parameters.AddWithValue("@ip1", maMH);
                    command.Parameters.AddWithValue("@ip2", trinhdo);
                    command.Parameters.AddWithValue("@ip3", noidung);
                    command.Parameters.AddWithValue("@ip4", a);
                    command.Parameters.AddWithValue("@ip5", b);
                    command.Parameters.AddWithValue("@ip6", c);
                    command.Parameters.AddWithValue("@ip7", d);
                    command.Parameters.AddWithValue("@ip8", dapan);
                    command.Parameters.AddWithValue("@ip9", maGV);


                    object res = command.ExecuteScalar();
                    if (res != null)
                    {
                        MessageBox.Show("Thêm dữ liệu thành công!");
                        int cauhoi = Convert.ToInt32(res);

                        ThongTinDangKy info = new ThongTinDangKy(cauhoi.ToString(), cbbTrinhDo.Text,
                                cbbDapAn.Text, cbbMaMH.Text, txtNoiDung.Text, txtA.Text, txtB.Text,
                                txtC.Text, txtD.Text, "them");

                        undoStack.Push(info);
                    }
                    else
                    {
                        MessageBox.Show("Không thể thêm dữ liệu!");
                    }
                }
                btnAdd.Visible = true;
                btnAdd.Enabled = true;
                btnSaveAdd.Visible = false;
                btnChinhSua.Enabled = false;
                btnChinhSua.Visible = true;
                btnSaveChange.Visible = false;
                btnThoatAdd.Visible = false;
                btnXoa.Enabled = false;
                btnXoa.Visible = true;


                cbbMaMH.Enabled = false;
                cbbTrinhDo.Enabled = false;
                cbbDapAn.Enabled = false;

                txtNoiDung.ReadOnly = false;
                txtA.ReadOnly = false;
                txtB.ReadOnly = false;
                txtC.ReadOnly = false;
                txtD.ReadOnly = false;

                cbbTrinhDo.SelectedItem = null;
                cbbDapAn.SelectedItem = null;
                cbbMaMH.SelectedItem = null;

                resetTextbox(txtNoiDung);
                resetTextbox(txtA);
                resetTextbox(txtB);
                resetTextbox(txtC);
                resetTextbox(txtD);

                gridViewCauHoi.Enabled = true;

                LoadDataIntoDataGridView("BODE", gridViewCauHoi, maGV);
                BindingText(gridViewCauHoi);
                LoadDataIntoComboBox("BODE", "TRINHDO", cbbTrinhDo);
                LoadDataIntoComboBox("MONHOC", "MAMH", cbbMaMH);
                LoadDataIntoComboBox("BODE", "DAP_AN", cbbDapAn);
                checkUnRedo();
            }
            else
            {
                MessageBox.Show("Lỗi: Dữ liệu không hợp lệ!");
            }
        }


        // BUTTON CHINH SUA
        private void btnChinhSua_Click(object sender, EventArgs e)
        {
            DataGridViewRow selectedRow = gridViewCauHoi.SelectedRows[0];
            int cauhoi = Convert.ToInt32(selectedRow.Cells[0].Value); ;
            if (CheckSP_CauHoiTonTaiBangKhac(cauhoi) == true)
            {
                btnChinhSua.Visible = false;
                btnSaveChange.Visible = true;
                btnSaveChange.Enabled = true;
                btnAdd.Enabled = false;
                btnSaveAdd.Visible = false;
                btnXoa.Visible = false;
                btnThoatAdd.Visible = true;
                btnThoatAdd.Enabled = true;

                btnKhoiPhuc.Enabled = false;
                btnRedo.Enabled = false;

                cbbTrinhDo.Enabled = true;
                cbbDapAn.Enabled = true;
                cbbMaMH.Enabled = true;

                txtNoiDung.ReadOnly = false;
                txtA.ReadOnly = false;
                txtB.ReadOnly = false;
                txtC.ReadOnly = false;
                txtD.ReadOnly = false;
            }
            else
            {
                MessageBox.Show("Không thể chỉnh sửa, câu hỏi đã đang sử dụng!");
            }
        }

        private void btnSaveChange_Click(object sender, EventArgs e)
        {
            string noidung = txtNoiDung.Text;
            string a = txtA.Text;
            string b = txtB.Text;
            string c = txtC.Text;
            string d = txtD.Text;

            if (CheckTextBox(noidung) && CheckTextBox(a) && CheckTextBox(b)
                && CheckTextBox(c) && CheckTextBox(d)
                && cbbDapAn.SelectedIndex >= 0 && cbbMaMH.SelectedIndex >= 0
                && cbbTrinhDo.SelectedIndex >= 0)
            {
                string maMH = cbbMaMH.SelectedItem.ToString();
                string trinhdo = cbbTrinhDo.SelectedItem.ToString();
                string dapan = cbbDapAn.SelectedItem.ToString();

                DataGridViewRow selectedRow = gridViewCauHoi.SelectedRows[0];
                int cauhoi = Convert.ToInt32(selectedRow.Cells[0].Value);

                // lay data tu database truoc khi thay doi
                ThongTinDangKy infoBeforeUpdate = GetInfoFromDatabase(cauhoi);

                string sqlQuery = "UPDATE BODE SET MAMH = @ip1, TRINHDO = @ip2, NOIDUNG = @ip3, A = @ip4," +
                    " B = @ip5, C = @ip6, D = @ip7, DAP_AN = @ip8 WHERE cauhoi = @cauhoi";

                using (SqlCommand command = new SqlCommand(sqlQuery, Program.conn))
                {
                    command.Parameters.AddWithValue("@ip1", maMH);
                    command.Parameters.AddWithValue("@ip2", trinhdo);
                    command.Parameters.AddWithValue("@ip3", noidung);
                    command.Parameters.AddWithValue("@ip4", a);
                    command.Parameters.AddWithValue("@ip5", b);
                    command.Parameters.AddWithValue("@ip6", c);
                    command.Parameters.AddWithValue("@ip7", d);
                    command.Parameters.AddWithValue("@ip8", dapan);
                    command.Parameters.AddWithValue("@cauhoi", cauhoi);


                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {

                        MessageBox.Show("Chỉnh sửa dữ liệu thành công!");

                        undoStack.Push(infoBeforeUpdate);

                    }
                    else
                    {
                        MessageBox.Show("Không thể chỉnh sửa dữ liệu!");
                    }
                }
                btnAdd.Enabled = true;
                btnChinhSua.Enabled = false;
                btnSaveChange.Visible = false;
                btnChinhSua.Visible = true;
                btnThoatAdd.Visible = false;
                btnXoa.Enabled = false;
                btnXoa.Visible = true;


                cbbMaMH.Enabled = false;
                cbbTrinhDo.Enabled = false;
                cbbDapAn.Enabled = false;

                txtNoiDung.ReadOnly = true;
                txtA.ReadOnly = true;
                txtB.ReadOnly = true;
                txtC.ReadOnly = true;
                txtD.ReadOnly = true;

                cbbTrinhDo.SelectedItem = null;
                cbbDapAn.SelectedItem = null;
                cbbMaMH.SelectedItem = null;

                resetTextbox(txtNoiDung);
                resetTextbox(txtA);
                resetTextbox(txtB);
                resetTextbox(txtC);
                resetTextbox(txtD);

                gridViewCauHoi.Enabled = true;

                LoadDataIntoDataGridView("BODE", gridViewCauHoi, maGV);
                BindingText(gridViewCauHoi);
                LoadDataIntoComboBox("BODE", "TRINHDO", cbbTrinhDo);
                LoadDataIntoComboBox("MONHOC", "MAMH", cbbMaMH);
                LoadDataIntoComboBox("BODE", "DAP_AN", cbbDapAn);
                checkUnRedo();
            }
            else
            {
                MessageBox.Show("Lỗi: Dữ liệu không hợp lệ!");
            }
        }

        ThongTinDangKy GetInfoFromDatabase(int cauhoi)
        {
            using (SqlCommand command = new SqlCommand("SELECT MAMH, TRINHDO, NOIDUNG," +
                " A, B, C, D, DAP_AN FROM BODE WHERE CAUHOI = @id", Program.conn))
            {
                command.Parameters.AddWithValue("@id", cauhoi);
                try
                {
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        string maMH = reader["MAMH"].ToString();
                        string trinhDo = reader["TRINHDO"].ToString();
                        string noiDung = reader["NOIDUNG"].ToString();
                        string a = reader["A"].ToString();
                        string b = reader["B"].ToString();
                        string c = reader["C"].ToString();
                        string d = reader["D"].ToString();
                        string dapAn = reader["DAP_AN"].ToString();

                        reader.Close();

                        ThongTinDangKy info = new ThongTinDangKy(cauhoi.ToString(), trinhDo, dapAn, maMH, noiDung, a, b, c, d, "chinhsua");
                        return info;
                    }

                    reader.Close();
                    return null; // Trả về null nếu không tìm thấy dữ liệu
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error:get data from database " + ex.Message);
                    return null;
                }
            }
        }
        //BUTTON XOA
        private void btnXoa_Click(object sender, EventArgs e)
        {

            DataGridViewRow selectedRow = gridViewCauHoi.SelectedRows[0];
            int cauhoi = Convert.ToInt32(selectedRow.Cells[0].Value); ;
            if (CheckSP_CauHoiTonTaiBangKhac(cauhoi) == true)
            {
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa không?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    DeleteData("BODE", "CAUHOI", cauhoi);
                }

            }
            else
            {
                MessageBox.Show("Không thể xóa, câu hỏi đã đang sử dụng!");
            }

        }

        public bool DeleteData(String selectedTable, String idColumn, int idValue)
        {

            ThongTinDangKy info = new ThongTinDangKy(idValue.ToString(), cbbTrinhDo.Text,
                        cbbDapAn.Text, cbbMaMH.Text, txtNoiDung.Text, txtA.Text, txtB.Text,
                        txtC.Text, txtD.Text, "xoa");

            using (SqlCommand command = new SqlCommand("DELETE FROM " + selectedTable + " WHERE " + idColumn + " = @id", Program.conn))
            {
                command.Parameters.AddWithValue("@id", idValue);
                try
                {
                    command.ExecuteNonQuery();
                    MessageBox.Show("Dữ liệu đã được xóa!");

                    undoStack.Push(info);

                    btnChinhSua.Enabled = false;
                    btnXoa.Enabled = false;
                    cbbTrinhDo.SelectedItem = null;
                    cbbDapAn.SelectedItem = null;
                    cbbMaMH.SelectedItem = null;

                    resetTextbox(txtNoiDung);
                    resetTextbox(txtA);
                    resetTextbox(txtB);
                    resetTextbox(txtC);
                    resetTextbox(txtD);

                    gridViewCauHoi.Enabled = true;

                    LoadDataIntoDataGridView("BODE", gridViewCauHoi, maGV);
                    BindingText(gridViewCauHoi);
                    LoadDataIntoComboBox("BODE", "TRINHDO", cbbTrinhDo);
                    LoadDataIntoComboBox("MONHOC", "MAMH", cbbMaMH);
                    LoadDataIntoComboBox("BODE", "DAP_AN", cbbDapAn);
                    checkUnRedo();
                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                    return false;
                }
            }
        }

        //BUTTON THOAT
        private void btnThoatAdd_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn thoát?", "Xác nhận thoát", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                btnThoatAdd.Visible = false;
                btnXoa.Visible = true;
                btnXoa.Enabled = false;
                btnAdd.Visible = true;
                btnAdd.Enabled = true;
                btnSaveAdd.Visible = false;
                btnChinhSua.Visible = true;
                btnChinhSua.Enabled = false;
                btnSaveChange.Visible = false;

                cbbTrinhDo.SelectedItem = null;
                cbbTrinhDo.Enabled = false;
                cbbDapAn.SelectedItem = null;
                cbbDapAn.Enabled = false;
                cbbMaMH.SelectedItem = null;
                cbbMaMH.Enabled = false;

                txtNoiDung.ReadOnly = true;
                txtA.ReadOnly = true;
                txtB.ReadOnly = true;
                txtC.ReadOnly = true;
                txtD.ReadOnly = true;

                gridViewCauHoi.Enabled = true;

                LoadDataIntoDataGridView("BODE", gridViewCauHoi, maGV);
                BindingText(gridViewCauHoi);
                LoadDataIntoComboBox("BODE", "TRINHDO", cbbTrinhDo);
                LoadDataIntoComboBox("MONHOC", "MAMH", cbbMaMH);
                LoadDataIntoComboBox("BODE", "DAP_AN", cbbDapAn);
                checkUnRedo();
            }
        }

        //--BUTTON UNDO------//
        private void btnKhoiPhuc_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn Undo?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {

                if (undoStack.Count > 0)
                {
                    ThongTinDangKy temp = undoStack.Pop();
                    if (temp.TrangThai == "xoa")
                    {
                        themDangKy(temp);
                        temp.TrangThai = "them";
                        redoStack.Push(temp);
                    }
                    else if (temp.TrangThai == "them")
                    {
                        xoaDangKy(temp);
                        temp.TrangThai = "xoa";
                        redoStack.Push(temp);
                    }
                    else if (temp.TrangThai == "chinhsua")
                    {
                        redoStack.Push(GetInfoFromDatabase(int.Parse(temp.cauhoi)));
                        chinhSuaDangKy(temp);
                    }
                }
                LoadDataIntoDataGridView("BODE", gridViewCauHoi, maGV);
                BindingText(gridViewCauHoi);
                LoadDataIntoComboBox("BODE", "TRINHDO", cbbTrinhDo);
                LoadDataIntoComboBox("MONHOC", "MAMH", cbbMaMH);
                LoadDataIntoComboBox("BODE", "DAP_AN", cbbDapAn);
                checkUnRedo();
            }
        }


        //--BUTTON REDO----//
        private void btnRedo_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn Redo?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                if (redoStack.Count > 0)
                {
                    ThongTinDangKy temp = redoStack.Pop();

                    if (temp.TrangThai == "xoa")
                    {
                        themDangKy(temp);
                        temp.TrangThai = "them";
                        undoStack.Push(temp);
                    }
                    else if (temp.TrangThai == "them")
                    {
                        xoaDangKy(temp);
                        temp.TrangThai = "xoa";
                        undoStack.Push(temp);
                    }
                    else if (temp.TrangThai == "chinhsua")
                    {
                        undoStack.Push(GetInfoFromDatabase(int.Parse(temp.cauhoi)));
                        chinhSuaDangKy(temp);
                    }
                }
                LoadDataIntoDataGridView("BODE", gridViewCauHoi, maGV);
                BindingText(gridViewCauHoi);
                LoadDataIntoComboBox("BODE", "TRINHDO", cbbTrinhDo);
                LoadDataIntoComboBox("MONHOC", "MAMH", cbbMaMH);
                LoadDataIntoComboBox("BODE", "DAP_AN", cbbDapAn);
                checkUnRedo();
            }
        }

        private void themDangKy(ThongTinDangKy info)
        {
            string sqlQuery = "INSERT INTO BODE (MAMH, TRINHDO, NOIDUNG, A, B, C, D, DAP_AN, MAGV) " +
                              "VALUES (@ip1, @ip2, @ip3, @ip4, @ip5, @ip6, @ip7, @ip8, @ip9); " +
                              "SELECT SCOPE_IDENTITY();";
            // LAY RA GIA TRI ID CUOI CUNG INSERT
            using (SqlCommand command = new SqlCommand(sqlQuery, Program.conn))
            {
                command.Parameters.AddWithValue("@ip1", info.MaMH);
                command.Parameters.AddWithValue("@ip2", info.TrinhDo);
                command.Parameters.AddWithValue("@ip3", info.NoiDung);
                command.Parameters.AddWithValue("@ip4", info.A);
                command.Parameters.AddWithValue("@ip5", info.B);
                command.Parameters.AddWithValue("@ip6", info.C);
                command.Parameters.AddWithValue("@ip7", info.D);
                command.Parameters.AddWithValue("@ip8", info.DapAn);
                command.Parameters.AddWithValue("@ip9", maGV);

                // Execute lay gia tri id cuoi cung
                string cauhoi = command.ExecuteScalar().ToString();

                ThongTinDangKy thongTinDangKy = new ThongTinDangKy(info.cauhoi,
                    info.TrinhDo, info.DapAn, info.MaMH, info.NoiDung,
                    info.A, info.B, info.C, info.D, "chinhsua") ;

                myDictionary.Add(info.cauhoi, thongTinDangKy);

                string temp = "";
                foreach (var dict in myDictionary)
                {
                    if (dict.Key == info.cauhoi)
                    {
                        temp = dict.Key;
                    }
                }
                myDictionary[temp].cauhoi = cauhoi;
            }
        }


        private void xoaDangKy(ThongTinDangKy info)
        {
            string sqlQuery = "DELETE FROM BODE WHERE CAUHOI = @ip1";
            using (SqlCommand command = new SqlCommand(sqlQuery, Program.conn))
            {
                command.Parameters.AddWithValue("@ip1", int.Parse(info.cauhoi));
                command.ExecuteNonQuery();

                //Dictionary<ThongTinDangKy,int > result = new Dictionary<ThongTinDangKy, int>();

                //// Duyệt qua dictionary
                //foreach (var kvp in myDictionary)
                //{
                //    if (!result.ContainsKey(kvp.Value))
                //    {
                //        result.Add(kvp.Value, kvp.Key);
                //    }
                //    else  //contain key
                //    {
                //        if (kvp.Key < result[kvp.Value])
                //        {
                //            result[kvp.Value] = kvp.Key;
                //        }
                //    }
                //}
                //ThongTinDangKy thongTinDangKy = new ThongTinDangKy(info.cauhoi,
                //    info.TrinhDo, info.DapAn, info.MaMH, info.NoiDung,
                //    info.A, info.B, info.C, info.D, "chinhsua");
                //myDictionary.Add(info.cauhoi, thongTinDangKy);
            }
        }

        private void chinhSuaDangKy(ThongTinDangKy info)
        {

            string sqlQuery = "UPDATE BODE SET MAMH = @ip1, TRINHDO = @ip2, NOIDUNG = @ip3, A = @ip4, B = @ip5, C = @ip6, D = @ip7, DAP_AN = @ip8 WHERE CAUHOI = @ip9";
            using (SqlCommand command = new SqlCommand(sqlQuery, Program.conn))
            {
                command.Parameters.AddWithValue("@ip1", info.MaMH);
                command.Parameters.AddWithValue("@ip2", info.TrinhDo);
                command.Parameters.AddWithValue("@ip3", info.NoiDung);
                command.Parameters.AddWithValue("@ip4", info.A);
                command.Parameters.AddWithValue("@ip5", info.B);
                command.Parameters.AddWithValue("@ip6", info.C);
                command.Parameters.AddWithValue("@ip7", info.D);
                command.Parameters.AddWithValue("@ip8", info.DapAn);
                command.Parameters.AddWithValue("@ip9", int.Parse(info.cauhoi));

                command.ExecuteNonQuery();

                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected == 0)
                {
                string temp = "";
                    foreach (var dict in myDictionary)
                    {
                        if (dict.Key == info.cauhoi)
                        {
                            MessageBox.Show("ok");
                            chinhSuaDangKyWithDict(info, dict.Value.cauhoi);
                            temp = dict.Key;
                        }
                    }
                    myDictionary.Remove(temp);
                }
            }

        }

        private void chinhSuaDangKyWithDict(ThongTinDangKy info, string dictionaryKey)
        {
            string sqlQuery = "UPDATE BODE SET MAMH = @ip1, TRINHDO = @ip2, NOIDUNG = @ip3, A = @ip4, B = @ip5, C = @ip6, D = @ip7, DAP_AN = @ip8 WHERE CAUHOI = @ip9";
            using (SqlCommand command = new SqlCommand(sqlQuery, Program.conn))
            {
                command.Parameters.AddWithValue("@ip1", info.MaMH);
                command.Parameters.AddWithValue("@ip2", info.TrinhDo);
                command.Parameters.AddWithValue("@ip3", info.NoiDung);
                command.Parameters.AddWithValue("@ip4", info.A);
                command.Parameters.AddWithValue("@ip5", info.B);
                command.Parameters.AddWithValue("@ip6", info.C);
                command.Parameters.AddWithValue("@ip7", info.D);
                command.Parameters.AddWithValue("@ip8", info.DapAn);
                command.Parameters.AddWithValue("@ip9", dictionaryKey);

                command.ExecuteNonQuery();
            }

        }


        /////------------------------------------------SP--------------------------------------------////////

        private bool CheckSP_CauHoiTonTaiBangKhac(int cauhoi)
        {
            using (SqlCommand command = new SqlCommand("SP_CheckBoDe_ExistOnOthersTables", Program.conn))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@cauhoi", cauhoi);

                // trả về 1: đã tồn tại -> false
                // trả về 0: chưa tồn tại -> true
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

        private void gridViewCauHoi_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }


    }


}


