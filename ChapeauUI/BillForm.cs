using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ChapeauModel;
using ChapeauService;

namespace ChapeauUI
{
    public partial class BillForm : Form
    {
        private PaymentService paymentService;
        private Bill bill;
        private Order order;
        List<Order> orders;
        private decimal total;
        private decimal Vat;
        private decimal tip;
        private decimal paid;
        private decimal unpaid;
        private decimal temptotal;
        private string feedback;
        PaymentMethod paymentMethod;
        private Table table;
        public BillForm()
        {
            InitializeComponent();
            paymentService = new PaymentService();
            LoadComboBoxData();
        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        private void LoadComboBoxData()
        {
            List<Table> OcuupiedTables = paymentService.GetAllOccupiedTables();
            comboBox1.DataSource = OcuupiedTables;
            comboBox1.DisplayMember = "TableId";
            comboBox1.SelectedIndex = 0;

            comboBox2.DataSource = Enum.GetValues(typeof(PaymentMethod));
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            total = 0;
            Vat = 0;
            paid = 0;
            Table table = (Table) comboBox1.SelectedItem;
            orders = paymentService.GetUnpaidOrdersByTableId(table.TableId);
            listView1.Items.Clear();
            foreach (Order order in orders) // fill the listview with ordered items
            {
                foreach (OrderItem item in order.items)
                {
                    ListViewItem Listitem = new ListViewItem(order.OrderId.ToString());
                    Listitem.SubItems.Add(item.MenuItem.Name);
                    Listitem.SubItems.Add(item.Count.ToString());
                    Listitem.SubItems.Add(item.MenuItem.Price.ToString("€ 0.00"));
                    listView1.Items.Add(Listitem);
                    if (item.MenuItem.Category is Category.Beers || item.MenuItem.Category is Category.Wines)
                    {
                        Vat = Vat + item.MenuItem.Price * item.Count * 0.21m;
                    }
                    else
                    {
                        Vat = Vat + item.MenuItem.Price * item.Count * 0.09m;
                    }
                    total = total + item.MenuItem.Price * item.Count;
                }
            }
            unpaid = total;
            UpdateAmount();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // Parse and handle tip input
                if (string.IsNullOrWhiteSpace(textBox1.Text))
                {
                    tip = 0; // Default to 0 if input is empty
                }
                else
                {
                    tip = decimal.Parse(textBox1.Text);
                }

                // Handle feedback input
                feedback = string.IsNullOrWhiteSpace(textBox2.Text) ? string.Empty : textBox2.Text;

                // Get the selected payment method
                paymentMethod = (PaymentMethod)comboBox2.SelectedItem;

                // Create a new Bill object with the collected data
                bill = new Bill(orders, total, tip, paymentMethod, DateTime.Today, DateTime.Now.TimeOfDay, feedback);

                // Finalize the payment
                paymentService.FinalizePayment(bill);

                // Mark each order as paid
                foreach (Order order in orders)
                {
                    paymentService.MarkOrderAsPaid(order.OrderId);
                }

                // Change the table status to free
                table = (Table)comboBox1.SelectedItem;
                paymentService.ChangeTableStatusToFree(table.TableId);

                MessageBox.Show("Payment finalized and orders marked as paid.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void button2_Click(object sender, EventArgs e)
        {

            paid = decimal.Parse(textBox3.Text);
            if (temptotal - paid < 0)
            {
                MessageBox.Show("The amount paid exceeds the total amount. Please check the payment amount.", "Payment Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            unpaid = temptotal - paid;
            UpdateAmount();

        }
        public void UpdateAmount()
        {
            labelTotalPrice.Text = total.ToString("€ 0.00");
            labelVat.Text = Vat.ToString("€ 0.00");

            temptotal = unpaid;



            label8.Text = unpaid.ToString("€ 0.00");
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
