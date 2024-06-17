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
        //private Order order;
        List<Order> orders;
        private decimal totalWithoutVat;
        private decimal Vat;
        private decimal tip;
        private decimal paid;
        /// </summary>
        private decimal unpaid;
        // private decimal temptotal;
        private string feedback;
        private decimal total;
        PaymentMethod paymentMethod;
        private Table table;
        public BillForm()
        {
            InitializeComponent();
            paymentService = new PaymentService();
            LoadComboBoxData();
            textBox1.TextChanged += new EventHandler(textBox1_TextChanged);
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

            if (OcuupiedTables.Count == 0)
            {
                OcuupiedTables.Add(new Table { TableId = 0 });
            }

            comboBox1.DataSource = OcuupiedTables;
            comboBox1.DisplayMember = "TableId"; // Sets the property to be displayed in the combo box
            comboBox1.ValueMember = "TableId";   //Sets the property that represents the actual value
            comboBox1.SelectedIndex = 0;

            comboBox2.DataSource = Enum.GetValues(typeof(PaymentMethod));
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            totalWithoutVat = 0;
            Vat = 0;
            paid = 0;
            total = 0;
            tip = 0;

            if (comboBox1.SelectedItem is Table table && table.TableId != 0)
            {
                orders = paymentService.GetUnpaidOrdersByTableId(table.TableId);
                listView1.Items.Clear();
                foreach (Order order in orders) // listview
                {
                    foreach (OrderItem item in order.items)
                    {
                        ListViewItem listItem = new ListViewItem(order.OrderId.ToString());
                        listItem.SubItems.Add(item.MenuItem.Name);
                        listItem.SubItems.Add(item.Count.ToString());
                        listItem.SubItems.Add(item.MenuItem.Price.ToString("€ 0.00"));
                        listView1.Items.Add(listItem);                                       

                        decimal itemTotalPrice = item.MenuItem.Price * item.Count;
                        totalWithoutVat += itemTotalPrice;

                        if (item.MenuItem.Category is Category.Beers || item.MenuItem.Category is Category.Wines || item.MenuItem.Category is Category.Spirit)
                        {
                            Vat += itemTotalPrice * 0.21m;
                        }
                        else
                        {
                            Vat += itemTotalPrice * 0.09m;
                        }
                        // Total += item.MenuItem.Price * item.Count + tip;
                    }
                }
                // unpaid = Total;
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
            ClearScreen();
        }
        private void ClearScreen()
        {
            // Clear text boxes
            textBox1.Text = string.Empty; // Tip
            textBox2.Text = string.Empty; // Feedback
            textBox3.Text = string.Empty; // Going Dutch

            // Reset combo boxes
            comboBox1.SelectedIndex = -1; // Table Number
            comboBox2.SelectedIndex = -1; // Payment Method

            // Clear ListView
            listView1.Items.Clear();

            // Clear labels
            labelTotalPrice.Text = "€ 0.00";
            labelVat.Text = "€ 0.00";
            lblTotalamount.Text = "€ 0.00";
            label8.Text = "€ 0.00";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                decimal goingDutchAmount = decimal.Parse(textBox3.Text);
                decimal tolerance = 0.01m; // Define a small tolerance for floating-point comparison to account for any minor differences due to floating-point precision errors.

                if (Math.Abs(unpaid - goingDutchAmount) > tolerance && unpaid - goingDutchAmount < 0)
                {
                    MessageBox.Show("The amount entered exceeds the unpaid amount. Please check the input.", "Payment Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                unpaid -= goingDutchAmount;
                UpdateUnpaidAmount();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void UpdateAmount()
        {


            total = totalWithoutVat + Vat + tip;
            unpaid = total;

            // Update the labels with the calculated values
            labelTotalPrice.Text = totalWithoutVat.ToString("€ 0.00");
            labelVat.Text = Vat.ToString("€ 0.00");
            lblTotalamount.Text = total.ToString("€ 0.00");
            label8.Text = unpaid.ToString("€ 0.00");
        }
        private void UpdateUnpaidAmount()
        {
            // Update only the unpaid amount
            label8.Text = unpaid.ToString("€ 0.00");
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
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

                UpdateAmount(); // Recalculate the total and unpaid amounts including the tip
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
