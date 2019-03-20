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
    public partial class SiparisDetaySorgulamaFormu : Form
    {
        public SiparisDetaySorgulamaFormu()
        {
            InitializeComponent();
        }
        //Butona tıklanınca textboxtaki OrderID ile birlikte sipariş detay formunu açan event
        private void btnOrderAra_Click(object sender, EventArgs e)
        {
            try
            {
                int newOrderID = Convert.ToInt32(txtOrderAra.Text);
                SiparisDetayFormu f3 = new SiparisDetayFormu(newOrderID);
                f3.Show();
                SiparisDetaySorgulamaFormu f4 = new SiparisDetaySorgulamaFormu();
                f4.Close();
                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
