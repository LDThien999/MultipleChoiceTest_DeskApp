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
using System.Threading.Tasks;
using System.Windows.Forms;
using TNCSDLPT;

namespace CSDLPT.thi_dangkythi
{
    public partial class frmThi : Form
    {

        

        public String lopSV;
        SqlDataAdapter adapter = new SqlDataAdapter();
        DataTable table = new DataTable();
        SqlCommand command;

        //tao class cau hoi
        void loadData()
        {
            String strComm = "";
            if (cmbChonMonThi.SelectedItem != null && cmbLanThi.SelectedItem == null && dateNgayThi.Text == " ")
            {
                strComm = "EXEC sp_LayThongTindangKyThiMon '"
                    + cmbChonMonThi.Text + "', '" + lopSV + "', '" + Program.MaSV + "'";
            }
            else if (cmbChonMonThi.SelectedItem == null && cmbLanThi.SelectedItem != null && dateNgayThi.Text == " ")
            {
                strComm = "SELECT  GVDK.MAMH,TENMH, MALOP, TRINHDO,NGAYTHI,LAN,SOCAUTHI,THOIGIAN FROM GIAOVIEN_DANGKY GVDK INNER JOIN (SELECT MAMH,TENMH FROM MONHOC) AS MH ON " 
                    +"GVDK.MAMH = MH.MAMH"+
                    " where malop = '" + lopSV + "' and lan = '"+ cmbLanThi.Text + "', '" + Program.MaSV + "'";
            }
            else if (cmbChonMonThi.SelectedItem == null && cmbLanThi.SelectedItem == null && dateNgayThi.Text != " ")
            {
                strComm = "EXEC sp_LayThongTindangKyThiNgay '"
                    + dateNgayThi.Text + "', '" + Program.MaSV + "'";
            }
            else if(cmbChonMonThi.SelectedItem != null && cmbLanThi.SelectedItem != null && dateNgayThi.Text == " ")
            {
                strComm = "EXEC sp_LayThongTindangKyThi '" + cmbChonMonThi.Text + "', '"
                    + lopSV + "', '" + cmbLanThi.Text + "', '" + Program.MaSV + "'";
            }
            try
            {
                command = Program.conn.CreateCommand();
                command.CommandText = strComm;
                adapter.SelectCommand = command;
                table.Clear();
                adapter.Fill(table);
                dtgvThongTinDeThi.DataSource = table;
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                return;
            }

        }
        public frmThi()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void frmThi_Load(object sender, EventArgs e)
        {

            try
            {
                String statement = "exec sp_ThongTinChiTietSinhVien '" + Program.userName + "'";
                Program.myReader = Program.ExecSqlDataReader(statement);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                return;
            }

            if (Program.myReader == null)
                return;
            Program.myReader.Read();
            lblMaSV.Text = Program.userName;
            lblHoTenSV.Text = Program.myReader.GetString(0);
            //lblNgaySinh.Text = Program.myReader.GetString(2);
            lblLop.Text = Program.myReader.GetString(2);
            lblKhoa.Text = Program.myReader.GetString(3);
            lopSV = Program.myReader.GetString(4);
            Program.myReader.Close();

            //DO CAC DU LIEU VAO CMBCHONMONHOC
            String statementCmbMonhoc = "select distinct MAMH from GIAOVIEN_DANGKY";
            Program.myReader = Program.ExecSqlDataReader(statementCmbMonhoc);
            if (Program.myReader == null)
                return;
            while (Program.myReader.Read())
            {
                cmbChonMonThi.Items.Add(Program.myReader.GetString(0));
            }
            Program.myReader.Close();
            //khoa btn Lam bai thi
            btnLamBai.Enabled = false;
            dateNgayThi.CustomFormat = " ";
            dateNgayThi.Format = DateTimePickerFormat.Custom;
            //loadData();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void btnTinDeThi_Click(object sender, EventArgs e)
        {
            loadData();
            dateNgayThi.Format = DateTimePickerFormat.Custom;
            dateNgayThi.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            btnLamBai.Enabled = false;  
        }

        private void dtgvThongTinDeThi_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int vitri;
            vitri = dtgvThongTinDeThi.CurrentRow.Index;
            Program.maDe = int.Parse(dtgvThongTinDeThi.Rows[vitri].Cells[0].Value.ToString());
            Program.maMonThi = cmbChonMonThi.Text = dtgvThongTinDeThi.Rows[vitri].Cells[1].Value.ToString();
            Program.monThi = dtgvThongTinDeThi.Rows[vitri].Cells[2].Value.ToString();
            cmbLanThi.Text = dtgvThongTinDeThi.Rows[vitri].Cells[6].Value.ToString();
            dateNgayThi.Text = dtgvThongTinDeThi.Rows[vitri].Cells[5].Value.ToString();
            Program.lan = int.Parse(cmbLanThi.Text);
            Program.trinhDo = dtgvThongTinDeThi.Rows[vitri].Cells[4].Value.ToString();
            Program.soCauHoi = int.Parse(dtgvThongTinDeThi.Rows[vitri].Cells[7].Value.ToString());
            Program.tgianLam = int.Parse(dtgvThongTinDeThi.Rows[vitri].Cells[8].Value.ToString());
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                dtgvThongTinDeThi.Rows[e.RowIndex].Selected = true;
            }
            if (DateTime.Parse(dateNgayThi.Text) <= DateTime.Now)
                btnLamBai.Enabled = true;
            else
                btnLamBai.Enabled = false;
        }



        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn đã sẵn sàng làm bài thi?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result == DialogResult.OK)
            {
                        frmLamBaiThi f = new frmLamBaiThi();
                        this.Hide();
                        f.ShowDialog();
                        
                    }
                }

        private void lblCmbLanThi_Click(object sender, EventArgs e)
        {

        }

        private void button1_MouseHover(object sender, EventArgs e)
        {
            btnLamBai.BackColor = Color.Gold;
        }

        private void btnLamBai_MouseLeave(object sender, EventArgs e)
        {
            btnLamBai.BackColor = Color.LightCoral;
        }

        private void btnTimDeThi_MouseHover(object sender, EventArgs e)
        {
            btnTimDeThi.BackColor = Color.PaleGreen;
        }

        private void btnTimDeThi_MouseLeave(object sender, EventArgs e)
        {
            btnTimDeThi.BackColor = Color.LemonChiffon;
        }

        private void lblTieuDe_Click(object sender, EventArgs e)
        {

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            cmbChonMonThi.SelectedItem = null;
            cmbLanThi.SelectedItem = null;
            dateNgayThi.CustomFormat = " ";
            dateNgayThi.Format = DateTimePickerFormat.Custom;
            dtgvThongTinDeThi.DataSource = null;
            btnLamBai.Enabled = false;

        }

        private void dateNgayThi_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dateNgayThi_Enter(object sender, EventArgs e)
        {
            dateNgayThi.Format = DateTimePickerFormat.Custom;
            dateNgayThi.CustomFormat = "yyyy-MM-dd HH:mm:ss";
        }
    }
        }



