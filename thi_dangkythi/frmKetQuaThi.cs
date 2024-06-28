using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TNCSDLPT;

namespace CSDLPT.thi_dangkythi
{
    public partial class frmKetQuaThi : Form
    {
        public frmKetQuaThi()
        {
            InitializeComponent();
        }

        private void frmKetQuaThi_Load(object sender, EventArgs e)
        {
            lblDiem.Text = Math.Round(Program.diemSo, 1).ToString() + "/10";
            lblSoCauDung.Text = (Math.Round(Program.diemSo * Program.soCauHoi/ 10,0)).ToString() + "/" + Program.soCauHoi;
            String  m = (Program.tgianLam - int.Parse(Program.min) - 1>9) ?(Program.tgianLam - int.Parse(Program.min) - 1).ToString() : "0" + (Program.tgianLam - int.Parse(Program.min) - 1).ToString();
            String s = (60 - int.Parse(Program.sec)>9) ?(60 - int.Parse(Program.sec)).ToString() : "0" + (60 - int.Parse(Program.sec)).ToString();
            lblThoiGianLam.Text = m + " : " + s;


        }
    }
}
