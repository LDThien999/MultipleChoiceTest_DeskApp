using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using TNCSDLPT;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;

namespace CSDLPT.thi_dangkythi
{
    
    public partial class frmLamBaiThi : Form
    {
        // xuat cac vi tri ngau nhien cho dap an
        public static String[] GetRandomElements(String[] array, int m)
        {
            String[] result = new String[m];
            HashSet<int> selectedIndices = new HashSet<int>();
            for (int i = 0; i < m; i++)
            {
    
                int randomIndex;
                do
                {
                    randomIndex = new Random().Next(array.Length);
                } while (selectedIndices.Contains(randomIndex));
                selectedIndices.Add(randomIndex);
                result[i] = array[randomIndex];
            }

            // Trả về mảng kết quả
            return result;
        }
        int time;
        int maBangDiem = 0;
        String[] selects = new String[4];
       
        // tao mot class cau hoi de chua thong tin
        class CauHoi
        {
            public int maCH;
            public String noiDung;
            public String A;
            public String B;
            public String C;
            public String D;
            public String dapAn;
            public String dapAnChon;
            public float diemTungCau;

            public CauHoi(string noiDung, string a, string b, string c, string d, string dapAn)
            {
                this.noiDung = noiDung;
                this.A = a;
                this.B = b;
                this.C = c;
                this.D = d;
                this.dapAn = dapAn;

            }
        }
     
        // tao mot mang 1 chieu chua [n] cau hoi ( tuong ung voi 1 doi tuong )
        CauHoi[] deThi = new CauHoi[Program.soCauHoi];
        
        public String tempChon;
       
        public int index = 0; // luu lai vi tri cau hoi tren mang deThi
        int count = 0;
        public frmLamBaiThi()
        {
            InitializeComponent();
        }

        void checkBtnLuiTien()
        {
            if (index == 0)
                btnLuiCauHoi.Enabled = false;
            else
                btnLuiCauHoi.Enabled = true;
            if (index == Program.soCauHoi - 1)
                btnTienCauHoi.Enabled =false;
            else
                btnTienCauHoi.Enabled=true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            String statementThi = "";
            if(Program.role == "GIANGVIEN")
            {
                lblTieuDeBaiThi.Text = Program.monThi;
                lblMa.Text = Program.userName;
                lblTen.Text = "Giáo viên: "+Program.staff;
                lblThoiGian.Text = Program.tgianLam.ToString() + " phút";
                lblSoCH.Text = Program.soCauHoi.ToString();
                statementThi = "select bode.cauhoi,mamh,noidung,a,b,c,d,dap_an from bode inner join CT_DANGKYTHI " +
                    "on bode.cauhoi = CT_DANGKYTHI.CAUHOI where madk = '" + Program.maDe + "'";
            }
            else if (Program.khoiPhuc == 0 && Program.role == "Sinh viên")
            {
                lblTieuDeBaiThi.Text = Program.monThi;
                lblMa.Text = Program.userName;
                lblTen.Text = Program.staff;
                lblThoiGian.Text = Program.tgianLam.ToString() + " phút";
                lblSoCH.Text = Program.soCauHoi.ToString();

                //statementThi = "exec sp_XuatCauHoi '" + Program.soCauHoi + "', '"
                //    + Program.maMonThi + "', '" + Program.trinhDo + "'";
                statementThi = "select bode.cauhoi,mamh,noidung,a,b,c,d,dap_an from bode inner join CT_DANGKYTHI " +
                    "on bode.cauhoi = CT_DANGKYTHI.CAUHOI where madk = '"+Program.maDe+"'";
            }
            else
            {
                lblTieuDeBaiThi.Text = Program.monThi;
                lblMa.Text = Program.userName;
                lblTen.Text = Program.staff;
                lblThoiGian.Text = Program.tgianLam.ToString() + " phút";
                lblSoCH.Text = Program.soCauHoi.ToString();
                //lblThoiGian.Text = Program.tgianLam.ToString() + " phút";
                //lblSoCH.Text = Program.soCauHoi.ToString();

                statementThi = "select ctbt.cauhoi, mamh, noidung,a,b,c,d,dap_an,noidungdachon from bode inner join (select mabd,cauhoi, noidungdachon from ct_baithi) as ctbt " +
                    " on ctbt.cauhoi = bode.CAUHOI" +
                    " where mabd = " + Program.khoiPhuc;
            }

            using (SqlCommand cmd = new SqlCommand(statementThi, Program.conn))
            {
                cmd.CommandType = CommandType.Text;
                using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                {
                    DataSet ds = new DataSet();
                    sda.Fill(ds);
                    int i = 0;
                    // truyen du lieu tu database vao cac doi tuong, sau do cho vao mang deThi
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        int maCH = int.Parse(row["cauhoi"].ToString());
                        String noidung = row["noidung"].ToString();
                        String a = row["a"].ToString();
                        String b = row["b"].ToString();
                        String c = row["c"].ToString();
                        String d = row["d"].ToString();
                        String dapAn = row["dap_an"].ToString();
                        CauHoi ch = new CauHoi(noidung, a, b, c, d, dapAn);
                        if((Program.khoiPhuc ==0 && Program.role == "Sinh viên") || (Program.khoiPhuc == 0 && Program.role == "GIANGVIEN"))
                            ch.dapAnChon = "";
                        else if(Program.khoiPhuc != 0 && Program.role == "Sinh viên")
                            ch.dapAnChon = row["noidungdachon"].ToString();
                        ch.maCH = maCH;
                        deThi[i] = ch;
                        i++;
                    }
                    i = 0;

                    // tao cac button tro den cau hoi tuong ung
                    for(i=0; i < Program.soCauHoi; i++)
                    {
                        Button btn = new Button();
                        fpanShowAllCH.Controls.Add(btn);
                        btn.Name = "btnCau" + (i+1);
                        btn.Text = "" + (i+1);
                        btn.BackColor = Color.Silver;

                        btn.Click += CommonButtonClickHandler;
                    }
                    // hien thi cau 1 len man hinh
                    grbNoiDungCH.Text = "Câu "+ (index+1);
                    lblNoiDungCauHoi.Text = deThi[index].noiDung;
                    
                    selects[0] = deThi[index].A;
                    selects[1] = deThi[index].B;
                    selects[2] = deThi[index].C;
                    selects[3] = deThi[index].D;
                    String[] randomElements = GetRandomElements(selects, 4);
                    radA.Text = randomElements[0];//deThi[index].A;
                    radB.Text = randomElements[1];//deThi[index].B;
                    radC.Text = randomElements[2];//deThi[index].C;
                    radD.Text = randomElements[3];//deThi[index].D;
                    if (Program.khoiPhuc != 0)
                    {
                        showDapAnChon(deThi[index]);
                        Control[] controls = fpanShowAllCH.Controls.Find("btnCau" + (index + 1), true);
                        if (controls[0] is Button)
                        {
                            Button button = (Button)controls[0];

                            button.BackColor = Color.Gold;
                        }
                    }
                    lblCauHoiHienTai.Text = "Câu "+(index+1);

                    if(Program.khoiPhuc == 0 && Program.role == "Sinh viên")
                    {
                        SqlCommand command0 = new SqlCommand();
                        command0 = Program.conn.CreateCommand();
                        DateTime today = DateTime.Today;
                        String tmp;
                        int sub = 1;
                        try
                        {
                            foreach (CauHoi u in deThi)
                            {
                                tmp = "";


                                command0.CommandText = "exec sp_LuuDiem '" +
                                    Program.userName + "', '" + Program.maMonThi + "', " + Program.lan + ", '" + today + "', @mach = '" +
                                    u.maCH + "',@dachon = '" + tmp + "' ,@noidung= '" + u.dapAnChon + "',@sub = " + sub;
                                command0.ExecuteNonQuery();
                                sub = 0;

                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        if (Program.role == "Sinh viên")
                        {
                            Program.myReader = Program.ExecSqlDataReader("select top(1) MABD FROM BANGDIEM ORDER BY MABD DESC");
                            if (Program.myReader == null)
                                return;
                            Program.myReader.Read();
                            maBangDiem = Program.myReader.GetInt32(0);
                            Program.myReader.Close();
                        }
                        time = Program.tgianLam; // sua o day
                        
                        SqlCommand command = new SqlCommand();
                        command = Program.conn.CreateCommand();
                        if (Program.role == "Sinh viên")
                        {
                            if (Program.khoiPhuc == 0)
                            {
                                command.CommandText = "update BANGDIEM set THOIGIANCONLAI = " + Program.tgianLam.ToString()
                                    + " where mabd = " + maBangDiem;
                            }
                            else
                            {
                                command.CommandText = "update BANGDIEM set THOIGIANCONLAI = " + Program.tgianLam.ToString()
                                    + " where mabd = " + Program.khoiPhuc;
                            }
                            command.ExecuteNonQuery();
                        }
                    }
                    else if(Program.role == "Sinh viên" && Program.khoiPhuc !=0)
                    {
                        maBangDiem = Program.khoiPhuc;
                        loadAllControlFromPanel();
                    }
                    time = Program.tgianLam; // sua o day
                    checkBtnLuiTien();
                    if(Program.tgianLam <=9 )
                        lblMin.Text = "0" + Program.tgianLam.ToString();
                    else
                        lblMin.Text = Program.tgianLam.ToString();
                    timer1.Start(); // bat dau tinh gio lam bai
                    if(Program.role == "Sinh viên")
                        timeTgianCon.Start();
                }
            }
        }
        void loadAllControlFromPanel() // kiem tra cac button tro den cau hoi da lam hay chua
        {
            int indexTemp;
            foreach(Control control in fpanShowAllCH.Controls)
            {
               
                indexTemp = int.Parse((control.Text).ToString()) - 1 ;
             
                if (deThi[indexTemp].dapAnChon != "")
                    control.BackColor = Color.LightGreen;
                else
                    control.BackColor = Color.Silver;
                

            }
        }
        void ghiNhanCauTraLoi()
        {
            String tmp = "";
            String da = "";
            if (deThi[index].dapAnChon == deThi[index].A)
            {
                tmp = "A";
                da = deThi[index].A;

            }
            else if (deThi[index].dapAnChon == deThi[index].B)
            {
                tmp = "B";
                da = deThi[index].B;
            }
            else if (deThi[index].dapAnChon == deThi[index].C)
            {
                tmp = "C";
                da = deThi[index].C;
            }
            else if (deThi[index].dapAnChon == deThi[index].D)
            {
                tmp = "D";
                da = deThi[index].D;
            }
            SqlCommand command = new SqlCommand();
            command = Program.conn.CreateCommand();
            command.CommandText = "update CT_BAITHI set DAPANCHON ='" + tmp + "', NOIDUNGDACHON='" + da + "' "+
                "where cauhoi= " + deThi[index].maCH+" and mabd = "+ maBangDiem;
            command.ExecuteNonQuery();
        }
        private void CommonButtonClickHandler(object sender, EventArgs e)// khi click vao cac button tro, se show cau hoi vi tri tuong ung cho user
        {
            
            Control control = sender as Control;
            
            
            kiemTraRad();
            ghiNhanCauTraLoi();
            loadAllControlFromPanel();
            control.BackColor = Color.Gold;
            index = int.Parse((control.Text).ToString()) - 1;
            cleanRadioButton();
            inNoiDung(index);
            checkBtnLuiTien();

        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void radB_CheckedChanged(object sender, EventArgs e)
        {
            
            lblDapAnB.ForeColor = Color.Red;
            radB.ForeColor = Color.Red;
            //deThi[index].dapAnChon = radB.Text;

            lblDapAnA.ForeColor = Color.Black;
            radA.ForeColor = Color.Black;

            lblDapAnC.ForeColor = Color.Black;
            radC.ForeColor = Color.Black;

            lblDapAnD.ForeColor = Color.Black;
            radD.ForeColor = Color.Black;
        }

        private void radA_Click(object sender, EventArgs e)
        {
     

        }

        private void panDapAnChon_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panCauHoiThi_Paint(object sender, PaintEventArgs e)
        {

        }
        void inNoiDung(int index)
        {
            grbNoiDungCH.Text = "Câu " + (index + 1);
            lblNoiDungCauHoi.Text = deThi[index].noiDung;
            //radA.Text = deThi[index].A;
            //radB.Text = deThi[index].B;
            //radC.Text = deThi[index].C;
            //radD.Text = deThi[index].D;
            selects[0] = deThi[index].A;
            selects[1] = deThi[index].B;
            selects[2] = deThi[index].C;
            selects[3] = deThi[index].D;
            String[] randomElements = GetRandomElements(selects, 4);
            radA.Text = randomElements[0];//deThi[index].A;
            radB.Text = randomElements[1];//deThi[index].B;
            radC.Text = randomElements[2];//deThi[index].C;
            radD.Text = randomElements[3];//deThi[index].D;
            if (deThi[index].dapAnChon != "")
                showDapAnChon(deThi[index]);

            lblCauHoiHienTai.Text = "Câu " + (index + 1);
            
        }
        void kiemTraRad()
        {
            if (radA.Checked == true)
            {
                deThi[index].dapAnChon = radA.Text;
            }
            if (radB.Checked == true)
            {
                deThi[index].dapAnChon = radB.Text;
            }
            if (radC.Checked == true)
            {
                deThi[index].dapAnChon = radC.Text;
            }
            if (radD.Checked == true)
            {
                deThi[index].dapAnChon = radD.Text;
            }
        }
            private void radA_CheckedChanged(object sender, EventArgs e)
        {
            lblDapAnA.ForeColor = Color.Red;
            radA.ForeColor = Color.Red;
            //deThi[index].dapAnChon = radA.Text;

            lblDapAnB.ForeColor = Color.Black;
            radB.ForeColor = Color.Black;

            lblDapAnC.ForeColor = Color.Black;
            radC.ForeColor = Color.Black;

            lblDapAnD.ForeColor = Color.Black;
            radD.ForeColor = Color.Black;
        }
    

        private void radC_CheckedChanged(object sender, EventArgs e)
        {
            
            lblDapAnC.ForeColor = Color.Red;
            radC.ForeColor = Color.Red;
            //deThi[index].dapAnChon = radC.Text;

            lblDapAnB.ForeColor = Color.Black;
            radB.ForeColor = Color.Black;

            lblDapAnA.ForeColor = Color.Black;
            radA.ForeColor = Color.Black;

            lblDapAnD.ForeColor = Color.Black;
            radD.ForeColor = Color.Black;
        }

        private void radD_CheckedChanged(object sender, EventArgs e)
        {
            
            lblDapAnD.ForeColor = Color.Red;
            radD.ForeColor = Color.Red;
            //deThi[index].dapAnChon = radD.Text;

            lblDapAnB.ForeColor = Color.Black;
            radB.ForeColor = Color.Black;

            lblDapAnC.ForeColor = Color.Black;
            radC.ForeColor = Color.Black;

            lblDapAnA.ForeColor = Color.Black;
            radA.ForeColor = Color.Black;
        }
        void cleanRadioButton()
        {
            radA.Checked = false; radB.Checked = false;
            radC.Checked = false; radD.Checked = false ;
            lblDapAnB.ForeColor = Color.Black;
            radB.ForeColor = Color.Black;

            lblDapAnC.ForeColor = Color.Black;
            radC.ForeColor = Color.Black;

            lblDapAnA.ForeColor = Color.Black;
            radA.ForeColor = Color.Black;
            
            lblDapAnD.ForeColor = Color.Black;
            radD.ForeColor = Color.Black;
        }

        void showDapAnChon(CauHoi x)
        {
            if (Program.khoiPhuc == 0)
            {
                if (radA.Text == x.dapAnChon)
                {
                    lblDapAnA.ForeColor = Color.Red;
                    radA.ForeColor = Color.Red;
                    radA.Checked = true;
                }
                if (radB.Text == x.dapAnChon)
                {
                    lblDapAnB.ForeColor = Color.Red;
                    radB.ForeColor = Color.Red;
                    radB.Checked = true;
                }
                if (radC.Text == x.dapAnChon)
                {
                    lblDapAnC.ForeColor = Color.Red;
                    radC.ForeColor = Color.Red;
                    radC.Checked = true;
                }
                if (radD.Text == x.dapAnChon)
                {
                    lblDapAnD.ForeColor = Color.Red;
                    radD.ForeColor = Color.Red;
                    radD.Checked = true;
                }
            }
            else
            {
                if (nameof(x.A) == x.dapAn)
                {
                    lblDapAnA.ForeColor = Color.Red;
                    radA.ForeColor = Color.Red;
                    radA.Checked = true;
                }
                if (nameof(x.B) == x.dapAn)
                {
                    lblDapAnB.ForeColor = Color.Red;
                    radB.ForeColor = Color.Red;
                    radB.Checked = true;
                }
                if (nameof(x.C) == x.dapAn)
                {
                    lblDapAnC.ForeColor = Color.Red;
                    radC.ForeColor = Color.Red;
                    radC.Checked = true;
                }
                if (nameof(x.D) == x.dapAn)
                {
                    lblDapAnD.ForeColor = Color.Red;
                    radD.ForeColor = Color.Red;
                    radD.Checked = true;
                }
            }
        }
        private void btnTienCauHoi_Click(object sender, EventArgs e)
        {
            
            kiemTraRad();
            if(Program.role == "Sinh viên")
                ghiNhanCauTraLoi();
            loadAllControlFromPanel();
            index += 1;
            cleanRadioButton();
            inNoiDung(index);
            checkBtnLuiTien();
            Control[] controls = fpanShowAllCH.Controls.Find("btnCau"+(index+1), true);
            if (controls.Length > 0 && controls[0] is Button)
            {
                Button button = (Button)controls[0];
                
                button.BackColor = Color.Gold;
            }
            
        }

        private void btnLuiCauHoi_Click(object sender, EventArgs e)
        {
            
            kiemTraRad();
            if(Program.role == "Sinh viên")
                ghiNhanCauTraLoi();
            loadAllControlFromPanel();
            //if (deThi[index].dapAnChon != "")
            //    fpanShowAllCH.Controls.GetEnumerator(index);
            index -=1;
            cleanRadioButton();
            inNoiDung(index);
            checkBtnLuiTien();
            Control[] controls = fpanShowAllCH.Controls.Find("btnCau" + (index + 1), true);
            if (controls.Length > 0 && controls[0] is Button)
            {
                Button button = (Button)controls[0];

                button.BackColor = Color.Gold;
            }
        }

        void ghiDiemVaoDataBase(double diem)
        {
            SqlCommand command = new SqlCommand();
            command = Program.conn.CreateCommand();
            if (Program.khoiPhuc == 0)
            {
                command.CommandText = "update BANGDIEM set DIEM = " + diem
                    + " where mabd = " + maBangDiem;
            }
            else
            {
                command.CommandText = "update BANGDIEM set DIEM = " + diem
                    + " where mabd = " + Program.khoiPhuc;
            }
            command.ExecuteNonQuery();
        }
        private void btnTienCauHoi_MouseHover(object sender, EventArgs e)
        {
            if (!DesignMode)
                btnTienCauHoi.BackColor = Color.Gold;
            base.OnMouseHover(e);
        }

        private void btnTienCauHoi_MouseLeave(object sender, EventArgs e)
        {
            if (!DesignMode)
                btnTienCauHoi.BackColor = Color.Aquamarine;
            base.OnMouseHover(e);
        }

        private void btnLuiCauHoi_MouseHover(object sender, EventArgs e)
        {
            if (!DesignMode)
                btnLuiCauHoi.BackColor = Color.Gold;
            base.OnMouseHover(e);
        }

        private void btnLuiCauHoi_MouseLeave(object sender, EventArgs e)
        {
            if (!DesignMode)
                btnLuiCauHoi.BackColor = Color.Aquamarine;
            base.OnMouseHover(e);
        }


        
        private void timer1_Tick(object sender, EventArgs e)
        {
            count--;
            if(count <0)
            {
                time--;
                count = 59;
                if (time <= 9)
                    lblMin.Text = "0" + time.ToString();
                else
                    lblMin.Text = time.ToString();

            }
            if (count <= 9)
                lblSecond.Text = "0" + count.ToString();
            else
                lblSecond.Text = count.ToString();

            if (lblSecond.Text == "00" && lblMin.Text == "00" && Program.role == "Sinh viên")
            {
                kiemTraRad();
                ghiNhanCauTraLoi();
                Program.min = lblMin.Text;
                Program.sec = lblSecond.Text;
                Program.diemSo = tinhDiem();
                frmKetQuaThi f = new frmKetQuaThi();
                timer1.Stop();
                ghiDiemVaoDataBase(Program.diemSo);
                Program.khoiPhuc = 0;
                f.ShowDialog();
                this.Close();
            }
            else if (lblSecond.Text == "00" && lblMin.Text == "00" && Program.role == "GIANGVIEN")
            {
                kiemTraRad();
              
                frmKetQuaThi f = new frmKetQuaThi();
                timer1.Stop();
                Program.khoiPhuc = 0;
                f.ShowDialog();
                this.Close();
            }


        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private double tinhDiem()
        {
            double diem = 0;
            string tmp;
            double diemCoban = ((double)10 / (double)Program.soCauHoi);
            foreach(CauHoi i in deThi)
            {
                tmp = "";
                if (i.dapAnChon == i.A)
                    tmp = "A";
                else if (i.dapAnChon == i.B)
                    tmp = "B";
                else if (i.dapAnChon == i.C)
                    tmp = "C";
                else if (i.dapAnChon == i.D)
                    tmp = "D";
                if (i.dapAn == tmp)
                    diem = diem + diemCoban;
            }
            diem = Math.Round(diem,2);
            return diem;
        }
        //void ghiDiemVaoDataBase()
        //{
        //    SqlCommand command0 = new SqlCommand();
        //    command0 = Program.conn.CreateCommand();
        //    DateTime today = DateTime.Today;
        //    String tmp;
        //    int sub = 1;
        //    try
        //    {
        //        foreach (CauHoi i in deThi)
        //        {
        //            tmp = "";
        //            if (i.dapAnChon == i.A)
        //                tmp = "A";
        //            else if (i.dapAnChon == i.B)
        //                tmp = "B";
        //            else if (i.dapAnChon == i.C)
        //                tmp = "C";
        //            else if (i.dapAnChon == i.D)
        //                tmp = "D";

        //            command0.CommandText = "exec sp_LuuDiem '" +
        //                Program.userName + "', '" + Program.maMonThi + "', " + Program.lan + ", '" + today + "', '" + Program.diemSo + "', " +
        //                i.maCH + ", '" + tmp + "', '" + i.dapAnChon + "', " + sub + "";
        //            command0.ExecuteNonQuery();
        //            sub = 0;

        //        }
        //    }
        //    catch(Exception ex)
        //    {
        //        MessageBox.Show(ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        return;
        //    }
            
        //}
        private void btnNopBai_Click(object sender, EventArgs e)
        {
            if (Program.role == "Sinh viên")
            {
                kiemTraRad();
                ghiNhanCauTraLoi();
                int soCauChuaLam = 0;
                foreach (Control control in fpanShowAllCH.Controls)
                {

                    if (deThi[int.Parse(control.Text) - 1].dapAnChon == "")
                        soCauChuaLam++;
                }
                if (soCauChuaLam > 0)
                {
                    DialogResult result = MessageBox.Show("Bạn còn " + soCauChuaLam + " câu chưa làm, vẫn muốn nộp bài?", "NỘP BÀI?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        Program.min = lblMin.Text;
                        Program.sec = lblSecond.Text;
                        Program.diemSo = tinhDiem();
                        frmKetQuaThi f = new frmKetQuaThi();
                        timer1.Stop();
                        ghiDiemVaoDataBase(Program.diemSo);
                        Program.khoiPhuc = 0;
                        f.ShowDialog();
                        this.Close();
                    }
                }
                else
                {
                    DialogResult result = MessageBox.Show("Bạn có chắn chắn muốn nộp bài?", "NỘP BÀI?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        Program.min = lblMin.Text;
                        Program.sec = lblSecond.Text;
                        Program.diemSo = tinhDiem();
                        frmKetQuaThi f = new frmKetQuaThi();
                        timer1.Stop();
                        ghiDiemVaoDataBase(Program.diemSo);
                        Program.khoiPhuc = 0;
                        f.ShowDialog();

                        this.Close();
                    }
                }
            }
            else if(Program.role == "GIANGVIEN")
            {
                kiemTraRad();
               
                int soCauChuaLam = 0;
                foreach (Control control in fpanShowAllCH.Controls)
                {

                    if (deThi[int.Parse(control.Text) - 1].dapAnChon == "")
                        soCauChuaLam++;
                }
                if (soCauChuaLam > 0)
                {
                    DialogResult result = MessageBox.Show("Bạn còn " + soCauChuaLam + " câu chưa làm, vẫn muốn nộp bài?", "NỘP BÀI?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        Program.min = lblMin.Text;
                        Program.sec = lblSecond.Text;
                        Program.diemSo = tinhDiem();
                        frmKetQuaThi f = new frmKetQuaThi();
                        timer1.Stop();
                        
                        Program.khoiPhuc = 0;
                        f.ShowDialog();
                        this.Close();
                    }
                }
                else
                {
                    DialogResult result = MessageBox.Show("Bạn có chắn chắn muốn nộp bài?", "NỘP BÀI?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                       
                        Program.diemSo = tinhDiem();
                        frmKetQuaThi f = new frmKetQuaThi();
                        timer1.Stop();
                      
                        Program.khoiPhuc = 0;
                        f.ShowDialog();

                        this.Close();
                    }
                }
            }
        }

        private void lblTen_Click(object sender, EventArgs e)
        {

        }

        private void frmLamBaiThi_Leave(object sender, EventArgs e)
        {
        }

        private void frmLamBaiThi_FormClosing(object sender, FormClosingEventArgs e)
        {

            Program.khoiPhuc = 0;
        }

        private void lblTieuDeBaiThi_Click(object sender, EventArgs e)
        {

        }

        private void timeTgianCon_Tick(object sender, EventArgs e)
        {
            Program.tgianLam--;
            SqlCommand command = new SqlCommand();
            command = Program.conn.CreateCommand();
            if(Program.khoiPhuc == 0)
            {
                command.CommandText = "update BANGDIEM set THOIGIANCONLAI = " + Program.tgianLam.ToString()
                    + " where mabd = " + maBangDiem;
            }
            else
            {
                command.CommandText = "update BANGDIEM set THOIGIANCONLAI = " + Program.tgianLam.ToString()
                    + " where mabd = " + Program.khoiPhuc;
            }
            command.ExecuteNonQuery();

        }
    }
}
