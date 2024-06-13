using ChapeauModel;
using ChapeauService;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace ChapeauUI
{
    public partial class TableInfoFormDetail : Form
    {
        private OverviewService overviewService;
        private Table table;
        public TableInfoFormDetail(Table table)
        {
            InitializeComponent();
            overviewService = new OverviewService();
            this.table = table;
            LoadComboBoxData();
        }
        public TableInfoFormDetail()
        {
            InitializeComponent();

        }
        private void LoadComboBoxData()
        {
            List<int> orderIds = overviewService.GetRunningOrderIdsByTableId(table.TableId);
            List<Order> orders = new List<Order>();
            foreach (int orderId in orderIds)
            {
                Order order = overviewService.GetOrderById(orderId);
                if (order != null) // Ensure order is not null before adding to the list
                {
                    orders.Add(order);
                }
            }
            if (orders.Count == 0)
            {

                DialogResult result = MessageBox.Show("There are no orders for this table.Set this table status to 'Free'?", "No Orders", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (result == DialogResult.Yes)
                {
                    overviewService.ChangeTableStatusToFree(table.TableId);
                }

            }


            comboBox1.DataSource = orders;
            comboBox1.DisplayMember = "OrderId"; // Assuming OrderId is a public int property in the Order class

        }


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Order order = comboBox1.SelectedItem as Order;
            if (order != null)
            {
                label3.Text = order.Status.ToString();
                label4.Text = CalculateTimeDifference(order);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Order order = comboBox1.SelectedItem as Order;

            if (order == null)
            {
                MessageBox.Show("Please select an order from the dropdown list.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string message = $"Are you sure you want to change the status of Order {order.OrderId} to Served?";
            string title = "Confirm Status Change";

            DialogResult result = MessageBox.Show(message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                overviewService.ChangeOrderStatusToServed(order.OrderId);
                MessageBox.Show("Status Changed to Served!", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            LoadComboBoxData();
        }
        public string CalculateTimeDifference(Order order)
        {
            string output = "";

            foreach (var item in order.items)
            {

                TimeSpan difference = DateTime.Now - item.OrderTime;
                double minutesDifference = difference.TotalMinutes;
                int waittime = (int)difference.TotalMinutes;
                string time = $"Wait time: {waittime}mins";

                string formattedOutput = $"{item.MenuItem.Name} x{item.Count} ";
                string formattedOutput1 = formattedOutput.PadRight(30) + time;
                output += formattedOutput1 + "\n";
            }
            return output;
        }
    }
}
