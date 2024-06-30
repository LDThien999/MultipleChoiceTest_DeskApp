using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using TNCSDLPT;

namespace CSDLPT.PerFeat
{
    public partial class frmGV : Form
    {
        private SqlCommand sqlCommand;
        private SqlDataAdapter adapter = new SqlDataAdapter();
        private DataTable table = new DataTable();
        private SqlDataReader reader;
        //List<String> listComboBox = new List<string>();
        string tt = "";
        //string maCS = "";
        Stack undoList = new Stack();
        Stack redoList = new Stack();
        int rowIndex = 0;
        bool flag = true;
        Stack maUn = new Stack();
        Stack maRe = new Stack();
        Stack ttRe = new Stack();
        Stack ttUn = new Stack();



        //các field phục vụ btn hoàn tác
        String mgv = "";
        string ho = "";
        string ten = "";
        string dc = "";
        string mk = "";

        /*
         lấy mã khoa
         */
        private void dsKhoa(String cmd)
        {
            DataTable dt = new DataTable();
            SqlConnection conn_publisher = new SqlConnection(Program.connstr);
            if (conn_publisher.State == ConnectionState.Closed) conn_publisher.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd, conn_publisher);
            da.Fill(dt);
            conn_publisher.Close();
            cbKh.DataSource = dt;
            cbKh.DisplayMember = "MAKH";
            cbKh.ValueMember = "MAKH";
        }

        //xóa liên kết data textbox và gridview
        private void clearBS()
        {
            tbMGV.DataBindings.Clear();//clearBinding
            tbHo.DataBindings.Clear();
            tbTen.DataBindings.Clear();
            tbDc.DataBindings.Clear();
            cbKh.DataBindings.Clear();
        }

        //xóa nội dung trên textbox 
        private void clearTB()
        {
            if (tt == "sửa")
            {
                clearBS();
            }
            else
            {
                tbMGV.Text = "";
                tbHo.Text = "";
                tbTen.Text = "";
                tbDc.Text = "";
                cbKh.SelectedIndex = -1;
            }

        }

        //view của từng vai trò (TRUONG, COSO)
        public void roleView()
        {
            if (Program.role == "TRUONG")
            {
                tabcn.Enabled = false;
                groupInfo.Enabled = false;
            }
            else if (Program.role == "COSO")
            {
                tabcn.Enabled = true;
                groupInfo.Enabled = true;
            }
        }

        //liên kết data textbox và gridview
        private void BindingText(DataGridView gridview)
        {
            clearBS();//clearBinding

            tbMGV.DataBindings.Add("Text", gridview.DataSource, "MAGV");//Binding textBox
            tbHo.DataBindings.Add("Text", gridview.DataSource, "HO");
            tbTen.DataBindings.Add("Text", gridview.DataSource, "TEN");
            tbDc.DataBindings.Add("Text", gridview.DataSource, "DIACHI");
            cbKh.DataBindings.Add("Text", gridview.DataSource, "MAKH");
        }

        public void LoadDataIntoDataGridView(String selectedTable, DataGridView gridview)
        {
            sqlCommand = Program.conn.CreateCommand();
            sqlCommand.CommandText = "SELECT * FROM " + selectedTable; 
            adapter.SelectCommand = sqlCommand;
            //table = new DataTable();
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
            //groupInfo.Enabled = false;

            setBtn();
            setTb(false);

        }

        private void setBtn()
        {
            if (tt == "undo" || tt == "redo")
            {
                btnRedo.Enabled = true;
            }
            else
            {
                this.redoList.Clear();
                btnRedo.Enabled = false;
                btnUn.Enabled = true;
            }

            if (tt == "")
            {
                btnUn.Enabled = false;
                btnRedo.Enabled = false;
            }

            if (undoList.Count == 0)
            {
                btnUn.Enabled = false;
            }

            if (redoList.Count == 0)
            {
                btnRedo.Enabled = false;
            }
        }

        private void setTb(bool f)
        {
            this.tbMGV.Enabled = f;
            this.tbHo.Enabled = f;
            this.tbTen.Enabled = f;
            this.tbDc.Enabled = f;
            this.cbKh.Enabled = f;
        }


        public frmGV()
        {
            InitializeComponent();

            LoadDataIntoDataGridView("GIAOVIEN", dgv);
            BindingText(dgv);
            dsKhoa("select * from KHOA");

            roleView();
        }

        private void frmGV_FormClosing(object sender, FormClosingEventArgs e)
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
                    return;
                }
            }
        }

        //kiểm tra tồn tại mã giảng viên
        private bool ktraMsv(String magv)
        {
            using (SqlCommand command = new SqlCommand("Sp_Tracuu_Kiemtramagiaovien", Program.conn))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@magv", magv);

                // Kiểm tra xem kết nối đã mở hay chưa
                if (Program.conn.State == ConnectionState.Closed)
                {
                    Program.conn.Open();
                }

                // Thực thi stored procedure
                // Trả về 1: đã tồn tại, trả về 0: có thể thêm
                reader = command.ExecuteReader();
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


        //Kiểm tra các trường nhập liệu
        private bool ktrDLV()
        {
            ErrorProvider errorProvider = new ErrorProvider();
            //kiểm tra mã sinh viên
            if (tbMGV.Text == "")
            {
                MessageBox.Show("Không được bỏ trống mã giảng viên!", "Thông báo", MessageBoxButtons.OK);
                tbMGV.Focus();
                return false;
            }
            if (tbMGV.Text.Length > 8)
            {
                MessageBox.Show("Mã giảng viên không lớn hơn 8 kí tự!", "Thông báo", MessageBoxButtons.OK);
                tbMGV.Focus();
                return false;
            }
            if (Regex.IsMatch(tbMGV.Text.Trim(), @"^[a-zA-Z0-9]+$") == false)
            {
                MessageBox.Show("Mã giảng viên chỉ nhận số!", "Thông báo", MessageBoxButtons.OK);
                tbMGV.Focus();
                return false;
            }

            //Kiểm tra họ
            if (tbHo.Text.Trim() == "")
            {
                MessageBox.Show("Không được bỏ trống Họ!", "Thông báo", MessageBoxButtons.OK);
                tbHo.Focus();
                return false;
            }
            if (tbHo.Text.Length > 40)
            {
                MessageBox.Show("Họ không lớn hơn 40 kí tự!", "Thông báo", MessageBoxButtons.OK);
                tbHo.Focus();
                return false;
            }
            if (Regex.IsMatch(tbHo.Text, @"^[a-zA-ZÀ-ỹ ]+$") == false)
            {
                MessageBox.Show("Họ chỉ nhận chữ cái và khoảng trắng!", "Thông báo", MessageBoxButtons.OK);
                tbHo.Focus();
                return false;
            }

            //kiểm tra tên
            if (tbTen.Text.Trim() == "")
            {
                MessageBox.Show("Không được bỏ trống Tên!", "Thông báo", MessageBoxButtons.OK);
                tbTen.Focus();
                return false;
            }
            if (tbTen.Text.Length > 10)
            {
                MessageBox.Show("Tên không lớn hơn 10 kí tự!", "Thông báo", MessageBoxButtons.OK);
                tbTen.Focus();
                return false;
            }
            if (Regex.IsMatch(tbTen.Text, @"^[a-zA-ZÀ-ỹ ]+$") == false)
            {
                MessageBox.Show("Tên chỉ nhận chữ cái và khoảng trắng!", "Thông báo", MessageBoxButtons.OK);
                tbTen.Focus();
                return false;
            }

            //kiểm tra địa chỉ
            if (tbDc.Text.Trim() == "")
            {
                MessageBox.Show("Không được bỏ trống Địa chỉ!", "Thông báo", MessageBoxButtons.OK);
                tbDc.Focus();
                return false;
            }
            if (tbDc.Text.Length > 100)
            {
                MessageBox.Show("Địa chỉ không lớn hơn 100 kí tự!", "Thông báo", MessageBoxButtons.OK);
                tbDc.Focus();
                return false;
            }
            if (Regex.IsMatch(tbDc.Text, @"^[a-zA-ZÀ-ỹ0-9 /]+$") == false)
            {
                MessageBox.Show("Địa chỉ chỉ nhận chữ cái, số và khoảng trắng!", "Thông báo", MessageBoxButtons.OK);
                tbDc.Focus();
                return false;
            }
            return true;
        }

        /*
         * Sự kiện Thêm
         */
        private void btnThem_Click(object sender, EventArgs e)
        {
            tt = "thêm";
            dgv.Enabled = false;

            //set các button
            btnThem.Enabled = false;
            btnXoa.Enabled = false;
            btnSua.Enabled = false;
            btnRe.Enabled = false;
            btnGhi.Enabled = true;
            btnUn.Enabled = true;
            this.tbMGV.Enabled = true;
            btnRedo.Enabled = false;

            //bật groupbox Info
            setTb(true);

            //xóa thông tin sau khi chọn thêm
            clearBS();
            clearTB();
        }


        /*
         *Sự kiện Xóa
         */
        private void btnXoa_Click(object sender, EventArgs e)
        {
            tt = "xóa";

            //set các button
            btnThem.Enabled = true;
            btnXoa.Enabled = true;
            btnSua.Enabled = true;
            btnRe.Enabled = true;
            btnGhi.Enabled = true;
            btnUn.Enabled = true;

            //bật groupbox Info và dataGidView
            groupInfo.Enabled = true;
            dgv.ReadOnly = true;

            //lấy hàng được click
            rowIndex = dgv.CurrentRow.Index;

            //xử lý xóa
            xuLyXoa();

            //xóa thông tin sau khi xóa
            clearBS();
            clearTB();

            table.Rows.Clear();
            BindingText(dgv);
            LoadDataIntoDataGridView("GIAOVIEN", dgv);
        }

        //xử lý xóa
        private void xuLyXoa()
        {
            DataRow row = table.Rows[rowIndex];

            string magv = row["MAGV"].ToString();
            string ho = row["HO"].ToString();
            string ten = row["TEN"].ToString();
            string dc = row["DIACHI"].ToString();
            string mk = row["MAKH"].ToString();

            if (MessageBox.Show("Bạn có chắc chắn muốn xóa giảng viên này không ?", "Thông báo",
                MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                try
                {
                    // Kiểm tra xem kết nối đã mở hay chưa
                    if (Program.conn.State == ConnectionState.Closed)
                    {
                        Program.conn.Open();
                    }

                    SqlCommand command = new SqlCommand("Sp_Xoagiaovien", Program.conn);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@magv", magv);
                    try
                    {
                        // Thực thi stored procedure
                        Program.myReader = command.ExecuteReader();

                        // Chuẩn bị câu truy vấn hoàn tác cho undo button
                        String ctvht;
                        ctvht = string.Format("INSERT INTO DBO.GIAOVIEN( MAGV,HO,TEN,DIACHI,MAKH)" +
            "VALUES('{0}', '{1}', '{2}', '{3}', '{4}')",
                             magv.Trim(), ho, ten, dc, mk.Trim());
                        undoList.Push(ctvht);
                        ttUn.Push("xóa");
                        maUn.Push(magv.Trim());

                        //đóng myreader
                        Program.myReader.Close();

                        //lấy dữ liệu
                        //LoadDataIntoDataGridView("GIAOVIEN", dgv);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Thực thi database thất bại!\n\n" + ex.Message, "Thông báo",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Console.WriteLine(ex.Message);
                        return;
                    }

                    MessageBox.Show("Xóa thành công ", "Thông báo", MessageBoxButtons.OK);
                    this.btnUn.Enabled = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi xóa giảng viên. Hãy thử lại\n" + ex.Message, "Thông báo", MessageBoxButtons.OK);
                    return;
                }
            }
            else
            {
                undoList.Pop();
            }
        }

        /*
         *Sự kiện Sửa
         */
        private void btnSua_Click(object sender, EventArgs e)
        {
            tt = "sửa";

            //bật groupbox Info và dataGidView
            setTb(true);

            //set các button
            btnThem.Enabled = false;
            btnXoa.Enabled = false;
            btnSua.Enabled = false;
            btnRe.Enabled = false;
            btnGhi.Enabled = true;
            btnUn.Enabled = true;
            tbMGV.Enabled = false;
            btnRedo.Enabled = false;

            //lấy hàng được click
            rowIndex = dgv.CurrentRow.Index;

            //chặn sửa thêm khi chưa ghi
            dgv.Enabled = false;

            //Xử lý sửa
            xuLySua();
        }

        //Xử lý sửa
        private void xuLySua()
        {
            DataRow row = table.Rows[rowIndex];

            this.mgv = row["MAGV"].ToString();
            this.ho = row["HO"].ToString();
            this.ten = row["TEN"].ToString();
            this.dc = row["DIACHI"].ToString();
            this.mk = row["MAKH"].ToString();
        }


        /*
        *Sự kiện Reload
        */
        private void btnRe_Click(object sender, EventArgs e)
        {
            Program.myReader.Close();
            try
            {
                //set các button
                btnThem.Enabled = true;
                btnXoa.Enabled = true;
                btnSua.Enabled = true;
                btnRe.Enabled = true;
                btnGhi.Enabled = true;
                btnUn.Enabled = true;


                dgv.ReadOnly = true;

                //xóa thông tin sau khi thêm
                clearBS();
                clearTB();
                undoList.Clear();

                table.Rows.Clear();
                BindingText(dgv);
                LoadDataIntoDataGridView("GIAOVIEN", dgv);

                MessageBox.Show("Reload thành công ", "Thông báo", MessageBoxButtons.OK);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi Làm mới" + ex.Message, "Thông báo", MessageBoxButtons.OK);
                return;
            }
        }


        /*
         *Sự kiện Ghi
         */
        private void btnGhi_Click(object sender, EventArgs e)
        {
            //kiểm tra dữ liệu vào
            if (ktrDLV() == false)
            {
                return;
            }

            //xử lý ghi
            xuLyGhi();
            if (flag == false) return;

            //set các button
            btnThem.Enabled = true;
            btnXoa.Enabled = true;
            btnSua.Enabled = true;
            btnRe.Enabled = true;
            btnGhi.Enabled = true;
            btnUn.Enabled = true;
            tbMGV.Enabled = true;

            //cập nhật trang thái
            tt = "ghi";

            //xóa thông tin sau khi ghi
            clearBS();
            clearTB();

            table.Rows.Clear();
            BindingText(dgv);
            LoadDataIntoDataGridView("GIAOVIEN", dgv);

            dgv.Enabled = true;

            xuLyGridView(mgv);
        }

        //xử lý ghi
        private void xuLyGhi()
        {
            // Kiểm tra xem kết nối đã mở hay chưa
            if (Program.conn.State == ConnectionState.Closed)
            {
                Program.conn.Open();
            }

            if (tt == "thêm")
            {
                if (ktraMsv(tbMGV.Text) == false)
                {
                    string magv = mgv = tbMGV.Text;
                    string ho = tbHo.Text;
                    string ten = tbTen.Text;
                    string dc = tbDc.Text;
                    string mk = cbKh.SelectedValue.ToString();

                    //THỰC THI SP THÊM SINH VIÊN
                    SqlCommand command = new SqlCommand("Sp_Themgiaovien", Program.conn);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@magv", magv);
                    command.Parameters.AddWithValue("@ho", ho);
                    command.Parameters.AddWithValue("@ten", ten);
                    command.Parameters.AddWithValue("@dc", dc);
                    command.Parameters.AddWithValue("@mk", mk);
                    try
                    {
                        // Thực thi stored procedure
                        Program.myReader = command.ExecuteReader();

                        // Chuẩn bị câu truy vấn hoàn tác cho undo button
                        String ctvht;
                        ctvht = "" +
                                    "DELETE DBO.GIAOVIEN " +
                                    "WHERE MAGV = '" + magv.Trim() + "'";
                        undoList.Push(ctvht);

                        Program.myReader.Close();
                        flag = true;

                        ttUn.Push("thêm");
                        this.maUn.Push(magv.Trim());

                        MessageBox.Show("Thêm thành công ", "Thông báo", MessageBoxButtons.OK);
                    }
                    catch (Exception ex)
                    {
                        flag = false;
                        MessageBox.Show("Thực thi database thất bại!\n\n" + ex.Message, "Thông báo",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                else
                {
                    flag |= false;
                    MessageBox.Show("Đã tồn tại mã giảng viên ở cơ sở này hoặc cơ sở khác!", "Thông báo", MessageBoxButtons.OK);
                }


            }
            else if (tt == "sửa")
            {
                //xuLySua();
                string maGv = mgv = tbMGV.Text;
                string ho = tbHo.Text;
                string ten = tbTen.Text;
                string dc = tbDc.Text;
                string mk = cbKh.SelectedValue.ToString();

                //THỰC THI SP SỬA SINH VIÊN
                SqlCommand command = new SqlCommand("Sp_Suagiaovien", Program.conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@maGv", maGv);
                command.Parameters.AddWithValue("@ho", ho);
                command.Parameters.AddWithValue("@ten", ten);
                command.Parameters.AddWithValue("@dc", dc);
                command.Parameters.AddWithValue("@mk", mk);
                try
                {
                    // Thực thi stored procedure
                    Program.myReader = command.ExecuteReader();

                    // Chuẩn bị câu truy vấn hoàn tác cho undo button
                    String ctvht;
                    ctvht = "UPDATE DBO.GIAOVIEN " +
                                "SET " +
                                "HO = '" + this.ho + "'," +
                                "TEN = '" + this.ten + "'," +
                                "DIACHI = '" + this.dc + "'," +
                                "MAKH = '" + this.mk.Trim() + "'  " +
                                "WHERE MAGV = '" + this.mgv.Trim() + "'";
                    undoList.Push(ctvht);

                    //đóng myreader
                    Program.myReader.Close();
                    flag = true;

                    ttUn.Push("sửa");
                    this.maUn.Push(maGv.Trim());

                    MessageBox.Show("Sửa thành công ", "Thông báo", MessageBoxButtons.OK);
                }
                catch (Exception ex)
                {
                    flag = false;
                    MessageBox.Show("Thực thi database thất bại!\n\n" + ex.Message, "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Console.WriteLine(ex.Message);
                    return;
                }
            }
            else
            {
                flag = false;
                MessageBox.Show("Không có thao tác cần ghi!\n\n!", "Thông báo", MessageBoxButtons.OK);
            }

        }


        /*
         *Sự kiện hoàn tác
         */
        private void btnUn_Click(object sender, EventArgs e)
        {
            //set textbox thông tin bật lên 
            this.groupInfo.Enabled = true;

            //xử lý undo
            xulyUn();

            //cập nhật trang thái
            tt = "undo";

            //set các button
            this.btnThem.Enabled = true;
            this.btnXoa.Enabled = true;
            this.btnGhi.Enabled = true;
            this.btnSua.Enabled = true;
            this.btnRe.Enabled = true;

            //xóa thông tin sau khi undo
            clearBS();
            clearTB();

            table.Rows.Clear();
            BindingText(dgv);
            LoadDataIntoDataGridView("GIAOVIEN", dgv);

            //mở lại bảng cho user tương tác
            dgv.Enabled = true;

            if (this.flag == false)
            {
                flag = true;
                return;
            }

            xuLyGridView(mgv);
        }

        //xử lý hoàn tác
        private void xulyUn()
        {
            /* Step 0 - */
            if (this.tt == "thêm" && this.btnThem.Enabled == false)
            {
                flag = false;
                return;
            }

            if (this.tt == "sửa")
            {
                flag = false;
                return;
            }

            /*Step 1*/
            if (undoList.Count == 1)
            {
                btnUn.Enabled = false;
            }
            else
            {
                btnUn.Enabled = true;
            }

            /*Step 2*/
            String cauTruyVanHoanTac = undoList.Pop().ToString();
            this.redoList.Push(cauTruyVanHoanTac);
            String magv = maUn.Pop().ToString().Trim();
            this.mgv = magv;
            String tt = ttUn.Pop().ToString().Trim();

            if (tt == "thêm")
            {
                flag = false;
                cbReThem(magv);
                maRe.Push(magv);
            }
            else if (tt == "xóa")
            {
                maRe.Push(magv);
            }
            else if (tt == "sửa")
            {
                maRe.Push(magv);
                cbReSua(magv);
            }


            ttRe.Push(tt);
            //Console.WriteLine(cauTruyVanHoanTac);
            try
            {
                if (Program.KetNoi() == 0)
                {
                    return;
                }
                int n = Program.ExecSqlNonQuery(cauTruyVanHoanTac);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Khôi phục thành công!", "Thông báo", MessageBoxButtons.OK);
                return;
            }
        }

        private void frmGV_Load(object sender, EventArgs e)
        {
            //LẤY thông tin vào group box
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
        }


        /**
        * Chức năng Redo
        */
        private void cnRedo()
        {
            if (maRe.Count == 0)
            {
                MessageBox.Show("Không có thao tác cần Redo!", "Thông báo", MessageBoxButtons.OK);
                return;
            }

            String tt = ttRe.Pop().ToString().Trim();
            String magv = maRe.Pop().ToString().Trim();

            if (tt == "xóa")
            {

                String cauRedo = "DELETE DBO.GIAOVIEN " +
                                    "WHERE MAGV = '" + magv.Trim() + "'";
                undoList.Push(redoList.Pop());

                try
                {
                    if (Program.KetNoi() == 0)
                    {
                        return;
                    }
                    int n = Program.ExecSqlNonQuery(cauRedo);

                    flag = false;

                    this.mgv = magv;
                    this.maUn.Push(magv);
                    this.ttUn.Push(tt);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Khôi phục không thành công!", "Thông báo", MessageBoxButtons.OK);
                    return;
                }
            }
            else if (tt == "thêm")
            {
                String cauRedo = this.redoList.Pop().ToString().Trim();
                undoList.Push(redoList.Pop());

                try
                {
                    if (Program.KetNoi() == 0)
                    {
                        return;
                    }
                    int n = Program.ExecSqlNonQuery(cauRedo);

                    this.mgv = magv;
                    this.maUn.Push(magv);
                    this.ttUn.Push(tt);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Khôi phục không thành công!", "Thông báo", MessageBoxButtons.OK);
                    return;
                }
            }
            else if (tt == "sửa")
            {
                String cauRedo = this.redoList.Pop().ToString().Trim();
                undoList.Push(redoList.Pop());

                try
                {
                    if (Program.KetNoi() == 0)
                    {
                        return;
                    }
                    int n = Program.ExecSqlNonQuery(cauRedo);

                    this.mgv = magv;
                    this.maUn.Push(magv);
                    this.ttUn.Push(tt);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Khôi phục không thành công!", "Thông báo", MessageBoxButtons.OK);
                    return;
                }
            }
        }

        private void cbReThem(String magv)
        {
            xuLyGridView(mgv);//chọn dòng xóa

            //lấy hàng được click
            rowIndex = dgv.CurrentRow.Index;

            //lấy thông tin
            DataRow row = table.Rows[rowIndex];

            this.mgv = row["MAGV"].ToString();
            this.ho = row["HO"].ToString();
            this.ten = row["TEN"].ToString();
            this.dc = row["DIACHI"].ToString();
            this.mk = row["MAKH"].ToString();

            //chuẩn bị truy vấn hoàn tác
            String ctvht;
            ctvht = string.Format("INSERT INTO DBO.GIAOVIEN(MAGV,HO,TEN,DIACHI,MAKH)" +
            "VALUES('{0}', '{1}', '{2}', '{3}', '{4}')",
                             magv.Trim(), ho, ten, dc, mk.Trim());

            //push ctvht vào redoList
            this.redoList.Push(ctvht);
        }

        private void cbReSua(String magv)
        {
            xuLyGridView(mgv);//chọn dòng xóa

            //lấy hàng được click
            rowIndex = dgv.CurrentRow.Index;

            //lấy thông tin
            DataRow row = table.Rows[rowIndex];

            this.mgv = row["MAGV"].ToString();
            this.ho = row["HO"].ToString();
            this.ten = row["TEN"].ToString();
            this.dc = row["DIACHI"].ToString();
            this.mk = row["MAKH"].ToString(); ;

            //chuẩn bị truy vấn hoàn tác
            String ctvht;
            ctvht = "UPDATE DBO.GIAOVIEN " +
                                "SET " +
                                "HO = '" + this.ho + "'," +
                                "TEN = '" + this.ten + "'," +
                                "DIACHI = '" + this.dc + "'," +
                                "MAKH = '" + this.mk.Trim() + "'  " +
                                "WHERE MAGV = '" + this.mgv.Trim() + "'";

            //push ctvht vào redoList
            this.redoList.Push(ctvht);
        }

        

        //Xử lý gridview
        private void xuLyGridView(String ma)
        {
            foreach (DataGridViewRow row in dgv.Rows)
            {
                string productCodeInRow = row.Cells["Column1"].Value.ToString().Trim();

                if (productCodeInRow == ma.Trim())
                {
                    // Đặt trọng tâm vào dòng
                    dgv.CurrentCell = row.Cells[0]; // Chọn ô đầu tiên trong dòng
                    break; // Dừng duyệt khi tìm thấy dòng cần thiết
                }
            }
        }

       

        private void dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //dgv.RowHeadersDefaultCellStyle.SelectionBackColor = Color.Empty;
            // Bỏ chọn tất cả các dòng
            dgv.ClearSelection();

            // Chọn dòng được nhấp chuột
            if (e.RowIndex >= 0)
            {
                dgv.Rows[e.RowIndex].Selected = true;
            }
        }

        private void btnRedo_Click(object sender, EventArgs e)
        {
            cnRedo();

            btnXoa.Enabled = true;
            btnSua.Enabled = true;
            btnRe.Enabled = true;
            btnGhi.Enabled = true;
            btnUn.Enabled = true;
            tbMGV.Enabled = true;

            //cập nhật trang thái
            tt = "redo";

            //xóa thông tin sau khi ghi
            clearBS();
            clearTB();

            table.Rows.Clear();
            BindingText(dgv);
            LoadDataIntoDataGridView("GIAOVIEN", dgv);
            dgv.Enabled = true;

            if (this.flag == false) { 
                flag = true;
                return; 
            }

            xuLyGridView(mgv);

            if (this.redoList.Count == 0)
            {
                btnRedo.Enabled = false;
                return;
            }
        }

        private void dgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void groupInfo_Enter(object sender, EventArgs e)
        {

        }
    }
}
