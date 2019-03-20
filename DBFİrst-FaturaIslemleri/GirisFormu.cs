using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DBFirst_FaturaIslemleri
{
    public partial class GirisFormu : Form
    {
        public GirisFormu()
        {
            InitializeComponent();
        }

        private void siparişEkleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SiparisEklemeFormu f2 = new SiparisEklemeFormu();
            f2.Show();
            GirisFormu f1 = new GirisFormu();
            f1.Close();
            this.Hide();
        }

        private void siparişDetayıEkleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SiparisDetaySorgulamaFormu f4 = new SiparisDetaySorgulamaFormu();
            f4.Show();
            GirisFormu f1 = new GirisFormu();
            f1.Close();
            this.Hide();
        }
    }
}
