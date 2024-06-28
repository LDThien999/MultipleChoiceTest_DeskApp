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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using TNCSDLPT;
using System.Net;
using System.Text.RegularExpressions;
using System.Collections;
using System.Xml.Linq;
using System.Net.Http.Headers;

namespace CSDLPT.PerFeat
{
    public partial class frmSV : Form
    {
        private SqlCommand sqlCommand;
        private SqlDataAdapter adapter = new SqlDataAdapter();
        private DataTable table = new DataTable();
        private SqlDataReader reader;
        //List<String> listComboBox = new List<string>();
        string tt = "";
        //string maCS = Program.brand2;
        Stack undoList = new Stack();
        Stack redoList = new Stack();
        int rowIndex = 0;
        bool flag = true;
        Stack maUn = new Stack();
        Stack maRe = new Stack();
        Stack ttRe = new Stack();
        Stack ttUn = new Stack();


        //các field phục vụ btn hoàn tác
        String msv = "";
        string ho = "";
        string ten = "";
        DateTime ns;
        string dc = "";
        string ml = "";

        /*
         lấy mã lớp
         */
        private void dsLop(String cmd)
        {
            DataTable dt = new DataTable();
            SqlConnection conn_publisher = new SqlConnection(Program.connstr);
            if (conn_publisher.State == ConnectionState.Closed) conn_publisher.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd, conn_publisher);
            da.Fill(dt);
            conn_publisher.Close();
            cbl.DataSource =dt;
            cbl.DisplayMember = "MALOP";
            cbl.ValueMember = "MALOP";
        }

        public frmSV()
        {
            InitializeComponent();
        }

        private void frmSV_FormClosing(object sender, FormClosingEventArgs e)
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

        //kiểm tra tồn tại mã sinh viên
        private bool ktraMsv(String masv)
        {
            using (SqlCommand command = new SqlCommand("Sp_Tracuu_Kiemtramasinhvien", Program.conn))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@masv", masv);

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


        private void clearBS()
        {
            tbMSV.DataBindings.Clear();//clearBinding
            tbHo.DataBindings.Clear();
            tbTen.DataBindings.Clear();
            tbDc.DataBindings.Clear();
            cbl.DataBindings.Clear();
            dtpNs.DataBindings.Clear();
        }

        //Clear data trong textbox
        private void clearTB()
        {
            if (tt == "sửa")
            {
                clearBS();
            }
            else
            {
                tbMSV.Text = "";
                tbHo.Text = "";
                tbTen.Text = "";
                tbDc.Text = "";
            }
            
        }

        private void BindingText(DataGridView gridview)
        {
            clearBS();//clearBinding

            tbMSV.DataBindings.Add("Text", gridview.DataSource, "MASV");//Binding textBox
            cbl.DataBindings.Add("Text", gridview.DataSource, "MALOP");
            tbHo.DataBindings.Add("Text", gridview.DataSource, "HO");
            tbTen.DataBindings.Add("Text", gridview.DataSource, "TEN");
            tbDc.DataBindings.Add("Text", gridview.DataSource, "DIACHI");
            dtpNs.DataBindings.Add("Value", gridview.DataSource, "NGAYSINH");
        }

        public void LoadDataIntoDataGridView(String selectedTable, DataGridView gridview)
        {
            sqlCommand = Program.conn.CreateCommand();
            sqlCommand.CommandText = "SELECT * FROM " + selectedTable; ;
            adapter.SelectCommand = sqlCommand;
            //table = new DataTable();
            try
            {
                adapter.Fill(table);
                table.Columns.Remove("rowguid"); //loai bo dong rowguid
                table.Columns.Remove("PASSWORD");
                gridview.DataSource = table; //add data tu table vao gridView
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            gridview.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            gridview.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            gridview.ReadOnly = true;
            groupInfo.Enabled = false;

            setBtn();

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

            if (undoList.Count == 0) { 
                btnUn.Enabled= false;
            }

            if(redoList.Count == 0)
            {
                btnRedo.Enabled= false;
            }
        }

        private void frmSV_Load(object sender, EventArgs e)
        {
            LoadDataIntoDataGridView("SINHVIEN", dgv);
            BindingText(dgv);

            //lấy cơ sở đăng nhập và view của mỗi vai trò đăng nhập
            layDSCS();
            roleView();
            dsLop("select * from LOP");

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

        //Hàm lấy dscs cho vào combobox và textbox cơ sở
        public void layDSCS()
        {
            cbCs.DataSource = Program.bds_dspm;
            cbCs.DisplayMember = "TENCS";
            cbCs.ValueMember = "TENSERVER";
            cbCs.SelectedIndex = Program.brand;
            tbCs.Text = Program.brand2;
        }

        //view của từng vai trò (TRUONG, COSO)
        public void roleView()
        {
            if(Program.role == "TRUONG")
            {
                tsCn.Enabled = false;
                groupInfo.Enabled = false;
            }else if(Program.role == "COSO")
            {
                cbCs.Enabled = false;
            }
        }

        /**
         * Đây là hàm xử lý chuyển cơ sở khi mà user chọn cơ sở trong combo box cơ sở
         */
        public void chuyenCoSo()
        {
            //Nếu không chọn gì thì thôi
            if(cbCs.SelectedValue.ToString() == "System.Data.DataRowView")
            {
                return;
            }

            Program.serverName = cbCs.SelectedValue.ToString();

            //Nếu chọn cơ sở khác với cơ sở hiện tại
            if(cbCs.SelectedIndex != Program.brand)
            {
                 Program.loginName = Program.remoteLogin;
                 Program.loginPassword = Program.remotePassword;
            }//Nếu trùng với chi nhánh của form đăng nhập thì dùng tài khoản đó luôn
            else
            {
                Program.loginName = Program.currentLogin;
                Program.loginPassword = Program.currentPassword;
            }
           

            if (Program.KetNoi() == 0)
            {
                MessageBox.Show("Xảy ra lỗi kết nối với chi nhánh hiện tại!", "Thông báo", MessageBoxButtons.OK);
            }
            else
            {
                Program.brand = cbCs.SelectedIndex;
                Program.brand2 = cbCs.Text;
                MessageBox.Show("Đã chuyển sang " + Program.brand2 + " thành công!");

                table.Rows.Clear();
                LoadDataIntoDataGridView("SINHVIEN", dgv);
                BindingText(dgv);
                layDSCS();
            }
        }

        private void cbCs_SelectedIndexChanged(object sender, EventArgs e)
        {
            chuyenCoSo();
        }

        //Kiểm tra các trường nhập liệu
        private bool ktrDLV()
        {
            ErrorProvider errorProvider = new ErrorProvider();
            //kiểm tra mã sinh viên
            if (tbMSV.Text == "")
            {
                MessageBox.Show("Không được bỏ trống mã sinh viên!", "Thông báo", MessageBoxButtons.OK);
                tbMSV.Focus();
                return false;
            }
            if (tbMSV.Text.Length>8)
            {
                MessageBox.Show("Mã sinh viên không lớn hơn 8 kí tự!", "Thông báo", MessageBoxButtons.OK);
                tbMSV.Focus();
                return false;
            }
            if (Regex.IsMatch(tbMSV.Text.Trim(), @"^[a-zA-Z0-9]+$") == false)
            {
                MessageBox.Show("Mã sinh viên chỉ nhận số!", "Thông báo", MessageBoxButtons.OK);
                tbMSV.Focus();
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

            //kiểm tra ngày sinh
            if (CalCulateAge(dtpNs.Value) < 18)
            {
                MessageBox.Show("Đối tượng chưa đủ 18 tuổi!", "Thông báo", MessageBoxButtons.OK);
                tbDc.Focus();
                return false;
            }

            return true;
        }

        //Hàm tính tuổi
        private static int CalCulateAge(DateTime dob)
        {
            int age = 0;
            age = DateTime.Now.Year - dob.Year;
            if(DateTime.Now.Year < dob.Year)
            {
                return 0;
            }
            return age;
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
            this.tbMSV.Enabled = true;

            //bật groupbox Info
            groupInfo.Enabled = true;

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
            LoadDataIntoDataGridView("SINHVIEN", dgv);
        }

        //Xử lý xóa
        private void xuLyXoa()
        {
            DataRow row = table.Rows[rowIndex];

            string masv = row["MASV"].ToString();
            string ho = row["HO"].ToString();
            string ten = row["TEN"].ToString();
            DateTime ns = (DateTime)row["NGAYSINH"];
            string dc = row["DIACHI"].ToString();
            string ml = row["MALOP"].ToString();

            if (MessageBox.Show("Bạn có chắc chắn muốn xóa sinh viên này không ?", "Thông báo",
                MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                try
                {
                    // Kiểm tra xem kết nối đã mở hay chưa
                    if (Program.conn.State == ConnectionState.Closed)
                    {
                        Program.conn.Open();
                    }

                    SqlCommand command = new SqlCommand("Sp_Xoasinhvien", Program.conn);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@StudentID", masv);
                    try
                    {
                        // Thực thi stored procedure
                        Program.myReader = command.ExecuteReader();

                        // Chuẩn bị câu truy vấn hoàn tác cho undo button
                        String ctvht;
                        ctvht = string.Format("INSERT INTO DBO.SINHVIEN( MASV,HO,TEN,DIACHI,NGAYSINH,MALOP)" +
            "VALUES('{0}', '{1}', '{2}', '{3}', CAST('{4}' AS DATE), '{5}')",
                             masv.Trim(), ho, ten, dc, ns.ToString("yyyy-MM-dd"), ml.Trim());
                        undoList.Push(ctvht);
                        ttUn.Push("xóa");
                        maUn.Push(masv.Trim());

                        //đóng myreader
                        Program.myReader.Close();

                        //lấy dữ liệu
                        LoadDataIntoDataGridView("SINHVIEN", dgv);
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
                    MessageBox.Show("Lỗi xóa sinh viên. Hãy thử lại\n" + ex.Message, "Thông báo", MessageBoxButtons.OK);
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
            BindingText(dgv);

            //set các button
            btnThem.Enabled = false;
            btnXoa.Enabled = false;
            btnSua.Enabled = false;
            btnRe.Enabled = false;
            btnGhi.Enabled = true;
            btnUn.Enabled = true;
            tbMSV.Enabled = false;

            //bật groupbox Info và dataGidView
            groupInfo.Enabled = true;

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

            this.msv = row["MASV"].ToString();
            this.ho = row["HO"].ToString();
            this.ten = row["TEN"].ToString();
            this.ns = (DateTime)row["NGAYSINH"];
            this.dc = row["DIACHI"].ToString();
            this.ml = row["MALOP"].ToString();
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
                LoadDataIntoDataGridView("SINHVIEN", dgv);

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
            tbMSV.Enabled = true;

            //cập nhật trang thái
            tt = "ghi";

            //xóa thông tin sau khi ghi
            clearBS();
            clearTB();

            table.Rows.Clear();
            LoadDataIntoDataGridView("SINHVIEN", dgv);
            BindingText(dgv);           
            dgv.Enabled = true;

            xuLyGridView(msv);
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
                if (ktraMsv(tbMSV.Text) == false)
                {
                    string masv = msv = tbMSV.Text;
                    string ho = tbHo.Text;
                    string ten = tbTen.Text;
                    DateTime ns = dtpNs.Value.Date;
                    string dc = tbDc.Text;
                    string ml = cbl.Text;

                    //THỰC THI SP THÊM SINH VIÊN
                    SqlCommand command = new SqlCommand("Sp_Themsinhvien", Program.conn);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@masv", masv);
                    command.Parameters.AddWithValue("@ho", ho);
                    command.Parameters.AddWithValue("@ten", ten);
                    command.Parameters.AddWithValue("@ns", ns.ToString("yyyy-MM-dd"));
                    command.Parameters.AddWithValue("@diachi", dc);
                    command.Parameters.AddWithValue("@malop", ml);
                    try
                    {
                        // Thực thi stored procedure
                        Program.myReader = command.ExecuteReader();

                        // Chuẩn bị câu truy vấn hoàn tác cho undo button
                        String ctvht;
                        ctvht =     "DELETE DBO.SINHVIEN " +
                                    "WHERE MASV = '" + masv.Trim() + "'";
                        undoList.Push(ctvht);

                        Program.myReader.Close();
                        flag = true;

                        ttUn.Push("thêm");
                        this.maUn.Push(masv.Trim());

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
                    flag = false;
                    MessageBox.Show("Đã tồn tại mã sinh viên ở cơ sở này hoặc cơ sở khác!", "Thông báo", MessageBoxButtons.OK);
                }

                
            }else if(tt=="sửa"){

                //xuLySua();

                string masv = msv = tbMSV.Text;
                string ho = tbHo.Text;
                string ten = tbTen.Text;
                DateTime ns = dtpNs.Value.Date;
                string dc = tbDc.Text;
                string ml = cbl.SelectedValue.ToString();

                //THỰC THI SP SỬA SINH VIÊN
                SqlCommand command = new SqlCommand("Sp_Suasinhvien", Program.conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@masv", masv);
                command.Parameters.AddWithValue("@ho", ho);
                command.Parameters.AddWithValue("@ten", ten);
                command.Parameters.AddWithValue("@ns", ns.ToString("yyyy-MM-dd"));
                command.Parameters.AddWithValue("@diachi", dc);
                command.Parameters.AddWithValue("@malop", ml);
                try
                {
                    // Thực thi stored procedure
                    Program.myReader = command.ExecuteReader();

                    // Chuẩn bị câu truy vấn hoàn tác cho undo button
                    String ctvht;
                    ctvht = "UPDATE DBO.SINHVIEN " +
                                "SET " +
                                "HO = '" + this.ho + "'," +
                                "TEN = '" + this.ten + "'," +
                                "DIACHI = '" + this.dc + "'," +
                                "NGAYSINH = CAST('" + this.ns.ToString("yyyy-MM-dd") + "' AS DATE)," +
                                "MALOP = '" + this.ml.Trim() + "'  " +
                                "WHERE MASV = '" + this.msv.Trim() + "'";
                    undoList.Push(ctvht);

                    //đóng myreader
                    Program.myReader.Close();
                    flag = true;

                    ttUn.Push("sửa");
                    this.maUn.Push(masv.Trim());

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
            LoadDataIntoDataGridView("SINHVIEN", dgv);

            
            //mở lại bảng cho user tương tác
            dgv.Enabled = true;

            if (this.flag == false)
            {
                flag = true;
                return;
            }

            xuLyGridView(msv);
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
            }else
            {
                btnUn.Enabled = true;
            }

            /*Step 2*/
            String cauTruyVanHoanTac = undoList.Pop().ToString().Trim();
            this.redoList.Push(cauTruyVanHoanTac);
            String masv = maUn.Pop().ToString().Trim();
            this.msv = masv;
            String tt = ttUn.Pop().ToString().Trim();

            if (tt == "thêm")
            {
                flag = false;
                cbReThem(masv);
                maRe.Push(masv);
            }
            else if(tt == "xóa")
            {
                
                maRe.Push(masv);
            }else if(tt == "sửa")
            {
                maRe.Push(masv);
                cbReSua(masv);
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
                MessageBox.Show("Khôi phục không thành công!", "Thông báo", MessageBoxButtons.OK);
                return;
            }
        }

        //Xử lý gridview
        private void xuLyGridView(String ma)
        {
            foreach (DataGridViewRow row in dgv.Rows)
            {
                string productCodeInRow = row.Cells["Column1"].Value.ToString().Trim(); 

                if (productCodeInRow == ma.Trim())
                {
                    // Bôi đen dòng tương ứng
                    //row.DefaultCellStyle.BackColor = Color.Cyan;

                    // Đặt trọng tâm vào dòng
                    dgv.CurrentCell = row.Cells[0]; // Chọn ô đầu tiên trong dòng
                    break; // Dừng duyệt khi tìm thấy dòng cần thiết
                }
            }
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
            String masv = maRe.Pop().ToString().Trim();

            if (tt == "xóa")
            {
                
                String cauRedo = "DELETE DBO.SINHVIEN " +
                                    "WHERE MASV = '" + masv.Trim() + "'";
                undoList.Push(redoList.Pop());

                try
                {
                    if (Program.KetNoi() == 0)
                    {
                        return;
                    }
                    int n = Program.ExecSqlNonQuery(cauRedo);

                    flag = false;

                    this.msv = masv;
                    this.maUn.Push(masv);
                    this.ttUn.Push(tt);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Khôi phục không thành công!", "Thông báo", MessageBoxButtons.OK);
                    return;
                }
            }else if (tt == "thêm")
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

                    this.msv = masv;
                    this.maUn.Push(masv);
                    this.ttUn.Push(tt);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Khôi phục không thành công!", "Thông báo", MessageBoxButtons.OK);
                    return;
                }
            }else if(tt == "sửa")
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

                    this.msv = masv;
                    this.maUn.Push(masv);
                    this.ttUn.Push(tt);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Khôi phục không thành công!", "Thông báo", MessageBoxButtons.OK);
                    return;
                }
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
            tbMSV.Enabled = true;

            //cập nhật trang thái
            tt = "redo";

            //xóa thông tin sau khi ghi
            clearBS();
            clearTB();

            table.Rows.Clear();
            BindingText(dgv);
            LoadDataIntoDataGridView("SINHVIEN", dgv);
            dgv.Enabled = true;

            if (this.flag == false)
            {
                flag = true;
                return;
            }

            xuLyGridView(msv);

            if (this.redoList.Count == 0) { 
                btnRedo.Enabled = false;
                return; 
            }
        }

        private void cbReThem(String masv)
        {
            xuLyGridView(msv);//chọn dòng xóa

            //lấy hàng được click
            rowIndex = dgv.CurrentRow.Index;

            //lấy thông tin
            DataRow row = table.Rows[rowIndex];

            this.msv = row["MASV"].ToString();
            this.ho = row["HO"].ToString();
            this.ten = row["TEN"].ToString();
            this.ns = (DateTime)row["NGAYSINH"];
            this.dc = row["DIACHI"].ToString();
            this.ml = row["MALOP"].ToString();

            //chuẩn bị truy vấn hoàn tác
            String ctvht;
            ctvht = string.Format("INSERT INTO DBO.SINHVIEN( MASV,HO,TEN,DIACHI,NGAYSINH,MALOP)" +
            "VALUES('{0}', '{1}', '{2}', '{3}', CAST('{4}' AS DATE), '{5}')",
                 this.msv.Trim(), this.ho, this.ten, this.dc, this.ns.ToString("yyyy-MM-dd"), this.ml.Trim());

            //push ctvht vào redoList
            this.redoList.Push(ctvht);
        }

        private void cbReSua(String masv)
        {
            xuLyGridView(msv);//chọn dòng xóa

            //lấy hàng được click
            rowIndex = dgv.CurrentRow.Index;

            //lấy thông tin
            DataRow row = table.Rows[rowIndex];

            this.msv = row["MASV"].ToString();
            this.ho = row["HO"].ToString();
            this.ten = row["TEN"].ToString();
            this.ns = (DateTime)row["NGAYSINH"];
            this.dc = row["DIACHI"].ToString();
            this.ml = row["MALOP"].ToString();

            //chuẩn bị truy vấn hoàn tác
            String ctvht;
            ctvht = "UPDATE DBO.SINHVIEN " +
                                "SET " +
                                "HO = '" + this.ho + "'," +
                                "TEN = '" + this.ten + "'," +
                                "DIACHI = '" + this.dc + "'," +
                                "NGAYSINH = CAST('" + this.ns.ToString("yyyy-MM-dd") + "' AS DATE)," +
                                "MALOP = '" + this.ml.Trim() + "'  " +
                                "WHERE MASV = '" + this.msv.Trim() + "'";

            //push ctvht vào redoList
            this.redoList.Push(ctvht);
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

        
    }
}
