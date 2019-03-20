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
    public partial class SiparisEklemeFormu : Form
    {
        public SiparisEklemeFormu()
        {
            InitializeComponent();
        }
        NorthwindEntities ctx = new NorthwindEntities();

        //Customer combobox ını müşteri listesiyle dolduran metot
        public void GetCustomers()
        {
            var customerList = ctx.Customers.Select(x => x).ToList();
            cbCustomer.DisplayMember = "CompanyName";
            cbCustomer.ValueMember = "CustomerID";
            cbCustomer.DataSource = customerList;
        }
        //Employee comboboxını işçi listesi ile dolduran metot
        public void GetEmployees()
        {
            var employeeList = ctx.Employees.Select(x => new
            {
            ad = x.FirstName + " " + x.LastName,
            ID = x.EmployeeID
            }).ToList();
            cbEmployee.DisplayMember = "ad";
            cbEmployee.ValueMember = "ID";
            cbEmployee.DataSource = employeeList;
        }
        //Shipper comboboxını kargo şirketi listesi ile dolduran metot
        public void GetShippers()
        {
            var shipperList = ctx.Shippers.Select(x => x).ToList();
            cbShipvia.DisplayMember = "CompanyName";
            cbShipvia.ValueMember = "ShipperID";
            cbShipvia.DataSource = shipperList;
        }
        //müşteri seçildiğinde müşterinin adres ve şehir bilgisini textboxlara atan event
        private void cbCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            var adres = ctx.Customers.Where(x => x.CustomerID == cbCustomer.SelectedValue.ToString())
                                     .Select(x => new
                                     { AdressInfo = x.Address,
                                       CityInfo = x.City });
            foreach (var item in adres)
            {
                txtAddress.Text = item.AdressInfo.ToString();
                txtCity.Text = item.CityInfo.ToString();
            }
        }
        private void Form2_Load(object sender, EventArgs e)
        {
            GetCustomers();
            GetEmployees();
            GetShippers();
        }
        //butona tıklandığında ilgili alanlarda yer alan bilgilerle Order tablosuna yeni bir kayıt atan event
        private void btnCreateOrder_Click(object sender, EventArgs e)
        {
            try
            {
                Order newOrder = new Order();
                newOrder.CustomerID = Convert.ToString(cbCustomer.SelectedValue);
                newOrder.EmployeeID = Convert.ToInt32(cbEmployee.SelectedValue);
                newOrder.OrderDate = dtpOrderDate.Value;
                newOrder.RequiredDate = dtpRequiredDate.Value;
                newOrder.ShipVia = Convert.ToInt32(cbShipvia.SelectedValue);
                newOrder.Freight = Convert.ToDecimal(txtFreight.Text);
                newOrder.ShipAddress = txtAddress.Text;
                newOrder.ShipCity = txtCity.Text;
                ctx.Orders.Add(newOrder);
                ctx.SaveChanges();
                SiparisDetayFormu f3 = new SiparisDetayFormu(newOrder.OrderID);
                f3.Show();
                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
