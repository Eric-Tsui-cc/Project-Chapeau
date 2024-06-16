using System;
using System.Collections.Generic;
using System.Linq;
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
        private List<Order> orders;
        private decimal total;
        private decimal Vat;
        private decimal tip;
        private decimal paid;
        private decimal unpaid;
        private decimal temptotal;
        private string feedback;
        private PaymentMethod paymentMethod;
        private Table table;

        public BillForm()
        {
            InitializeComponent();
            paymentService = new PaymentService();
            LoadComboBoxData();
            textBox3.TextChanged += textBox3_TextChanged;
        }

        private void label12_Click(object sender, EventArgs e) { }

        private void panel1_Paint(object sender, PaintEventArgs e) { }

        private void LoadComboBoxData()
        {
            List<Table> occupiedTables = paymentService.GetAllOccupiedTables();

            if (occupiedTables.Count == 0)
            {
                occupiedTables.Add(new Table { TableId = -1 });
            }

            comboBox1.DataSource = occupiedTables;
            comboBox1.DisplayMember = "TableId";
            comboBox1.ValueMember = "TableId";
            comboBox1.SelectedIndex = 0;

            comboBox2.DataSource = Enum.GetValues(typeof(PaymentMethod));
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            total = 0;
            Vat = 0;
            paid = 0;

            if (comboBox1.SelectedItem is Table selectedTable && selectedTable.TableId != -1)
            {
                orders = paymentService.GetUnpaidOrdersByTableId(selectedTable.TableId);
                listView1.Items.Clear();
                foreach (Order order in orders)
                {
                    foreach (OrderItem item in order.items)
                    {
                        ListViewItem listItem = new ListViewItem(order.OrderId.ToString());
                        listItem.SubItems.Add(item.MenuItem.Name);
                        listItem.SubItems.Add(item.Count.ToString());
                        listItem.SubItems.Add(item.MenuItem.Price.ToString("€ 0.00"));
                        listView1.Items.Add(listItem);

                        if (item.MenuItem.Category is Category.Beers || item.MenuItem.Category is Category.Wines)
                        {
                            Vat += item.MenuItem.Price * item.Count * 0.21m;
                        }
                        else
                        {
                            Vat += item.MenuItem.Price * item.Count * 0.09m;
                        }
                        total += item.MenuItem.Price * item.Count;
                    }
                }
                unpaid = total;
                UpdateAmount();
            }
            else
            {
                listView1.Items.Clear();
                MessageBox.Show("There are no occupied tables available for billing.", "No Occupied Tables", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(textBox1.Text))
                {
                    tip = 0;
                }
                else
                {
                    tip = decimal.Parse(textBox1.Text);
                }

                feedback = string.IsNullOrWhiteSpace(textBox2.Text) ? string.Empty : textBox2.Text;
                paymentMethod = (PaymentMethod)comboBox2.SelectedItem;
                bill = new Bill(orders, total, tip, paymentMethod, DateTime.Today, DateTime.Now.TimeOfDay, feedback);
                paymentService.FinalizePayment(bill);

                foreach (Order order in orders)
                {
                    paymentService.MarkOrderAsPaid(order.OrderId);
                }

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
            try
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
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void UpdateAmount()
        {
            labelTotalPrice.Text = total.ToString("€ 0.00");
            labelVat.Text = Vat.ToString("€ 0.00");
            temptotal = total - paid;
            unpaid = temptotal;

            if (temptotal < 0)
            {
                MessageBox.Show("Total amount cannot be less than zero.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                temptotal = 0;
            }

            label8.Text = unpaid.ToString("€ 0.00");
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            MessageBox.Show("Text changed event triggered");

            decimal originalTotal = total;
            if (decimal.TryParse(textBox1.Text, out decimal parsedTip))
            {
                tip = parsedTip;
                total = originalTotal + tip;
                UpdateAmount();
            }
            else
            {
                tip = 0;
                total = originalTotal;
                UpdateAmount();
            }
        }
    }
}
