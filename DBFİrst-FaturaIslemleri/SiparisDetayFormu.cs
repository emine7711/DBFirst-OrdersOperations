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
    public partial class SiparisDetayFormu : Form
    {  
        NorthwindEntities ctx = new NorthwindEntities();
        //Önceki formlardan aktarılan OrderID ve Order static değişkenlerinin tanımlanması ve atanması
        private int gelenOrderID;
        private Order gelenOrder;

        public SiparisDetayFormu(int orderID)
        {
            gelenOrderID = orderID;
            gelenOrder = ctx.Orders.Find(gelenOrderID);
            InitializeComponent();
        }
        //Order Detaylarını gösteren datagridviewi dolduran metot
        private void GetOrderDetails()
        {
            var list = ctx.Order_Details.Where(x => x.OrderID == gelenOrderID).Select(x => new
            {
                x.OrderID,
                x.ProductID,
                x.Product.ProductName,
                x.UnitPrice,
                x.Quantity,
                x.Discount,
                Total = x.Quantity * x.UnitPrice
            }).ToList();
            dgOrderDetail.DataSource = list;
            txtToplamsiparis.Text = list.Sum(x=>x.Total).ToString();
        }
        //Order Detaya ürün eklemeye yarayan combobox ı ürün adlarıyla dolduran metot
        private void GetProducts()
        {
            var list = ctx.Products.Select(x => x).ToList();
            cbProducts.DisplayMember = "ProductName";
            cbProducts.ValueMember = "ProductID";
            cbProducts.DataSource = list;
        }
        //Order ın müşterilerini seçemeye yarayan combobox ı dolduran metot
        private void GetCustomers()
        {
            var list = ctx.Customers.Select(x => new
            {
                x.CustomerID,
                x.CompanyName
            }).ToList();
            cbCustomer.DisplayMember = "CompanyName";
            cbCustomer.ValueMember = "CustomerID";
            cbCustomer.DataSource = list;
        }
        //Order ın Employee lerini seçmeye yarayan combobox ı dolduran metot
        private void GetEmployee()
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
        //Order ın Shipperlarını seçmeye yarayan combobox ı dolduran metot
        public void GetShippers()
        {
            var shipperList = ctx.Shippers.Select(x => x).ToList();
            cbShipVia.DisplayMember = "CompanyName";
            cbShipVia.ValueMember = "ShipperID";
            cbShipVia.DataSource = shipperList;
        }
        //Order silindiğinde order a ait tüm alanları boşaltan metot
        public void DelOrders()
        {
            txtOrderID.Text = " ";
            cbCustomer.Text = " ";
            cbEmployee.Text = " ";
            dtpOrderDate.Value = DateTime.Now;
            dtpRequiredDate.Value = DateTime.Now;
            cbShipVia.Text = " ";
            txtFreight.Text = " ";
            txtAddress.Text = " ";
            txtCity.Text = " ";
        }
        //Önceki formlardan gelen OrderID ye göre Order a ait alanları dolduran metot
        public void GetOrderHeader()
        {
            txtOrderID.Text = gelenOrder.OrderID.ToString();
            cbCustomer.SelectedValue = gelenOrder.CustomerID;
            cbEmployee.SelectedValue = gelenOrder.EmployeeID;
            cbShipVia.SelectedValue = gelenOrder.ShipVia;
            dtpOrderDate.Value = Convert.ToDateTime(gelenOrder.OrderDate);
            dtpRequiredDate.Value = Convert.ToDateTime(gelenOrder.RequiredDate);
            txtFreight.Text = gelenOrder.Freight.ToString();
            txtAddress.Text = gelenOrder.ShipAddress;
            txtCity.Text = gelenOrder.ShipCity;
        }
        private void Form3_Load(object sender, EventArgs e)
        {
            GetOrderDetails();
            GetCustomers();
            GetEmployee();
            GetShippers();
            GetProducts();
            GetOrderHeader();
        }
        //Farklı bir OrderID girildiğinde ona ait bilgileri getiren buton
        private void btnGetOrder_Click(object sender, EventArgs e)
        {
            try
            {
                gelenOrderID = Convert.ToInt32(txtOrderID.Text);
                gelenOrder = ctx.Orders.Find(gelenOrderID);
                cbCustomer.SelectedValue = gelenOrder.CustomerID;
                cbEmployee.SelectedValue = gelenOrder.EmployeeID;
                cbShipVia.SelectedValue = gelenOrder.ShipVia;
                dtpOrderDate.Value = Convert.ToDateTime(gelenOrder.OrderDate);
                dtpRequiredDate.Value = Convert.ToDateTime(gelenOrder.RequiredDate);
                txtFreight.Text = gelenOrder.Freight.ToString();
                txtAddress.Text = gelenOrder.ShipAddress;
                txtCity.Text = gelenOrder.ShipCity;
                GetOrderDetails();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //textboxtaki OrderID için diğer bilgileri güncellemeye yarayan buton
        public static int selectedID, selectedProID;
        private void btnOrderUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                selectedID = Convert.ToInt32(txtOrderID.Text);
                var order = ctx.Orders.Where(x => x.OrderID == selectedID).FirstOrDefault();
                order.CustomerID = Convert.ToString(cbCustomer.SelectedValue);
                order.EmployeeID = Convert.ToInt32(cbEmployee.SelectedValue);
                order.OrderDate = dtpOrderDate.Value;
                order.RequiredDate = dtpRequiredDate.Value;
                order.ShipVia = Convert.ToInt32(cbShipVia.SelectedValue);
                order.Freight = Convert.ToDecimal(txtFreight.Text);
                ctx.SaveChanges();
                MessageBox.Show("Güncellendi");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //textboxtaki OrderID yi detayları ile birlikte silmeye yarayan buton
        private void btnOrderDelete_Click(object sender, EventArgs e)
        {
            try
            {
                selectedID = Convert.ToInt32(txtOrderID.Text);
                var order = ctx.Orders.Where(x => x.OrderID == selectedID).FirstOrDefault();
                if (MessageBox.Show("Detaylarla birlikte silinecek.Onaylıyor musunuz?", "Onay",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    ctx.Orders.Remove(order);
                    ctx.SaveChanges();
                    MessageBox.Show("Silindi");
                    GetOrderDetails();
                    DelOrders();
                };
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //textboxtaki OrderID için Order Detaylarını güncellemeye yarayan buton
        private void btnODUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                selectedID = Convert.ToInt32(dgOrderDetail.CurrentRow.Cells[0].Value);
                selectedProID = Convert.ToInt32(dgOrderDetail.CurrentRow.Cells[1].Value);
                var orderDet = ctx.Order_Details.Find(selectedID, selectedProID);
                ctx.Order_Details.Remove(orderDet);
                Order_Detail od = new Order_Detail();
                od.OrderID = Convert.ToInt32(txtOrderID.Text);
                od.ProductID = Convert.ToInt32(cbProducts.SelectedValue);
                od.Quantity = Convert.ToInt16(txtQuantity.Text);
                od.UnitPrice = Convert.ToDecimal(txtUnitPrice.Text);
                ctx.Order_Details.Add(od);
                ctx.SaveChanges();
                GetOrderDetails();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //textboxtaki OrderID için yeni bir Order Detay eklemeye yarayan buton
        private void btnODInsert_Click(object sender, EventArgs e)
        {
            try
            {
                Order_Detail od = new Order_Detail();
                od.OrderID = Convert.ToInt32(txtOrderID.Text);
                od.ProductID = Convert.ToInt32(cbProducts.SelectedValue);
                od.Quantity = Convert.ToInt16(txtQuantity.Text);
                od.UnitPrice = Convert.ToDecimal(txtUnitPrice.Text);
                ctx.Order_Details.Add(od);
                ctx.SaveChanges();
                GetOrderDetails();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //textboxtaki OrderID için detay silmeye yarayan buton
        private void btnODDelete_Click(object sender, EventArgs e)
        {
            try
            {
                selectedID = Convert.ToInt32(dgOrderDetail.CurrentRow.Cells[0].Value);
                selectedProID = Convert.ToInt32(dgOrderDetail.CurrentRow.Cells[1].Value);
                var p = ctx.Order_Details.Where(x => x.OrderID == selectedID && x.ProductID == selectedProID).FirstOrDefault();
                ctx.Order_Details.Remove(p);
                ctx.SaveChanges();
                GetOrderDetails();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //datagridviewdeki herhangi bir hücreye tıklandığında o hücrenin bulunduğu satırdaki bilgileri 
        //ilgili alanlara atan datagrid eventi
        private void dgOrderDetail_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                selectedID = Convert.ToInt32(dgOrderDetail.CurrentRow.Cells[0].Value);
                selectedProID = Convert.ToInt32(dgOrderDetail.CurrentRow.Cells[1].Value);
                var p = ctx.Order_Details.Where(x => x.OrderID == selectedID && x.ProductID == selectedProID).FirstOrDefault();
                txtQuantity.Text = p.Quantity.ToString();
                cbProducts.SelectedValue = p.ProductID;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
