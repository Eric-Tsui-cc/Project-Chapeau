using ChapeauModel;
using ChapeauService;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChapeauUI
{
    public partial class MakeOrderPage : Form
    {
        private OrderService orderService;
        private Order order;
        private string summary;
        private OrderItem orderitem;
        public MakeOrderPage()
        {
            InitializeComponent();
            orderService = new OrderService();
            order = new Order();
            order.items = new List<OrderItem>();
            order.Table = new Table();
            order.Employee = new Employee();
            LoadComboBoxData();
        }

        private void LoadComboBoxData()
        {
            // Load cards into comboBox1
            List<string> cards = orderService.GetAllCards();
            comboBox1.DataSource = cards;

            // Load categories into comboBox2
            List<string> categories = orderService.GetAllCategories();
            comboBox2.DataSource = categories;

            // Load quantities into comboBox3
            List<int> quantities = new List<int>();
            for (int i = 0; i <= 20; i++)
            {
                quantities.Add(i);
            }
            comboBox3.DataSource = quantities;
            // Load quantities into comboBox4
            List<Employee> employees = orderService.GetAllActiveEmployees();
            comboBox4.DataSource = employees;
            comboBox4.DisplayMember = "FirstName";
            // Load quantities into comboBox5
            List<Table> tables = orderService.GetAllFreeTables();
            comboBox5.DataSource = tables;
            comboBox5.DisplayMember = "TableId";
            // Load quantities into comboBox6
            List<MenuItem> items = orderService.GetAllMenuItems();
            comboBox6.DataSource = items;
            comboBox6.DisplayMember = "Name";
            label6.Text = " ";

        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshDishes();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshDishes();
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Handle selected index change for comboBox4 if needed
        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedCard = comboBox1.SelectedItem.ToString();
            string selectedCategory = comboBox2.SelectedItem.ToString();
            List<MenuItem> menuItems = orderService.GetMenuItemGetByCardAndCategory(selectedCard, selectedCategory);
            comboBox6.DataSource = menuItems;
            comboBox6.DisplayMember = "Name";
            comboBox6.Refresh();
        }
        private void RefreshDishes()
        {
            if (comboBox1.SelectedItem != null && comboBox2.SelectedItem != null)
            {
                string selectedCard = comboBox1.SelectedItem.ToString();
                string selectedCategory = comboBox2.SelectedItem.ToString();

                List<MenuItem> menuItems = orderService.GetMenuItemGetByCardAndCategory(selectedCard, selectedCategory);

                comboBox6.DataSource = menuItems;
                comboBox6.DisplayMember = "Name";
                comboBox6.Refresh();

                // Reset quantity to 0
                comboBox3.Refresh();
            }
        }

        private void FinishOrderButton_Click(object sender, EventArgs e)
        {
            order.Status = StatusOfOrder.Running;
            order.OrderId = OrderIdGenerator.GetNextOrderId();
            order.Employee = (Employee)comboBox4.SelectedItem;
            order.Table = (Table)comboBox5.SelectedItem;
            orderService.AddOrder(order);
            orderService.ChangeTableStatusToOccupied(order.Table.TableId);
            MessageBox.Show("Order already added！", "Confirm", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadComboBoxData();

        }

        private void AddItemButton_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem == null || comboBox2.SelectedItem == null || comboBox6.SelectedItem == null || comboBox3.SelectedItem == null)
            {
                MessageBox.Show("Please select all options before adding the item.");
                return;
            }

            string selectedCard = (string)comboBox1.SelectedItem;
            string selectedCategory = (string)comboBox2.SelectedItem;
            MenuItem selectedItem = (MenuItem)comboBox6.SelectedItem;

            if (!int.TryParse(comboBox3.SelectedItem.ToString(), out int count) || count == 0)
            {
                MessageBox.Show("Invalid quantity selected. Please select a valid number greater than zero.");
                return;
            }

            DateTime datetime = DateTime.Now;
            orderitem = new OrderItem(datetime, selectedItem, order, count, StatusOfOrderitem.Available);
            order.items.Add(orderitem);
            RefreshSummary(orderitem);
        }
        private void RefreshSummary(OrderItem item)
        {
            //put info to summary
            summary = summary + item.MenuItem.Name.ToString() + " " + $"x{item.Count}"+"\n";
            label6.Text = summary;
        }

    }
}
