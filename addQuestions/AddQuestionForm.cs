﻿using Microsoft.ReportingServices.Diagnostics.Internal;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
                    frmMain main = new frmMain();
                    main.Show();
                }
            }
        }

        private void AddQuestionForm_Load(object sender, EventArgs e)
        {
            LoadDataIntoDataGridView("BODE", gridViewCauHoi, maGV);
            BindingText(gridViewCauHoi);
            LoadDataIntoComboBox("BODE", "TRINHDO", cbbTrinhDo);
            LoadDataIntoComboBox("MONHOC", "MAMH", cbbMaMH);
            LoadDataIntoComboBox("BODE", "DAP_AN", cbbDapAn);


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

            EnableTextBox(txtNoiDung);
            EnableTextBox(txtA);
            EnableTextBox(txtB);
            EnableTextBox(txtC);
            EnableTextBox(txtD);
            BindingText(gridViewCauHoi);

        }

        //CUSTOM TETXBOX
        public void EnableTextBox(System.Windows.Forms.TextBox textBox)
        {
            textBox.Enabled = false;
            resetTextbox(textBox);
        }

        public void resetTextbox(System.Windows.Forms.TextBox textBox)
        {
            textBox.ResetText();
        }


        private bool CheckTextBox(string input)
        {
            string pattern = @"^[a-zA-Z0-9\sàáạảãâầấậẩẫăằắặẳẵèéẹẻẽêềếệểễìíịỉĩòóọỏõô
                            ồốộổỗơờớợởỡùúụủũưừứựửữỳýỵỷỹÀÁẠẢÃÂẦẤẬẨẪĂẰẮẶẲẴÈÉẸẺẼÊỀẾỆỂỄ
                            ÌÍỊỈĨÒÓỌỎÕÔỒỐỘỔỖƠỜỚỢỞỠÙÚỤỦŨƯỪỨỰỬỮỲÝỴỶỸ]+$";
            if (Regex.IsMatch(input, pattern))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //COMBO BOX
        private void LoadDataIntoComboBox(String selectedTable, string selectRow, System.Windows.Forms.ComboBox comboBox)
        {
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
            sqlCommand.CommandText = "SELECT * FROM " + selectedTable + " WHERE MAGV = @maGV";
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



            txtNoiDung.Enabled = true;
            cbbMaMH.Enabled = true;
            cbbTrinhDo.Enabled = true;
            cbbDapAn.Enabled = true;
            txtA.Enabled = true;
            txtB.Enabled = true;
            txtC.Enabled = true;
            txtD.Enabled = true;

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
            string maMH = cbbMaMH.SelectedItem.ToString();
            string trinhdo = cbbTrinhDo.SelectedItem.ToString();
            string dapan = cbbDapAn.SelectedItem.ToString();
            string a = txtA.Text;
            string b = txtB.Text;
            string c = txtC.Text;
            string d = txtD.Text;

            if (CheckTextBox(noidung) && CheckTextBox(a) && CheckTextBox(b) && CheckTextBox(c) && CheckTextBox(d))
            {
                string sqlQuery = "INSERT INTO BODE (MAMH, TRINHDO, NOIDUNG, A, B, C, D, DAP_AN, MAGV) " +
                                "VALUES (@ip1, @ip2, @ip3, @ip4, @ip5, @ip6, @ip7, @ip8, @ip9)";

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
                btnAdd.Visible = true;
                btnAdd.Enabled = true;
                btnSaveAdd.Visible = false;
                btnChinhSua.Enabled = false;
                btnChinhSua.Visible = true;
                btnSaveChange.Visible = false;
                btnThoatAdd.Visible = false;
                btnXoa.Enabled = false;
                btnXoa.Visible = true;


                txtNoiDung.Enabled = false;
                cbbMaMH.Enabled = false;
                cbbTrinhDo.Enabled = false;
                cbbDapAn.Enabled = false;
                txtA.Enabled = false;
                txtB.Enabled = false;
                txtC.Enabled = false;
                txtD.Enabled = false;

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
                LoadDataIntoComboBox("BODE", "TRINHDO", cbbTrinhDo);
                LoadDataIntoComboBox("MONHOC", "MAMH", cbbMaMH);
                LoadDataIntoComboBox("BODE", "DAP_AN", cbbDapAn);

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
            int cauhoi = Convert.ToInt32(selectedRow.Cells[0].Value);;
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

                cbbTrinhDo.Enabled = true;
                cbbDapAn.Enabled = true;
                cbbMaMH.Enabled = true;

                txtNoiDung.Enabled = true;
                txtA.Enabled = true;
                txtB.Enabled = true;
                txtC.Enabled = true;
                txtD.Enabled = true;
            }
            else
            {
                MessageBox.Show("Không thể chỉnh sửa, đã tồn tại ràng buộc dữ liệu!");
            }
        }

        private void btnSaveChange_Click(object sender, EventArgs e)
        {
            string noidung = txtNoiDung.Text;
            string maMH = cbbMaMH.SelectedItem.ToString();
            string trinhdo = cbbTrinhDo.SelectedItem.ToString();
            string dapan = cbbDapAn.SelectedItem.ToString();
            string a = txtA.Text;
            string b = txtB.Text;
            string c = txtC.Text;
            string d = txtD.Text;

            DataGridViewRow selectedRow = gridViewCauHoi.SelectedRows[0];
            int cauhoi = Convert.ToInt32(selectedRow.Cells[0].Value);;

            if (CheckTextBox(noidung) && CheckTextBox(a) && CheckTextBox(b)
                && CheckTextBox(c) && CheckTextBox(d))
            {
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


                txtNoiDung.Enabled = false;
                cbbMaMH.Enabled = false;
                cbbTrinhDo.Enabled = false;
                cbbDapAn.Enabled = false;
                txtA.Enabled = false;
                txtB.Enabled = false;
                txtC.Enabled = false;
                txtD.Enabled = false;

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

            }
            else
            {
                MessageBox.Show("Lỗi: Dữ liệu không hợp lệ!");
            }
        }

        //BUTTON XOA
        private void btnXoa_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa không?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                DataGridViewRow selectedRow = gridViewCauHoi.SelectedRows[0];
                int cauhoi = Convert.ToInt32(selectedRow.Cells[0].Value);;
                if (CheckSP_CauHoiTonTaiBangKhac(cauhoi) == true)
                {
                    DeleteData("BODE", "CAUHOI", cauhoi);
                }
                else
                {
                    MessageBox.Show("Không thể xóa, đã tồn tại ràng buộc dữ liệu!");
                }
            }
        }

        public bool DeleteData(String selectedTable, String idColumn, int idValue)
        {
            using (SqlCommand command = new SqlCommand("DELETE FROM " + selectedTable + " WHERE " + idColumn + " = @id", Program.conn))
            {
                command.Parameters.AddWithValue("@id", idValue);
                try
                {
                    command.ExecuteNonQuery();
                    MessageBox.Show("Dữ liệu đã được xóa!");

                    btnChinhSua.Enabled = false;
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

                EnableTextBox(txtNoiDung);
                EnableTextBox(txtA);
                EnableTextBox(txtB);
                EnableTextBox(txtC);
                EnableTextBox(txtD);

                gridViewCauHoi.Enabled = true;

                LoadDataIntoDataGridView("BODE", gridViewCauHoi, maGV);
                BindingText(gridViewCauHoi);
                LoadDataIntoComboBox("BODE", "TRINHDO", cbbTrinhDo);
                LoadDataIntoComboBox("MONHOC", "MAMH", cbbMaMH);
                LoadDataIntoComboBox("BODE", "DAP_AN", cbbDapAn);
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

